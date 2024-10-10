<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APIVentas.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIVentas" Async="true" MasterPageFile="~/PlantillaNew.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_SimecModificar.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">
                    <div>
                        <asp:Label runat="server" Text="Label"> API VENTAS </asp:Label><br />
                        <br />
                    </div>
                    <!------------------------          API GET VENTAS/{USUARIO}        ------------------------------>
                    <div class="panel-body">
                        <br>
                        <asp:Label runat="server" Text="GET - CONSULTA VENTAS" ID="Label1"></asp:Label><br>
                        <asp:Button ID="btn_getVentaSimple" runat="server" Text="Ver Ventas Simples" OnClick="btn_getVentaSimple_Click"></asp:Button><br /><br />

                        <asp:GridView ID="gv_VentaSimple" runat="server"> </asp:GridView>
                    </div>
                    <br /><br />









































                                        <br>
                    <br>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
