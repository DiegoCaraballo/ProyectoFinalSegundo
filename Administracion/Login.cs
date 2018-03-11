using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Administracion.ServicioWCF;
using System.Xml;
using System.IO;

namespace Administracion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                IServicio serv = new ServicioClient();

                Usuario usu = null;

                usu = serv.Logueo(txtUsuario.Text);

                //Si el usuario no existe
                if (usu == null)
                {
                    throw new Exception("No se encontro el usuario");
                }

                //Si el usuario Existe reviso que tipo de usuario es
                if (usu.Pass == txtPass.Text)
                {
                    //Si es Cajero
                    if (usu is Cajero)
                    {
                        //Creo el XML para las horas extras
                        string destino = @"C:\Program Files\BiosMoney\horas.xml";
                        if (File.Exists(destino))
                        {
                            File.Delete(destino);
                        }

                        XmlDocument horas = new XmlDocument();

                        XmlNode usuCajero = horas.CreateNode(XmlNodeType.Element, "usuCajero", "");

                        XmlNode nodoCedula = horas.CreateNode(XmlNodeType.Element, "Cedula", "");
                        nodoCedula.InnerText = usu.Cedula.ToString();
                        usuCajero.AppendChild(nodoCedula);

                        XmlNode nodoHoraIni = horas.CreateNode(XmlNodeType.Element, "HoraIni", "");
                        nodoHoraIni.InnerText = ((Cajero)usu).HoranIni.ToShortTimeString();
                        usuCajero.AppendChild(nodoHoraIni);

                        XmlNode nodoHoraFin = horas.CreateNode(XmlNodeType.Element, "HoraFin", "");
                        nodoHoraFin.InnerText = ((Cajero)usu).HoranFin.ToShortTimeString();
                        usuCajero.AppendChild(nodoHoraFin);
                        horas.AppendChild(usuCajero);

                        horas.Save(destino);

                        this.Hide();
                        Form unForm = new Default(usu);
                        unForm.ShowDialog();
                        this.Close();

                    }
                    else
                    {
                        this.Hide();
                        Form unForm = new Default(usu);
                        unForm.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    throw new Exception("Contraseña Incorrecta");
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensajes.Text = ex.Message.Substring(0, 80);
                else
                    lblMensajes.Text = ex.Message;
            }

        }
    }
}
