using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IHorasExtrasPersistencia
    {
        void GuardarHorasExtras(DateTime fecha, int minutos, Usuario cajero);
    }
}
