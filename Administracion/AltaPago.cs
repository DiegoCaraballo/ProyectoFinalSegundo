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
    public partial class AltaPago : Form
    {
        public AltaPago()
        {
            InitializeComponent();
        }

        //Borra los datos de la factura
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarDatosFactura();
        }

        //Borrar los datos de la facutra
        private void LimpiarDatosFactura()
        {
            txtCodBarra.Text = "";
            txtCodCli.Text = "";
            txtCodEmp.Text = "";
            txtFVencimiento.Text = "";
            txtMonto.Text = "";
            txtTipoContrato.Text = "";
            lblMensaje.Text = "";
        }

        //Dejar todos los datos en 0
        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LimpiarDatosFactura();
            txtMontoTotal.Text = "";
            gvListaFacturas.DataSource = null;
        }

        //Quitar linea de factura del GridView
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvListaFacturas.Rows.Count == 0)
                    throw new Exception("No hay Facturas");
                else if (gvListaFacturas.CurrentRow == null)
                    throw new Exception("No hay una factura seleccionada");
                else
                {
                    foreach (DataGridViewRow item in this.gvListaFacturas.SelectedRows)
                    {
                        gvListaFacturas.Rows.RemoveAt(item.Index);
                    }
                }
            }
            catch(Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        //Agregar facturas al GridView
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodBarra.Text.Trim() == "")
                    throw new Exception("Debe ingresar un código de barras");
                else if (txtCodBarra.Text.Trim().Count() != 25)
                    throw new Exception("El código de barras debe tener 25 caracteres");
            }
            catch(Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }

            try
            {
                Empresa unaEmpresa = null;
                TipoContrato unContrato = null;

            }
            catch(Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}
