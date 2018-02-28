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
    }
}
