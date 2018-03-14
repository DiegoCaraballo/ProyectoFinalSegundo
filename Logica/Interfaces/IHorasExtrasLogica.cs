using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface IHorasExtrasLogica
    {
        void GuardarHorasExtras(DateTime fecha, int minutos, Usuario cajero);
    }
}
