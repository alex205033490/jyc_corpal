<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sidebar_Plantillanew.ascx.cs" Inherits="jycboliviaASP.net.NegocioPlantilla.Sidebar_Plantillanew" %>

  <!-- ======= Sidebar ======= -->
  <aside id="sidebar" class="sidebar">

    <ul class="sidebar-nav" id="sidebar-nav">

      <li class="nav-item">
        <a class="nav-link " href="../Presentacion/index.aspx">
          <i class="bi bi-grid"></i>
          <span>Dashboard</span>
        </a>
      </li><!-- End Dashboard Nav -->

        <li class="nav-item">
          <a class="nav-link collapsed" href="../Presentacion/FA_Login.aspx">
            <i class="bi bi-box-arrow-in-right"></i>
            <span>Login</span>
          </a>
        </li><!-- End Login Page Nav -->


         <li></li>         
         <li></li>       
         <li></li>       
         <li></li>       


      <li class="nav-item">
        <a class="nav-link collapsed" data-bs-target="#components-nav" data-bs-toggle="collapse" href="#">
          <i class="bi bi-gear"></i><span>Configuracion</span><i class="bi bi-chevron-down ms-auto"></i>
        </a>
        <ul id="components-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
          <li><a href="../Presentacion/FCorpal_GestonarResponsable.aspx" id="mn_gestionarResponsable" runat="server" ><i class="bi bi-circle"></i><span>Gestionar Responsable</span></a></li>
          <li><a href="../Presentacion/FCorpal_GestionarPermisos.aspx" id="mn_gestionarPermisos" runat="server" ><i class="bi bi-circle"></i><span>Gestionar Permisos</span></a></li>
          <li><a href="../Presentacion/FCorpal_GestionarFormularios.aspx" id="mn_gestionarFormularios" runat="server" ><i class="bi bi-circle"></i><span>Gestionar Formularios</span></a></li>          
        </ul>
      </li><!-- End Components Nav -->

      <li class="nav-item">
        <a class="nav-link collapsed" data-bs-target="#tienda-nav" data-bs-toggle="collapse" href="#">
          <i class="bi bi-journal-text"></i><span>Tienda</span><i class="bi bi-chevron-down ms-auto"></i>
        </a>
        <ul id="tienda-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
            <li><a href="../Presentacion/FCorpal_GestionarTiendaPropietario.aspx" id="mn_gestionarTiendaPropietario" runat="server" ><i class="bi bi-circle"></i><span>Gestionar Tienda</span></a></li>       
            <li><a href="../Presentacion/FCorpal_RutaEntrega.aspx" id="mn_rutaEntrega" runat="server" ><i class="bi bi-circle"></i><span>Gestionar Ruta Entrega</span></a></li>       
            <li><a href="../Presentacion/FCorpal_ReciboIngreso.aspx" id="mn_regiboIngreso" runat="server" ><i class="bi bi-circle"></i><span>Recibo Ingreso</span></a></li>          
            <li><a href="../Presentacion/FCorpal_ReciboEgreso.aspx" id="mn_regiboEgreso" runat="server" ><i class="bi bi-circle"></i><span>Recibo Egreso</span></a></li>          
            <li><a href="../Presentacion/FCorpal_ConsultaIngresoEgreso.aspx" id="mn_consultaIngresoEgreso" runat="server" ><i class="bi bi-circle"></i><span>Consulta Recibos (Ingreso/Egreso)</span></a></li>          
        </ul>
      </li><!-- End Forms Nav -->

        <li class="nav-item">
          <a class="nav-link collapsed" data-bs-target="#producto-nav" data-bs-toggle="collapse" href="#">
            <i class="bi bi-journal-text"></i><span>Productos</span><i class="bi bi-chevron-down ms-auto"></i>
          </a>
          <ul id="producto-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
                <li><a href="../Presentacion/FCorpal_SolicitudPedido.aspx" id="mn_solicitudproductos" runat="server"><i class="bi bi-circle"></i><span>Solicitud Productos</span></a></li>                                             
                <li><a href="../Presentacion/FCorpal_EntregaSolicitudProducto.aspx" id="mn_entregaSolicitudProducto" runat="server"><i class="bi bi-circle"></i><span>Entrega Solicitud Productos</span></a></li>                                                         
                <li role="separator" class="divider"></li>            
                <li><a href="../Presentacion/FCorpal_ConsultaProducto_SolicitudEntrega.aspx" id="mn_detallesolicitudproductos" runat="server"><i class="bi bi-circle"></i><span>Consulta Productos</span></a></li>                                                         
                <li role="separator" class="divider"></li>                                
                <li><a href="../Presentacion/FCorpal_DevoluciondeProductoTerminado.aspx" id="mn_devolucionProductoTerminado" runat="server"><i class="bi bi-circle"></i><span>Devolucion Productos</span></a></li>                                                 
                <li><a href="../Presentacion/FCorpal_AprobacionDevolucionProductoTerminado.aspx" id="mn_AprobaciondevolucionProductoTerminado" runat="server"><i class="bi bi-circle"></i><span>Aprobacion Devolucion Productos</span></a></li>                                                 
          </ul>
        </li><!-- End Forms Nav -->

        <li class="nav-item">
          <a class="nav-link collapsed" data-bs-target="#produccion-nav" data-bs-toggle="collapse" href="#">
            <i class="bi bi-layout-text-window-reverse"></i><span>Produccion</span><i class="bi bi-chevron-down ms-auto"></i>
          </a>
          <ul id="produccion-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">                                      
            <li><a href="../Presentacion/FCorpal_OrdenProduccion.aspx" id="mn_ordendeProduccion" runat="server"><i class="bi bi-circle"></i><span>Orden Produccion</span></a></li>    
            <li role="separator" class="divider"></li> 
            <li><a href="../Presentacion/FCorpal_EntregaProduccion.aspx" id="mn_entregaProduccion" runat="server"><i class="bi bi-circle"></i><span>Entrega Produccion</span></a></li>                                             
            <li><a href="../Presentacion/FCorpal_ObjetivoVentasProduccion.aspx" id="mn_objetivoProduccion" runat="server"><i class="bi bi-circle"></i><span>Objetivo Ventas Produccion</span></a></li>                                             
            <li><a href="../Presentacion/FCorpal_ObjetivoVentasProduccionMensual.aspx" id="mn_objetivoProduccionMensual" runat="server"><i class="bi bi-circle"></i><span>Objetivo Mensual Ventas Produccion</span></a></li>                                             
            <li><a href="../Presentacion/FCorpal_ConsutaProduccion.aspx" id="mn_ConsutaProduccion" runat="server"><i class="bi bi-circle"></i><span>Consulta Produccion</span></a></li>
                          
            <li role="separator" class="divider"></li>                                
          </ul>
        </li><!-- End Tables Nav -->

        <li class="nav-item">
          <a class="nav-link collapsed" data-bs-target="#insumos-nav" data-bs-toggle="collapse" href="#">
            <i class="bi bi-layout-text-window-reverse"></i><span>Recetas e Insumos</span><i class="bi bi-chevron-down ms-auto"></i>
          </a>
          <ul id="insumos-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
              <li><a href="../Presentacion/FCorpal_GestionarInsumos.aspx" id="mn_Insumos" runat="server"><i class="bi bi-circle"></i><span>Gestionar Insumo</span></a></li>
              <li><a href="../Presentacion/FCorpal_AgregarInsumoCreado.aspx" id="mn_InsumosCompuesto" runat="server"><i class="bi bi-circle"></i><span>Gestionar Insumo Compuesto</span></a></li>
              <li><a href="../Presentacion/FCorpal_Recetas.aspx" id="mn_recetas" runat="server"><i class="bi bi-circle"></i><span>Gestionar Receta</span></a></li>    
              <li><a href="../Presentacion/FCorpal_SolicitudInsumosMateriales.aspx" id="mn_solicitudMaterial" runat="server"><i class="bi bi-circle"></i><span>Solicitud Material</span></a></li>                                                         
              <li><a href="../Presentacion/FCorpal_CompraDeMaterialeInsumos.aspx" id="mn_compraMaterial" runat="server"><i class="bi bi-circle"></i><span>Compra Material</span></a></li>                                                         
              <li><a href="../Presentacion/FCorpal_RecibioMaterialInsumos.aspx" id="mn_MaterialRecibido" runat="server"><i class="bi bi-circle"></i><span>Material e Insumos Recibidos</span></a></li>
              <li><a href="../Presentacion/FCorpal_ConsultaReceta.aspx" id="mn_consultaRecetaInsumo" runat="server"><i class="bi bi-circle"></i><span>Consulta</span></a></li>
              
              <li role="separator" class="divider"></li>            
          </ul>
        </li><!-- End Tables Nav -->

        <li class="nav-item">
          <a class="nav-link collapsed" data-bs-target="#bancarizacion-nav" data-bs-toggle="collapse" href="#">
            <i class="bi bi-layout-text-window-reverse"></i><span>Bancarizacion</span><i class="bi bi-chevron-down ms-auto"></i>
          </a>
          <ul id="bancarizacion-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
                <li><a href="../Presentacion/FCorpal_ConciliacionBancaria.aspx" id="mn_conciliacionBancaria" runat="server"><i class="bi bi-circle"></i><span>Conciliacion Bancaria</span></a></li>                                             
                <li><a href="../Presentacion/FCorpal_movimientoCheques.aspx" id="mn_movimientoCheques" runat="server"><i class="bi bi-circle"></i><span>Movimiento Cheques</span></a></li>                                             
                <li><a href="../Presentacion/FCorpal_saldosCuentasGeneral.aspx" id="mn_saldosCuentasGeneral" runat="server"><i class="bi bi-circle"></i><span>Vista Saldos General</span></a></li>                                             
                <li><a href="../Presentacion/FCorpal_Facturacion.aspx" id="mn_facturacion" runat="server"><i class="bi bi-circle"></i><span>Recopilacion de Bancarizacion</span></a></li>                                             
          </ul>
        </li><!-- End Tables Nav -->


        <li class="nav-heading">SGI</li>
        <li class="nav-item">
          <a class="nav-link collapsed" data-bs-target="#sgi-nav" data-bs-toggle="collapse" href="#">
            <i class="bi bi-layout-text-window-reverse"></i><span>SGI</span><i class="bi bi-chevron-down ms-auto"></i>
          </a>
            <ul id="sgi-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
                <li><a href="../Presentacion/FCorpal_AgendaTelefonica.aspx" id="mn_agenda" runat="server"><i class="bi bi-circle"></i><span>Agenda Telefonica</span></a></li>                               
                <li><a href="../Presentacion/FCorpal_GestionArchivosSGI.aspx" id="mn_SGI" runat="server"><i class="bi bi-circle"></i><span>Gestion Archivos SGI</span></a></li>                                          
                <li><a href="../Presentacion/FCorpal_SGI_Actividades.aspx" id="mn_actividades" runat="server"><i class="bi bi-circle"></i><span>Actividades</span></a></li>                               
                <li><a href="../Presentacion/FCorpal_ConsultaSGI.aspx" id="mn_consultaActividades" runat="server"><i class="bi bi-circle"></i><span>Consulta SGI Actividades</span></a></li>                               
                <li><a href="../Presentacion/FCorpal_agendanegociacion.aspx" id="mn_agendaTrabajo" runat="server"><i class="bi bi-circle"></i><span>Agenda de Trabajo</span></a></li>                                   
                <li><a href="../Presentacion/FCorpal_ActivosJYC.aspx" id="mn_activosjyc" runat="server"><i class="bi bi-circle"></i><span>Activos JYC</span></a></li>                       
            </ul>
        </li><!-- End Tables Nav -->

    
      <li class="nav-heading"> APIs</li>          
      <li class="nav-item">
      <a class="nav-link collapsed" data-bs-target="#uponweb-nav" data-bs-toggle="collapse" href="#">
        <i class="bi bi-layout-text-window-reverse"></i><span>Upon Web</span><i class="bi bi-chevron-down ms-auto"></i>
      </a>
        <ul id="uponweb-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
        
        <li><a href="../Presentacion/FCorpal_VaciadoUponPedido.aspx" id="mn_vaciadoUponPedido" runat="server"><i class="bi bi-circle"></i><span>Vaciado Upon Pedido</span></a></li>                                                 
        <li><a href="../Presentacion/FCorpal_VaciadoUponVenta.aspx" id="mn_vaciadoUponVenta" runat="server"><i class="bi bi-circle"></i><span>Vaciado Upon Venta</span></a></li>                                                             
        <li><a href="../Presentacion/FUpon_CargaCompras.aspx" id="mn_CargaExcelComprasUpon" runat="server"><i class="bi bi-circle"></i><span>Carga Excel Compras Upon</span></a></li>                                                 
        <li><a href="../Presentacion/FUpon_CargaVentas.aspx" id="mn_CargaExcelVentasUpon" runat="server"><i class="bi bi-circle"></i><span>Carga Excel Ventas Upon</span></a></li>                                                 
        </ul>
        </li>  


      <li class="nav-item">
      <a class="nav-link collapsed" data-bs-target="#prueba-nav" data-bs-toggle="collapse" href="#">
        <i class="bi bi-layout-text-window-reverse"></i><span>Clientes Upon</span><i class="bi bi-chevron-down ms-auto"></i>
      </a>
        <ul id="prueba-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
        <li><a href="../Presentacion/FCorpal_APIClientes.aspx" id="mn_apiclientes" runat="server" ><i class="bi bi-circle"></i><span>Cliente/Proveedor Upon</span></a></li>
        </ul>
        </li>


        <li class="nav-item">
        <a class="nav-link collapsed" data-bs-target="#productosapi-nav" data-bs-toggle="collapse" href="#">
            <i class="bi bi-layout-text-window-reverse"></i><span>Productos Upon</span><i class="bi bi-chevron-down ms-auto"></i>
        </a>
        <ul id="productosapi-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
            <li><a href="../Presentacion/FCorpal_APIProductos.aspx" id="mn_apiproductos" runat="server" ><i class="bi bi-circle"></i><span>Productos Upon</span></a></li>
        </ul>
        </li>

      <li class="nav-item">
      <a class="nav-link collapsed" data-bs-target="#inventario-nav" data-bs-toggle="collapse" href="#">
        <i class="bi bi-layout-text-window-reverse"></i><span>Inventario Upon</span><i class="bi bi-chevron-down ms-auto"></i>
      </a>

        <ul id="inventario-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
        <li><a href="../Presentacion/FCorpal_APIInventarioIngresos.aspx" id="mn_apiinventariosingresos" runat="server" ><i class="bi bi-circle"></i><span>Inventario Ingresos Upon</span></a></li>
        <li><a href="../Presentacion/FCorpal_APIInventarioEgresos.aspx" id="mn_apiinventariosegresos" runat="server" ><i class="bi bi-circle"></i><span>Inventario Egresos Upon</span></a></li>
        <li><a href="../Presentacion/FCorpal_APIInventarioTraspasos.aspx" id="mn_apiinventariostraspasos" runat="server" ><i class="bi bi-circle"></i><span>Inventario Traspasos  Upon</span></a></li>
        </ul>
        </li>

        <li class="nav-item">
        <a class="nav-link collapsed" data-bs-target="#APIproduccion-nav" data-bs-toggle="collapse" href="#">
          <i class="bi bi-layout-text-window-reverse"></i><span>Producción Upon</span><i class="bi bi-chevron-down ms-auto"></i>
        </a>
          <ul id="APIproduccion-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
          <li><a href="../Presentacion/FCorpal_APIProduccion.aspx" id="mn_APIProduccion" runat="server" ><i class="bi bi-circle"></i><span>Producción Upon</span></a></li>
          </ul>
          </li>


        <li class="nav-item">
        <a class="nav-link collapsed" data-bs-target="#APIpedidoVent-nav" data-bs-toggle="collapse" href="#">
          <i class="bi bi-layout-text-window-reverse"></i><span>Pedido/Venta Upon</span><i class="bi bi-chevron-down ms-auto"></i>
        </a>
          <ul id="APIpedidoVent-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
          <li><a href="../Presentacion/FCorpal_APIPedido.aspx" id="A2" runat="server" ><i class="bi bi-circle"></i><span>Pedido Upon</span></a></li>
          <li><a href="../Presentacion/FCorpal_APIVentas.aspx" id="A4" runat="server" ><i class="bi bi-circle"></i><span>Ventas Upon</span></a></li>
          </ul>
          </li>


        <li class="nav-item">
        <a class="nav-link collapsed" data-bs-target="#APICuentas-nav" data-bs-toggle="collapse" href="#">
          <i class="bi bi-layout-text-window-reverse"></i><span>Cuentas Upon</span><i class="bi bi-chevron-down ms-auto"></i>
        </a>
          <ul id="APICuentas-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
          <li><a href="../Presentacion/FCorpal_APICuentasCobranza.aspx" id="A3" runat="server" ><i class="bi bi-circle"></i><span>Cuentas/Cobranza Upon</span></a></li>
          </ul>
          </li>


        <li class="nav-item">
        <a class="nav-link collapsed" data-bs-target="#APICompras-nav" data-bs-toggle="collapse" href="#">
          <i class="bi bi-layout-text-window-reverse"></i><span>Compras Upon</span><i class="bi bi-chevron-down ms-auto"></i>
        </a>
          <ul id="APICompras-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
            <li><a href="../Presentacion/FCorpal_APICompras.aspx" id="A1" runat="server" ><i class="bi bi-circle"></i><span>Compras Upon</span></a></li>
          </ul>
            
          </li>
        

        

    </ul>

  </aside><!-- End Sidebar-->
