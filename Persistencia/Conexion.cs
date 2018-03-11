using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    internal class Conexion
    {

        public static string Cnn
        {
            get { return cnn; }
        }

        //  //Nico

        //private static string cnn = "Data Source=PC-NICO-PC\\SQLEXPRESS; Initial Catalog = BiosMoney; Integrated Security = true";

        //public string cnnUsu(Usuario unUsuario)
        //{
        //    return ("Data Source=PC-NICO-PC\\SQLEXPRESS; Initial Catalog = BiosMoney; Integrated Security = false; User ID =" + unUsuario.NomUsu + "; Password =" + unUsuario.Pass);
        //}


        ////Diego

        private static string cnn = "Data Source=LENOVO-PC\\SA; Initial Catalog = BiosMoney; Integrated Security = true";

        public string cnnUsu(Usuario unUsuario)
        {
            return ("Data Source=LENOVO-PC\\SA; Initial Catalog = BiosMoney; Integrated Security = false; User ID =" + unUsuario.NomUsu + "; Password =" + unUsuario.Pass);
        }




        // Nico Virtual
        //private static string cnn = "Data Source=Bios1-PC\\SQLEXPRESS; Initial Catalog = BiosMoney; Integrated Security = true";

        //public string cnnUsu(Usuario unUsuario)
        //{
        //    return ("Data Source=Bios1-PC\\SQLEXPRESS; Initial Catalog = BiosMoney; Integrated Security = false; User ID =" + unUsuario.NomUsu + "; Password =" + unUsuario.Pass);
        //}

        // BIOS
        //private static string cnn = "Data Source=.; Initial Catalog = BiosMoney; Integrated Security = true";

        //public string cnnUsu(Usuario unUsuario)
        //{
        //    return ("Data Source=.; Initial Catalog = BiosMoney; Integrated Security = false; User ID =" + unUsuario.NomUsu + "; Password =" + unUsuario.Pass);
        //}

    }
}
