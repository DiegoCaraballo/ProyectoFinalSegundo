using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica.Clases
{
    internal class PagoLogica : IPagoLogica
    {
        private static PagoLogica _instancia = null;
        private PagoLogica() { }
        public static PagoLogica GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PagoLogica();
            return _instancia;
        }
    }
}
