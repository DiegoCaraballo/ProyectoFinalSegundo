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
        private Empresa unaEmp;
        private int monto;
        private int codCli;
        private DateTime fechaVto;

        public DateTime FechaVto
        {
            get { return fechaVto; }
            set {
                if (fechaVto > DateTime.Now)
                {
                    throw new Exception("La factura está vencida");
                }
                else
                    fechaVto = value;
                }
        }
        
        public Empresa UnaEmp
        {
            get { return unaEmp; }
            set { unaEmp = value; }
        }
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
        
        public Factura()
        { }
     
        public Factura(int pMonto, int pCodCli, Empresa pUnaEmp, Pago pUnPago)
        {
            CodCli = pCodCli;
            Monto = pMonto;
            UnaEmp = pUnaEmp;
        }
    }
}
