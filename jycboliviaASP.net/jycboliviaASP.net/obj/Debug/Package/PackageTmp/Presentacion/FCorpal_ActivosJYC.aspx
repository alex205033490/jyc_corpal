<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FCorpal_ActivosJYC.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ActivosJYC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

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
    
        .style1
        {
            width: 156px;
        }
    
        .style2
        {
            width: 160px;
        }
    
        .style3
        {
            width: 91px;
        }
    
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class="container" style="padding-top: 1em;">
    


<div class="row">
    <div class="col-md-6 ">
        <div class="panel panel-success class">
            <div class="panel-heading">
            <h3 class="panel-title">Simec:</h3>
            </div>
            <div class="panel-body">
            
        <table>
        <tr>
            <td class="style1">
                <asp:Label ID="Label32" runat="server" Text="Activo Simec:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_detalleActivo" class="form-control" runat="server" 
                    Width="400px" Height="100px" TextMode="MultiLine"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label33" runat="server" Text="Custodio Simec:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_custodio" class="form-control" runat="server" Width="400px"></asp:TextBox>
                <asp:AutoCompleteExtender ID="tx_custodio_AutoCompleteExtender" runat="server" 
                    TargetControlID="tx_custodio"
                     ServiceMethod="GetlistaResponsableSimec" MinimumPrefixLength="1"
                                    UseContextKey="True"
                                    CompletionListCssClass="CompletionList" 
                                    CompletionListItemCssClass="CompletionlistItem"                                     
                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" 
                    CompletionInterval="10">
                </asp:AutoCompleteExtender>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label37" runat="server" Text="Tipo Valor Actual :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="dd_tipoValorActual" class="form-control" 
                runat="server" Width="350px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label31" runat="server" Text="Comprobante Simec:" 
                    Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_comprobante" runat="server"
                    class="form-control"  Width="150px" Font-Size="Small" ></asp:TextBox>                
            </td>            
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label39" runat="server" Text="Valor Simec $:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_valorSimec" class="form-control" runat="server"></asp:TextBox>
            </td>
        </tr>
        </table>

        <table>      
        
    
    </table>
        </br>

        <table>
        <tr>            
            <td>        
            
            <asp:Button ID="bt_buscar" class="btn btn-success" runat="server"   
                    onclick="bt_Actualizar_Click" Text="Buscar" />
            </td>
            <td><asp:Button ID="bt_limpiar"  class="btn btn-success" runat="server" 
                    Text="Limpiar" onclick="bt_limpiar_Click" /></td>
            <td>
                <asp:Button ID="bt_actualizar"  class="btn btn-success" runat="server" 
                    Text="Actualizar" onclick="bt_actualizar_Click1" />
            </td>
        </tr>
        </table>

            </div>
        </div>
     </div>

