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
            //Timer para el control del tiempo entre llamadas.
            Timer myTimer = new System.Timers.Timer();
            //Intervalo de tiempo entre llamadas.
             myTimer.Interval = 1500;
            //Evento a ejecutar cuando se cumple el tiempo.
             myTimer.Elapsed += OnTimedEvent;
            //Habilitar el Timer.
            myTimer.Enabled = true;
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


        private void cronometro_Tick(object sender, EventArgs e)
        {
        
            RevisarHorasExtras();
        }

        private void RevisarHorasExtras()
        {
            File.Create(@"C:\desarrollo\horaskjsdnfkj.txt");
            
        }


        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            File.Create(@"C:\desarrollo\horaskjsdnfkj"+DateTime.Now.ToString()+".txt"); 
        }

    }
}
