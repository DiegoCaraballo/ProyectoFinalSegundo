using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Administracion.ServicioWCF;

namespace Administracion
{
    public partial class ABMGerente : Form
    {
        public ABMGerente()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Gerente usu = new Gerente();
                usu.Cedula = Convert.ToInt32(txtCedula.Text);
                usu.NomUsu = txtUsuario.Text;
                usu.Pass = txtPass.Text;
                usu.NomCompleto = txtNomApe.Text;
                usu.Correo = txtCorreo.Text;

                Servicio serv = new Servicio();
                serv.AltaUsuario(usu);
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 80)
                    lblMensajes.Text = ex.Detail.InnerText.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Detail.InnerText;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensajes.Text = ex.Message.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Message;
            }


        }
    }
}
