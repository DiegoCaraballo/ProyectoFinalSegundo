using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface ITipoContratoPersistencia
    {
        TipoContrato BuscarContrato(int codEmp, int codTipoContrato);
        void AltaTipoContrato(TipoContrato unTipoContrato);
        void BajaTipoContrato(TipoContrato unTipoContrato);
        void ModificarTipoContrato(TipoContrato unTipoContrato);
    }
}
