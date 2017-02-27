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

        public Paciente (String Name, String Expiracy)
        {
            this.Name = Name;
            this.Expiracy = Expiracy;
        }
    }
}
