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
    public partial class ABMTipoContrato : Form
    {
        public ABMTipoContrato()
        {
            InitializeComponent();
        }

        //Borro los datos del tipo de contrato
        private void LimpiarDatosTipoContrato()
        {
            txtCodEmpresa.Text = "";
            txtCodTipoContrato.Text = "";
            txtNombre.Text = "";
        }
    }
}
