namespace ServicioWIN
{
    partial class ServicioExtras
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cronometro = new System.Windows.Forms.Timer(this.components);
            this.Mensajes = new System.Diagnostics.EventLog();
            this.fswRevision = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.Mensajes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fswRevision)).BeginInit();
            // 
            // cronometro
            // 
            this.cronometro.Interval = 5000;
            // 
            // fswRevision
            // 
            this.fswRevision.EnableRaisingEvents = true;
            this.fswRevision.Created += new System.IO.FileSystemEventHandler(this.fswRevision_Created);
            // 
            // ServicioExtras
            // 
            this.ServiceName = "ServicioExtras";
            ((System.ComponentModel.ISupportInitialize)(this.Mensajes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fswRevision)).EndInit();

        }

        #endregion

        private System.Windows.Forms.Timer cronometro;
        private System.Diagnostics.EventLog Mensajes;
        private System.IO.FileSystemWatcher fswRevision;
    }
}
