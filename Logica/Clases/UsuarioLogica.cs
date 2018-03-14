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

        public void AltaUsuario(Usuario unUsuario, Usuario usuLogueado)
        {
            try
            {
                if (unUsuario is Cajero)
                {
                    FabricaPersistencia.GetPersistenciaCajero().AltaCajero((Cajero)unUsuario, usuLogueado);
                }
                else
                {
                    FabricaPersistencia.GetPersistenciaGerente().AltaGerente((Gerente)unUsuario, usuLogueado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BajaUsuario(Usuario unUsuario, Usuario usuLogueado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaCajero().BajaCajero((Cajero)unUsuario, usuLogueado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificarusuario(Usuario unUsuario, Usuario usuLogueado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaCajero().ModificarCajero((Cajero)unUsuario, usuLogueado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Buscar(int cedula,Usuario usuLogueado)
        {
            try
            {
                return ((Cajero)FabricaPersistencia.GetPersistenciaCajero().BuscarCajero(cedula,usuLogueado));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario BuscarCajeroServicioWin(int cedula)
        {
            try
            {
                return ((Cajero)FabricaPersistencia.GetPersistenciaCajero().BuscarCajeroServicioWin(cedula));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambioPass(Usuario unUsuario, Usuario usuLogueado)
        {
            try
            {
                if (unUsuario is Cajero)
                {
                    FabricaPersistencia.GetPersistenciaCajero().CambioPass(unUsuario, usuLogueado);
                }
                else
                {
                    FabricaPersistencia.GetPersistenciaGerente().CambioPass(unUsuario, usuLogueado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Logueo(string pNomUsu)
        {
            try
            {
                Usuario usu = null;
                usu = FabricaPersistencia.GetPersistenciaCajero().LogueoCajero(pNomUsu);
                if (usu == null)
                {
                    usu = FabricaPersistencia.GetPersistenciaGerente().LogueoGerente(pNomUsu);
                }
                return usu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregaExtras(int pCedula, DateTime pFecha, int pMinutos)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaCajero().AgregaExtras(pCedula, pFecha, pMinutos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
