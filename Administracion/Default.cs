using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Administracion
{
    public partial class Default : Form
    {
        //TODO - falta la parte de inicializar el usuario
        //Ver ejemplo del obligatorio pasado

        public Default()
        {
            InitializeComponent();
        }

        // ABM Usuario de tipo Gerente
        private void altaDeUsuariosTipoGerenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMGerente _unForm = new ABMGerente();
            _unForm.ShowDialog();
        }

        // ABM Usuario de tipo Cajero
        private void aBMDeUsuarioTipoCajeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMCajero _unForm = new ABMCajero();
            _unForm.ShowDialog();
        }

        //ABM de Empresa
        private void aBMDeEmpresasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMEmpresa _unForm = new ABMEmpresa();
            _unForm.ShowDialog();
        }

        //ABM tipo de contrato
        private void aBMTipoDeContratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMTipoContrato _unForm = new ABMTipoContrato();
            _unForm.ShowDialog();
        }

        //Cambio de Contraseña
        private void cambioDeContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambioPassword _unForm = new CambioPassword();
            _unForm.ShowDialog();
        }

        //Alta de pagos
        private void altaDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaPago _unForm = new AltaPago();
            _unForm.ShowDialog();
        }

        //Listado de Pagos
        private void listadoDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListadoDePagos _unForm = new ListadoDePagos();
            _unForm.ShowDialog();
        }

    }
}
