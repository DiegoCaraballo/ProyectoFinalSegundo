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

            //Creamos el Timer
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

        //Se repite cada 10 Segundos
        private void crono_tick(object sender, ElapsedEventArgs e)
        {

            string destino = Application.StartupPath + "\\horas.xml";

            try
            {   
                //Hora Actual del Sistema
                DateTime horaActual = DateTime.Now;

                //Si existe el archivo
                if (File.Exists(destino))
                {
                    //Cargo el XML
                    XmlDocument documento = new XmlDocument();
                    documento.Load(destino);

                    //Saco los datos necesarios
                    XmlNodeList horas = (documento.GetElementsByTagName("HoraFin"));
                    XmlNodeList cedula = (documento.GetElementsByTagName("Cedula"));

                    DateTime h = DateTime.Parse(horas[0].InnerText.Trim().ToString());

                    //Hora actual
                    var t1 = TimeSpan.Parse(horaActual.ToShortTimeString());
                    //Hora del finalización del Cajero
                    var t2 = TimeSpan.Parse(h.ToShortTimeString());

                    //Si la hora actual es mayor a la de finalización de tareas
                    //Registramos los minutos extras
                    if (t1 > t2)
                    {
                        //Diferencia
                        TimeSpan totalMinutos = (t1 - t2);
                        //Pasado a minutos
                        int minutosExtras = Convert.ToInt32(totalMinutos.TotalMinutes);

                        //LLamada al servicio para registrar los minutos
                        IServicio serv = new ServicioClient();
                        serv.AgregaExtras(Convert.ToInt32(cedula[0].InnerText.Trim().ToString()), horaActual.Date, minutosExtras);

                        //Registro en Log
                        Mensajes.WriteEntry("Se generaron " + minutosExtras + " Minutos Extras");
                    }

                }
                else
                {
                    Mensajes.WriteEntry("No existe el archivo");
                }

            }
            catch (Exception ex)
            {
                Mensajes.WriteEntry("Error: " + ex.Message);
            }
        }


    }
}
