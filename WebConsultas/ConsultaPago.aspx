﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultaPago.aspx.cs" Inherits="ConsultaPago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
        .style1
        {
        }
        .style2
        {
            text-align: right;
            width: 527px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container text-center">
        <div class="row">
            <div class="col-lg-12">
                <br><br><br><br>
                <h1>
                    Consultar Pagos</h1>
                <p>
                    &nbsp;</p>
                <table class="table" style="width: 100%;">
                    <tr>
                        <td class="style2">
                            
                            Código de barras:&nbsp;
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCodBarra" runat="server"></asp:TextBox>
                        &nbsp;<asp:Button ID="btnBuscar" runat="server" onclick="btnBuscar_Click" 
                                Text="Buscar" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblIdPago" runat="server" Text="Id Pago: "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtIdPago" runat="server" Enabled="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblFecha" runat="server" Text="Fecha: "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFecha" runat="server" Enabled="False"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

