using System;
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
        void CambioPass(Cajero unCajero, Usuario usuLogueado);
        Cajero BuscarCajero(int cedula);
        Cajero LogueoCajero(string nomUsu);
        void AgregaExtras(int pCedula, DateTime pFecha, int pMinutos);
    }
}
