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
    public partial class ABMTipoContrato : Form
    {
        public ABMTipoContrato()
        {
            InitializeComponent();
        }
        TipoContrato TipoContratoBuscado = null;

        //Borro los datos del tipo de contrato
        private void LimpiarDatosTipoContrato()
        {
            txtCodEmpresa.Text = "";
            txtCodTipoContrato.Text = "";
            txtNombre.Text = "";
        }

        private void txtCodEmpresa_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ServicioClient serv = new ServicioClient();
                TipoContratoBuscado = serv.BuscarContrato(Convert.ToInt32(txtCodEmpresa.Text), Convert.ToInt32(txtCodTipoContrato.Text));
                txtNombre.Text = TipoContratoBuscado.Nombre;
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 80)
                    lblMensaje.Text = ex.Detail.InnerText.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Detail.InnerText;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensaje.Text = ex.Message.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Message;
            }

        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                ServicioClient serv = new ServicioClient();

                TipoContrato unTipoContrato = new TipoContrato();
                unTipoContrato.CodContrato = Convert.ToInt32(txtCodTipoContrato.Text);
                unTipoContrato.UnaEmp = serv.BuscarEmpresa(Convert.ToInt32(txtCodEmpresa.Text));
                unTipoContrato.Nombre = txtNombre.Text;

                serv.AltaTipoContrato(unTipoContrato);
                lblMensaje.Text = "Tipo Contrato ingresado con exito";
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 80)
                    lblMensaje.Text = ex.Detail.InnerText.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Detail.InnerText;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensaje.Text = ex.Message.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Message;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                TipoContratoBuscado.Nombre = txtNombre.Text;
                ServicioClient serv = new ServicioClient();
                serv.ModificarTipoContrato(TipoContratoBuscado);

                lblMensaje.Text = "Tipo contrato modificado con exito";
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 80)
                    lblMensaje.Text = ex.Detail.InnerText.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Detail.InnerText;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensaje.Text = ex.Message.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Message;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ServicioClient serv = new ServicioClient();
                serv.BajaTipoContrato(TipoContratoBuscado);

                lblMensaje.Text = "Tipo Contrato Dado de Baja exitosamente";
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 80)
                    lblMensaje.Text = ex.Detail.InnerText.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Detail.InnerText;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensaje.Text = ex.Message.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Message;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtCodEmpresa.Text = "";
            txtCodTipoContrato.Text = "";
            txtNombre.Text = "";
        }

      


    }
}
