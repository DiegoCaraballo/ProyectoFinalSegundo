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
    public partial class CambioPassword : Form
    {
        Usuario usuLogueado = null;
        public CambioPassword(Usuario pUsuLogueado)
        {
            usuLogueado = pUsuLogueado;
            InitializeComponent();
        }

        //Validación de Password
        private void txtActualPass_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtActualPass.Text == "")
                {
                    throw new Exception();
                }
                else
                {
                    epPass.Clear();
                }
            }
            catch (Exception)
            {
                epPass.SetError(txtActualPass, "la contraseña no puede estar vacia");
                e.Cancel = true;
                return;
            }
            try
            {
                if (usuLogueado.Pass != txtActualPass.Text)
                {
                    throw new Exception("La Contraseña actual ingresada no es correcta");
                }
                else
                {
                    txtActualPass.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensajes.Text = ex.Message.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Message;
            }
        }

        //Cambiar contraseña de usuario
        private void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNuevaPass.Text == txtRepitePass.Text)
                {
                    Usuario usuCambiado = new Usuario();
                    usuCambiado.Cedula = usuLogueado.Cedula;
                    usuLogueado.NomUsu = usuLogueado.NomUsu;
                    usuLogueado.NomCompleto = usuLogueado.NomCompleto;
                    usuCambiado.Pass = txtNuevaPass.Text;

                    IServicio serv = new ServicioClient();
                    serv.CambioPass(usuCambiado, usuLogueado);
                    LimpiarCampos();
                    usuLogueado.Pass = usuCambiado.Pass;//Ver si este cambio funciona
                    lblMensajes.Text = "Contraseña actualizada con exito";
                }
                else
                {
                    throw new Exception("La nueva contraseña ingresada no coincide");
                }
            }
            catch (Exception ex)
            { lblMensajes.Text = ex.Message; }
        }

        //Limpiar los campos
        private void LimpiarCampos()
        {
            txtActualPass.Text = "";
            txtNuevaPass.Text = "";
            txtRepitePass.Text = "";
            txtActualPass.Enabled = true;
        }

        //Limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            lblMensajes.Text = "";
        }


    }
}
