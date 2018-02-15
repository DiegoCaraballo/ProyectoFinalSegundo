using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
        [DataContract]
        public class Usuario
        {
            private int cedula;
            private string nomUsu;
            private string pass;
            private string nomComp;

            public int Cedula
            {
                get { return cedula; }
                set { cedula = value; }
            }

            public string NomUsu
            {
                get { return nomUsu; }
                set { nomUsu = value; }
            }

            public string Pass
            {
                get { return pass; }
                set { pass = value; }
            }

            public string NomComp
            {
                get { return nomComp; }
                set { nomComp = value; }
            }

            public Usuario()
            { }

            public Usuario(int pCedula, string pNomUsu, string pPass, string pNomComp)
            {
                Cedula = cedula;
                NomUsu = nomUsu;
                Pass = pass;
                NomComp = nomComp;
            }
        }
    
}
