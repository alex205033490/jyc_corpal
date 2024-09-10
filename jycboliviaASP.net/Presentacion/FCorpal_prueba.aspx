<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_prueba.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_prueba" Async="true"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_SimecModificar.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="padding-top: 1em;">
   


<div class="row">
    <div class="col-md-10 col-md-offset-1">
        <div class="panel panel-success class">

















<!------------------------          API GET OBTENER PRODUCTO            ------------------------------>
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
<!------------------------          API GET INVENTARIO INGRESOS            ------------------------------>
            <div class="panel-body">
                <br>
                <asp:Label runat="server" Text="GET - CONSULTA INVENTARIO INGRESOS" ID="ctl10"></asp:Label><br>
                <asp:TextBox ID="tx_Ntransaccion1" runat="server"></asp:TextBox><asp:Button runat="server" Text="Buscar" OnClick="Unnamed2_Click"></asp:Button>&nbsp;
                <asp:GridView ID="gv_Inventario" runat="server">
                  
                </asp:GridView>
            </div>
            <br>

<!------------------------          API POST INVENTARIO INGRESOS            ------------------------------>
            <div class="panel-body">&nbsp;
                <label>POST - INVENTARIO INGRESOS</label>
            <br><br><asp:TextBox ID="TextBox1" runat="server" placeholder="Numero Ingreso"></asp:TextBox>
                
                <asp:TextBox ID="TextBox3" runat="server" placeholder="Referencia"></asp:TextBox>
                <asp:TextBox ID="TextBox4" runat="server" placeholder="Codigo Moneda"></asp:TextBox>
                <asp:TextBox ID="TextBox5" runat="server" placeholder="Codigo Almacen"></asp:TextBox>
                <asp:TextBox ID="TextBox6" runat="server" placeholder="Motivo Movimiento"></asp:TextBox>
                <asp:TextBox ID="TextBox7" runat="server" placeholder="Item Analisis"></asp:TextBox>
                <asp:TextBox ID="TextBox8" runat="server" placeholder="Glosa"></asp:TextBox><br>
                <label>Detalle Productos</label> <br>
                <asp:TextBox ID="TextBox9" runat="server" placeholder="Item"></asp:TextBox>
                <asp:TextBox ID="TextBox10" runat="server" placeholder="Codigo Producto"></asp:TextBox>
                <asp:TextBox ID="TextBox11" runat="server" placeholder="Unidad Medida"></asp:TextBox>
                <asp:TextBox ID="TextBox12" runat="server" placeholder="Cantidad"></asp:TextBox>
                <asp:TextBox ID="TextBox13" runat="server" placeholder="Costo Unitario"></asp:TextBox><br>
                
                <asp:Button ID="Button_Post" runat="server" Text="Insertar datos" OnClick="Button_Post_Click" />
            
                
        </div>
            <br><br>

<!------------------------          API GET INVENTARIO INGRESOS DETALLES            ------------------------------>
            <div class="panel-body">&nbsp;  
                <label> GET - INVENTARIO INGRESOS DETALLE</label><br><br>
                <asp:TextBox ID="txt_buscarInventario" runat="server"></asp:TextBox>  
                <asp:Button ID="Btn_buscarInventario" runat="server" Text="BuscarRegistro" OnClick="Btn_buscarInventario_Click" /><br>
                <br>
        <div>
<asp:GridView ID="gvInventarioIngresos" runat="server" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="NumeroIngreso" HeaderText="NumeroIngreso" />
        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
        <asp:BoundField DataField="Referencia" HeaderText="Referencia" />
        <asp:BoundField DataField="CodigoMoneda" HeaderText="CodigoMoneda" />
        <asp:BoundField DataField="CodigoAlmacen" HeaderText="CodigoAlmacen" />
        <asp:BoundField DataField="MotivoMovimiento" HeaderText="MotivoMovimiento" />
        <asp:BoundField DataField="ItemAnalisis" HeaderText="ItemAnalisis" />
        <asp:BoundField DataField="Glosa" HeaderText="Glosa" />
        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
    </Columns>
</asp:GridView>



<asp:GridView ID="gvDetalleProductos" runat="server" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="Item" HeaderText="Item" />
        <asp:BoundField DataField="CodigoProducto" HeaderText="Código Producto" />
        <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" />
        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
        <asp:BoundField DataField="CostoUnitario" HeaderText="Costo Unitario" />
        <asp:BoundField DataField="CostoTotal" HeaderText="Costo Total" />
    </Columns>
</asp:GridView>


        </div>
            </div>

     </div>
    </div>
</div>
        <div class="row">&nbsp;</div>

</div>

</asp:Content>
