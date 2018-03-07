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
        try
        {
            if (!IsPostBack)
            {
                CargarLista();
            }
        }
        catch(Exception)
        {
            throw new Exception();
        }

    }

    //Cargar lista de empresas al repeater
    protected void CargarLista()
    {
        try 
        {
            IServicio serv = new ServicioClient();

            List<Empresa> lasEmpresas = new List<Empresa>();
            lasEmpresas = serv.ListarEmpresas().ToList();

            //Cargo repeater
            rpEmpresas.DataSource = lasEmpresas;
            rpEmpresas.DataBind();

            //Si no hay empresas
            if(rpEmpresas.Items.Count == 0)
                throw new Exception("No hay empresas para listar");

        }
        catch(Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}