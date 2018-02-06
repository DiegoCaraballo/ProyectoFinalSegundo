

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace entidadesCompartidas
{
    [DataContract]
    public class Cajero : Usuario
    {
        private DateTime horaIni;
        private DateTime horaFin;

        public DateTime HoranIni
        {
            get { return horaIni; }
            set { horaIni = value; }
        }

        public DateTime HoranFin
        {
            get { return horaFin; }
            set { horaFin = value; }
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
