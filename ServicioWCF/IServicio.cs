using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using EntidadesCompartidas;

namespace ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicio" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicio
    {
        #region Usuario
        [OperationContract]
        void AltaUsuario(Usuario unUsuario);

        [OperationContract]
        void BajaUsuario(Usuario unUsuario);

        [OperationContract]
        void ModificarUsuario(Usuario unUsuario);

        [OperationContract]
        Usuario BuscarUsuario(int pCedula);

        [OperationContract]
        void CambioPass(Usuario unUsuario);

        [OperationContract]
        Usuario Logueo(string pNomUsu);
        #endregion

        #region Pago
        [OperationContract]
        Pago AltaPago(Pago unPago);
        #endregion
    }
}
