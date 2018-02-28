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

        public void AltaPago(Pago unPago)
        {
            FabricaPersistencia.GetPersistenciaPago().AltaPago((Pago)unPago);
        }

        //TODO - Ver si el BajaPago y ModificarPago se utilizan en algun momento
        //Lo mismo para persistencia
        public void BajaPago(Pago unPago)
        {
            FabricaPersistencia.GetPersistenciaPago().BajaPago((Pago)unPago);
        }

        public void ModificarPago(Pago unPago)
        {
            FabricaPersistencia.GetPersistenciaPago().ModificarPago((Pago)unPago);
        }

        public Pago Buscar(int numInterno)
        {
            return FabricaPersistencia.GetPersistenciaPago().BuscarPago((int)numInterno);
        }
    }
}
