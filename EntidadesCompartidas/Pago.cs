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
        private List<Factura> lasFacturas;

        [DataMember]
        public List<Factura> LasFacturas
        {
            get { return lasFacturas; }
            set { lasFacturas = value; }
        }

        [DataMember]
        public Cajero UsuCajero
        {
            get { return usuCajero; }
            set
            {
                if (value != null)
                {
                    usuCajero = value;
                }
                else
                {
                    throw new Exception("No existe el Cajero");
                }
            }
        }

        [DataMember]
        public int MontoTotal
        {
            get { return montoTotal; }
            set
            {
                if (value > 0)
                    montoTotal = value;
                else
                    throw new Exception("El monto no puede ser menor a 0");
            }
        }

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                if (value == DateTime.Today)
                    fecha = value;
                else
                    throw new Exception("La fecha tiene que ser igual a la fecha actual");
            }
        }

        [DataMember]
        public int NumeroInt
        {
            get { return numeroInt; }
            set { numeroInt = value;}
        }

        public Pago()
        { }

        public Pago(int pNumeroInt, DateTime pFecha, int pMontoTotal, Cajero pUsuCajero, List<Factura> pLasFacturas)
        {
            NumeroInt = pNumeroInt;
            Fecha = pFecha;
            MontoTotal = pMontoTotal;
            UsuCajero = pUsuCajero;
            LasFacturas = pLasFacturas;
        }
    }
}
