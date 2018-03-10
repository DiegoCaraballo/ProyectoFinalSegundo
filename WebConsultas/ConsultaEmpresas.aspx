<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ConsultaEmpresas.aspx.cs" Inherits="ConsultaEmpresas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container text-center">
        <div class="row">
            <div class="col-lg-12">
                <br>
                <br>
                <br>
                <br>
                <h1>
                    Consultar Empresas</h1>
                <p class="lead">
                    Consulta empresas adheridas a la red de cobranzas.</p>
                <p class="lead">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </p>
            </div>
        </div>
        <br>
        <div id="wrap">
            <table>
                <tr bgcolor="#8C8D88">
                    <td>
                        Codigo
                    </td>
                    <td>
                        Rut
                    </td>
                    <td>
                        Dirección Fiscal
                    </td>
                    <td>
                        Telefono
                    </td>
                    <td>
                        Tipo Contrato
                    </td>
                </tr>
                <tr bgcolor="##2d81ad">
                    <asp:Repeater ID="rpEmpresas" runat="server" OnItemCommand="rpEmpresas_ItemCommand">
                        <ItemTemplate>
                            <td>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("Codigo") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRut" runat="server" Text='<%# Bind("Rut") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDirFiscal" runat="server" Text='<%# Bind("DirFiscal") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTelefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnMostrar" runat="server" CommandName="Mostrar" Style="text-algin: center"
                                    Text="Mostrar" />
                            </td>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr bgcolor="#2F64A0">
                                <td>
                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("Codigo") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRut" runat="server" Text='<%# Bind("Rut") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDirFiscal" runat="server" Text='<%# Bind("DirFiscal") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTelefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnMostrar" runat="server" CommandName="Mostrar" Style="text-algin: center"
                                        Text="Mostrar" />
                                </td>
                                <tr />
                        </AlternatingItemTemplate>
                    </asp:Repeater>
            </table>
            <br>
            <asp:Label ID="lblTipoContrato" runat="server" Font-Bold="True" 
                Font-Size="X-Large" Text="Tipos de Contratos   " Visible="False"></asp:Label>
            <br>
            <tr>
                <td>
                    <asp:Xml ID="ctrlXML" runat="server" TransformSource="~/XSLT/XSLTContratos.xslt"></asp:Xml>
                </td>
            </tr>
        </div>
    </div>
</asp:Content>
