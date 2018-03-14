using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class HorasExtrasLogica : IHorasExtrasLogica
    {
        private static HorasExtrasLogica _instancia = null;
        private HorasExtrasLogica() { }
        public static HorasExtrasLogica GetInstancia()
        {
            if (_instancia == null)
                _instancia = new HorasExtrasLogica();
            return _instancia;
        }

        public void GuardarHorasExtras(HorasExtras horasExtras)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaHorasExtras().GuardarHorasExtras(horasExtras);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
