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
    <asp:Repeater ID="rpEmpresas" runat="server">
    </asp:Repeater>
    </div>
</asp:Content>

