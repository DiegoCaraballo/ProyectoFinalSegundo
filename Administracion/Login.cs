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
              IServicio serv= new ServicioClient();
                
                Usuario usu = null;
                
                
        //ServicioClient  serv = new ServicioClient();

        
                usu = serv.Logueo(txtUsuario.Text);

                if (usu == null)
                {
                    throw new Exception("No se encontro el usuario");
                }

                if (usu.Pass == txtPass.Text)
                {
                    if (usu is Cajero)
                    {
                        string destino = @"C:\desarrollo\horas.xml";
                        XmlDocument horas = new XmlDocument();

                        XmlNode usuCajero = horas.CreateNode(XmlNodeType.Element, "usuCajero", "");

                        XmlNode nodoCedula = horas.CreateNode(XmlNodeType.Element, "Cedula", "");
                        nodoCedula.InnerText = usu.Cedula.ToString();
                        usuCajero.AppendChild(nodoCedula);

                        XmlNode nodoHoraIni = horas.CreateNode(XmlNodeType.Element, "HoraIni", "");
                        nodoHoraIni.InnerText = ((Cajero)usu).HoranIni.ToString();
                        usuCajero.AppendChild(nodoHoraIni);

                        XmlNode nodoHoraFin = horas.CreateNode(XmlNodeType.Element, "HoraFin", "");
                        nodoHoraFin.InnerText = ((Cajero)usu).HoranIni.ToString();
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
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText.Length > 40)
                    lblMensajes.Text = ex.Detail.InnerText.Substring(0, 40);
                else
                    lblMensajes.Text = ex.Detail.InnerText;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 40)
                    lblMensajes.Text = ex.Message.Substring(0, 40);
                else
                    lblMensajes.Text = ex.Message;
            }

        }
    }
}
