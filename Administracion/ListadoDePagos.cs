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
                               _Cajero = lista.UsuCajero.NomUsu
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
            //cboEmpresa.SelectedIndex = 0;
            //cboCajero.SelectedIndex = 0;
            CargoListaPagos();
        }



        //Resumen por Cajero
        private void btnResumenCajero_Click(object sender, EventArgs e)
        {
            gvPagos.DataSource = null;
            try
            {//TODO - Falta Suma de montos
                gvPagos.DataSource = null;
                var res = (from lista in listadoPagos
                           group lista by lista.UsuCajero.Cedula  into listaCajeros
                           select new
                           {
                               _Cajero = listaCajeros.First().UsuCajero.NomUsu,
                               Cantidad = listaCajeros.Count()
                               
                               

                           }).ToList();

                gvPagos.DataSource = res;


            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void txtCajero_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtCajero.Text != "")
                {
                    gvPagos.DataSource = null;
                    var res = (from lista in listadoPagos
                               where lista.UsuCajero.Cedula == Convert.ToInt32(txtCajero.Text)
                               orderby lista.Fecha descending
                               select new
                               {
                                   _Cajero = lista.UsuCajero.Cedula,
                                   Fecha = lista.Fecha,
                                   Monto = lista.MontoTotal
                               }).ToList();

                    gvPagos.DataSource = res;
                }

            }
            catch (FormatException)
            {
                lblMensaje.Text = "El campo Cajero debe ser numerico";
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void txtEmpresa_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtEmpresa.Text != "")
                {
                    gvPagos.DataSource = null;
                   
                    var res
                        = (from l in listadoPagos
                           from asd in l.LasFacturas
                              where asd.UnTipoContrato.UnaEmp.Codigo == Convert.ToInt32(txtEmpresa.Text)
                           select new {
                               _Empresa = asd.UnTipoContrato.UnaEmp.Codigo,
                               Fecha= asd.FechaVto,
                               Monto = asd.Monto,
                               Contrato = asd.UnTipoContrato.Nombre
                           }).ToList();

                    gvPagos.DataSource = res;
                }


            }
            catch (FormatException)
            {
                lblMensaje.Text = "El campo Cajero debe ser numerico";
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

      
    }
}
