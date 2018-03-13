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
        Usuario usuLogueado = null;
        public ABMTipoContrato(Usuario usu)
        {
            usuLogueado = usu;
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

        private void HabilitarBotones()
        {
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
        }

        //Ingresar Tipo de contrato
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                IServicio serv = new ServicioClient();

                TipoContrato unTipoContrato = new TipoContrato();
                unTipoContrato.CodContrato = Convert.ToInt32(txtCodTipoContrato.Text);
                unTipoContrato.UnaEmp = serv.BuscarEmpresa(Convert.ToInt32(txtCodEmpresa.Text),usuLogueado);
                unTipoContrato.Nombre = txtNombre.Text;

                if (unTipoContrato.UnaEmp == null)
                    throw new Exception("No existe la empresa");

                serv.AltaTipoContrato(unTipoContrato, usuLogueado);
                lblMensaje.Text = "Tipo Contrato ingresado con exito";
                LimpiarCampos();
            }         

            catch (FormatException)
            {
                lblMensaje.Text = ("El codigo de Emprasa y de Contratos deben ser numéricos");
            }

            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensaje.Text = ex.Message.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Message;
            }
        }

        //Modificar tipo de contrato
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                TipoContratoBuscado.Nombre = txtNombre.Text;
                IServicio serv = new ServicioClient();
                serv.ModificarTipoContrato(TipoContratoBuscado, usuLogueado);

                lblMensaje.Text = "Tipo contrato modificado con exito";
                LimpiarCampos();
            }
            
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensaje.Text = ex.Message.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Message;
            }
        }

        //Eliminar tipo de contrato
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                IServicio serv = new ServicioClient();
                serv.BajaTipoContrato(TipoContratoBuscado, usuLogueado);

                lblMensaje.Text = "Tipo Contrato Dado de Baja exitosamente";
                LimpiarCampos();
            }
            
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensaje.Text = ex.Message.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Message;
            }
        }

        //Limpiar campos
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            lblMensaje.Text = "";
            EPTipoContrato.Clear();
        }

        private void LimpiarCampos()
        {
            txtCodEmpresa.Text = "";
            txtCodTipoContrato.Text = "";
            txtNombre.Text = "";
            btnModificar.Enabled = false;
            btnIngresar.Enabled = false;
            btnEliminar.Enabled = false;
            txtCodEmpresa.Enabled = true;
            txtCodTipoContrato.Enabled = true;
        }

        //Validar los 2 campos (Código de Empresa y Tipo de Contrato)
        private void txtCodTipoContrato_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtCodEmpresa.Text.Trim() != "" && txtCodEmpresa.Text.Trim().Length <= 4 && txtCodTipoContrato.Text.Trim() != "" && txtCodTipoContrato.Text.Trim().Length <= 2)
                {
                    EPTipoContrato.Clear();
                }
                else
                {
                    throw new Exception("El codigo de Empresa debe tener entre 1 y 4 dígitos y el CodContrato entre 1 y 2");
                }
            }
            catch (Exception ex)
            {
                EPTipoContrato.SetError(txtCodEmpresa, ex.Message);
                e.Cancel = true;
                return;
            }

            try
            {
                lblMensaje.Text = "";
                IServicio serv = new ServicioClient();

                //Busco el tipo de contrato
                TipoContratoBuscado = serv.BuscarContrato(Convert.ToInt32(txtCodEmpresa.Text), Convert.ToInt32(txtCodTipoContrato.Text),usuLogueado);

                if (TipoContratoBuscado != null)
                {
                    HabilitarBotones();
                    txtNombre.Text = TipoContratoBuscado.Nombre;
                    txtCodEmpresa.Enabled = false;
                    txtCodTipoContrato.Enabled = false;
                    btnIngresar.Enabled = false;
                }
                else
                {
                    btnIngresar.Enabled = true;
                }
            }           

            catch (FormatException)
            {
                lblMensaje.Text = ("El codigo de Emprasa y de Contratos deben ser numéricos");
            }

            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensaje.Text = ex.Message.Substring(0, 80);
                else
                    lblMensaje.Text = ex.Message;
            }
        }
    }
}
