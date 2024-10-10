<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_ReciboEgreso.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ReciboEgreso" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<<<<<<< HEAD

=======
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
>>>>>>> origin/modulo3
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/Style_SimecModificar.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
          .CompletionList
        {
            padding: 5px 0 ;
            margin: 2px 0 0;            
          /*  position:absolute;  */
            height:150px;
            width:200px;
            background-color: White;
            cursor: pointer;
            border: solid ;  
            border-width: 1px;    
            font-size:x-small;
            overflow: auto;
                        }
                        
           .CompletionlistItem
           {
               font-size:x-small;           
            }             
                        
        .CompletionListMighlightedItem
        {
             background-color: Green;
             color: White;
            /* color: Lime;
           padding: 3px 20px;
            text-decoration: none;           
            background-repeat: repeat-x;
            outline: 0;*/            
            }     
        
        
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: auto;
    }
    
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="container" style="padding-top: 1em;">
   


<div class="row">
    <div class="col-md-10 col-md-offset-1">
        <div class="panel panel-success class">
            <div class="panel-heading">
            <h3 class="panel-title">COMPROBANTE DE EGRESO:</h3>
            </div>
            <div class="panel-body">
            
       <table>   
       <tr>
        <td>
            <asp:Label ID="Label40" runat="server" Text="NroRecibo"></asp:Label>
           </td>
        <td>
            <asp:TextBox ID="tx_nrorecibo" runat="server" Enabled="False"></asp:TextBox>
           </td>
        <td></td>
       </tr>   

        <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Text="Fecha:"></asp:Label>
           </td>
        <td>
            <asp:TextBox ID="tx_fechaegreso" class="form-control" runat="server" 
                Width="150px"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaegreso_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaegreso">
            </asp:CalendarExtender>
           </td>
        <td></td>
       </tr>  

        <tr>
        <td>
            <asp:Label ID="Label30" runat="server" Font-Size="Small" Text="Pagado ha :"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_pagadoha" class="form-control" runat="server" 
                Font-Size="Small" Width="400px"></asp:TextBox>
            </td>                    
            <td><asp:Button ID="bt_buscar" class="btn btn-success" runat="server"   
                    onclick="bt_buscar_Click" Text="Buscar" /></td>   
            <td>&nbsp;</td>
        </tr>       
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label31" runat="server" Text="Monto :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_montototal" class="form-control" runat="server" 
                    Width="150px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label32" runat="server" Text="Moneda :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="dd_moneda" class="form-control" runat="server" 
                    Width="150px">
                    <asp:ListItem>Bolivianos</asp:ListItem>
                    <asp:ListItem>Dolares</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>            
        </tr>        
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label38" runat="server" Text="Retencion IUE %"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_retencionIUEporcentaje" class="form-control" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label39" runat="server" Text="Retencion IUE Bs:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_retencionIUEBS" class="form-control" runat="server" 
                    Width="100px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Retencion IT %"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_retencionITporcentaje" class="form-control" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Retencion IT Bs:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_retencionITBS" class="form-control" runat="server" 
                    Width="100px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
    </table>

    <table>
    <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Total a Pagar :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_totalaPagar" class="form-control" runat="server" 
                    Width="300px"></asp:TextBox>
            </td>
            <td></td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label33" runat="server" Text="Cheque Nro. :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_chequenro" class="form-control" runat="server" 
                    Width="300px"></asp:TextBox>
            </td>
            <td></td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Nro Factura :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_facturanro" class="form-control" runat="server" 
                    Width="300px"></asp:TextBox>
            </td>
            <td></td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label36" runat="server" Text="Banco :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_banco" class="form-control" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td></td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label37" runat="server" Text="Efectivo :"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="cb_efectivo" runat="server" />
            </td>
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label34" runat="server" Text="Concepto :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_concepto" class="form-control" runat="server" Width="450px" 
                    Height="100px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td></td>            
        </tr>    
    </table>
    <table>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="bt_limpiar" class="btn btn-light" runat="server" Text="Limpiar" 
                    onclick="bt_limpiar_Click" />
            </td>
            <td>
                <asp:Button ID="bt_insertar" class="btn btn-success" runat="server" 
                    Text="Insertar" onclick="bt_insertar_Click" />
            </td>
            <td>
                <asp:Button ID="bt_modificar" class="btn btn-warning" runat="server" 
                    Text="Modificar" onclick="bt_modificar_Click" />
            </td>
            
            <td>
                <asp:Button ID="bt_verEgreso" class="btn btn-light" runat="server" Text="Ver Egreso" 
                    onclick="bt_verEgreso_Click" />
            </td>
            
            <td>
                <asp:Button ID="bt_eliminar" class="btn btn-danger" runat="server" 
                    Text="Eliminar" onclick="bt_eliminar_Click" />
            </td>
            
        </tr>
    </table>

            </div>
        </div>
     </div>
</div>


<div class="row">
    <div class="col-md-10 col-md-offset-1">
        <div class="panel panel-success class">
            <div class="panel-heading">
            <h3 class="panel-title">Datos:</h3>
            </div>
            <div class="panel-body">
            <div class="DatosProyecto">
                

                <asp:GridView ID="gv_reciboIngresoEgreso" 
<<<<<<< HEAD
                    runat="server" BackColor="White" 
=======
                    CssClass="table table-responsive table-striped" runat="server" BackColor="White" 
>>>>>>> origin/modulo3
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                    Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
                    onselectedindexchanged="gv_reciboIngresoEgreso_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
                

            </div>
            <asp:Button ID="bt_excel" class="btn btn-success mr-2" runat="server" 
                    Text="Excel" onclick="bt_excel_Click" />
            </div>
        </div>
    </div>
</div>


</div>
</asp:Content>
