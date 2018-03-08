using System;
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
        Usuario Buscar(int cedula);
        void CambioPass(Usuario unUsuario, Usuario usuLogueado);
        Usuario Logueo(string pNomUsu);
        void AgregaExtras(int pCedula, DateTime pFecha, int pMinutos);
    }
}
