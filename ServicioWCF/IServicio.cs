﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

using EntidadesCompartidas;

namespace ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicio" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicio
    {
        #region Usuario
        [OperationContract]
        void AltaUsuario(Usuario unUsuario, Usuario usuLogueado);

        [OperationContract]
        void BajaUsuario(Usuario unUsuario, Usuario usuLogueado);

        [OperationContract]
        void ModificarUsuario(Usuario unUsuario, Usuario usuLogueado);

        [OperationContract]
        Usuario BuscarUsuario(int pCedula);

        [OperationContract]
        void CambioPass(Usuario unUsuario, Usuario usuLogueado);

        [OperationContract]
        Usuario Logueo(string pNomUsu);

        [OperationContract]
        void AgregaExtras(int pCedula, DateTime pFecha, int pMinutos);
        #endregion

        #region Pago
        [OperationContract]
        void AltaPago(Pago unPago, Usuario usuLogueado);

        [OperationContract]
        DateTime PagoDeUnaFactura(int codContrato, int codEmp, int monto, int codCli, DateTime fecha);

        [OperationContract]
        List<Pago> listar(Usuario usuLogueado);
        #endregion

        #region Empresa
        [OperationContract]
        Empresa BuscarEmpresa(int codEmp);
        [OperationContract]
        void AltaEmpresa(Empresa unaEmpresa, Usuario usuLogueado);
        [OperationContract]
        void BajaEmpresa(Empresa unaEmpresa, Usuario usuLogueado);
        [OperationContract]
        void ModificarEmpresa(Empresa unaEmpresa, Usuario usuLogueado);
        [OperationContract]
        List<Empresa> ListarEmpresas();

        #endregion

        #region Tipo Contrato
        [OperationContract]
        TipoContrato BuscarContrato(int codEmp, int codTipoContrato);
        [OperationContract]
        void AltaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado);
        [OperationContract]
        void BajaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado);
        [OperationContract]
        void ModificarTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado);
        [OperationContract]
        XmlDocument ListarContratos();

        #endregion
    }
}
