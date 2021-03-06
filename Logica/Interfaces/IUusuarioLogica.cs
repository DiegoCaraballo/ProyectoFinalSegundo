﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface IUusuarioLogica
    {
        void AltaUsuario(Usuario unUsuario, Usuario usuLogueado);
        void BajaUsuario(Usuario unUsuario, Usuario usuLogueado);
        void Modificarusuario(Usuario unUsuario, Usuario usuLogueado);
        Usuario Buscar(int cedula,Usuario usuLogueado);
        Usuario BuscarCajeroServicioWin(int cedula);
        void CambioPass(Usuario unUsuario, Usuario usuLogueado);
        Usuario Logueo(string pNomUsu);
    }
}
