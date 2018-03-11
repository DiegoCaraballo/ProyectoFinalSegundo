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
        private int monto;
        private int codCli;
        private DateTime fechaVto;
        private TipoContrato unTipoContrato;

        [DataMember]
        public TipoContrato UnTipoContrato
        {
            get { return unTipoContrato; }
            set {
                if (value != null)
                    unTipoContrato = value;
                else
                {
                    throw new Exception("No existe el tipo de contrato");
                }
            }
        }

        [DataMember]
        public DateTime FechaVto
        {
            get { return fechaVto; }
            set
            {
                if (fechaVto > DateTime.Today)
                {
                    throw new Exception("La factura está vencida");
                }
                else
                    fechaVto = value;
            }
        }

        [DataMember]
        public int CodCli
        {
            get { return codCli; }
            set
            {
                if (codCli.ToString().Trim().Length >= 1 && codCli.ToString().Trim().Length <= 6)
                    codCli = value;
                else
                    throw new Exception("El monto debe tener entre 1 y 5 digitos");

            }
        }

        [DataMember]
        public int Monto
        {
            get { return monto; }
            set
            {
                if (monto.ToString().Trim().Length >= 1 && monto.ToString().Trim().Length <= 5)
                    monto = value;
                else
                    throw new Exception("El monto debe tener entre 1 y 5 digitos");
            }
        }

        public Factura()
        { }

        public Factura(int pMonto, int pCodCli, DateTime pFechaVto, TipoContrato pUnContrato)
        {
            CodCli = pCodCli;
            Monto = pMonto;
            FechaVto = pFechaVto;
            UnTipoContrato = pUnContrato;
        }
    }
}
