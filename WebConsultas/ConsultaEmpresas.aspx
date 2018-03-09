<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultaEmpresas.aspx.cs" Inherits="ConsultaEmpresas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container text-center">
        <div class="row">
            <div class="col-lg-12">
                <br><br><br><br>
                <h1>
                    Consultar Empresas</h1>
                <p class="lead">
                    Consulta empresas adheridas a la red de cobranzas.</p>
                <p class="lead">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </p>
            </div>
        </div>

<div class="container">
<article>
<table class="empresas">
                    <tr bgcolor ="#8C8D88">
                <td  class="style1">
                    Codigo                                                                   
                </td>
                <td class="style1">
                    Rut
                </td>
                <td class="style1">
                    Dirección Fiscal
                </td>
                <td class="style1">
                    Telefono
                </td>
                <td class="style1">                        
                    Tipo Contrato
                </td>
            </tr>
    <asp:Repeater ID="rpEmpresas" runat="server" onitemcommand="rpEmpresas_ItemCommand">
    <ItemTEmplate>                                                               
                </table>
                <table class="empresas">
                <tr bgcolor ="##2d81ad">
                   
                        <td>
                               <asp:Label ID="lblCodigo" runat="server" Text= '<%# Bind("Codigo") %>' ></asp:Label>                                                                       
                        </td>
                        <td>
                               <asp:Label ID="lblRut" runat="server" Text='<%# Bind("Rut") %>' ></asp:Label>
                        </td>
                        <td>
                               <asp:Label ID="lblDirFiscal" runat="server" Text='<%# Bind("DirFiscal") %>' ></asp:Label>
                        </td>
                        <td>
                               <asp:Label ID="lblTelefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
                        </td>
                        <td>                        
                           <asp:Button ID="btnMostrar"  runat="server" CommandName="Mostrar" style="text-algin: center" Text="Mostrar" />
                        </td>
                    </tr>                
   
     </table>
            </ItemTEmplate>
              <AlternatingItemTEmplate>
                     <table class="empresas">
                         <tr bgcolor ="#2F64A0">
                        <td>
                            <asp:Label ID="lblCodigo" runat="server" Text= '<%# Bind("Codigo") %>' ></asp:Label>                                                                     
                        </td>
                        <td>
                            <asp:Label ID="lblRut" runat="server" Text='<%# Bind("Rut") %>' ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDirFiscal" runat="server"  Text='<%# Bind("DirFiscal") %>' ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTelefono" runat="server" Text='<%# Bind("Telefono") %>' ></asp:Label>
                        </td>
                        <td>                        
                            <asp:Button ID="btnMostrar"  runat="server" CommandName="Mostrar" style=" text-algin: center" Text="Mostrar" />

                        </td>
                    </tr>
                                                    
                </table>
                    </AlternatingItemTEmplate>
                    
     
    </asp:Repeater>
                    <tr>
                        <td>
                            <asp:Xml ID="ctrlXML" runat="server" 
                                TransformSource="~/XSLT/XSLTContratos.xslt"></asp:Xml>
                            <br />
                        </td>
                    </tr>
    <article>
    </div>
    </div>
</asp:Content>

