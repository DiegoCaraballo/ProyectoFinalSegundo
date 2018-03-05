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
        void BajaPago(Pago unPago, Usuario usuLogueado);
        void ModificarPago(Pago unPago, Usuario usuLogueado);
        Pago Buscar(int numInterno, Usuario usuLogueado);
        Pago PagoDeUnaFactura(int codContrato, int codEmp, int monto, int codCli, DateTime fecha);
        List<Pago> listar(Usuario usuLogueado);
    }
}
