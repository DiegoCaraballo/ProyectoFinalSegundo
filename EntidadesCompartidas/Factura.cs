using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Factura
    {
        private int idPago;
        private int codContrato;
        private int codEmp;
        private int monto;
        private int codCli;


        public int CodCli
        {
            get { return codCli; }
            set
            {
                if (codCli.ToString().Length >= 1 && codCli.ToString().Length <= 6)
                    codCli = value;
                else
                    throw new Exception("El monto debe tener entre 1 y 5 digitos");

            }
        }

        public int Monto
        {
            get { return monto; }
            set
            {
                if (monto.ToString().Length >= 1 && monto.ToString().Length <= 5)
                    monto = value;
                else
                    throw new Exception("El monto debe tener entre 1 y 5 digitos");
            }
        }

        public int CodEmp
        {
            get { return codEmp; }
            set { codEmp = value; }
        }

        public int CodContrato
        {
            get { return codContrato; }
            set { codContrato = value; }
        }
        public int IdPago
        {
            get { return idPago; }
            set { idPago = value; }
        }

        public Factura()
        { }

        public Factura(int pIdPago, int pCodContrato, int pCodEmp, int pMonto, int pCodCli)
        {
            CodCli = pCodCli;
            Monto = pMonto;
            CodEmp = CodEmp;
            CodContrato = pCodContrato;
            IdPago = pIdPago;
        }
    }
}