<div class="col-md-6">
        <div class="panel panel-success class">
            <div class="panel-heading">
            <h3 class="panel-title">Sistema JYC:</h3>
            </div>
            <div class="panel-body">
            
        <table>
        
        <tr>
        <td class="style2">
                <asp:Label ID="Label34" runat="server" Text="Asignar Custodio:"></asp:Label>
            </td>
        <td>
                <asp:TextBox ID="tx_asignarCustodio" class="form-control" runat="server" 
                    Width="400px"></asp:TextBox>
                
                <asp:AutoCompleteExtender ID="tx_asignarCustodio_AutoCompleteExtender" 
                    runat="server" TargetControlID="tx_asignarCustodio"
                    ServiceMethod="GetlistaResponsable2" MinimumPrefixLength="1"
                                    UseContextKey="True"
                                    CompletionListCssClass="CompletionList" 
                                    CompletionListItemCssClass="CompletionlistItem" 
                                    
                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" 
                    CompletionInterval="10">
                </asp:AutoCompleteExtender>
                
            </td>
        </tr>

        <tr>
        <td class="style2">
                <asp:Label ID="Label38" runat="server" Text="Ubicacion UNE:"></asp:Label>
            </td>
        <td>
                <asp:DropDownList ID="dd_ubicacionUNE" class="form-control" runat="server" 
                    Width="300px">
                    <asp:ListItem>Ninguno</asp:ListItem>
                    <asp:ListItem>Interlogi SRL</asp:ListItem>
                    <asp:ListItem>Interlogi-Santa Cruz</asp:ListItem>
                    <asp:ListItem>Elevamerica SRL</asp:ListItem>
                    <asp:ListItem>Elevamerica-La Paz</asp:ListItem>
                    <asp:ListItem>Elevamerica-Santa Cruz</asp:ListItem>
                    <asp:ListItem>Elevamerica-Tarija</asp:ListItem>
                    <asp:ListItem>Elevamerica-Potosi</asp:ListItem>
                    <asp:ListItem>Melevar SRL</asp:ListItem>
                    <asp:ListItem>Melevar-Cochabamba</asp:ListItem>
                    <asp:ListItem>Melevar-Oruro</asp:ListItem>
                    <asp:ListItem>JYCIA-Santa Cruz</asp:ListItem>
                    <asp:ListItem>JYCIA-Cochabamba</asp:ListItem>
                    <asp:ListItem>JYCIA-La Paz</asp:ListItem>
                    <asp:ListItem>JYCIA-Almacen</asp:ListItem>
                    <asp:ListItem>JYC-Santa Cruz</asp:ListItem>
                    <asp:ListItem>IMVEN-Santa Cruz</asp:ListItem>
                    <asp:ListItem>IMVEN-Cochabamba</asp:ListItem>
                    <asp:ListItem>IMVEN-La Paz</asp:ListItem>
                    <asp:ListItem>CORPAL-Santa Cruz</asp:ListItem>
                    <asp:ListItem>ConoSub</asp:ListItem>
                    <asp:ListItem>Otros</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:CheckBox ID="cb_dadodebaja" runat="server" Text="Dar de Baja" />
            </td>
        </tr>

        <tr>
            <td class="style2">
                <asp:Label ID="Label1" runat="server" Text="Observaciones:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_observacion" class="form-control" runat="server" 
                    Width="400px" Height="100px" TextMode="MultiLine"></asp:TextBox>
            </td>            
        </tr>

        </table>

        <table>
                <tr>
                    <td>                
            <asp:Label ID="Label2" runat="server" Text="Estado:" Font-Size="Small"></asp:Label></td>
                    <td>
                
                <asp:DropDownList ID="dd_estadoSimec" class="form-control" runat="server" Width="150px">
                </asp:DropDownList>
                    </td>
                    <td></td>
                    <td class="style3">
            <asp:CheckBox ID="cbx_noAplica" runat="server" Text="No Aplica" 
                oncheckedchanged="cbx_noAplica_CheckedChanged" AutoPostBack=true/>
                    </td>
                </tr>
                <tr>
                    <td>
            <asp:Label ID="Label35" runat="server" Text="Monto Valor $:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tx_montoValorJYC" class="form-control" runat="server"></asp:TextBox>
                    </td>
                    <td>
            <asp:Label ID="Label36" runat="server" Text="Tipo:"></asp:Label>
                    </td>
                    <td class="style3">
            <asp:TextBox ID="tx_tipocambio" class="form-control" runat="server" 
                Width="60px">6.96</asp:TextBox>
                    </td>
                </tr>
            </table>
            </br>
            </br>

            </div>
        </div>
     </div>

    <div class="col-md-12 ">
        <div class="panel panel-success class">
            <div class="panel-heading">            
            <h3 class="panel-title">Datos:</h3>
            </div>
            <div class="panel-body">
            <div class="DatosProyecto">
                <asp:GridView ID="gv_activosJyC" 
                    CssClass="table table-responsive table-striped" runat="server" BackColor="White" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                    ForeColor="Black" GridLines="Vertical" Font-Size="Small" 
                    onselectedindexchanged="gv_activosJyC_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#99CC00" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>

            </div>
                <asp:Button ID="bt_excel" class="btn btn-success" runat="server" Text="Excel" 
                    onclick="bt_excel_Click" />
            </div>
        </div>
    </div>
</div>



</div>


</asp:Content>
