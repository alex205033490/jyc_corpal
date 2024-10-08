<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CostosPolizaImportacion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CostosPolizaImportacion" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <link href="../Styles/Style_CostosPolizasImportacion.css" rel="stylesheet" type="text/css" />

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
        
        .style1
        {
            height: 26px;
            width: 18px;
        }
        .style14
        {
            height: 26px;
            width: 77px;
        }
        .style15
        {
            width: 77px;
            height: 35px;
        }
        .style50
    {
        width: 23px;
    }
    .style51
    {
        width: 23px;
        height: 35px;
    }
    .style52
    {
        width: 22px;
    }
    .style53
    {
        width: 22px;
        height: 59px;
    }
    .style56
    {
        height: 48px;
    }
    .style57
    {
        height: 59px;
    }
    .style58
    {
        height: 50px;
    }
    .style59
    {
        height: 66px;
    }
    .style60
    {
        height: 41px;
    }
    .style61
    {
        height: 44px;
    }
    .style62
    {
        height: 38px;
    }
    .style63
    {
        height: 40px;
    }
    .style64
    {
        width: 136px;
    }
    .style65
    {
        width: 85px;
    }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class = "menu">  
        
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>
   
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

<div class="Centrar">
    <div class="titulo">
        <h3>COSTOS POLIZAS IMPORTACION</h3>
    </div>


