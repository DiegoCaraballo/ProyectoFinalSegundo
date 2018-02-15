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
        private int codEmp;
        private int codContrato;
        private string nombre;


        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
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

        public int CodEmp
        {
            get { return codEmp; }
            set { codEmp = value; }
        }

        public TipoContrato()
        { }

        public TipoContrato(int pCodEmp, int pCodContrato, string pNombre)
        {
            Nombre = pNombre;
            CodContrato = pCodContrato;
            CodEmp = pCodEmp;
        }
    }
}
