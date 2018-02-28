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
    public partial class CambioPassword : Form
    {
        Usuario usuLogueado = null;
        public CambioPassword(Usuario pUsuLogueado)
        {
            usuLogueado = pUsuLogueado;
            InitializeComponent();
        }
    }
}
