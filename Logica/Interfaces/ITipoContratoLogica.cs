﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface ITipoContratoLogica
    {
        TipoContrato BuscarContrato(int codEmp, int codContrato, Usuario usuLogueado);
        void AltaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado);
        void BajaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado);
        void ModificarTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado);
        List<TipoContrato> ListarContratos();
    }
}
