using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class TipoContratoLogica : ITipoContratoLogica
    {
        private static TipoContratoLogica _instancia = null;
        private TipoContratoLogica() { }
        public static TipoContratoLogica GetInstancia()
        {
            if (_instancia == null)
                _instancia = new TipoContratoLogica();
            return _instancia;
        }

        public TipoContrato BuscarContrato(int codEmp, int codContrato)
        {
            try
            {
                return ((TipoContrato)FabricaPersistencia.GetPersistenciaTipoContrato().BuscarContrato(codEmp, codContrato));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void AltaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaTipoContrato().AltaTipoContrato(unTipoContrato, usuLogueado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public void BajaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaTipoContrato().BajaTipoContrato(unTipoContrato, usuLogueado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaTipoContrato().ModificarTipoContrato(unTipoContrato, usuLogueado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoContrato> ListarContratos()
        {
            try
            {
                return (FabricaPersistencia.GetPersistenciaTipoContrato().ListarContratos());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
