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
        private int rut;
        private string dirFiscal;
        private string telefono;
       
        public int Codigo
        {
            get { return codigo; }
            set
            {
                if (codigo.ToString().Length >= 1 && codigo.ToString().Length <= 4)
                    codigo = value;
                else
                    throw new Exception("El codigo de la emprea debe tener entre 1 y 4 digitos");
            }
        }
        
        public int Rut
        {
            get { return rut; }
            set
            {
                if (rut.ToString().Length <= 12)
                    rut = value;
                else
                    throw new Exception("El rut debe ser un nuemero entre 1 y 12 digitos");
            }
        }
        
        public string DirFiscal
        {
            get { return dirFiscal; }
            set { dirFiscal = value; }
        }
        
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
              
        public Empresa()
        { }
      
        public Empresa(int pCodigo, int pRut, string pDirFiscal, string pTelefono)
        {
            Codigo = pCodigo;
            Rut = pRut;
            DirFiscal = pDirFiscal;
            Telefono = pTelefono;
        }


    }
}
