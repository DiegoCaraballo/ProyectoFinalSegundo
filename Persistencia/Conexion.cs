using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    internal class Conexion
    {
        //Nico
//        private static string _cnn = "Data Source=PC-NICO-PC\\SQLEXPRESS; Initial Catalog = BiosMoney; Integrated Security = true";
          private static string _cnn = "Data Source=Bios1-PC\\SQLEXPRESS; Initial Catalog = BiosMoney; Integrated Security = true";

        
        //TODO string para conexion a servidor con usuario y contraseña
        private static string _cnn = "Data Source=Bios1-PC\\SQLEXPRESS; Initial Catalog = BiosMoney; user id =  "+ "  " + "; password= "+" "+   ";";

        //Diego
       // private static string _cnn = "Data Source=LENOVO-PC\\SA; Initial Catalog = BiosMoney; Integrated Security = true";

        public static string Cnn
        {
            get { return _cnn; }
        }

        
    }
}
