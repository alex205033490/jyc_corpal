<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ConsultasCallCenter.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ConsultasCallCenter"  EnableEventValidation = "false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ConsultasCallCenter.css" rel="stylesheet" type="text/css" />
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
        .style3
        {
            height: 26px;
            width: 111px;
        }
        .style4
        {
            width: 111px;
            height: 30px;
        }
        .style5
        {
            height: 30px;
            width: 18px;
        }
        .style13
        {
            height: 30px;
            width: 77px;
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
        .style18
        {
            width: 18px;
            height: 29px;
        }
        .style20
        {
            width: 49px;
            height: 29px;
        }
        .style21
        {
            width: 43px;
            height: 29px;
        }
        .style22
        {
            width: 26px;
            height: 29px;
        }
        .style26
        {
            height: 30px;
            width: 10px;
        }
        .style27
        {
            height: 26px;
            width: 10px;
        }
        .style28
        {
            height: 35px;
            width: 10px;
        }
        .style29
        {
            width: 111px;
            height: 35px;
        }
        .style33
        {
            height: 30px;
            width: 4px;
        }
        .style34
        {
            height: 26px;
            width: 4px;
        }
        .style35
        {
            height: 35px;
            width: 4px;
        }
        .style36
        {
            height: 30px;
            width: 160px;
        }
        .style37
        {
            height: 26px;
            width: 160px;
        }
        .style38
        {
            height: 35px;
            width: 160px;
        }
        .style39
        {
            height: 26px;
            width: 187px;
        }
        .style41
        {
            height: 30px;
            width: 187px;
        }
        .style42
        {
            height: 30px;
            width: 103px;
        }
        .style43
        {
            height: 26px;
            width: 103px;
        }
        .style45
        {
            height: 30px;
            width: 91px;
        }
        .style46
        {
            height: 26px;
            width: 91px;
        }
        .style47
        {
            height: 35px;
        }
        .style48
        {
            height: 35px;
            width: 187px;
        }
        .style49
        {
            width: 187px;
        }
        .style50
        {
            width: 111px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul>
        <li></li>
    </ul>
<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
    </div>

     
    <div class="Centrar">
    <div class="titulo"><h3>
        <asp:Label ID="lb_titulo" runat="server" Text="Label"></asp:Label></h3></div>
    

    
    <table>
    <tr>
    <td></td><td>
        <asp:Label ID="Label1" runat="server" Text="Desde:" Font-Size="Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_fechaDesde" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaDesde_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaDesde">
            </asp:CalendarExtender>
        </td><td></td><td></td><td>
        <asp:Label ID="Label2" runat="server" Text="Hasta:" Font-Size="Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_fechahasta" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechahasta_CalendarExtender" runat="server" 
                TargetControlID="tx_fechahasta">
            </asp:CalendarExtender>
        </td><td>
            <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
                onclick="bt_buscar_Click" style="height: 26px" />
        </td>
    </tr>   
    </table>

    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label4" runat="server" Font-Size="Small" Text="Edificio:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_edificio" runat="server" Width="300px"></asp:TextBox>
        <asp:AutoCompleteExtender ID="tx_edificio_AutoCompleteExtender" runat="server"  
                    TargetControlID="tx_edificio"
                    Enabled="True" BehaviorID="AutoCompleteEx" CompletionInterval="200"
                    ServicePath="" servicemethod="GetlistaProyectos"
                    minimumprefixlength="2"  DelimiterCharacters="" enablecaching="true"                     
                    completionsetcount="30" 
                    ShowOnlyCurrentWordInCompletionListItem="True" 
                    CompletionListCssClass="CompletionList" 
            CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem"
                    >
        </asp:AutoCompleteExtender>
        </td>
    <td>&nbsp;</td>
    <td></td>
    </tr>    
    </table>

    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label3" runat="server" Text="Consulta :" Font-Size="Small"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:DropDownList ID="dd_consulta" runat="server" Height="25px" Width="310px">
        </asp:DropDownList>
        </td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    </tr>
    </table>

    
    <div class="consultaCallCenter">
        <asp:GridView ID="gv_consulta" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
            Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
            onrowdatabound="gv_consulta_RowDataBound" CellSpacing="2">
            <AlternatingRowStyle BackColor="#CCCCCC" />
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

    <div class="blanco">
        <asp:LinkButton ID="linkb_excel" runat="server" onclick="linkb_excel_Click" >Excel</asp:LinkButton>
    </div>

    </div>




</asp:Content>
