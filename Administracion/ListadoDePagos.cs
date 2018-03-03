using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml.Linq;
using Administracion.ServicioWCF;

using System.Xml;

namespace Administracion
{
    public partial class ListadoDePagos : Form
    {
       // private XElement documento = null;
        public ListadoDePagos()
        {
            InitializeComponent();
            cboCajero.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEmpresa.DropDownStyle = ComboBoxStyle.DropDownList;
            CargoListaPagos();
        }

        List<Pago> listadoPagos = new List<Pago>();

        //Se carga la lista de pagos por defecto con més y año actual
        private void CargoListaPagos()
        {
            ServicioClient serv = new ServicioClient();
            listadoPagos = serv.listar().ToList();

            gvPagos.DataSource = listadoPagos;

        }

        //Limpiar Filtros
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            gvPagos.DataSource = null;
            cboEmpresa.SelectedIndex = 0;
            cboCajero.SelectedIndex = 0;
          //  XElement doc = documento;
        }

        //Filtrar por Cajero
        private void cboCajero_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvPagos.DataSource = null;
            try
            {
                //TODO - Hacer filtro por Cajero
            }
            catch(Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        //Filtrar por Empresa
        private void cboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvPagos.DataSource = null;
            try
            {
                //TODO - Hacer filtro por Empresa
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        //Resumen por Cajero
        private void btnResumenCajero_Click(object sender, EventArgs e)
        {
            gvPagos.DataSource = null;
            try
            {
                //TODO - Hacer filtro por resumen Cajeros
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}
