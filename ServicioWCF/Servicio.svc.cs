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
        public void AltaUsuario(Usuario unUsuario)
        {
            FabricaLogica.GetLogicaUsuario().AltaUsuario(unUsuario);
        }
        public void BajaUsuario(Usuario unUsuario)
        {
            FabricaLogica.GetLogicaUsuario().BajaUsuario(unUsuario);
        }
        public void ModificarUsuario(Usuario unUsuario)
        {
            FabricaLogica.GetLogicaUsuario().Modificarusuario(unUsuario);
        }
        public Usuario BuscarUsuario(int pCedula)
        {
         return   (FabricaLogica.GetLogicaUsuario().Buscar(pCedula));
        }
        public void CambioPass(Usuario unUsuario)
        {
            FabricaLogica.GetLogicaUsuario().CambioPass(unUsuario);
        }
        public Usuario Logueo(string pNomUsu)
        {
            return (FabricaLogica.GetLogicaUsuario().Logueo(pNomUsu));
        }
        #endregion

        #region Pago
        public void AltaPago(Pago unPago)
        {
            FabricaLogica.GetLogicaPago().AltaPago(unPago);
        }

        #endregion

        #region Empresa
        public Empresa BuscarEmpresa(int codEmp)
        {
            return (FabricaLogica.GetLogicaEmpresa().BuscarEmpresa(codEmp));
        }
        public void AltaEmpresa(Empresa unaEmpresa)
        {
            FabricaLogica.GetLogicaEmpresa().AltaEmpresa(unaEmpresa);
        }
        public void BajaEmpresa(Empresa unaEmpresa)
        {
            FabricaLogica.GetLogicaEmpresa().BajaEmpresa(unaEmpresa);
        }
        public void ModificarEmpresa(Empresa unaEmpresa)
        {
            FabricaLogica.GetLogicaEmpresa().ModificarEmpresa(unaEmpresa);
        }
        #endregion

        #region Tipo Contrato
        public TipoContrato BuscarContrato(int codEmp, int codTipoContrato)
        {
            return (FabricaLogica.GetLogicaTipoContrato().BuscarContrato(codEmp, codTipoContrato));
        }
        public void AltaTipoContrato(TipoContrato unTipoContrato)
        {
            FabricaLogica.GetLogicaTipoContrato().AltaTipoContrato(unTipoContrato);
        }
        public void BajaTipoContrato(TipoContrato unTipoContrato)
        {
            FabricaLogica.GetLogicaTipoContrato().BajaTipoContrato(unTipoContrato);
        }
        public void ModificarTipoContrato(TipoContrato unTipoContrato)
        {
            FabricaLogica.GetLogicaTipoContrato().ModificarTipoContrato(unTipoContrato);
        }

        public List<Pago> listar()
        {
            return (FabricaLogica.GetLogicaPago().listar());
        }
        #endregion
    }
}
