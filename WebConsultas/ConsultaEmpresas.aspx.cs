using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

using ServicioWCF;
using System.Xml;

public partial class ConsultaEmpresas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarLista();
            }
        }
        catch (Exception)
        {
            throw new Exception();
        }

    }

    //Cargar lista de empresas al repeater
    protected void CargarLista()
    {
        try
        {
            IServicio serv = new ServicioClient();

            //obtengo el xml desde el WCF
            string st = serv.ListarContratos().ToString();

            //creo y cargo con los datos el documento q me devolvio el WS- formato para Linq
            XElement _documento = XElement.Parse(st);
            Session["Documento"] = _documento;

            var resultado = (from res in _documento.Elements("TipoContrato")
                             group res by res.Element("EmpCod").Value into codigos
                             select new
                             {
                                 Rut = codigos.First().Element("EmpRut").Value,
                                 Codigo = codigos.First().Element("EmpCod").Value,
                                 DirFiscal = codigos.First().Element("EmpDir").Value,
                                 Telefono = codigos.First().Element("EmpTel").Value
                             }).ToList();

            //Cargo repeater
            rpEmpresas.DataSource = resultado;
            rpEmpresas.DataBind();

            //Si no hay empresas
            if (rpEmpresas.Items.Count == 0)
                throw new Exception("No hay empresas para listar");

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    //Funcionalidad Mostrar dentro del Repeater
    protected void rpEmpresas_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Mostrar")
            {
                try
                {
                    //Cargo el documento de la session
                    XElement _documento = (XElement)Session["Documento"];

                    int CodEmp = Convert.ToInt32(((Label)e.Item.FindControl("lblCodigo")).Text);
                    //LINQ
                    var resultado = (from res in _documento.Elements("TipoContrato")
                                     where Convert.ToInt32(res.Element("EmpCod").Value) == CodEmp
                                     select new
                                     {
                                         Codigo = res.Element("CodContrato").Value,
                                         Nombre = res.Element("NomContrato").Value
                                     }).ToList();


                    //Armamos xml del resultado
                    XmlDocument exportar = new XmlDocument();
                    exportar.LoadXml("<?xml version='1.0' encoding='utf-8' ?> <TiposDeContratos> </TiposDeContratos>");

                    XmlNode _raiz = exportar.DocumentElement;

                    foreach (var tc in resultado)
                    {
                        XmlElement _Codigo = exportar.CreateElement("CodContrato");
                        _Codigo.InnerText = tc.Codigo.ToString();

                        XmlElement _Nombre = exportar.CreateElement("NomContrato");
                        _Nombre.InnerText = tc.Nombre.ToString();

                        XmlNode nodoContrato = exportar.CreateElement("TipoContrato");
                        nodoContrato.AppendChild(_Codigo);
                        nodoContrato.AppendChild(_Nombre);

                        _raiz.AppendChild(nodoContrato);

                    }

                    //Cargamos el control XML
                    //string st = exportar.OuterXml;
                    ctrlXML.DocumentContent = exportar.OuterXml;
                    //ctrlXML.DocumentSource = exportar.OuterXml;


                    int i = 0;
                }

                catch (Exception ex)
                {
                    lblMensaje.Text = ex.Message;
                }
            }
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}