using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class EmpresaLogica: IEmpresaLogica
    {
        private static EmpresaLogica _instancia = null;
        private EmpresaLogica() { }
        public static EmpresaLogica GetInstancia()
        {
            if (_instancia == null)
                _instancia = new EmpresaLogica();
            return _instancia;
        }

        public Empresa BuscarEmpresa(int codEmp)
        {
            return((Empresa)FabricaPersistencia.GetPersistenciaEmpresa().BuscarEmpresa(codEmp));
        }

        public void AltaEmpresa(Empresa unaEmpresa)
        {
          FabricaPersistencia.GetPersistenciaEmpresa().AltaEmpresa(unaEmpresa);
        }
        public void BajaEmpresa(Empresa unaEmpresa)
        {
            FabricaPersistencia.GetPersistenciaEmpresa().BajaEmpresa(unaEmpresa);
        }
        public void ModificarEmpresa(Empresa unaEmpresa)
        {
            FabricaPersistencia.GetPersistenciaEmpresa().ModificarEmpresa(unaEmpresa);
        }
    }
}
