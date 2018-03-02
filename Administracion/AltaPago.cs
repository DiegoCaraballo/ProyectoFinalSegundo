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
            CargarColumnasGridView();
        }

        DataTable dt = new DataTable();

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
            btnAgregar.Enabled = false;
        }

        //Dejar todos los datos en 0
        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LimpiarDatosFactura();
            txtMontoTotal.Text = "";
            gvListaFacturas.DataSource = null;
            btnAgregar.Enabled = false;
        }

        //Quitar linea de factura del GridView
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvListaFacturas.Rows.Count == 0)
                {
                    throw new Exception("No hay Facturas");
                }
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
            // TODO - Agregar filas al grid

            DataRow dr = this.dt.NewRow();
            dr["codEmp"] = txtCodEmp.Text;
            dr["codContrato"] = txtTipoContrato.Text;
            dr["fechaVto"] = txtFVencimiento.Text;
            dr["codCli"] = txtCodCli.Text;
            dr["monto"] = txtMonto.Text;

            dt.Rows.Add(dr);

            //Limpio los dato de la factura
            LimpiarDatosFactura();
        }

        private void CargarColumnasGridView()
        {
            dt.Columns.Add("codEmp");
            dt.Columns.Add("codContrato");
            dt.Columns.Add("fechaVto");
            dt.Columns.Add("codCli");
            dt.Columns.Add("monto");

            this.gvListaFacturas.DataSource = dt;
        }

        //Validación del Codigo de barras y carga de factura
        private void txtCodBarra_Validating_1(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtCodBarra.Text.Trim() != "" && txtCodBarra.Text.Trim().Length == 25)
                    EPBarras.Clear();
                else
                    throw new Exception("El código de barras debe contener 25 números");
            }
            catch (Exception ex)
            {
                EPBarras.SetError(txtCodBarra, ex.Message);
                e.Cancel = true;
                return;
            }

            try
            {

                int codEmp = Convert.ToInt32(txtCodBarra.Text.Substring(0, 4).TrimStart('0'));
                int codTipoContrato = Convert.ToInt32(txtCodBarra.Text.Substring(4, 2).TrimStart('0'));

                ServicioClient serv = new ServicioClient();

                //Busco la Empresa
                Empresa unaEmpresa = serv.BuscarEmpresa(codEmp);
                if (unaEmpresa == null)
                    throw new Exception("La empresa no existe");

                //Busco el Tipo de Contrato
                TipoContrato unContrato = serv.BuscarContrato(codEmp, codTipoContrato);
                if (unContrato == null)
                    throw new Exception("El tipo de contrato no existe");

                btnAgregar.Enabled = true;

                //Cargo todos los textbox
                txtCodCli.Text = Convert.ToInt32(txtCodBarra.Text.Substring(14, 6).TrimStart('0')).ToString();
                txtCodEmp.Text = unaEmpresa.Rut.ToString();
                txtFVencimiento.Text = Convert.ToInt32(txtCodBarra.Text.Substring(6, 8).TrimStart('0')).ToString();
                txtMonto.Text = Convert.ToInt32(txtCodBarra.Text.Substring(20, 5).TrimStart('0')).ToString();
                txtTipoContrato.Text = unContrato.Nombre.ToString();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        //Calcular el total de las facturas
        private void gvListaFacturas_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int total = 0;

            foreach (DataRow dr in dt.Rows)
            {
                total = total + Convert.ToInt32(dr["monto"]);
            }

            txtMontoTotal.Text = total.ToString();
        }


        //Si no quedan filas se deja en 0 el monto total
        private void gvListaFacturas_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                if (gvListaFacturas.Rows.Count == 0)
                {
                    txtMontoTotal.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
