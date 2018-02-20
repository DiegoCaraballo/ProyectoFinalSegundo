using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface IUusuarioLogica
    {

        void AltaUsuario(Usuario unUsuario);
        void BajaUsuario(Usuario unUsuario);
        void Modificarusuario(Usuario unUsuario);
        Usuario Buscar(int cedula);
        void CambioPass(Usuario unUsuario);
    }
}
