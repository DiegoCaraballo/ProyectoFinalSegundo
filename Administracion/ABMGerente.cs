﻿using System;
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
    public partial class ABMGerente : Form
    {
        Usuario usuLogueado = null;
        public ABMGerente(Usuario usu)
        {
            usuLogueado = usu;
            InitializeComponent();
        }

        //Ingresar Gerente
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Gerente gerente = new Gerente();
                gerente.Cedula = Convert.ToInt32(txtCedula.Text);
                gerente.NomUsu = txtUsuario.Text;
                gerente.Pass = txtPass.Text;
                gerente.NomCompleto = txtNomApe.Text;
                gerente.Correo = txtCorreo.Text;

                IServicio serv = new ServicioClient();
                serv.AltaUsuario(gerente, usuLogueado);

                lblMensajes.Text = "Usuario ingresado exitosamente";
                EstadoInicial();
            }
            catch (FormatException)
            {
                lblMensajes.Text = "La cedula debe tener formato numerico";
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensajes.Text = ex.Message.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Message;
            }

        }

        //Limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            EstadoInicial();
            lblMensajes.Text = "";
        }

        //Controles en estado inicial
        private void EstadoInicial()
        {
            txtCedula.Text = "";
            txtUsuario.Text = "";
            txtPass.Text = "";
            txtNomApe.Text = "";
            txtCorreo.Text = "";

            txtCedula.Enabled = true;
            txtUsuario.Enabled = true;
            txtPass.Enabled = true;
            txtNomApe.Enabled = true;
            txtCorreo.Enabled = true;
        }

    }
}
