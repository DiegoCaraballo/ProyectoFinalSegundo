using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class HorasExtras
    {
        private DateTime fecha;
        private int minutos;
        private Cajero cajero;

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        [DataMember]
        public int Minutos
        {
            get { return minutos; }
            set { minutos = value; }
        }

        [DataMember]
        public Cajero Cajero
        {
            get { return cajero; }
            set { cajero = value; }
        }

        public HorasExtras()
        { }

        public HorasExtras(DateTime pFecha, int pMinutos, Cajero pCajero)
        {
            Fecha = pFecha;
            Minutos = pMinutos;
            Cajero = pCajero;
        }
    }
}
