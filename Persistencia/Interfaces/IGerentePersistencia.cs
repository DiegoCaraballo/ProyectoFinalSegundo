using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IGerentePersistencia
    {
        void AltaGerente(Gerente unGerente, Usuario usuLogueado);
        void CambioPass(Gerente unGerente, Usuario usuLogueado);
        Gerente LogueoGerente(string nomUsu);

    }
}
