using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPagoPersistencia
    {
        void AltaPago(Pago unPago, Usuario usuLogueado);
        DateTime PagoDeUnaFactura(int codContrato, int codEmp, int monto, int codCli, DateTime fecha);
        List<Pago> listar(Usuario usuLogueado);
    }
}
