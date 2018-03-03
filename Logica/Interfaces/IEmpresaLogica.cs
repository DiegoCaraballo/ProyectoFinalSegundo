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
        void AltaEmpresa(Empresa unaEmpresa);
        void BajaEmpresa(Empresa unaEmpresa);
        void ModificarEmpresa(Empresa unaEmpresa);
        List<Empresa> ListarEmpresas();
    }
}
