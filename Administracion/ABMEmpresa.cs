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
        Usuario usuLogueado = null;

        public ABMEmpresa(Usuario usu)
        {
            usuLogueado = usu;
            InitializeComponent();
        }

        private Empresa empBuscada = null;

        //Valida código de Empresa
        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtCodigo.Text.Trim() != "" && txtCodigo.Text.Trim().Length <= 4)
                {
                    EPCodigoEmp.Clear();
                }
                else
                    throw new Exception("El código de empresa debe tener entre 1 y 4 dígitos");
            }

            catch (Exception ex)
            {
                EPCodigoEmp.SetError(txtCodigo, ex.Message);
                e.Cancel = true;
                return;
            }

            try
            {
                IServicio serv = new ServicioClient();

                //Busco empresa
                empBuscada = serv.BuscarEmpresa(Convert.ToInt32(txtCodigo.Text));

                if (empBuscada != null)
                {
                    txtCodigo.Text = empBuscada.Codigo.ToString();
                    txtRut.Text = empBuscada.Rut.ToString();
                    txtDireccion.Text = empBuscada.DirFiscal;
                    txtTelefono.Text = empBuscada.Telefono;
                    HabilitarBotones();
                    txtCodigo.Enabled = false;
                }
                else
                {
                    btnIngresar.Enabled = true;
                }
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

        //Modificar Empresa
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                empBuscada.Rut = Convert.ToInt32(txtRut.Text.Trim());
                empBuscada.DirFiscal = txtDireccion.Text.Trim();
                empBuscada.Telefono = txtTelefono.Text.Trim();

                IServicio serv = new ServicioClient();
                serv.ModificarEmpresa(empBuscada, usuLogueado);
                lblMensajes.Text = "Empresa Modificada con exito";
                LimpiarCampos();
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 80)
                    lblMensajes.Text = ex.Detail.InnerText.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Detail.InnerText;
            }

            catch (FormatException)
            {
                lblMensajes.Text = "El Código y Rut de la Empresa deben ser numéricos";
            }

            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensajes.Text = ex.Message.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Message;
            }
        }

        //Eliminar Empresa
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                IServicio serv = new ServicioClient();
                serv.BajaEmpresa(empBuscada, usuLogueado);
                LimpiarCampos();
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

        //Limpiar los controles
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            lblMensajes.Text = "";
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = "";
            txtDireccion.Text = "";
            txtRut.Text = "";
            txtTelefono.Text = "";
            btnEliminar.Enabled = false;
            btnIngresar.Enabled = false;
            btnModificar.Enabled = false;
            txtCodigo.Enabled = true;
        }

        private void HabilitarBotones()
        {
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        //Ingresar Empresa
        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            try
            {
                Empresa emp = new Empresa();
                emp.Codigo = Convert.ToInt32(txtCodigo.Text.Trim());
                emp.Rut = Convert.ToInt32(txtRut.Text.Trim());
                emp.DirFiscal = txtDireccion.Text.Trim();
                emp.Telefono = txtTelefono.Text.Trim();

                IServicio serv = new ServicioClient();
                serv.AltaEmpresa(emp, usuLogueado);

                lblMensajes.Text = "Empresa ingresada exitosamente";

                LimpiarCampos();
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 80)
                    lblMensajes.Text = ex.Detail.InnerText.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Detail.InnerText;
            }

            catch (FormatException)
            {
                lblMensajes.Text = "El Código y Rut de la Empresa deben ser numéricos";
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
