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
        Pago PagoDeUnaFactura(int codContrato, int codEmp, int monto, int codCli, DateTime fecha);
        List<Pago> listar();
    }
}
