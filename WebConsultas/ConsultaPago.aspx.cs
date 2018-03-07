using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServicioWCF;
using System.Globalization;

public partial class ConsultaPago : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                QuitarControles();
            }
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    //Buscar Factura
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            IServicio serv = new ServicioClient();

            if (txtCodBarra.Text.Length != 25 || txtCodBarra.Text == "")
                throw new Exception("El código de barras debe contener 25 números");

            int codEmp = Convert.ToInt32(txtCodBarra.Text.Substring(0, 4).TrimStart('0'));
            int codTipoContrato = Convert.ToInt32(txtCodBarra.Text.Substring(4, 2).TrimStart('0'));
            var fechaFactura = DateTime.ParseExact(txtCodBarra.Text.Substring(6, 8).TrimStart('0').ToString(),
                  "yyyyMMdd",
                   CultureInfo.InvariantCulture);
            int codCli = Convert.ToInt32(txtCodBarra.Text.Substring(14, 6).TrimStart('0'));
            int monto = Convert.ToInt32(txtCodBarra.Text.Substring(20, 5).TrimStart('0'));

            //Armo el pago para buscar
            Pago unPago = serv.PagoDeUnaFactura(codTipoContrato, codEmp, monto, codCli, fechaFactura);

            //Si el pago existe muestro los controles con los datos
            if (unPago != null)
            {
                MostrarControles();
                txtFecha.Text = unPago.Fecha.ToString();
                txtIdPago.Text = unPago.NumeroInt.ToString();
            }
            else
                throw new Exception("No existe un pago asociado a la factura ingresada");

        }
        catch(Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    //Dejar los controles invisibles
    protected void QuitarControles()
    {
        lblFecha.Visible = false;
        lblIdPago.Visible = false;
        txtFecha.Visible = false;
        txtIdPago.Visible = false;
    }

    //Dejar los controles visibles
    protected void MostrarControles()
    {
        lblFecha.Visible = true;
        lblIdPago.Visible = true;
        txtFecha.Visible = true;
        txtIdPago.Visible = true;
    }
}