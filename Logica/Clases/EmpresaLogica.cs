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

        public Empresa BuscarEmpresa(int codEmp,Usuario usuLogueado)
        {
            try
            {
                return ((Empresa)FabricaPersistencia.GetPersistenciaEmpresa().BuscarEmpresa(codEmp,usuLogueado));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public void AltaEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaEmpresa().AltaEmpresa(unaEmpresa, usuLogueado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BajaEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaEmpresa().BajaEmpresa(unaEmpresa, usuLogueado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaEmpresa().ModificarEmpresa(unaEmpresa, usuLogueado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
