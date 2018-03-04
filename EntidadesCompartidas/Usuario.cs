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
            set
            {
                   if (value.ToString().Length < 7 || value.ToString().Length > 8)
                    {
                        throw new Exception("La cedula debe tener 7 u 8 digitos");
                    }
                    else
                    {
                        cedula = value;
                    }
                
             
            }
        }

        [DataMember]
        public string NomUsu
        {
            get { return nomUsu; }
            set
            {
                if (value.Length < 4 || value.Length > 15)
                {
                    throw new Exception("El Nombre de Usuario debe tener entre 4 y 15 caracteres");
                }
                else
                {
                    nomUsu = value;
                }
            }
        }

        [DataMember]
        public string Pass
        {
            get { return pass; }
            set
            {
                if (value.Length == 7)
                {
                    pass = value;
                }

                else
                {
                    throw new Exception("La contraseña debe contener 7 caracteres");
                }
            }
        }

        [DataMember]
        public string NomCompleto
        {
            get { return nomCompleto; }
            set
            {
                if (value.Length < 4 || value.Length > 50)
                {
                    throw new Exception("El Nombre/Apellido debe tener entre 4 y 50 caracteres");
                }
                else
                {
                    nomCompleto = value;
                }
            }
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
