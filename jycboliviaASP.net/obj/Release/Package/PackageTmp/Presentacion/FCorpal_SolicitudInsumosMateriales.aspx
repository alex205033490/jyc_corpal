<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_SolicitudInsumosMateriales.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_SolicitudInsumosMateriales" %>
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
            width: 9px;
        }
    
        </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <div class="container" style="padding-top: 1em;">
   


<div class="row">
    <div class="col-md-10 col-md-offset-1">
        <div class="panel panel-success class">
            <div class="panel-heading">
            <h3 class="panel-title">Compra Materia Prima:</h3>
            </div>
            <div class="panel-body">
            
        <table>
        <tr>
        <td>
            <asp:Label ID="Label36" runat="server" Text="Nro:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_nro" class="form-control" runat="server" 
                Width="100px" ></asp:TextBox>
            </td>
        

           
            <td>
                <asp:Label ID="Label32" runat="server" Text="Fecha Estimada Entrega:" 
                    Font-Size="Small"></asp:Label>
            </td>
            
            <td>
                <asp:TextBox ID="tx_fechaestimadaEntrega" class="form-control" runat="server" 
                    Width="150px"></asp:TextBox>
                <asp:CalendarExtender ID="tx_fechaestimadaEntrega_CalendarExtender" 
                    runat="server" TargetControlID="tx_fechaestimadaEntrega">
                </asp:CalendarExtender>
            </td>            
        </tr>
       
    </table>
       
       <table>   
             
   
        <tr>
        <td>
            <asp:Label ID="Label30" runat="server" Font-Size="Small" 
                Text="Solicitante:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_solicitante" class="form-control" runat="server" 
                Font-Size="Small" Width="400px"></asp:TextBox>           
            </td>                    
            <td class="style1">&nbsp;</td>   
            <td>
                <asp:Button ID="bt_guardarSolicitud" class="btn btn-success" runat="server" 
                    Text="Guardar Solicitud" onclick="bt_guardarSolicitud_Click" />
            </td>
        </tr>       
     
    </table>  
    
            </div>
        </div>

        <div class="panel panel-success class">  
        <div class="panel-heading">
            <h3 class="panel-title">Materia Prima e Insumos:</h3>
            </div>
                     
            <div class="panel-body">
            
       <table>   
       
        <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Font-Size="Small" 
                Text="Provoeedor:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_proveedor" class="form-control" runat="server" 
                Font-Size="Small" Width="400px"></asp:TextBox> 
            </td>                    
            <td>&nbsp;</td>   
            <td>&nbsp;</td>
        </tr>       
        <tr>
            <td>
                <asp:Label ID="Label37" runat="server" Text="Insumo:" Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_descripcion" class="form-control" runat="server" 
                    Width="400px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
     
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Cantidad:" 
                    Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_cantidad" class="form-control" runat="server" 
                    Width="100px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Medida:" 
                    Font-Size="Small"></asp:Label>
            </td>
            
            <td>
                <asp:TextBox ID="tx_unidad" class="form-control" runat="server" 
                    Width="100px"></asp:TextBox>
            </td>            
            <td>
                &nbsp;</td>
            <td></td>
            <td>
                &nbsp;</td>
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
                    Text="Adicionar" onclick="bt_insertar_Click" />
            </td>
            <td>
                &nbsp;</td>

            <td>
                &nbsp;</td>

            <td>
                &nbsp;</td>
            
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
                

                <asp:GridView ID="gv_MaterialSolicitado" 
                    runat="server" BackColor="White" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                    Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
                    onrowdeleting="gv_MaterialSolicitado_RowDeleting">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
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
            </div>
        </div>
    </div>
</div>


</div>
</asp:Content>
