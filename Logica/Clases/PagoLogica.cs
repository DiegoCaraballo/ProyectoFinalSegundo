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
            FabricaPersistencia.GetPersistenciaPago().AltaPago((Pago)unPago,usuLogueado);
        }

        //TODO - Ver si el BajaPago y ModificarPago se utilizan en algun momento
        //Lo mismo para persistencia
        public void BajaPago(Pago unPago, Usuario usuLogueado)
        {
            FabricaPersistencia.GetPersistenciaPago().BajaPago((Pago)unPago,usuLogueado);
        }

        public void ModificarPago(Pago unPago,Usuario usuLogueado)
        {
            FabricaPersistencia.GetPersistenciaPago().ModificarPago((Pago)unPago,usuLogueado);
        }

        public Pago Buscar(int numInterno, Usuario usuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaPago().BuscarPago((int)numInterno,usuLogueado);
        }

        public Pago PagoDeUnaFactura(int codContrato, int codEmp, int monto, int codCli, DateTime fecha)
        {
            return FabricaPersistencia.GetPersistenciaPago().PagoDeUnaFactura((int)codContrato, (int)codEmp, (int)monto, (int)codCli, (DateTime)fecha);
        }

        public List<Pago> listar(Usuario usuLogueado) 
        {
            return (FabricaPersistencia.GetPersistenciaPago().listar(usuLogueado));
        }
    }
}
