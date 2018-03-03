﻿using System;
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
            set { usuCajero = value; }
        }

        [DataMember]
        public int MontoTotal
        {
            get { return montoTotal; }
            set
            {
                montoTotal = value;
                //if (montoTotal > 0)
                //    montoTotal = value;
                //else
                //    throw new Exception("El monto no puede ser menor a 0");
            }
        }

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;
                // TODO - ver porque no paso este codigo defensivo
                //if (fecha == DateTime.Today)
                //    fecha = value;
                //else
                //    throw new Exception("La fecha tiene que ser igual a la fecha actual");
            }
        }

        [DataMember]
        public int NumeroInt
        {
            get { return numeroInt; }
            set { numeroInt = value; }
        }
        
        public Pago()
        { }
        
        public Pago(int pNumeroInt, DateTime pFecha, int pMontoTotal, Cajero pUsuCajero, List<Factura> pLasFacturas)
        {
            NumeroInt = pNumeroInt;
            Fecha = pFecha;
            NumeroInt = pMontoTotal;
            UsuCajero = pUsuCajero;
            LasFacturas = pLasFacturas;
        }
    }
}
