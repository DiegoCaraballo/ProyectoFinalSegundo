using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;
namespace Logica
{
    internal class UsuarioLogica : IUusuarioLogica
    {
        private static UsuarioLogica _instancia = null;
        private UsuarioLogica() { }
        public static UsuarioLogica GetInstancia()
        {
            if (_instancia == null)
                _instancia = new UsuarioLogica();
            return _instancia;
        }

        public void AltaUsuario(Usuario unUsuario)
        {
            if (unUsuario is Cajero)
            {
                FabricaPersistencia.GetPersistenciaCajero().AltaCajero((Cajero)unUsuario);
            }
            else
            {
                FabricaPersistencia.GetPersistenciaGerente().AltaGerente((Gerente)unUsuario);
            }
        }

        public void BajaUsuario(Usuario unUsuario)
        {
            FabricaPersistencia.GetPersistenciaCajero().BajaCajero((Cajero)unUsuario);
        }

        public void Modificarusuario(Usuario unUsuario)
        {
            FabricaPersistencia.GetPersistenciaCajero().ModificarCajero((Cajero)unUsuario);
        }

        public Usuario Buscar(int cedula)
        {
            return ((Cajero)FabricaPersistencia.GetPersistenciaCajero().BuscarCajero(cedula));

        }

        public void CambioPass(Usuario unUsuario)
        {
            if (unUsuario is Cajero)
            {
                FabricaPersistencia.GetPersistenciaCajero().CambioPass((Cajero)unUsuario);
            }
            else
            {
                FabricaPersistencia.GetPersistenciaGerente().CambioPass((Gerente)unUsuario);
            }
        }

    }
}
