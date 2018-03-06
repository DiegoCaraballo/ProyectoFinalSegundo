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

using System.Timers;
using System.Windows.Forms;
using System.Globalization;
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
            
        }

        protected override void OnStart(string[] args)
        {          
            Mensajes.WriteEntry("Se inicia el servicio - ServicioExtras");

            //Creamos le Timer aquí
            System.Timers.Timer crono = new System.Timers.Timer();
            crono.Interval = 10000;
            crono.Elapsed += new ElapsedEventHandler(crono_tick);
            crono.Enabled = true;     
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

        private void crono_tick(object sender, ElapsedEventArgs e)
        {
            Mensajes.WriteEntry("Esto se debería ejecutar cada 10 seg..");

            int horaActual = DateTime.Now.Hour;

            if(File.Exists(@"C:\desarrollo\horas.xml"))
            {
                try
                {
                    XmlDocument documento = new XmlDocument();
                    documento.Load(@"C:\desarrollo\horas.xml");

                    XmlNodeList horas = documento.GetElementsByTagName("HoraFin");

                    //DateTime horaFin = DateTime.ParseExact(horas[0].InnerText.Trim().ToString(),
                    //  "dd/MM/yyyy HH:mm:ss tt",
                    //   CultureInfo.InvariantCulture);

                    Mensajes.WriteEntry("Hora Fin - " + horas[0].InnerText);
                    Mensajes.WriteEntry("Hora Actual - " + horaActual.ToString());
                }
                catch (Exception ex)
                {
                    Mensajes.WriteEntry("Error: " + ex.Message);
                }
            }
        }


    }
}
