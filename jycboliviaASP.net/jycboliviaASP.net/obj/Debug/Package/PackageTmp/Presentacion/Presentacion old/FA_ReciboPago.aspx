<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FA_ReciboPago.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ReciboPago" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style_ReciboPago.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 373px;
        }
        .style2
        {
            width: 181px;
        }
        .style4
        {
            width: 100px;
        }
        .style5
        {
            width: 55px;
        }
        .style6
        {
            width: 59px;
        }
        .style7
        {
            width: 44px;
        }
        .style8
        {
            width: 353px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class = "container">
    <table>
    <tr>
    <td>
    <div class = "logoIzquierdo"></div>
    </td>
    <td class="style1">
        <div class = "Titulo_logo">
        <div><h1>Recibo de Pago</h1></div>
        <div>
            <table style="margin-left: 92px" >
            <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Nº : " Font-Size="X-Small"></asp:Label></td>
            <td><asp:TextBox ID="tx_numeroRegistro" runat="server" Font-Size="X-Small" 
                    BorderColor="Silver"></asp:TextBox></td>
            </tr>
            <tr>
           <td></td>
            <td>
            
       <asp:Label ID="tx_FechaRecibo" runat="server" Text="Fecha" Font-Size="X-Small"></asp:Label>
            
            </td>
            </tr>
            </table>
        </div>
        
        </div>
    
    </td>
    <td>
    <div class = "logo_Derecho"></div>
    </td>        
    </tr>
    </table>

   <div class = "responsableRecibo">
   <table style="margin-left: 36px">
   <tr>
   <td class="style2">
       <asp:Label ID="Label2" runat="server" Text="Nombre Responsable : " 
           Font-Size="X-Small"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="tx_Responsable" runat="server" Width="318px" 
           Font-Size="X-Small" BorderColor="Silver"></asp:TextBox>
   </td>
   </tr>
   <tr>
   <td class="style2">
       <asp:Label ID="Label3" runat="server" Text="Nombre Proyecto :" 
           Font-Size="X-Small"></asp:Label>
       </td>
   <td>
       <asp:TextBox ID="tx_NombreProyecto" runat="server" Width="317px" 
           Font-Size="X-Small" BorderColor="Silver"></asp:TextBox>
       </td>
   </tr>

   <tr>
   <td class="style2">
       <asp:Label ID="Label4" runat="server" Text="NombreResponsablePago : " 
           Font-Size="X-Small"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="tx_ResponsablePago" runat="server" Width="314px" 
           Font-Size="X-Small" BorderColor="Silver"></asp:TextBox>
   </td>
   </tr>
    <tr>
   <td class="style2">
       <asp:Label ID="Label5" runat="server" Text="Monto Pagado : " 
           Font-Size="X-Small"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="tx_montoPagado" runat="server" Font-Size="X-Small" 
           BorderColor="Silver"></asp:TextBox>
   </td>
   </tr>
   <tr>
   <td>
       <asp:Label ID="Label22" runat="server" Text="Mes Cancelado :" 
           Font-Size="X-Small"></asp:Label></td>
   <td>
       <asp:TextBox ID="tx_MesCancelado0" runat="server" BorderColor="Silver" 
           Font-Size="X-Small"></asp:TextBox>
       </td>
   </tr>
   <tr>
   <td class="style2">
       &nbsp;</td>
   <td>
       <asp:CheckBox ID="CheckBox_Efectivo" runat="server" Text="Efectivo " 
           Font-Size="X-Small" />
       <asp:CheckBox ID="CheckBox_Deposito" runat="server" Text="Deposito" 
           Font-Size="X-Small" />
   </td>
   
   </tr>
   <tr>
   <td>
       <asp:Label ID="Label7" runat="server" Text="Numero Cheque :" 
           Font-Size="X-Small"></asp:Label>
       </td>
   <td>
       <asp:TextBox ID="tx_NroCheques" runat="server" Width="214px" 
           Font-Size="X-Small" BorderColor="Silver">0</asp:TextBox>
       </td>
   </tr>
   <tr>
   
   <td class="style2">
       <asp:Label ID="Label6" runat="server" Text="Detalle :" Font-Size="X-Small"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="tx_Detalle" runat="server" TextMode= "MultiLine" Height="58px" 
           Width="314px" Font-Size="X-Small" BorderColor="Silver"></asp:TextBox>
       </td>
   </tr>
    </table>

   <table>
   <tr>
   <td class="style6"></td>
   <td></td>
   <td class="style7"></td>
   <td class="style5"></td>
   <td></td>
   <td class="style4"></td>
   </tr>
   <tr>
   <td class="style6"></td>
   <td>
       <asp:Label ID="Label9" runat="server" Text="________________________"></asp:Label>
       </td>
   <td class="style7"></td>
   <td class="style5"></td>
   <td>
       <asp:Label ID="Label10" runat="server" Text="__________________________"></asp:Label>
       </td>
   <td class="style4"></td>
   </tr>
   <tr>
   <td class="style6"></td>
   <td>
       <asp:Label ID="lb_reponsable" runat="server" Text="Responsable" 
           Font-Size="X-Small"></asp:Label>
       </td>
   <td class="style7"></td>
   <td class="style5"></td>
   <td>
       <asp:Label ID="Label8" runat="server" Text="Recibio Conforme" 
           Font-Size="X-Small"></asp:Label>
&nbsp;</td>
   <td class="style4"></td>
   </tr>

   </table>


   </div>

   
    </div>
    <div class="fin">
    <table>
    <tr>
    <td class="style8">
    <asp:Button ID="bt_imprimir" runat="server" Text="Imprimir" 
           onclick="bt_imprimir_Click" style="margin-left: 205px; margin-top: 13px" 
            Width="64px" />
    </td>
    <td>
     <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton1_Click">Volver Atras</asp:LinkButton>
    </td>
    </tr>
    </table>

    
   
    </div>
   
   
    <div class = "container">
    <table>
    <tr>
    <td>
    <div class = "logoIzquierdo"></div>
    </td>
    <td class="style1">
        <div class = "Titulo_logo">
        <div><h1>Recibo de Pago</h1></div>
        <div>
            <table style="margin-left: 92px" >
            <tr>
            <td><asp:Label ID="Label11" runat="server" Text="Nº : " Font-Size="X-Small"></asp:Label></td>
            <td><asp:TextBox ID="tx_numeroRegistro0" runat="server" Font-Size="X-Small" 
                    BorderColor="Silver"></asp:TextBox></td>
            </tr>
            <tr>
           <td></td>
            <td>
            
       <asp:Label ID="tx_FechaRecibo0" runat="server" Text="Fecha" Font-Size="X-Small"></asp:Label>
            
            </td>
            </tr>
            </table>
        </div>
        
        </div>
    
    </td>
    <td>
    <div class = "logo_Derecho"></div>
    </td>        
    </tr>
    </table>

   <div class = "responsableRecibo">
   <table style="margin-left: 36px">
   <tr>
   <td class="style2">
       <asp:Label ID="Label12" runat="server" Text="Nombre Responsable : " 
           Font-Size="X-Small"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="tx_Responsable0" runat="server" Width="318px" 
           Font-Size="X-Small" BorderColor="Silver"></asp:TextBox>
   </td>
   </tr>
   <tr>
   <td class="style2">
       <asp:Label ID="Label13" runat="server" Text="Nombre Proyecto :" 
           Font-Size="X-Small"></asp:Label>
       </td>
   <td>
       <asp:TextBox ID="tx_NombreProyecto0" runat="server" Width="317px" 
           Font-Size="X-Small" BorderColor="Silver"></asp:TextBox>
       </td>
   </tr>

   <tr>
   <td class="style2">
       <asp:Label ID="Label14" runat="server" Text="NombreResponsablePago : " 
           Font-Size="X-Small"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="tx_ResponsablePago0" runat="server" Width="314px" 
           Font-Size="X-Small" BorderColor="Silver"></asp:TextBox>
   </td>
   </tr>
    <tr>
   <td class="style2">
       <asp:Label ID="Label15" runat="server" Text="Monto Pagado : " 
           Font-Size="X-Small"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="tx_montoPagado0" runat="server" Font-Size="X-Small" 
           BorderColor="Silver"></asp:TextBox>
   </td>   
   </tr>
   <tr>
   <td>
       <asp:Label ID="Label21" runat="server" Text="Mes Cancelado :" 
           Font-Size="X-Small"></asp:Label>
    </td>
   <td>
       <asp:TextBox ID="tx_MesCancelado" runat="server" BorderColor="Silver" 
           Font-Size="X-Small"></asp:TextBox>
       </td>
   </tr>
   <tr>
   <td class="style2">
       &nbsp;</td>
   <td>
       <asp:CheckBox ID="CheckBox_Efectivo0" runat="server" Text="Efectivo " 
           Font-Size="X-Small" />
       <asp:CheckBox ID="CheckBox_Deposito0" runat="server" Text="Deposito" 
           Font-Size="X-Small" />
   </td>
   
   </tr>
   <tr>
   <td>
       <asp:Label ID="Label16" runat="server" Text="Numero Cheque :" 
           Font-Size="X-Small"></asp:Label>
       </td>
   <td>
       <asp:TextBox ID="tx_NroCheques0" runat="server" Width="214px" 
           Font-Size="X-Small" BorderColor="Silver">0</asp:TextBox>
       </td>
   </tr>
   <tr>
   
   <td class="style2">
       <asp:Label ID="Label17" runat="server" Text="Detalle :" Font-Size="X-Small"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="tx_Detalle0" runat="server" TextMode= "MultiLine" Height="58px" 
           Width="314px" Font-Size="X-Small" BorderColor="Silver"></asp:TextBox>
       </td>
   </tr>
 
   </table>

   <table>
   <tr>
   <td class="style6"></td>
   <td></td>
   <td class="style7"></td>
   <td class="style5"></td>
   <td></td>
   <td class="style4"></td>
   </tr>
   <tr>
   <td class="style6"></td>
   <td>
       <asp:Label ID="Label18" runat="server" Text="________________________"></asp:Label>
       </td>
   <td class="style7"></td>
   <td class="style5"></td>
   <td>
       <asp:Label ID="Label19" runat="server" Text="__________________________"></asp:Label>
       </td>
   <td class="style4"></td>
   </tr>
   <tr>
   <td class="style6"></td>
   <td>
       <asp:Label ID="lb_reponsable0" runat="server" Text="Responsable" 
           Font-Size="X-Small"></asp:Label>
       </td>
   <td class="style7"></td>
   <td class="style5"></td>
   <td>
       <asp:Label ID="Label20" runat="server" Text="Recibio Conforme" 
           Font-Size="X-Small"></asp:Label>
&nbsp;</td>
   <td class="style4"></td>
   </tr>

   </table>


   </div>






    </div>
    </form>
</body>
</html>
