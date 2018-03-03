using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{

    [DataContract]
    public class Cajero : Usuario
    {
        private DateTime horaIni;
        private DateTime horaFin;

        [DataMember]
        public DateTime HoranIni
        {
            get { return horaIni; }
            set { horaIni = value; }
        }

        [DataMember]
        public DateTime HoranFin
        {
            get { return horaFin; }
            set
            {
                horaFin = value;
                //if (horaIni > horaFin)
                //    horaFin = value;
                //else
                //    throw new Exception("La hora fin debe ser mayor a la hora de inicio");

            }
        }

        
        public Cajero()
        { }

        
        public Cajero(int pCedula, string pNomUsu, string pPass, string pNomComp, DateTime pHoraIni, DateTime pHoraFin)
            : base(pCedula, pNomUsu, pPass, pNomComp)
        {
            HoranIni = pHoraIni;
            HoranFin = pHoraFin;
        }

    }
}
