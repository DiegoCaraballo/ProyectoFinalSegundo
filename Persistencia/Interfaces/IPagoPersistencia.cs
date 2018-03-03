using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPagoPersistencia
    {
        void AltaPago(Pago unPago);
        void BajaPago(Pago unPago);
        void ModificarPago(Pago unPago);
        Pago BuscarPago(int numeroInt);
        List<Pago> listar();
    }
}
