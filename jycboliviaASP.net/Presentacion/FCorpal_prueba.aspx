﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_prueba.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_prueba" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_SimecModificar.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="padding-top: 1em;">
   


<div class="row">
    <div class="col-md-10 col-md-offset-1">
        <div class="panel panel-success class">
            <div class="panel-heading">
            <h3 class="panel-title">Orden Produccion:</h3>
            </div>
            <div class="panel-body">
            
       <table>             
       <tr>        
        <td>
            <asp:Label ID="Label46" runat="server" Text="Producto:"></asp:Label>
           </td>                    
        <td>
            <asp:TextBox ID="tx_producto" class="form-control" runat="server"
                Width="300px"></asp:TextBox>
           </td>   
        
       </tr>       
        <tr>
            <td>
                
            </td>
            <td>
                
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
        <td>
            
            </td>
        <td>
            
            
            </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>        
        </tr>        
             
   
    </table>
   
    
    <table>
        <tr>
            <td>
                <asp:Label ID="Label35" runat="server" Text="Resultado :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_resultado" class="form-control" runat="server" Height="100px" TextMode="MultiLine"
                    Width="500px"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            
            <td>
                <asp:Button ID="bt_insertar" class="btn btn-success" runat="server"
                    Text="Prueba" OnClick="bt_insertar_Click" />
            </td>
            
            
        </tr>
    </table>

            </div>
            <div class="panel-body">
                <br>
                <asp:Label runat="server" Text="Consulta Inventario" ID="ctl10"></asp:Label><br>
                <asp:TextBox ID="tx_Ntransaccion1" runat="server"></asp:TextBox><asp:Button runat="server" Text="Buscar" OnClick="Unnamed2_Click"></asp:Button>&nbsp;
                <asp:GridView ID="gv_Inventario" runat="server">
                  
                </asp:GridView>
            </div>
            <br>
            <div class="panel-body">&nbsp;
            POST - INVENTARIO INGRESOS<br><br><asp:TextBox ID="TextBox1" runat="server" placeholder="NumeroIngreso"></asp:TextBox>
                <asp:TextBox ID="TextBox2" runat="server" placeholder="Fecha"></asp:TextBox>
                <asp:TextBox ID="TextBox3" runat="server" placeholder="Referencia"></asp:TextBox>
                <asp:TextBox ID="TextBox4" runat="server" placeholder="Codigo Moneda"></asp:TextBox>
                <asp:TextBox ID="TextBox5" runat="server" placeholder="Codigo Almacen"></asp:TextBox>
                <asp:TextBox ID="TextBox7" runat="server" placeholder="Motivo Movimiento"></asp:TextBox>
                <asp:TextBox ID="TextBox8" runat="server" placeholder="Item analisis"></asp:TextBox>
                <asp:TextBox ID="TextBox6" runat="server" placeholder="Glosa"></asp:TextBox></div>
        </div>
     </div>
</div>
        <div class="row">&nbsp;</div>


</div>


</asp:Content>