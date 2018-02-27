using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using EntidadesCompartidas;
using Logica;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;

namespace ServicioWIN
{
    partial class ServicioExtras : ServiceBase
    {
        public ServicioExtras()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("MiServicioExtras"))
                System.Diagnostics.EventLog.CreateEventSource("MiServicioExtras", "ServicioExtrasLog");

            Mensajes.Source = "MiServicioExtras";
            Mensajes.Log = "ServicioExtrasLog";
            fswRevision.Path = ConfigurationManager.AppSettings["fswPath"];
        }

        protected override void OnStart(string[] args)
        {
            Mensajes.WriteEntry("Inicia el servicio - ServicioExtras");
            cronometro.Enabled = true;
            cronometro.Start();
        }

        protected override void OnStop()
        {
            Mensajes.WriteEntry("Se detiene el servicio - ServicioExtras");

            cronometro.Enabled = false;
            cronometro.Stop();
        }

        protected override void OnPause()
        {

            Mensajes.WriteEntry("Se pausa el servicio - ServicioExtras");

            cronometro.Enabled = false;
            cronometro.Stop();
        }

        protected override void OnContinue()
        {
            Mensajes.WriteEntry("Se continua ejecutando el servicio - ServicioExtras");
            cronometro.Enabled = true;
            cronometro.Start();

        }

        private void fswRevision_Created(object sender, FileSystemEventArgs e)
        {
            try 
            {
                if (e.Name.ToLowerInvariant().Contains("xmlExtras"))
                {
                    FileInfo info = new FileInfo(e.FullPath);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(info.ToString());
                    XElement docu = new XElement(doc.ToString());
                  int horafin= Convert.ToInt32( docu.Element("HoraFin"));

                    int horactual = DateTime.Now.Hour;

                    if (horactual > horafin)
                    {
                        Mensajes.WriteEntry("Suma minutos " + horactual);
                    }
                }
                else
                    Mensajes.WriteEntry("El archivo " + e.Name + " no existe");
            }
            catch (Exception ex)
            {
                Mensajes.WriteEntry(ex.Message + " - arcvhivo " + e.Name);
            }

        }




    }
}
