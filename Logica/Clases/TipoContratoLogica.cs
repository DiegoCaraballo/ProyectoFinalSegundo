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
            return ((TipoContrato)FabricaPersistencia.GetPersistenciaTipoContrato().BuscarContrato(codEmp, codContrato));
        }
        public void AltaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            FabricaPersistencia.GetPersistenciaTipoContrato().AltaTipoContrato(unTipoContrato,usuLogueado);
        }
        public void BajaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            FabricaPersistencia.GetPersistenciaTipoContrato().BajaTipoContrato(unTipoContrato,usuLogueado);
        }
        public void ModificarTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            FabricaPersistencia.GetPersistenciaTipoContrato().ModificarTipoContrato(unTipoContrato,usuLogueado);
        }

        public List<TipoContrato> ListarContratos()
        {
            return (FabricaPersistencia.GetPersistenciaTipoContrato().ListarContratos());
        }
    }
}