<div class="datos1">
<table>
    <tr>
        <td></td>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Nro. DUI" Font-Size="Small" 
                Width="100px" style="font-weight: 700"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_nrodui" runat="server" Font-Size="Small"></asp:TextBox>
            <asp:AutoCompleteExtender ID="tx_nrodui_AutoCompleteExtender" runat="server" 
                TargetControlID="tx_nrodui"
                CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="GetlistaNroDUI" 
                UseContextKey="True" CompletionListCssClass="CompletionList" 
                CompletionListItemCssClass="CompletionlistItem" 
                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10" >

            </asp:AutoCompleteExtender>
        </td>
        <td class="style1">
            <asp:Button ID="bt_verificar" runat="server" Text="Verificar" />
        </td>
        <td>
            <asp:Button ID="bt_limpiza" runat="server" 
                Text="Limpiar" />
        </td>
        <td>
            <asp:Button ID="bt_actualizar" runat="server" Text="Actualizar" />
            <asp:ConfirmButtonExtender ID="bt_actualizar_ConfirmButtonExtender" 
                runat="server" ConfirmText="Se actualizaran los Datos del DUI indicado" 
                TargetControlID="bt_actualizar">
            </asp:ConfirmButtonExtender>
        </td>
        
    </tr>
    
    <tr>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        
    </tr>

    <tr>
        <td class="style58"></td>
        <td class="style58">
            <asp:Label ID="Label2" runat="server" Text="Fecha de la Factura (DUI)" Font-Size="Small" 
                Width="180px"></asp:Label>
            </td>
        <td class="style58">
            <asp:Label ID="Label3" runat="server" Text="No. de NIT del Proveedor (DUI)" Font-Size="Small" 
                Width="180px"></asp:Label>
            </td>
        <td class="style58">
            <asp:Label ID="Label4" runat="server" Text="Nombre o Razon Social del Proveedor (DUI)" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
        <td class="style58">
            <asp:Label ID="Label5" runat="server" Text="Imp. Base para Credito Fiscal (DUI)" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
        <td class="style58">
            <asp:Label ID="Label6" runat="server" Text="Credito Fiscal (DUI)" Font-Size="Small" 
                Width="180px"></asp:Label>
            </td>
        
    </tr>


        <tr>
        <td class="style15"></td>
        <td class="style15" >
            <asp:TextBox ID="tx_fechafactura" runat="server" Font-Size="Small"></asp:TextBox>

            <asp:CalendarExtender ID="tx_fechafactura_CalendarExtender" runat="server" 
                TargetControlID="tx_fechafactura">
            </asp:CalendarExtender>

            </td>
        <td class="style15">
            <asp:TextBox ID="tx_nitproveedor" runat="server" Font-Size="Small"></asp:TextBox>
            </td>
        <td class="style14">
            <asp:TextBox ID="tx_nombrerazonsocialproveedor" runat="server" 
                Font-Size="Small"></asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_impbaseparacreditofiscal" runat="server" Font-Size="Small" 
                Enabled="False">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_creditofiscal" runat="server" Font-Size="Small">0</asp:TextBox>
            </td>
     
    </tr>
    
    <tr>
        <td class="style59"></td>    
        <td class="style59">
            <asp:Label ID="Label10" runat="server" 
                Text="Iva-CF Poliza [Credito Fiscal (DUI) + IVA-CF. Planilla Aduanera]" Font-Size="Small" 
                Width="180px"></asp:Label>
            </td>    
        <td class="style59">
            <asp:Label ID="Label11" runat="server" Text="Importe Planilla Aduanera" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>    
        <td class="style59">
            <asp:Label ID="Label12" runat="server" Text="Importe Pago Planilla Aduanera" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>    
        <td class="style59">
            <asp:Label ID="Label37" runat="server" Font-Size="Small" 
                Text="Nro Factura Agencia Aduanera"></asp:Label>
            </td>    
        <td class="style59">
            <asp:Label ID="Label38" runat="server" Text="Importe Factura Agencia Aduanera" 
                Font-Size="Small"></asp:Label>
            </td>    
       

    </tr>


        <tr>
        <td class="style15"></td>
        <td class="style15">
            <asp:TextBox ID="tx_iva_cf_poliza" runat="server" Font-Size="Small" 
                Enabled="False">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_planillaaduanera" runat="server" Font-Size="Small">0</asp:TextBox>
            </td>
        <td class="style14">
            <asp:TextBox ID="tx_pagoplanillaaduanera" runat="server" Font-Size="Small">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_nrofacturaAgencia" runat="server" Font-Size="Small"></asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_importeFac" runat="server" Font-Size="Small">0</asp:TextBox>
            </td>
        
    </tr>

    <tr>
        <td class="style60"></td>
        <td class="style60">
            <asp:Label ID="Label13" runat="server" Text="IVA-CF Planilla Aduanera" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
        <td class="style60">
            <asp:Label ID="Label14" runat="server" Text="Valor Neto Planilla Aduanera" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
        <td class="style60">
            <asp:Label ID="Label15" runat="server" Text="Dif. En Pago Planilla Aduanera" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
        <td class="style60">
            <asp:Label ID="Label28" runat="server" Text="Cantidad de Equipos segun la DUI" Font-Size="Small" 
                Width="180px"></asp:Label>
            </td>
        <td class="style60">
            <asp:Label ID="Label29" runat="server" Text="Descripcion del Producto" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
        
    </tr>

        <tr>
        <td class="style15"></td>
        <td class="style15">
            <asp:TextBox ID="tx_iva_cf_planillaaduanera" runat="server" Font-Size="Small" 
                Enabled="False">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_valornetoplanillaaduanera" runat="server" Font-Size="Small" 
                Enabled="False">0</asp:TextBox>
            </td>
        <td class="style14">
            <asp:TextBox ID="tx_dif_enpago_pa" runat="server" Font-Size="Small" 
                Enabled="False">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_cantidad_equiposDui" runat="server" Font-Size="Small">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_descripciondelproducto" runat="server" Width="180px" 
                Font-Size="Small"></asp:TextBox>
            </td>       
     </tr>

     <tr>
     <td></td>
     <td>
            <asp:Label ID="Label52" runat="server" Font-Size="Small" 
                Text="Nro Factura Almacenera"></asp:Label>
         </td>
     <td>
            <asp:Label ID="Label51" runat="server" Font-Size="Small" 
                Text="Importe Almacenera"></asp:Label>
         </td>
     <td></td>
     <td></td>
     <td></td>     
     </tr>

     <tr>
     <td></td>
     <td>
            <asp:TextBox ID="tx_nroFacturaAlmacenera" runat="server"></asp:TextBox>
          </td>
     <td>
            <asp:TextBox ID="tx_importeAlmacenera" runat="server">0</asp:TextBox>
          </td>
     <td></td>
     <td></td>
     <td></td>     
     </tr>

