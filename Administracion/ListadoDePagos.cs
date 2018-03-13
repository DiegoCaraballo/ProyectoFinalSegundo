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
        Usuario usuLogueado = null;
        // private XElement documento = null;
        public ListadoDePagos(Usuario usu)
        {
            usuLogueado = usu;
            InitializeComponent();
            //cboCajero.DropDownStyle = ComboBoxStyle.DropDownList;
            //cboEmpresa.DropDownStyle = ComboBoxStyle.DropDownList;
            CargoListaPagos();
        }

        List<Pago> listadoPagos = new List<Pago>();

        //Se carga la lista de pagos por defecto con més y año actual
        private void CargoListaPagos()
        {
            try
            {
                IServicio serv = new ServicioClient();
                listadoPagos = serv.listar(usuLogueado).ToList();

                var res = (from lista in listadoPagos
                           where lista.Fecha.Month == DateTime.Now.Month && lista.Fecha.Year == DateTime.Now.Year
                           select new
                           {
                               IdPago = lista.NumeroInt,
                               Fecha = lista.Fecha,
                               Monto = lista.MontoTotal,
                               Cajero = lista.UsuCajero.NomUsu
                           }).ToList();

                gvPagos.DataSource = res;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        //Limpiar Filtros
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            gvPagos.DataSource = null;
            txtCajero.Text = "";
            txtEmpresa.Text = "";
            CargoListaPagos();
            lblMensaje.Text = "";
        }

        //Resumen por Cajero
        private void btnResumenCajero_Click(object sender, EventArgs e)
        {
            gvPagos.DataSource = null;
            try
            {
                gvPagos.DataSource = null;
                var res = (from lista in listadoPagos
                           group lista by lista.UsuCajero.Cedula into listaCajeros
                           select new
                           {
                               Cajero = listaCajeros.First().UsuCajero.NomUsu,
                               Cantidad = listaCajeros.Count(),
                               Total = listaCajeros.Sum(monto => monto.MontoTotal)
                           }).ToList();

                gvPagos.DataSource = res;
                lblMensaje.Text = "";


            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        //Validar el usuario
        private void txtCajero_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtCajero.Text.Trim() != "")
                {
                    gvPagos.DataSource = null;
                    var res = (from lista in listadoPagos
                               where lista.UsuCajero.Cedula == Convert.ToInt32(txtCajero.Text.Trim())
                               orderby lista.Fecha descending
                               select new
                               {
                                   Cajero = lista.UsuCajero.Cedula,
                                   Fecha = lista.Fecha,
                                   Monto = lista.MontoTotal
                               }).ToList();

                    gvPagos.DataSource = res;
                    txtCajero.Text = "";
                    lblMensaje.Text = "";
                 
                }

            }
            catch (FormatException)
            {
                lblMensaje.Text = "El campo Cajero debe ser numérico";
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        //Validar la Empresa
        private void txtEmpresa_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtEmpresa.Text.Trim() != "" && txtEmpresa.Text.Trim().Length <= 4)
                {
                    gvPagos.DataSource = null;

                    var res = (from l in listadoPagos
                               from asd in l.LasFacturas
                               where asd.UnTipoContrato.UnaEmp.Codigo == Convert.ToInt32(txtEmpresa.Text)
                               select new
                               {
                                   Empresa = asd.UnTipoContrato.UnaEmp.Codigo,
                                   Fecha = asd.FechaVto,
                                   Monto = asd.Monto,
                                   Contrato = asd.UnTipoContrato.Nombre
                               }).ToList();

                    gvPagos.DataSource = res;
                    txtEmpresa.Text = "";
                    lblMensaje.Text = "";
                }
                else
                {
                    throw new Exception("El codigo de empresa debe contener entre 1 y 4 cracteres");
                }
            }
            catch (FormatException)
            {
                lblMensaje.Text = "El campo Empresa debe ser numérico";
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }


    }
}
