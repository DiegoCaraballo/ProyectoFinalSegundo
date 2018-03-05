using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface IEmpresaLogica
    {
        Empresa BuscarEmpresa(int codEmp);
        void AltaEmpresa(Empresa unaEmpresa, Usuario usuLogueado);
        void BajaEmpresa(Empresa unaEmpresa, Usuario usuLogueado);
        void ModificarEmpresa(Empresa unaEmpresa, Usuario usuLogueado);
        List<Empresa> ListarEmpresas();
    }
}
