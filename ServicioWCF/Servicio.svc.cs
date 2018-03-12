using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

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
        public Usuario BuscarUsuario(int pCedula,Usuario usuLogueado)
        {
            return (FabricaLogica.GetLogicaUsuario().Buscar(pCedula,usuLogueado));
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

        public string ListarContratos()
        {
            try
            {
                ITipoContratoLogica listado = FabricaLogica.GetLogicaTipoContrato();
                XmlDocument exportar = ListarContratosXML(listado.ListarContratos());
                string st = exportar.OuterXml;
                return st;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private XmlDocument ListarContratosXML(List<TipoContrato> lista)
        {
            try
            {
                XmlDocument exportar = new XmlDocument();
                exportar.LoadXml("<?xml version='1.0' encoding='utf-8' ?> <TiposDeContratos> </TiposDeContratos>");

                XmlNode _raiz = exportar.DocumentElement;

                foreach (TipoContrato tc in lista)
                {
                    XmlElement _EmpCod = exportar.CreateElement("EmpCod");
                    _EmpCod.InnerText = tc.UnaEmp.Codigo.ToString();

                    XmlElement _EmpRut = exportar.CreateElement("EmpRut");
                    _EmpRut.InnerText = tc.UnaEmp.Rut.ToString();

                    XmlElement _EmpDir = exportar.CreateElement("EmpDir");
                    _EmpDir.InnerText = tc.UnaEmp.DirFiscal.ToString();

                    XmlElement _EmpTel = exportar.CreateElement("EmpTel");
                    _EmpTel.InnerText = tc.UnaEmp.Telefono.ToString();

                    XmlElement _CodContrato = exportar.CreateElement("CodContrato");
                    _CodContrato.InnerText = tc.CodContrato.ToString();

                    XmlElement _NomContrato = exportar.CreateElement("NomContrato");
                    _NomContrato.InnerText = tc.Nombre.ToString();

                    XmlNode nodoContrato = exportar.CreateElement("TipoContrato");
                    nodoContrato.AppendChild(_EmpCod);
                    nodoContrato.AppendChild(_EmpRut);
                    nodoContrato.AppendChild(_EmpDir);
                    nodoContrato.AppendChild(_EmpTel);
                    nodoContrato.AppendChild(_CodContrato);
                    nodoContrato.AppendChild(_NomContrato);

                    _raiz.AppendChild(nodoContrato);

                }
                return exportar;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
