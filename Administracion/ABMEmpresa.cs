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
    public partial class ABMEmpresa : Form
    {
        public ABMEmpresa()
        {
            InitializeComponent();
        }
        private Empresa empBuscada = null;

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ServicioClient serv = new ServicioClient();
            empBuscada=    serv.BuscarEmpresa(Convert.ToInt32(txtCodigo.Text));
            txtCodigo.Text = empBuscada.Codigo.ToString();
            txtRut.Text = empBuscada.Rut.ToString();
            txtDireccion.Text = empBuscada.DirFiscal;
            txtTelefono.Text = empBuscada.Telefono;

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

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Empresa emp = new Empresa();
                emp.Codigo = Convert.ToInt32(txtCodigo.Text);
                emp.Rut = Convert.ToInt32(txtRut.Text);
                emp.DirFiscal = txtDireccion.Text;
                emp.Telefono = txtTelefono.Text;

                ServicioClient serv = new ServicioClient();
                serv.AltaEmpresa(emp);

                lblMensajes.Text = "Empresa ingresada exitosamente";
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
                empBuscada.Rut = Convert.ToInt32(txtRut.Text);
                empBuscada.DirFiscal = txtDireccion.Text;
                empBuscada.Telefono = txtTelefono.Text;

                ServicioClient serv = new ServicioClient();
                serv.ModificarEmpresa(empBuscada);
                lblMensajes.Text = "Empresa Modificada con exito";
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
                serv.BajaEmpresa(empBuscada);
                lblMensajes.Text = "Empresa Dada de Baja";
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos() 
        {
            txtCodigo.Text = "";
            txtDireccion.Text = "";
            txtRut.Text = "";
            txtTelefono.Text = "";
        }
    }
}
