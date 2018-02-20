using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IGerentePersistencia
    {
        void AltaGerente(Gerente unGerente);
        void CambioPass(Gerente unGerente);

    }
}
