using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface ITipoContratoLogica
    {
        TipoContrato BuscarContrato(int codEmp, int codContrato);
        void AltaTipoContrato(TipoContrato unTipoContrato);
        void BajaTipoContrato(TipoContrato unTipoContrato);
        void ModificarTipoContrato(TipoContrato unTipoContrato);
    }
}
