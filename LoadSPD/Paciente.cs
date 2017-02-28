using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSPD
{
    class Paciente
    {
        public String Name { get; set; }
        public String Expiracy { get; set; }
        public long ExpiracyMilisec { get; set; }

        public Paciente (String Name, String Expiracy)
        {
            this.Name = Name;
            this.Expiracy = Expiracy;

            DateTime dateValue;
            DateTime.TryParse(Expiracy, out dateValue);
            this.ExpiracyMilisec = dateValue.Ticks;
        }
    }
}
