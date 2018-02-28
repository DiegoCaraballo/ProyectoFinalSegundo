using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class TipoContrato
    {
        private Empresa unaEmp;
        private int codContrato;
        private string nombre;

               [DataMember]
        public Empresa UnaEmp
        {
            get { return unaEmp; }
            set { unaEmp = value; }
        }

               [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

               [DataMember]
        public int CodContrato
        {
            get { return codContrato; }
            set
            {
                if (codContrato.ToString().Length >= 1 && codContrato.ToString().Length <= 2)
                    codContrato = value;
                else
                    throw new Exception("El codigo debe de tener 1 o 2 digitos");
            }
        }  
     
        public TipoContrato()
        { }
        
        public TipoContrato(Empresa pUnaEmp, int pCodContrato, string pNombre)
        {
            Nombre = pNombre;
            CodContrato = pCodContrato;
            UnaEmp = pUnaEmp;
        }
    }
}
