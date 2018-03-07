using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Logica;
using EntidadesCompartidas;

namespace ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Servicio" en el código, en svc y en el archivo de configuración a la vez.
    public class Servicio : IServicio
    {
        #region Usuario
        public void AltaUsuario(Usuario unUsuario, Usuario usuLogueado)
        {
            FabricaLogica.GetLogicaUsuario().AltaUsuario(unUsuario, usuLogueado);
        }
        public void BajaUsuario(Usuario unUsuario, Usuario usuLogueado)
        {
            FabricaLogica.GetLogicaUsuario().BajaUsuario(unUsuario, usuLogueado);
        }
        public void ModificarUsuario(Usuario unUsuario, Usuario usuLogueado)
        {
            FabricaLogica.GetLogicaUsuario().Modificarusuario(unUsuario, usuLogueado);
        }
        public Usuario BuscarUsuario(int pCedula)
        {
            return (FabricaLogica.GetLogicaUsuario().Buscar(pCedula));
        }
        public void CambioPass(Usuario unUsuario, Usuario usuLogueado)
        {
            FabricaLogica.GetLogicaUsuario().CambioPass(unUsuario, usuLogueado);
        }
        public Usuario Logueo(string pNomUsu)
        {
            return (FabricaLogica.GetLogicaUsuario().Logueo(pNomUsu));
        }
        public void AgregaExtras(int pCedula, DateTime pFecha, int pMinutos)
        {
            FabricaLogica.GetLogicaUsuario().AgregaExtras(pCedula, pFecha, pMinutos);
        }
        #endregion

        #region Pago
        public void AltaPago(Pago unPago, Usuario usuLogueado)
        {
            FabricaLogica.GetLogicaPago().AltaPago(unPago, usuLogueado);
        }

        public DateTime PagoDeUnaFactura(int codContrato, int codEmp, int monto, int codCli, DateTime fecha)
        {
            return (FabricaLogica.GetLogicaPago().PagoDeUnaFactura((int)codContrato, (int)codEmp, (int)monto, (int)codCli, (DateTime)fecha));
        }

        #endregion

        #region Empresa
        public Empresa BuscarEmpresa(int codEmp)
        {
            return (FabricaLogica.GetLogicaEmpresa().BuscarEmpresa(codEmp));
        }

        public void AltaEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            FabricaLogica.GetLogicaEmpresa().AltaEmpresa(unaEmpresa, usuLogueado);
        }

        public void BajaEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            FabricaLogica.GetLogicaEmpresa().BajaEmpresa(unaEmpresa, usuLogueado);
        }

        public void ModificarEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            FabricaLogica.GetLogicaEmpresa().ModificarEmpresa(unaEmpresa, usuLogueado);
        }

        public List<Empresa> ListarEmpresas()
        {
            return (FabricaLogica.GetLogicaEmpresa().ListarEmpresas());
        }

        #endregion

        #region Tipo Contrato
        public TipoContrato BuscarContrato(int codEmp, int codTipoContrato)
        {
            return (FabricaLogica.GetLogicaTipoContrato().BuscarContrato(codEmp, codTipoContrato));
        }
        public void AltaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            FabricaLogica.GetLogicaTipoContrato().AltaTipoContrato(unTipoContrato, usuLogueado);
        }
        public void BajaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            FabricaLogica.GetLogicaTipoContrato().BajaTipoContrato(unTipoContrato, usuLogueado);
        }
        public void ModificarTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            FabricaLogica.GetLogicaTipoContrato().ModificarTipoContrato(unTipoContrato, usuLogueado);
        }

        public List<Pago> listar(Usuario usuLogueado)
        {
            return (FabricaLogica.GetLogicaPago().listar(usuLogueado));
        }
        #endregion
    }
}