</table>
</div>

<div>
<p><strong>PRORRATEO DE COSTOS (Transporte)</strong></p>
</div>
<div class="datos2">
<table>    
        <tr>
            <td class="style61"></td>
            <td class="style61">
            <asp:Label ID="Label17" runat="server" Text="Importe Factura (Tramo - Internacional)" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
            <td class="style61">
            <asp:Label ID="Label18" runat="server" Text="Importe de la Factura (Tramo - Nacional)" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
            <td class="style61">
            <asp:Label ID="Label19" runat="server" Text="Nro. Fact. Transpt. Nac e Inter" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
            <td class="style61">
            <asp:Label ID="Label20" runat="server" Text="Importe de la Fact. Logistica para Transporte" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
            <td class="style61">
            <asp:Label ID="Label21" runat="server" Text="Nro. Fact. Logistica para Transporte" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
            
        </tr>

        <tr>
        <td class="style15"></td>
        <td class="style15">
            <asp:TextBox ID="tx_prorrateodecostos_transporte_internacional" runat="server" 
                Font-Size="Small">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_prorrateodecostos_transporte_nacional" runat="server" 
                Font-Size="Small">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_prorrateodecostos_nrofacttransptnaceinter" runat="server" 
                Font-Size="Small">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_prorrateodecostos_logisticaparatransporte" runat="server" 
                Font-Size="Small">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_prorrateodecostos_nrofactlogistica" runat="server" 
                Font-Size="Small">0</asp:TextBox>
            </td>
        
    </tr>

    <tr>
        <td class="style62"></td>
        <td class="style62">
            <asp:Label ID="Label22" runat="server" Text="Importe de la Factura MSC LTDA" Font-Size="Small" 
                Width="180px"></asp:Label>
            </td>
        <td class="style62">
            <asp:Label ID="Label23" runat="server" Text="Nro. de la Fact. MSC" Font-Size="Small" 
                Width="180px"></asp:Label>
            </td>
        <td class="style62">
            <asp:Label ID="Label24" runat="server" Text="Importe de la Planilla de ASPB" Font-Size="Small" 
                Width="180px"></asp:Label>
            </td>
        <td class="style62">
            <asp:Label ID="Label25" runat="server" Text="Nro. Deposito o Planilla ASPB" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
        <td class="style62">
            <asp:Label ID="Label26" runat="server" Text="Importe Mercaderia en Transito" 
                Font-Size="Small" Width="180px"></asp:Label>
          </td>
     
     
    </tr>

        <tr>
        <td class="style15"></td>
        <td class="style15">
            <asp:TextBox ID="tx_prorrateodecostos_mscltda" runat="server" Font-Size="Small">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_prorrateodecostos_nrofactmsc" runat="server" 
                Font-Size="Small">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_prorrateodecostos_aspb" runat="server" Font-Size="Small"></asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_prorrateodecostos_nrodepositooplanillaaspb" runat="server" 
                Font-Size="Small">0</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_mercaderiaentransito" runat="server" Font-Size="Small" 
                Enabled="False">0</asp:TextBox>
            </td>
       
    </tr>
    
    <tr>
        <td class="style63"></td>
        <td class="style63">
            <asp:Label ID="Label27" runat="server" Text="Total Costo Poliza" 
                Font-Size="Small" Width="180px"></asp:Label>
          </td>
        <td class="style63">
            &nbsp;</td>
        <td class="style63">
            &nbsp;</td>
        <td class="style63">
            &nbsp;</td>
        <td class="style63"></td>
        
    </tr>

      <tr>
        <td class="style15"></td>
        <td class="style15">
            <asp:TextBox ID="tx_totalcostopoliza" runat="server" Font-Size="Small" 
                Enabled="False">0</asp:TextBox>
          </td>
        <td class="style15">
            &nbsp;</td>
        <td class="style15">
            &nbsp;</td>
        <td class="style15">
            &nbsp;</td>
        <td class="style15">
            </td>
       
    </tr>

