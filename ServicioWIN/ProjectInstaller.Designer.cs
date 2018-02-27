namespace ServicioWIN
{
    partial class ProjectInstaller
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
            this.InstaladoProcesoServicio = new System.ServiceProcess.ServiceProcessInstaller();
            this.InstaladorServicio = new System.ServiceProcess.ServiceInstaller();
            // 
            // InstaladoProcesoServicio
            // 
            this.InstaladoProcesoServicio.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.InstaladoProcesoServicio.Password = null;
            this.InstaladoProcesoServicio.Username = null;
            // 
            // InstaladorServicio
            // 
            this.InstaladorServicio.Description = "Se instala servicio Horas Extras";
            this.InstaladorServicio.DisplayName = "Servicio Horas Extras";
            this.InstaladorServicio.ServiceName = "ServicioExtras";
            this.InstaladorServicio.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.InstaladoProcesoServicio,
            this.InstaladorServicio});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller InstaladoProcesoServicio;
        private System.ServiceProcess.ServiceInstaller InstaladorServicio;
    }
}