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
    public partial class ABMCajero : Form
    {
        Usuario usuBuscado = null;
        Usuario usuLogueado = null;
        public ABMCajero(Usuario usu)
        {
            usuLogueado = usu;
            InitializeComponent();
            EstadoInicial();

        }


        //Ingresar un Cajero
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Cajero cajero = new Cajero();
                cajero.Cedula = Convert.ToInt32(txtCedula.Text);
                cajero.NomUsu = txtUsuario.Text;
                cajero.Pass = txtPass.Text;
                cajero.NomCompleto = txtNomApe.Text;
                cajero.HoranIni = Convert.ToDateTime(txtHoraIni.Text);
                cajero.HoranFin = Convert.ToDateTime(txtHoraFin.Text);

                IServicio serv = new ServicioClient();
                serv.AltaUsuario(cajero, usuLogueado);

                EstadoInicial();
                lblMensajes.Text = "El usuario ingresado exitosamente";
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensajes.Text = ex.Message.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Message;
            }
        }

        //Validar el Cajero por la cédula
        private void txtCedula_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Convert.ToInt32(txtCedula.Text);
                epCedula.Clear();
            }
            catch (Exception)
            {
                epCedula.SetError(txtCedula, "La cedula debe ser numerica");
                e.Cancel = true;
                return;
            }

            try
            {
                IServicio serv = new ServicioClient();
                usuBuscado = serv.BuscarUsuario(Convert.ToInt32(txtCedula.Text));

                if (usuBuscado != null)
                {
                    txtCedula.Text = usuBuscado.Cedula.ToString();
                    txtUsuario.Text = usuBuscado.NomUsu;
                    txtNomApe.Text = usuBuscado.NomCompleto;
                    txtHoraFin.Text = ((Cajero)usuBuscado).HoranFin.ToShortTimeString();
                    txtHoraIni.Text = ((Cajero)usuBuscado).HoranIni.ToShortTimeString();

                    btnEliminar.Enabled = true;
                    btnModificar.Enabled = true;
                    txtCedula.Enabled = false;
                    txtCedula.Enabled = false;
                    txtPass.Enabled = false;
                }
                else
                {
                    btnIngresar.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensajes.Text = ex.Message.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Message;
            }
        }

        //Modificar Cajero
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                usuBuscado.NomUsu = txtUsuario.Text;
                usuBuscado.NomCompleto = txtNomApe.Text;
                ((Cajero)usuBuscado).HoranFin = Convert.ToDateTime(txtHoraFin.Text);
                ((Cajero)usuBuscado).HoranIni = Convert.ToDateTime(txtHoraIni.Text);

                IServicio serv = new ServicioClient();
                serv.ModificarUsuario(usuBuscado, usuLogueado);

                EstadoInicial();
                lblMensajes.Text = "El usuario fue modificado exitosamente";

            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensajes.Text = ex.Message.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Message;
            }
        }

        //Eliminar Cajero
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                IServicio serv = new ServicioClient();
                serv.BajaUsuario(usuBuscado, usuLogueado);

                EstadoInicial();
                lblMensajes.Text = "El usuario fue modificado exitosamente";
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensajes.Text = ex.Message.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Message;
            }
        }

        //Los controles quedan en estado inicial
        private void EstadoInicial()
        {
            txtCedula.Text = "";
            txtUsuario.Text = "";
            txtPass.Text = "";
            txtNomApe.Text = "";
            txtHoraIni.Text = "";
            txtHoraFin.Text = "";

            txtCedula.Enabled = true;
            txtUsuario.Enabled = true;
            txtPass.Enabled = true;
            txtNomApe.Enabled = true;
            txtHoraIni.Enabled = true;
            txtHoraFin.Enabled = true;

            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnIngresar.Enabled = false;
        }

        //Limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            EstadoInicial();
        }

    }
}
