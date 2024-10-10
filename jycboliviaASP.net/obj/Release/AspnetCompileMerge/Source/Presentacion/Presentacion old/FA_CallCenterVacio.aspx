<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CallCenterVacio.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CallCenterVacio" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menuIzquierdo" Src="MenuIzquierdo.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="../Styles/Style_CallCenter.css" rel="stylesheet" type="text/css" />
    
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
        
        .style3
        {
            width: 11px;
        }
        .style5
        {
            width: 50px;
        }
        .style6
        {
            width: 237px;
        }
    </style>
  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 

 <table>
 <tr>
 <td>
     <table>
         <tr>
         <td>
            <inmoInfo:menuIzquierdo ID="MenuIzquierdo1"  runat="server"/>
           </td>
         </tr>
         
         <tr>
         <td style="height:100px;"></td>
         </tr>

           
     </table>
  </td>   
  <td>
   
 </td>
 </tr>
 </table>



</asp:Content>
