using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class PagoLogica : IPagoLogica
    {
        private static PagoLogica _instancia = null;
        private PagoLogica() { }
        public static PagoLogica GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PagoLogica();
            return _instancia;
        }

        public void AltaPago(Pago unPago, Usuario usuLogueado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaPago().AltaPago((Pago)unPago, usuLogueado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime PagoDeUnaFactura(int codContrato, int codEmp, int monto, int codCli, DateTime fecha)
        {
            try
            {
                return FabricaPersistencia.GetPersistenciaPago().PagoDeUnaFactura((int)codContrato, (int)codEmp, (int)monto, (int)codCli, (DateTime)fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Pago> listar(Usuario usuLogueado)
        {
            try
            {
                return (FabricaPersistencia.GetPersistenciaPago().listar(usuLogueado));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