</table>
<div class="datos4">
    <asp:GridView ID="gv_prorrateoCostos" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
        onselectedindexchanged="gv_prorrateoCostos_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="Gray" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>  

</div>


</div>

<div>
<p><strong>Nro. Factura Comercial</strong></p>
</div>
<div>
<table>
<tr>
    <td class="style53"></td>
    <td class="style57">
        <asp:Label ID="Label39" runat="server" Font-Size="Small" 
            Text="Nro. de la Factura Comercial (Proveedor del Exterior)" Width="180px"></asp:Label>
    </td>
    <td class="style57">
        <asp:Label ID="Label40" runat="server" Font-Size="Small" 
            Text="Giro al Exterior (Proveedor del Exterior Moneda de Factura) EURO" 
            Width="180px"></asp:Label>
    </td>
    <td class="style57">
        <asp:Label ID="Label41" runat="server" Font-Size="Small" 
            Text="Proveedores por Pagar (Proveedor del Exterior)" Width="180px"></asp:Label>
    </td>
    <td class="style57">
        <asp:Label ID="Label42" runat="server" Font-Size="Small" 
            Text="Moneda (del Giro al Exterior)" Width="180px"></asp:Label>
    </td>
    <td class="style57">
        &nbsp;</td>
    <td class="style57">
        </td>
    
    
    
</tr>
<tr>
    <td class="style52"></td>
    <td>
        <asp:TextBox ID="tx_facProv_FacComercial" runat="server" Font-Size="Small"></asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="tx_facProv_GiroalExterior" runat="server" Font-Size="Small">0</asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="tx_facProv_ProveedoresporPagar" runat="server" 
            Font-Size="Small"></asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="tx_facProv_moneda" runat="server" Font-Size="Small"></asp:TextBox>
    </td>
    <td>
        &nbsp;</td>
    <td>
        &nbsp;</td>
    
</tr>
<tr>
    <td></td>
    <td>
        <asp:Label ID="Label43" runat="server" Font-Size="Small" 
            Text="Giro Factura Comercial (del giro al Exterior) DOLARES" Width="180px"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label53" runat="server" Font-Size="Small" 
            Text="TC Giro Factura Comercial (Dolares)"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label54" runat="server" Font-Size="Small" 
            Text="Giro Factura Comercial (del giro al Exterior) EURO" Width="180px"></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label55" runat="server" Font-Size="Small" 
            Text="TC Giro Factura Comercial (Euros)"></asp:Label>
    </td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td>
        <asp:TextBox ID="tx_facProv_giroFacturaComercial" runat="server" 
            Font-Size="Small">0</asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="tx_tcgirofactuacomercialalexterior" runat="server">0</asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="tx_girofacturaComercial_giroalexteriorEuro" runat="server">0</asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="tx_girofacturacomercialExterior_Euro" runat="server">0</asp:TextBox>
    </td>
    <td></td>
    <td></td>
</tr>

<tr>
    <td class="style56"></td>
    <td class="style56">
        <asp:Label ID="Label44" runat="server" Text="Comisión (del giro al Exterior)" 
            Font-Size="Small"></asp:Label>
    </td>
    <td class="style56">
        <asp:Label ID="Label45" runat="server" Font-Size="Small" 
            Text="ITF (del giro al Exterior)"></asp:Label>
    </td>
    <td class="style56">
        <asp:Label ID="Label46" runat="server" Font-Size="Small" 
            Text="Dif. de Cambio (del giro al Exterior)"></asp:Label>
    </td>
    <td class="style56">
            &nbsp;</td>
    <td class="style56"></td>
    <td class="style56"></td>

