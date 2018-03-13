using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IEmpresaPersistencia
    {
        Empresa BuscarEmpresa(int codEmp,Usuario usuLogueado);
        Empresa BuscarEmpresaWEB(int codEmp);
        
        void AltaEmpresa(Empresa unaEmpresa, Usuario usuLogueado);
        void BajaEmpresa(Empresa unaEmpresa, Usuario usuLogueado);
        void ModificarEmpresa(Empresa unaEmpresa, Usuario usuLogueado);

     
    }
}
