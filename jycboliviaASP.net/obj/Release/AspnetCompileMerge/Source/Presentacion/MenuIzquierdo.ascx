<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuIzquierdo.ascx.cs" Inherits="jycboliviaASP.net.Presentacion.MenuIzquierdo" %>

<table>
<tr>
<td>
        <div id='cssmenuV'>
        <ul>
           <li class='active'><a href='FA_CallCenter_EventoNuevo.aspx' id="mn_eventonuevo" runat="server"><span>Evento Nuevo</span></a></li>
           <li class='active'><a href='FA_CallCenter_EventoCotiRepuesto.aspx' id="mn_cotiRinCallcenter" runat="server"><span>Evento CotiRepuesto</span></a></li> 
           <li><a href='FA_CallCenter_Eventos.aspx' id="mn_eventos" runat="server"><span>Eventos</span></a></li>   
           <li><a href='FA_CallCenter_EventoRIN.aspx' id="mn_eventosRin" runat="server"><span>Eventos RIN</span></a></li>
           <li><a href='FA_CallCenter_EventoRCC.aspx' id="mn_eventosRCC" runat="server"><span>Eventos RCC</span></a></li>
           <li><a href='FA_CallCenterEstadisticaNormal.aspx' id="mn_eventoestadisticaNormal" runat="server"><span>Estadistica Normal</span></a></li>
           <li><a href='FA_ConsultasCallCenter.aspx' id="mn_consultaCallcenter" runat="server"><span>Reportes CallCenter</span></a></li>           
           <li><a href='FA_ConsultaCallCenterReporte.aspx' id="A1" runat="server"><span>Reportes CallCenter 2</span></a></li>
           <li><a href='FA_DeudaPlanPago.aspx' id="mn_deudaPlanPago" runat="server"><span>Banderas</span></a></li>
           <li><a href='FA_CallCenterAreaCliente.aspx' id="mn_areaCliente" runat="server"><span>Area Cliente</span></a></li>
           <li><a href='FA_CallCenter_AreaCotiRepuesto.aspx' id="mn_areaCotiRepuesto" runat="server"><span>Area Coti Repuesto</span></a></li>
        </ul>
        </div>

</td>
</tr>

<tr>
<td>
        <div id='eventoAbiertos'>
            <asp:GridView ID="gv_eventoAbierto" runat="server" BackColor="White" 
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                Font-Size="Small" ForeColor="Black" GridLines="Vertical">
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
</td>
</tr>

</table>
