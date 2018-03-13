using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface IPagoLogica
    {
        void AltaPago(Pago unPago, Usuario usuLogueado);
        List<Pago> listar(Usuario usuLogueado);
        DateTime PagoDeUnaFactura(int codContrato, int codEmp, int monto, int codCli, DateTime fecha);
    }
}
