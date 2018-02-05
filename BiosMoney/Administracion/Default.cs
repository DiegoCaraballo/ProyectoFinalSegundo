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
        public Default()
        {
            InitializeComponent();
        }

        // TODO - Pasar empleado logueado al formulario

        // ABM Cajero
        private void aBMCajeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMCajero _unForm = new ABMCajero();
            _unForm.ShowDialog();
        }

        // ABM Gerente
        private void aBMGerenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMGerente _unForm = new ABMGerente();
            _unForm.ShowDialog();
        }

        // ABM Empresa
        private void aBMEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMEmpresa _unForm = new ABMEmpresa();
            _unForm.ShowDialog();
        }
        
        // ABM Tipo de Contrato
        private void aBMTipoContratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMTipoContrato _unForm = new ABMTipoContrato();
            _unForm.ShowDialog();
        }

        // Alta de Pagos
        private void altaDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaDePago _unForm = new AltaDePago();
            _unForm.ShowDialog();
        }

        // Listado de Pagos
        private void listadoDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListadoDePagos _unForm = new ListadoDePagos();
            _unForm.ShowDialog();
        }

        // Cambiar contraseña
        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambioPassword _unForm = new CambioPassword();
            _unForm.ShowDialog();
        }


    }
}
