﻿using System;
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

        public static IPagoPersistencia GetPersistenciaPago()
        {
            return (PagoPersistencia.GetInstancia());
        }

        public static IEmpresaPersistencia GetPersistenciaEmpresa()
        {
            return (EmpresaPersistencia.GetInstancia());
        }

        public static ITipoContratoPersistencia GetPersistenciaTipoContrato()
        {
            return (TipoContratoPersistencia.GetInstancia());
        }

        public static IHorasExtrasPersistencia GetPersistenciaHorasExtras()
        {
            return (HorasExtrasPersistencia.GetInstancia());
        }
    }
}
