<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlUser.ascx.cs" Inherits="jycboliviaASP.net.Presentacion.ControlUser" %>


<div id='Div1'>
<nav class="navbar navbar-default"  role="navigation">
  <div class="container-fluid">     
  <!-- Brand and toggle get grouped for better mobile display -->   
   <div class="navbar-header">
      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
        <span class="sr-only"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
      <a class="navbar-brand" href="#"></a>
    </div>
  <!-- Collect the nav links, forms, and other content for toggling -->
  <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
   <ul class="nav navbar-nav">	     
        <li class="nav-item">
          <a class="nav-link active" aria-current="page" href="../Presentacion/FA_Login.aspx"><span>Inicio </span></a>
        </li>

	<li class="dropdown">
		<a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">	
        Configuracion</a>	
		<ul class="dropdown-menu">
            <li><a href="../Presentacion/FCorpal_GestonarResponsable.aspx" id="mn_gestionarResponsable" runat="server" ><span>Gestionar Responsable</span></a></li>         
            <li><a href="../Presentacion/FCorpal_GestionarPermisos.aspx" id="mn_gestionarPermisos" runat="server" ><span>Gestionar Permisos</span></a></li>       
            <li><a href="../Presentacion/FCorpal_GestionarFormularios.aspx" id="mn_gestionarFormularios" runat="server" ><span>Gestionar Formularios</span></a></li>       
		 <li role="separator" class="divider"></li>
		</ul>
	</li>
	
	<li class="dropdown">
	<a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">	
    Tienda</a>
    <ul class="dropdown-menu">
    <li role="separator" class="divider"></li>
	   <li><a href="../Presentacion/FCorpal_GestionarTiendaPropietario.aspx" id="mn_gestionarTiendaPropietario" runat="server" ><span>Gestionar Tienda</span></a></li>       
       <li><a href="../Presentacion/FCorpal_RutaEntrega.aspx" id="mn_rutaEntrega" runat="server" ><span>Gestionar Ruta Entrega</span></a></li>       
       <li><a href="../Presentacion/FCorpal_ReciboIngreso.aspx" id="mn_regiboIngreso" runat="server" ><span>Recibo Ingreso</span></a></li>          
       <li><a href="../Presentacion/FCorpal_ReciboEgreso.aspx" id="mn_regiboEgreso" runat="server" ><span>Recibo Egreso</span></a></li>          
       <li><a href="../Presentacion/FCorpal_ConsultaIngresoEgreso.aspx" id="mn_consultaIngresoEgreso" runat="server" ><span>Consulta Recibos (Ingreso/Egreso)</span></a></li>          
        
    </ul>
    </li>	

     <li class="dropdown">
	<a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">	
    Productos 	</a>
        <ul class="dropdown-menu">
           <!-- <li><a href="../Presentacion/FCorpal_GestionarTiendaPropietario.aspx" id="A1" runat="server"><span>G Tienda Propietario</span></a></li>                                             
            <li><a href="../Presentacion/FCorpal_RutaEntrega.aspx" id="A2" runat="server"><span>Ruta Entrega</span></a></li>    -->                                         
            <li><a href="../Presentacion/FCorpal_SolicitudPedido.aspx" id="mn_solicitudproductos" runat="server"><span>Solicitud Productos</span></a></li>                                             
            <li><a href="../Presentacion/FCorpal_EntregaSolicitudProducto.aspx" id="mn_entregaSolicitudProducto" runat="server"><span>Entrega Solicitud Productos</span></a></li>                                             
            <li><a href="../Presentacion/FCorpal_ConsultaProducto_SolicitudEntrega.aspx" id="mn_detallesolicitudproductos" runat="server"><span>Consulta Solicitud Productos</span></a></li>                                             
            
        </ul>
    </li>


    <li class="dropdown">
	<a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">	
    Bancarizacion 	</a>
        <ul class="dropdown-menu">
            <li><a href="../Presentacion/FCorpal_ConciliacionBancaria.aspx" id="mn_conciliacionBancaria" runat="server"><span>Conciliacion Bancaria</span></a></li>                                             
            <li><a href="../Presentacion/FCorpal_movimientoCheques.aspx" id="mn_movimientoCheques" runat="server"><span>Movimiento Cheques</span></a></li>                                             
            <li><a href="../Presentacion/FCorpal_saldosCuentasGeneral.aspx" id="mn_saldosCuentasGeneral" runat="server"><span>Vista Saldos General</span></a></li>                                             
            <li><a href="../Presentacion/FCorpal_Facturacion.aspx" id="mn_facturacion" runat="server"><span>Recopilacion de Bancarizacion</span></a></li>                                             
        </ul>
    </li>

   
     <li class="dropdown">
	<a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">	SGI 	</a>
        <ul class="dropdown-menu">
            <li><a href="../Presentacion/FCorpal_AgendaTelefonica.aspx" id="mn_agenda" runat="server"><span>Agenda Telefonica</span></a></li>                               
            <li><a href="../Presentacion/FCorpal_GestionArchivosSGI.aspx" id="mn_SGI" runat="server"><span>Gestion Archivos SGI</span></a></li>                                          
            <li><a href="../Presentacion/FCorpal_SGI_Actividades.aspx" id="mn_actividades" runat="server"><span>Actividades</span></a></li>                               
            <li><a href="../Presentacion/FCorpal_agendanegociacion.aspx" id="mn_agendaTrabajo" runat="server"><span>Agenda de Trabajo</span></a></li>                       
        </ul>
    </li>
     
    </div>
  </div>
</nav>
</div> 	

