using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface IPagoLogica
    {
        void AltaPago(Pago unPago);
        void BajaPago(Pago unPago);
        void ModificarPago(Pago unPago);
        Pago Buscar(int numInterno);
        List<Pago> listar();
    }
}
