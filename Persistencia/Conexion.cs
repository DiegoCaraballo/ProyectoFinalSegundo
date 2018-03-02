using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia
{
    internal class Conexion
    {
        //Nico
        //private static string _cnn = "Data Source=PC-NICO-PC\\SQLEXPRESS; Initial Catalog = BiosMoney; Integrated Security = true";

        //Diego
        private static string _cnn = "Data Source=LENOVO-PC\\SA; Initial Catalog = BiosMoney; Integrated Security = true";

        public static string Cnn
        {
            get { return _cnn; }
        }
    }
}
