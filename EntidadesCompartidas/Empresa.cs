using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{

    [DataContract]
    public class Empresa
    {
        private int codigo;
        private string rut;
        private string dirFiscal;
        private string telefono;

        [DataMember]
        public int Codigo
        {
            get { return codigo; }
            set
            {
                if (codigo.ToString().Trim().Length >= 1 && codigo.ToString().Trim().Length <= 4)
                    codigo = value;
                else
                    throw new Exception("El codigo de la emprea debe tener entre 1 y 4 digitos");
            }
        }

        [DataMember]
        public string Rut
        {
            get { return rut; }
            set
            {
                if (value.ToString().Trim().Length >= 5 &&value.ToString().Trim().Length <= 12)
                {
                    rut = value;
                }
                else
                {
                    throw new Exception("El rut debe ser un nuemero entre 5 y 12 digitos");
                }
            }
        }

        [DataMember]
        public string DirFiscal
        {
            get { return dirFiscal; }
            set
            {
                if (value.ToString().Trim().Length < 5 || value.ToString().Length > 100)
                {
                    throw new Exception("La direccion debe tener entre 5 y 100 caracteres");
                }
                else
                {
                    dirFiscal = value;
                }
            }
        }

        [DataMember]
        public string Telefono
        {
            get { return telefono; }
            set
            {
                if (value.ToString().Trim().Length < 5 || value.ToString().Trim().Length > 30)
                {
                    throw new Exception("El telefono debe de tener entre 5 y 30 digitos");
                }
                else
                {
                    telefono = value;
                }
            }
        }

        public Empresa()
        { }

        public Empresa(int pCodigo, string pRut, string pDirFiscal, string pTelefono)
        {
            Codigo = pCodigo;
            Rut = pRut;
            DirFiscal = pDirFiscal;
            Telefono = pTelefono;
        }


    }
}
