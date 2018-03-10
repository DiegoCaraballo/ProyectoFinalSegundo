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
            if (unUsuario is Cajero)
            {
                FabricaPersistencia.GetPersistenciaCajero().AltaCajero((Cajero)unUsuario, usuLogueado);
            }
            else
            {
                FabricaPersistencia.GetPersistenciaGerente().AltaGerente((Gerente)unUsuario, usuLogueado);
            }
        }

        public void BajaUsuario(Usuario unUsuario, Usuario usuLogueado)
        {
            FabricaPersistencia.GetPersistenciaCajero().BajaCajero((Cajero)unUsuario, usuLogueado);
        }

        public void Modificarusuario(Usuario unUsuario, Usuario usuLogueado)
        {
            FabricaPersistencia.GetPersistenciaCajero().ModificarCajero((Cajero)unUsuario, usuLogueado);
        }

        public Usuario Buscar(int cedula)
        {
            return ((Cajero)FabricaPersistencia.GetPersistenciaCajero().BuscarCajero(cedula));

        }

        public void CambioPass(Usuario unUsuario, Usuario usuLogueado)
        {
            if (unUsuario is Cajero)
            {
                FabricaPersistencia.GetPersistenciaCajero().CambioPass((Cajero)unUsuario, usuLogueado);
            }
            else
            {
                FabricaPersistencia.GetPersistenciaGerente().CambioPass(unUsuario, usuLogueado);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
        }


    }
}
