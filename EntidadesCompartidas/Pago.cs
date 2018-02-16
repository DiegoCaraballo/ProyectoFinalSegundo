using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Pago
    {
        private int numeroInt;
        private DateTime fecha;
        private int montoTotal;
        private Cajero usuCajero;

        public Cajero UsuCajero
        {
            get { return usuCajero; }
            set { usuCajero = value; }
        }

        public int MontoTotal
        {
            get { return montoTotal; }
            set {
                if (montoTotal > 0)
                    montoTotal = value;
                else
                    throw new Exception("El monto no puede ser menor a 0");
            }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                if (fecha == DateTime.Now)
                    fecha = value;
                else
                    throw new Exception("La fecha tiene que ser igual a la fecha actual");
            }
        }
        public int NumeroInt
        {
            get { return numeroInt; }
            set { numeroInt = value; }
        }

        public Pago()
        { }

        public Pago(int pNumeroInt, DateTime pFecha, int pMontoTotal, Cajero pUsuCajero)
        {
            NumeroInt = pNumeroInt;
            Fecha = pFecha;
            NumeroInt = pMontoTotal;
            UsuCajero = pUsuCajero;
        }
    }
}
