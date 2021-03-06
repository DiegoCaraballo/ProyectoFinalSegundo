﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface ICajeroPersistencia
    {
        void AltaCajero(Cajero unCajero, Usuario usuLogueado);
        void BajaCajero(Cajero unCajero, Usuario usuLogueado);
        void ModificarCajero(Cajero unCajero, Usuario usuLogueado);
        void CambioPass(Usuario unCajero, Usuario usuLogueado);
        Cajero BuscarCajero(int cedula,Usuario usuLogueado);
        Cajero BuscarCajeroServicioWin(int cedula);
        Cajero LogueoCajero(string nomUsu);
    }
}
