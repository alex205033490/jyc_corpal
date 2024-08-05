<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_Importacion_CredinForm.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_Importacion_CredinForm" %>
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
            width: 128px;
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
        <h3> <asp:Label ID="lbtitulo" runat="server" Text="Seguros"></asp:Label> </h3>
    </div>


<div class="datos1">
<table>
    <tr>
        <td class="style72"></td>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Nro. de Chasis:" Font-Size="Small" 
                Width="180px"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_nroChasis" runat="server" Font-Size="Small"></asp:TextBox>
               <asp:AutoCompleteExtender ID="tx_nroChasis_AutoCompleteExtender" runat="server" 
            CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="getListaEquipo_Total" 
            TargetControlID="tx_nroChasis"  UseContextKey="True"
            CompletionListCssClass="CompletionList" 
            CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
            >
        </asp:AutoCompleteExtender>

        </td>
        <td class="style1">
            <asp:Button ID="bt_verificar" runat="server" Text="Verificar" 
                onclick="bt_verificar_Click" />
        </td>
        <td>
            <asp:Button ID="bt_limpiza" runat="server" 
                Text="Limpiar" onclick="bt_limpiza_Click" />
        </td>
        <td>
            &nbsp;</td>
        
    </tr>
    
    <tr>
        <td class="style72"></td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        
    </tr>

    <tr>
        <td class="style69"></td>
        <td class="style67">
            <asp:Label ID="Label2" runat="server" Text="Nro de Aplicacion:" Font-Size="Small" 
                Width="180px"></asp:Label>
            </td>
        <td class="style67">
            <asp:Label ID="Label4" runat="server" Text="Nombre del Proyecto:" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
        <td class="style67">
            <asp:Label ID="Label5" runat="server" Text="Consignatario:" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
        <td class="style67">
            &nbsp;</td>
        <td class="style67">
            </td>
        
    </tr>


        <tr>
        <td class="style73"></td>
        <td class="style15" >
            <asp:TextBox ID="tx_nrodeAplicacion" runat="server" Font-Size="Small"></asp:TextBox>

            </td>
        <td class="style15">
            <asp:TextBox ID="tx_nombreProyecto" runat="server" 
                Font-Size="Small" Enabled="False"></asp:TextBox>
            </td>
        <td class="style14">
            <asp:TextBox ID="tx_consignatario" runat="server" Font-Size="Small"></asp:TextBox>
            </td>
        <td class="style15">
            &nbsp;</td>
        <td class="style15">
            &nbsp;</td>
     
    </tr>
    
    <tr>
        <td class="style70"></td>    
        <td class="style66">
            <asp:Label ID="Label10" runat="server" 
                Text="Fecha de Pago:" Font-Size="Small" 
                Width="180px"></asp:Label>
            </td>    
        <td class="style66">
            <asp:Label ID="Label11" runat="server" Text="Fecha de Solicitud:" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>    
        <td class="style66">
            <asp:Label ID="Label66" runat="server" Font-Size="Small" Text="Ciudad:"></asp:Label>
        </td>    
        <td class="style66">
            <asp:Label ID="Label56" runat="server" Text="Contenedor:" Font-Size="Small"></asp:Label>
            </td>    
        <td class="style66">
            </td>    
       

    </tr>


        <tr>
        <td class="style73"></td>
        <td class="style15">
            <asp:TextBox ID="tx_fechaPago" runat="server" Font-Size="Small"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaPago_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaPago">
            </asp:CalendarExtender>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_fechaSolicitud" runat="server" Font-Size="Small"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaSolicitud_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaSolicitud">
            </asp:CalendarExtender>
            </td>
        <td class="style14">
            <asp:TextBox ID="tx_ciudad" runat="server" Enabled="False"></asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_contenedor" runat="server"></asp:TextBox>
            </td>
        <td class="style15">
            &nbsp;</td>
        
    </tr>

    <tr>
        <td class="style71"></td>
        <td class="style60">
            <asp:Label ID="Label13" runat="server" Text="Valor Porcentaje:" 
                Font-Size="Small" Width="180px"></asp:Label>
            </td>
        <td class="style60">
            <asp:Label ID="Label67" runat="server" Font-Size="Small" 
                Text="Valor Fob :"></asp:Label>
            </td>
        <td class="style60">
            <asp:Label ID="Label68" runat="server" Font-Size="Small" Text="Tipo Cambio:"></asp:Label>
            </td>
        <td class="style60">
            </td>
        <td class="style60">
            </td>
        
    </tr>

        <tr>
        <td class="style73"></td>
        <td class="style15">
            <asp:TextBox ID="tx_valorPorcentaje" runat="server" Font-Size="Small" 
                Enabled="False">0.40</asp:TextBox>
            </td>
        <td class="style15">
            <asp:TextBox ID="tx_valorFobOriginal" runat="server" Enabled="False" 
                Font-Size="Small">0</asp:TextBox>
            </td>
        <td class="style14">
            <asp:TextBox ID="tx_tipocambio" runat="server" Font-Size="Small">6.96</asp:TextBox>
            </td>
        <td class="style15">
            &nbsp;</td>
        <td class="style15">
            &nbsp;</td>       
     </tr>

     <tr>
     <td class="style72"></td>
     <td>
            <asp:Label ID="Label52" runat="server" Font-Size="Small" 
                Text="Valor Fob $:"></asp:Label>
         </td>
     <td>
            <asp:Label ID="Label51" runat="server" Font-Size="Small" 
                Text="Valor Fob $ Resultado:"></asp:Label>
         </td>
     <td>
            <asp:Label ID="Label57" runat="server" Font-Size="Small" 
                Text="Valor CFR $:"></asp:Label>
         </td>
     <td>
            <asp:Label ID="Label58" runat="server" Font-Size="Small" 
                Text="Valor CFR $ Resultado:"></asp:Label>
         </td>
     <td></td>     
     </tr>

     <tr>
     <td class="style72"></td>
     <td>
            <asp:TextBox ID="tx_valorFob" runat="server">0</asp:TextBox>
          </td>
     <td>
            <asp:TextBox ID="tx_valorFobResultado" runat="server" Enabled="False">0</asp:TextBox>
          </td>
     <td>
            <asp:TextBox ID="tx_valorCFR" runat="server">0</asp:TextBox>
          </td>
     <td>
            <asp:TextBox ID="tx_valorCFR_resultado" runat="server" Enabled="False">0</asp:TextBox>
          </td>
     <td></td>     
     </tr>

     <tr>
        <td></td>
        <td>
            <asp:Label ID="Label59" runat="server" Font-Size="Small" 
                Text="Transp Maritimo $:"></asp:Label>
         </td>
        <td>
            <asp:Label ID="Label60" runat="server" Font-Size="Small" 
                Text="Transp Maritimo $ Resultado:"></asp:Label>
         </td>
        <td>
            <asp:Label ID="Label61" runat="server" Font-Size="Small" 
                Text="Transp Terrestre $:"></asp:Label>
         </td>
        <td>
            <asp:Label ID="Label62" runat="server" Font-Size="Small" 
                Text="Transp Terrestre $ Resultado:"></asp:Label>
         </td>
        <td></td>        
     </tr>

     <tr>
        <td></td>
        <td>
            <asp:TextBox ID="tx_transpMaritimo" runat="server">0</asp:TextBox>
         </td>
        <td>
            <asp:TextBox ID="tx_transpMaritimoResultado" runat="server" Enabled="False">0</asp:TextBox>
         </td>
        <td>
            <asp:TextBox ID="tx_transpTerrestre" runat="server">0</asp:TextBox>
         </td>
        <td>
            <asp:TextBox ID="tx_transpTerrestreResultado" runat="server" Enabled="False">0</asp:TextBox>
         </td>
        <td></td>        
     </tr>

     <tr>
        <td></td>
        <td>
            <asp:Label ID="Label63" runat="server" Font-Size="Small" 
                Text="Nro Equipos del Contenedor:"></asp:Label>
         </td>
        <td>
            <asp:Label ID="Label64" runat="server" Font-Size="Small" 
                Text="Base Imponible para el Seguro:"></asp:Label>
         </td>
        <td>
            <asp:Label ID="Label65" runat="server" Font-Size="Small" 
                Text="Total Valor Seguro:"></asp:Label>
         </td>
        <td></td>
        <td></td>        
     </tr>

      <tr>
        <td></td>
        <td>
            <asp:TextBox ID="tx_nroEquiposContenedor" runat="server">0</asp:TextBox>
          </td>
        <td>
            <asp:TextBox ID="tx_baseImponibleParaelSeguro" runat="server" Enabled="False">0</asp:TextBox>
          </td>
        <td>
            <asp:TextBox ID="tx_totalValorSeguro" runat="server" Enabled="False">0</asp:TextBox>
          </td>
        <td></td>
        <td></td>        
     </tr>

      <tr>
        <td></td>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>        
     </tr>

      <tr>
        <td></td>
        <td>
            <asp:Button ID="bt_calcular" runat="server" onclick="bt_calcular_Click" 
                Text="Calcular" />
          </td>
        <td>
            <asp:Button ID="bt_actualizar" runat="server" Text="Actualizar" 
                onclick="bt_actualizar_Click" />
            <asp:ConfirmButtonExtender ID="bt_actualizar_ConfirmButtonExtender" 
                runat="server" ConfirmText="Se actualizaran los Datos de CredinForm" 
                TargetControlID="bt_actualizar">
            </asp:ConfirmButtonExtender>
          </td>
        <td></td>
        <td></td>
        <td></td>        
     </tr>

</table>
</div>

<div class="Blanco">
</div>

</div>


</asp:Content>
