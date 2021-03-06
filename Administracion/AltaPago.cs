﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Administracion.ServicioWCF;
using System.Globalization;

namespace Administracion
{
    public partial class AltaPago : Form
    {
        Cajero usuLogueado = null;

        public AltaPago(Cajero pUsuLogueado)
        {
            InitializeComponent();
            CargarColumnasGridView();
            usuLogueado = pUsuLogueado;
        }

        List<Factura> lasFacturas = new List<Factura>();
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
            try
            {
                LimpiarDatosFactura();
                txtMontoTotal.Text = "";
                dt = new DataTable();
                CargarColumnasGridView();
                btnAgregar.Enabled = false;
                txtMontoTotal.Text = "";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        //Quitar linea de factura del GridView
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            int total = 0;

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
                    //Quito las facturas de la lista
                    int indice = gvListaFacturas.CurrentCell.RowIndex;
                    lasFacturas.RemoveAt(indice);

                    foreach (DataGridViewRow item in this.gvListaFacturas.SelectedRows)
                    {
                        //Quito las facturas del grid
                        gvListaFacturas.Rows.RemoveAt(item.Index);
                    }

                    //Borro el monto total si no hay facturas
                    if (gvListaFacturas.Rows.Count == 1)
                    {
                        txtMontoTotal.Text = "";
                    }
                    else
                    {   //Si no lo vuelvo a calcualar
                        foreach (DataRow dr in dt.Rows)
                        {
                            total = total + Convert.ToInt32(dr["Monto"]);
                        }

                        txtMontoTotal.Text = total.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        //Agregar facturas al GridView
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //Capturo el codEmp y codTipoContrato de la facutra ingresada por el usuario
                int codEmp = Convert.ToInt32(txtCodBarra.Text.Substring(0, 4).TrimStart('0'));
                int codTipoContrato = Convert.ToInt32(txtCodBarra.Text.Substring(4, 2).TrimStart('0'));

                IServicio serv = new ServicioClient();

                //Si no existe tipo de contrato salgo
                TipoContrato unContrato = serv.BuscarContrato(codEmp, codTipoContrato, usuLogueado);
                if (unContrato == null)
                    throw new Exception("El tipo de contrato no existe");

                Factura unaFactura = new Factura();

                //Agrego datos a la factura
                unaFactura.UnTipoContrato = unContrato;
                unaFactura.Monto = Convert.ToInt32(txtMonto.Text);
                unaFactura.CodCli = Convert.ToInt32(txtCodCli.Text);
                var fechaFactura = DateTime.ParseExact(txtCodBarra.Text.Substring(6, 8).TrimStart('0').ToString(),
                  "yyyyMMdd",
                   CultureInfo.InvariantCulture);
                unaFactura.FechaVto = fechaFactura;

                //Si la factura esta vencida salgo
                if (fechaFactura < DateTime.Today)
                    throw new Exception("La factura esta vencida");

                //Agrego a la lista de facturas
                lasFacturas.Add(unaFactura);

                //Agrego al Grid
                DataRow dr = this.dt.NewRow();
                dr["Rut Emp"] = txtCodEmp.Text;
                dr["Cod Contrato"] = txtTipoContrato.Text;
                dr["Fecha Vto"] = txtFVencimiento.Text;
                dr["Cod Cli"] = txtCodCli.Text;
                dr["Monto"] = txtMonto.Text;

                dt.Rows.Add(dr);

                //Limpio los dato de la factura
                LimpiarDatosFactura();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        private void CargarColumnasGridView()
        {
            dt.Columns.Add("Rut Emp");
            dt.Columns.Add("Cod Contrato");
            dt.Columns.Add("Fecha Vto");
            dt.Columns.Add("Cod Cli");
            dt.Columns.Add("Monto");

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

                IServicio serv = new ServicioClient();

                //Busco la Empresa
                Empresa unaEmpresa = serv.BuscarEmpresa(codEmp, usuLogueado);
                if (unaEmpresa == null)
                    throw new Exception("La empresa no existe");

                //Busco el Tipo de Contrato
                TipoContrato unContrato = serv.BuscarContrato(codEmp, codTipoContrato, usuLogueado);
                if (unContrato == null)
                    throw new Exception("El tipo de contrato no existe");

                //Cargo todos los textbox
                txtCodCli.Text = Convert.ToInt32(txtCodBarra.Text.Substring(14, 6).TrimStart('0')).ToString();
                txtCodEmp.Text = unaEmpresa.Rut.ToString();


                try
                {
                    var newDate = DateTime.ParseExact(txtCodBarra.Text.Substring(6, 8).TrimStart('0').ToString(),
                                      "yyyyMMdd",
                                       CultureInfo.InvariantCulture);
                    txtFVencimiento.Text = newDate.ToShortDateString();
                }
                catch (Exception)
                {
                    throw new Exception("La fecha de la factura tiene un formato incorrecto");
                }

                txtMonto.Text = Convert.ToInt32(txtCodBarra.Text.Substring(20, 5).TrimStart('0')).ToString();
                txtTipoContrato.Text = unContrato.Nombre.ToString();

                //Si llego acá habilito el boton agregar factura y saco mensaje de error
                btnAgregar.Enabled = true;
                lblMensaje.Text = "";

            }

            catch (FormatException)
            {
                lblMensaje.Text = "Error: el código de barras debe ser numérico";
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
                total = total + Convert.ToInt32(dr["Monto"]);
            }

            txtMontoTotal.Text = total.ToString();
        }

        //Ingresar pago
        private void ingresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IServicio serv = new ServicioClient();

                try
                {   //Si hay facturas ingresadas
                    if (lasFacturas.Count != 0)
                    {
                        Pago unPago = new Pago();

                        unPago.NumeroInt = 0;
                        unPago.MontoTotal = Convert.ToInt32(txtMontoTotal.Text);
                        unPago.Fecha = DateTime.Today;
                        unPago.LasFacturas = lasFacturas.ToArray();
                        unPago.UsuCajero = usuLogueado;

                        //Alto el pago
                        serv.AltaPago(unPago, usuLogueado);

                        //Limpio los controles
                        LimpiarDatosFactura();
                        gvListaFacturas.Columns.Clear();
                        lasFacturas.Clear();
                        txtMontoTotal.Text = "";

                        dt = new DataTable();
                        CargarColumnasGridView();

                        lblMensaje.Text = "Pago agregado correctamente";
                    }
                    else
                    {
                        throw new Exception("El pago debe contener al menos una factura");
                    }
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

    }
}
