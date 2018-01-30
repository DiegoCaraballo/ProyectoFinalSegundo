<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultaPago.aspx.cs" Inherits="Consulta_Pago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 527px;
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
                <table style="width: 100%;">
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
                        <td class="style2">
                            &nbsp;
                            Código de barras:
                        </td>
                        <td style="text-align: left">
                            &nbsp;
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            &nbsp;
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
                </table>
            </div>
        </div>
    </div>
</asp:Content>

