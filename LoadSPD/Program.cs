using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Excel;
using Firebase.Database;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace LoadSPD
{
    class Program
    {
        private static List<Paciente> pacientes = new List<Paciente>();

        static void Main(string[] args)
        {
            CargarPacientes();

            SubidaFirebase().Wait();

            Console.WriteLine("Pulsa para salir...");
            Console.Read();
        }

     
        /// <summary>
        /// Lectura del fichero para cargar los pacientes en la lista.
        /// </summary>
        private static void CargarPacientes()
        {
            Application myApplication = new Application();
            myApplication.Visible = true;
            Workbook myWorkbook = myApplication.Workbooks.Open(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + @"renovacion_spd.xls");
            Worksheet myWorksheet = myWorkbook.Sheets["Hoja1"] as Worksheet;


            for (int row = 2; row < myWorksheet.Rows.Count; row++)
            {
                Range NameRange = (Range)myWorksheet.Cells[row, 1];
                Range ExpiracyRange = (Range)myWorksheet.Cells[row, 3];

                if (String.IsNullOrEmpty(NameRange.Text.ToString()))
                    break;

                pacientes.Add(new Paciente(NameRange.Text.ToString(), ExpiracyRange.Text.ToString()));

            }
        }

        /// <summary>
        /// Subida de la lista de pacientes a firebase
        /// </summary>
        private static async Task SubidaFirebase()
        {
            var client = new FirebaseClient("https://spds-22cc3.firebaseio.com/");

            // delete entire conversation list
            await client.Child("pacientes").DeleteAsync();
            // load data
            await client.Child("lista_pacientes").PutAsync(pacientes);

            foreach (Paciente paciente in pacientes)
            {
                await client.Child("pacientes").Child(paciente.Name).PutAsync(paciente.Expiracy);
            }

            client.Child("pacientes").Dispose();
        }
    }
}
