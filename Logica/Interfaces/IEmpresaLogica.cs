using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface IEmpresaLogica
    {
        Empresa BuscarEmpresa(int codEmp,Usuario usuLogueado);

        void AltaEmpresa(Empresa unaEmpresa, Usuario usuLogueado);
        void BajaEmpresa(Empresa unaEmpresa, Usuario usuLogueado);
        void ModificarEmpresa(Empresa unaEmpresa, Usuario usuLogueado);

    }
}
