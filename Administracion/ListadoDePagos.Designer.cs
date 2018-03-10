namespace Administracion
{
    partial class ListadoDePagos
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
            this.gvPagos = new System.Windows.Forms.DataGridView();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnResumenCajero = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.txtCajero = new System.Windows.Forms.TextBox();
            this.txtEmpresa = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvPagos)).BeginInit();
            this.SuspendLayout();
            // 
            // gvPagos
            // 
            this.gvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPagos.Location = new System.Drawing.Point(-1, 137);
            this.gvPagos.Name = "gvPagos";
            this.gvPagos.RowTemplate.Height = 24;
            this.gvPagos.Size = new System.Drawing.Size(787, 384);
            this.gvPagos.TabIndex = 0;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(547, 29);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(107, 27);
            this.btnLimpiar.TabIndex = 1;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnResumenCajero
            // 
            this.btnResumenCajero.Location = new System.Drawing.Point(416, 29);
            this.btnResumenCajero.Name = "btnResumenCajero";
            this.btnResumenCajero.Size = new System.Drawing.Size(125, 27);
            this.btnResumenCajero.TabIndex = 2;
            this.btnResumenCajero.Text = "Resumen Cajero";
            this.btnResumenCajero.UseVisualStyleBackColor = true;
            this.btnResumenCajero.Click += new System.EventHandler(this.btnResumenCajero_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Cajero";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Empresa";
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(265, 137);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(0, 18);
            this.lblMensaje.TabIndex = 7;
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtCajero
            // 
            this.txtCajero.Location = new System.Drawing.Point(92, 31);
            this.txtCajero.Name = "txtCajero";
            this.txtCajero.Size = new System.Drawing.Size(100, 25);
            this.txtCajero.TabIndex = 8;
            this.txtCajero.Validating += new System.ComponentModel.CancelEventHandler(this.txtCajero_Validating);
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.Location = new System.Drawing.Point(216, 30);
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Size = new System.Drawing.Size(100, 25);
            this.txtEmpresa.TabIndex = 9;
            this.txtEmpresa.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmpresa_Validating);
            // 
            // ListadoDePagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(781, 522);
            this.Controls.Add(this.txtEmpresa);
            this.Controls.Add(this.txtCajero);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnResumenCajero);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.gvPagos);
            this.Font = new System.Drawing.Font("Palatino Linotype", 9.75F);
            this.Name = "ListadoDePagos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ListadoDePagos";
            ((System.ComponentModel.ISupportInitialize)(this.gvPagos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvPagos;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnResumenCajero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.TextBox txtCajero;
        private System.Windows.Forms.TextBox txtEmpresa;
    }
}