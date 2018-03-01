using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class TipoContratoLogica: ITipoContratoLogica
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
    }
}
