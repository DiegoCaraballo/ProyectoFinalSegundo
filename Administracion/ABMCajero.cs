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
    public partial class ABMCajero : Form
    {
        Usuario usuBuscado = null;
        public ABMCajero()
        {
            InitializeComponent();
        }

        private void ABMCajero_Load(object sender, EventArgs e)
        {
           
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Cajero cajero = new Cajero();
                cajero.Cedula = Convert.ToInt32(txtCedula.Text);
                cajero.NomUsu = txtUsuario.Text;
                cajero.Pass = txtPass.Text;
                cajero.NomCompleto = txtNomApe.Text;
                cajero.HoranIni = Convert.ToDateTime(txtHoraInicio.Text);
                cajero.HoranFin = Convert.ToDateTime(txtHoraFin.Text);

                ServicioClient serv = new ServicioClient();
                serv.AltaUsuario(cajero);

                lblMensajes.Text = "El usuario ingresado exitosamente";
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

        private void txtCedula_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ServicioClient serv = new ServicioClient();
                usuBuscado = serv.BuscarUsuario(Convert.ToInt32(txtCedula.Text));
                txtCedula.Text = usuBuscado.Cedula.ToString();
                txtUsuario.Text = usuBuscado.NomUsu;
                txtNomApe.Text = usuBuscado.NomCompleto;
                txtHoraFin.Text = ((Cajero)usuBuscado).HoranFin.ToString();
                txtHoraInicio.Text = ((Cajero)usuBuscado).HoranIni.ToString();
                
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
               // usuBuscado.Cedula =Convert.ToInt32( txtCedula.Text) ;
                usuBuscado.NomUsu=txtUsuario.Text ;
                usuBuscado.NomCompleto=txtNomApe.Text;
              ((Cajero)usuBuscado).HoranFin=Convert.ToDateTime( txtHoraFin.Text);
                 ((Cajero)usuBuscado).HoranIni=Convert.ToDateTime( txtHoraInicio.Text);

                 ServicioClient serv = new ServicioClient();
                serv.ModificarUsuario(usuBuscado);

                lblMensajes.Text = "El usuario fue modificado exitosamente";
                
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ServicioClient serv = new ServicioClient();
                serv.BajaUsuario(usuBuscado);
                lblMensajes.Text = "El usuario fue modificado exitosamente";
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

        private void LimpiarCampos()
        {
            txtCedula.Text = "";
            txtUsuario.Text = "";
            txtPass.Text = "";
            txtNomApe.Text = "";
            txtHoraInicio.Text = "";
            txtHoraFin.Text = "";

            txtCedula.Enabled = true;
            txtUsuario.Enabled = true;
            txtPass.Enabled = true;
            txtNomApe.Enabled = true;
            txtHoraInicio.Enabled = true;
            txtHoraFin.Enabled = true;
        }

        private void BLoquearCampos()
        {
            txtCedula.Enabled = false;
            txtUsuario.Enabled = true;
            txtPass.Enabled = false;
            txtNomApe.Enabled = true;
            txtHoraInicio.Enabled = true;
            txtHoraFin.Enabled = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

    }
}
