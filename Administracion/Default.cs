using System;
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
        //TODO - falta la parte de inicializar el usuario
        //Ver ejemplo del obligatorio pasado
        Usuario usuLogueado = null;
        public Default(Usuario pUsuLogueado)
        {
            usuLogueado = pUsuLogueado;
            InitializeComponent();
        }

        // ABM Usuario de tipo Gerente
        private void altaDeUsuariosTipoGerenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usuLogueado is Gerente)
            {
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
            CambioPassword _unForm = new CambioPassword(usuLogueado);
            _unForm.ShowDialog();
        }

        //Alta de pagos
        private void altaDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usuLogueado is Cajero)
            {
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
                ListadoDePagos _unForm = new ListadoDePagos(usuLogueado);
                _unForm.ShowDialog();
            }
            else
            {
                lblMensajes.Text = "Usted no cuenta con permisos para acceder a este formulario";

            }
        }

        private void Default_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (File.Exists(@"C:\desarrollo\horas.xml"))
            {
                File.Delete(@"C:\desarrollo\horas.xml");
            }
        }


    }
}
