using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Persistencia
{
    public class FabricaPersistencia
    {
        public static ICajeroPersistencia GetPersistenciaCajero()
        {
            return (CajeroPersistencia.GetInstancia());
        }

        public static IGerentePersistencia GetPersistenciaGerente()
        {
            return (GerentePersistencia.GetInstancia());
        }
    }
}