</tr>
<tr>
    <td></td>
    <td>
        <asp:TextBox ID="tx_facProv_comision" runat="server" Font-Size="Small">0</asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="tx_facProv_itf" runat="server" Font-Size="Small">0</asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="tx_facProv_difdeCambio" runat="server" Font-Size="Small" 
            Enabled="False">0</asp:TextBox>
    </td>
    <td>
        <asp:Button ID="bt_facProv_agregarFac" runat="server" Text="Agregar Fac" />
    </td>
    <td>
        &nbsp;</td>
    <td></td>
</tr>


</table>
</div>
<div class="datos4">
    <asp:GridView ID="gv_facturasProveedores" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onrowdeleting="gv_facturasProveedores_RowDeleting">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
</div>

<div>
<p><strong>Seguro del Equipo</strong></p>
</div>

<div class="datos5">
<table>
    <tr>
        <td></td>
        <td class="style64">
            <asp:Label ID="Label47" runat="server" Text="Exbo (Seguro)" Font-Size="Small"></asp:Label>
        </td>

        <td class="style65">
            <asp:Label ID="Label50" runat="server" Font-Size="Small" 
                Text="Fecha Factura Seguro"></asp:Label>
        </td>

        <td>
            <asp:Label ID="lb_nitSeguro" runat="server" Font-Size="Small" Text="Nit Seguro"></asp:Label>
        </td>

        <td Width="180px">
            <asp:Label ID="Label48" runat="server" Text="Nro. Factura del Seguro" 
                Font-Size="Small"></asp:Label>
        </td>
        <td Width="180px">
            <asp:Label ID="Label49" runat="server" Text="Importe de la Factura Seguro" 
                Font-Size="Small"></asp:Label>
        </td>
        <td Width="180px">
            &nbsp;</td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td class="style64">
            <asp:TextBox ID="tx_SegEq_Exbo" runat="server" Font-Size="Small"></asp:TextBox>
               <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                TargetControlID="tx_SegEq_Exbo"
                CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="getListaEquipo" 
                UseContextKey="True" CompletionListCssClass="CompletionList" 
                CompletionListItemCssClass="CompletionlistItem" 
                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10" >

            </asp:AutoCompleteExtender>
        </td>

        <td class="style65">
            <asp:TextBox ID="tx_fechaFacturaSeguro" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaFacturaSeguro_CalendarExtender" 
                runat="server" TargetControlID="tx_fechaFacturaSeguro">
            </asp:CalendarExtender>
        </td>

        <td>
            <asp:TextBox ID="tx_nitSeguro" runat="server"></asp:TextBox>
        </td>

        <td>
            <asp:TextBox ID="tx_SegEq_NroFactura" runat="server" Font-Size="Small"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="tx_SegEq_Seguro" runat="server" Font-Size="Small">0</asp:TextBox>
        </td>
        <td><strong>
            <asp:Button ID="bt_SegEq_AgregarEquipo" runat="server" Text="Agregar Equipo" 
                onclick="bt_SegEq_AgregarEquipo_Click" />
        

        </td>
        <td></td>
    </tr>
</table>
</div>

<div class="datos4">

    <asp:GridView ID="gv_SegurosdeEquipos" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onrowdeleting="gv_SegurosdeEquipos_RowDeleting">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>

</div>

<div class="datos3">

<table>
    <tr>
        <td class="style50"></td>
        <td>
            <asp:Label ID="Label36" runat="server" Font-Size="Small" Text="Observaciones" 
                Width="180px"></asp:Label>
        </td>
        <td></td>
        <td>
            &nbsp;</td>
        
    </tr>

<tr>
        <td class="style51"></td>
        <td class="style15">
            <asp:TextBox ID="tx_observaciones" runat="server" Height="150px" Width="650px" 
                TextMode="MultiLine"></asp:TextBox>
        </td>
        <td class="style15">
        </td>
        <td class="style15">
            &nbsp;</td>
       
    </tr>
</table>
</div>

<div class="Blanco">
</div>

</div>



</asp:Content>
