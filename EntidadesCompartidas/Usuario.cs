using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [KnownType(typeof(Cajero))]
    [KnownType(typeof(Gerente))]
    [DataContract]


    public class Usuario
    {
        private int cedula;
        private string nomUsu;
        private string pass;
        private string nomCompleto;

    [DataMember]
        public int Cedula
        {
            get { return cedula; }
            set { cedula = value; }
        }

    [DataMember]    
        public string NomUsu
        {
            get { return nomUsu; }
            set { nomUsu = value; }
        }

        [DataMember]
        public string Pass
        {
            get { return pass; }
            set
            {
               // if (pass.Length == 7)
                    pass = value;

                //else
                  //  throw new Exception("La contraseña debe contener 7 caracteres");
            }
        }

        [DataMember]
        public string NomCompleto
        {
            get { return nomCompleto; }
            set { nomCompleto = value; }
        }

      
        public Usuario()
        { }

        
        public Usuario(int pCedula, string pNomUsu, string pPass, string pNomCompleto)
        {
            Cedula = pCedula;
            NomUsu = pNomUsu;
            Pass = pPass;
            NomCompleto = pNomCompleto;
        }
    }

}
