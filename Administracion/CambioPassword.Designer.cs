namespace Administracion
{
    partial class CambioPassword
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnLimpiar = new System.Windows.Forms.ToolStripLabel();
            this.txtRepitePass = new System.Windows.Forms.TextBox();
            this.txtNuevaPass = new System.Windows.Forms.TextBox();
            this.txtActualPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensajes = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLimpiar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(64, 287);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::Administracion.Properties.Resources.Cancelar;
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(61, 16);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtRepitePass
            // 
            this.txtRepitePass.Location = new System.Drawing.Point(191, 61);
            this.txtRepitePass.MaxLength = 7;
            this.txtRepitePass.Name = "txtRepitePass";
            this.txtRepitePass.PasswordChar = '*';
            this.txtRepitePass.Size = new System.Drawing.Size(100, 20);
            this.txtRepitePass.TabIndex = 1;
            this.txtRepitePass.Validating += new System.ComponentModel.CancelEventHandler(this.txtRepitePass_Validating);
            // 
            // txtNuevaPass
            // 
            this.txtNuevaPass.Location = new System.Drawing.Point(191, 35);
            this.txtNuevaPass.MaxLength = 7;
            this.txtNuevaPass.Name = "txtNuevaPass";
            this.txtNuevaPass.PasswordChar = '*';
            this.txtNuevaPass.Size = new System.Drawing.Size(100, 20);
            this.txtNuevaPass.TabIndex = 2;
            // 
            // txtActualPass
            // 
            this.txtActualPass.Location = new System.Drawing.Point(191, 9);
            this.txtActualPass.MaxLength = 7;
            this.txtActualPass.Name = "txtActualPass";
            this.txtActualPass.PasswordChar = '*';
            this.txtActualPass.Size = new System.Drawing.Size(100, 20);
            this.txtActualPass.TabIndex = 3;
            this.txtActualPass.Validating += new System.ComponentModel.CancelEventHandler(this.txtActualPass_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Contraseña Actual";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nueva Contraseña";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Repita Contraseña";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensajes});
            this.statusStrip1.Location = new System.Drawing.Point(64, 265);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(372, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMensajes
            // 
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(0, 17);
            // 
            // CambioPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 287);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtActualPass);
            this.Controls.Add(this.txtNuevaPass);
            this.Controls.Add(this.txtRepitePass);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CambioPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CambioPassword";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel btnLimpiar;
        private System.Windows.Forms.TextBox txtRepitePass;
        private System.Windows.Forms.TextBox txtNuevaPass;
        private System.Windows.Forms.TextBox txtActualPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMensajes;
    }
}