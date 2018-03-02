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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usu = null;

                
               ServicioClient  serv = new ServicioClient();

                usu = serv.Logueo(txtUsuario.Text);
                if (usu == null)
                {
                    lblMensajes.Text = "No se encontro el usuario";
                }
                if (usu.Pass == txtPass.Text)
                {
                    this.Hide();
                    Form unForm = new Default(usu);
                    unForm.ShowDialog();
                    this.Close();
                
                }

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 40)
                    lblMensajes.Text = ex.Detail.InnerText.Substring(0, 40);
                else
                    lblMensajes.Text = ex.Detail.InnerText;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 40)
                    lblMensajes.Text = ex.Message.Substring(0, 40);
                else
                    lblMensajes.Text = ex.Message;
            }

        }
    }
}
