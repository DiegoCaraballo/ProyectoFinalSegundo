using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface ICajeroPersistencia
    {

        void AltaCajero(Cajero unCajero);
        void BajaCajero(Cajero unCajero);
        void ModificarCajero(Cajero unCajero);
        void CambioPass(Cajero unCajero);
        Cajero BuscarCajero(int cedula);
        Cajero LogueoCajero(string nomUsu);
    }
}
