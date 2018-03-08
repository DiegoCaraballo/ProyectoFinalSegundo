using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;


using System.IO;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;

using System.Timers;
using System.Windows.Forms;
using System.Globalization;

using ServicioWIN.ServicioWCF;
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
            DateTime horaActual = DateTime.Now;


            if (File.Exists(@"C:\desarrollo\horas.xml"))
            {
                try
                {
                    XmlDocument documento = new XmlDocument();
                    documento.Load(@"C:\desarrollo\horas.xml");

                    XmlNodeList horas = (documento.GetElementsByTagName("HoraFin"));
                    XmlNodeList cedula = (documento.GetElementsByTagName("Cedula"));

                    DateTime h = DateTime.Parse(horas[0].InnerText.Trim().ToString());

                    var t1 = TimeSpan.Parse(horaActual.ToShortTimeString());

                    var t2 = TimeSpan.Parse(h.ToShortTimeString());

                    if (t1 > t2)
                    {

                        TimeSpan totalMinutos = (t1 - t2);
                        int minutosExtras = Convert.ToInt32(totalMinutos.TotalMinutes);
                        //TODO
                        IServicio serv = new ServicioClient();
                        serv.AgregaExtras(Convert.ToInt32(cedula[0].InnerText.Trim().ToString()), horaActual.Date, minutosExtras);


                        Mensajes.WriteEntry("Se generaron " + minutosExtras + " Minutos Extras");
                    }

                }
                catch (Exception ex)
                {
                    Mensajes.WriteEntry("Error: " + ex.Message);
                }
            }
        }


    }
}
