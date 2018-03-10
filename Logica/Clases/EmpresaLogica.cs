using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class EmpresaLogica : IEmpresaLogica
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
            return ((Empresa)FabricaPersistencia.GetPersistenciaEmpresa().BuscarEmpresa(codEmp));
        }

        public void AltaEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            FabricaPersistencia.GetPersistenciaEmpresa().AltaEmpresa(unaEmpresa, usuLogueado);
        }

        public void BajaEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            FabricaPersistencia.GetPersistenciaEmpresa().BajaEmpresa(unaEmpresa, usuLogueado);
        }

        public void ModificarEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            FabricaPersistencia.GetPersistenciaEmpresa().ModificarEmpresa(unaEmpresa, usuLogueado);
        }

        public List<Empresa> ListarEmpresas()
        {
            List<Empresa> lista = FabricaPersistencia.GetPersistenciaEmpresa().ListarEmpresas();

            return lista;
        }
    }
}
