﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Administracion.ServicioWCF;
using System.IO;

namespace Administracion
{
    public partial class Default : Form
    {
        Usuario usuLogueado = null;
        public Default(Usuario pUsuLogueado)
        {
            usuLogueado = pUsuLogueado;
            InitializeComponent();
            if (usuLogueado is Cajero)
            {
                altaDeUsuariosTipoGerenteToolStripMenuItem.Visible = false;
                aBMDeUsuarioTipoCajeroToolStripMenuItem.Visible = false;
                aBMDeEmpresasToolStripMenuItem.Visible = false;
                aBMTipoDeContratoToolStripMenuItem.Visible = false;
                listadoDePagosToolStripMenuItem.Visible = false;
            }
            else
            {
                altaDePagosToolStripMenuItem.Visible = false;

            }
        }

        // ABM Usuario de tipo Gerente
        private void altaDeUsuariosTipoGerenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usuLogueado is Gerente)
            {
                lblMensajes.Text = "";
                ABMGerente _unForm = new ABMGerente(usuLogueado);
                _unForm.ShowDialog();
            }
            else
            {
                lblMensajes.Text = "Usted no cuenta con permisos para acceder a este formulario";
            }

        }

        // ABM Usuario de tipo Cajero
        private void aBMDeUsuarioTipoCajeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usuLogueado is Gerente)
            {
                lblMensajes.Text = "";
                ABMCajero _unForm = new ABMCajero(usuLogueado);
                _unForm.ShowDialog();
            }
            else
            {
                lblMensajes.Text = "Usted no cuenta con permisos para acceder a este formulario";
            }
        }

        //ABM de Empresa
        private void aBMDeEmpresasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usuLogueado is Gerente)
            {
                lblMensajes.Text = "";
                ABMEmpresa _unForm = new ABMEmpresa(usuLogueado);
                _unForm.ShowDialog();
            }
            else
            {
                lblMensajes.Text = "Usted no cuenta con permisos para acceder a este formulario";
            }
        }

        //ABM tipo de contrato
        private void aBMTipoDeContratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usuLogueado is Gerente)
            {
                lblMensajes.Text = "";
                ABMTipoContrato _unForm = new ABMTipoContrato(usuLogueado);
                _unForm.ShowDialog();
            }
            else
            {
                lblMensajes.Text = "Usted no cuenta con permisos para acceder a este formulario";
            }
        }

        //Cambio de Contraseña
        private void cambioDeContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblMensajes.Text = "";
            CambioPassword _unForm = new CambioPassword(usuLogueado);
            _unForm.ShowDialog();
        }

        //Alta de pagos
        private void altaDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usuLogueado is Cajero)
            {
                lblMensajes.Text = "";
                AltaPago _unForm = new AltaPago((Cajero)usuLogueado);
                _unForm.ShowDialog();
            }
            else
            {
                lblMensajes.Text = "Usted no cuenta con permisos para acceder a este formulario";
            }
        }

        //Listado de Pagos
        private void listadoDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usuLogueado is Gerente)
            {
                lblMensajes.Text = "";
                ListadoDePagos _unForm = new ListadoDePagos(usuLogueado);
                _unForm.ShowDialog();
            }
            else
            {
                lblMensajes.Text = "Usted no cuenta con permisos para acceder a este formulario";

            }
        }

        //Cuando se cierra el formulario se elimina el XML para las Horas Extras
        private void Default_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string destino = Application.StartupPath + "\\horas.xml";
                if (File.Exists(destino))
                {
                    File.Delete(destino);
                }
            }
            catch (Exception ex)
            { lblMensajes.Text = ex.Message; }
        }


    }
}
