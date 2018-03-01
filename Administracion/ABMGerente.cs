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
        public ABMGerente()
        {
            InitializeComponent();
        }

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

                Servicio serv = new Servicio();
                serv.AltaUsuario(gerente);
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
    }
}
