namespace Administracion
{
    partial class ABMTipoContrato
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ABMTipoContrato));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnIngresar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnModificar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLimpiar = new System.Windows.Forms.ToolStripMenuItem();
            this.txtCodEmpresa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodTipoContrato = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.EPCodEmpresa = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EPCodEmpresa)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnIngresar,
            this.btnModificar,
            this.btnEliminar,
            this.btnLimpiar});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(92, 203);
            this.menuStrip1.TabIndex = 51;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnIngresar
            // 
            this.btnIngresar.Image = ((System.Drawing.Image)(resources.GetObject("btnIngresar.Image")));
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(79, 20);
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(79, 20);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(79, 19);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.Image")));
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(79, 20);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtCodEmpresa
            // 
            this.txtCodEmpresa.Location = new System.Drawing.Point(220, 29);
            this.txtCodEmpresa.Margin = new System.Windows.Forms.Padding(2);
            this.txtCodEmpresa.Name = "txtCodEmpresa";
            this.txtCodEmpresa.Size = new System.Drawing.Size(76, 20);
            this.txtCodEmpresa.TabIndex = 52;
            this.txtCodEmpresa.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodEmpresa_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Cod. Empresa: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Cod.Tip. Contrato: ";
            // 
            // txtCodTipoContrato
            // 
            this.txtCodTipoContrato.Location = new System.Drawing.Point(220, 53);
            this.txtCodTipoContrato.Margin = new System.Windows.Forms.Padding(2);
            this.txtCodTipoContrato.Name = "txtCodTipoContrato";
            this.txtCodTipoContrato.Size = new System.Drawing.Size(76, 20);
            this.txtCodTipoContrato.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Nombre: ";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(220, 76);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(76, 20);
            this.txtNombre.TabIndex = 57;
            // 
            // EPCodEmpresa
            // 
            this.EPCodEmpresa.ContainerControl = this;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensaje});
            this.statusStrip1.Location = new System.Drawing.Point(92, 181);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(286, 22);
            this.statusStrip1.TabIndex = 58;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMensaje
            // 
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(0, 17);
            // 
            // ABMTipoContrato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 203);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCodTipoContrato);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCodEmpresa);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ABMTipoContrato";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABMTipoContrato";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EPCodEmpresa)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnIngresar;
        private System.Windows.Forms.ToolStripMenuItem btnModificar;
        private System.Windows.Forms.ToolStripMenuItem btnEliminar;
        private System.Windows.Forms.ToolStripMenuItem btnLimpiar;
        private System.Windows.Forms.TextBox txtCodEmpresa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodTipoContrato;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.ErrorProvider EPCodEmpresa;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMensaje;
    }
}