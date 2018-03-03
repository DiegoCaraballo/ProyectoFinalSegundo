using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServicioWCF;

public partial class ConsultaEmpresas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Cargar lista de empresas al repeater
    protected void CargarLista()
    {
        try 
        {
            ServicioClient serv = new ServicioClient();

            //List<Empresa> lasEmpresas = serv.ListarEmpresas();


        }
        catch(Exception ex)
        {
            lblMensaje.Text = "Error: " + ex.Message;
        }
    }
}