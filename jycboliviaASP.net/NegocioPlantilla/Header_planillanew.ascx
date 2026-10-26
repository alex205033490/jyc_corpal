<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header_planillanew.ascx.cs" Inherits="jycboliviaASP.net.NegocioPlantilla.Header_planillanew" %>

         <!-- ======= Header ======= -->
 <header id="header" class="header fixed-top d-flex align-items-center">

   <div class="d-flex align-items-center justify-content-between">
     <a href="../Presentacion/index.aspx" class="logo d-flex align-items-center">
       <img src="../NegocioPlantilla/assets/img/naxsnax.png" alt="">
       <span class="d-none d-lg-block">NaxSnax</span>
     </a>
     <i class="bi bi-list toggle-sidebar-btn"></i>
   </div><!-- End Logo -->

     <!--
   <div class="search-bar">
     <form class="search-form d-flex align-items-center" method="POST" action="#">
       <input type="text" name="query" placeholder="Search" title="Enter search keyword">
       <button type="submit" title="Search"><i class="bi bi-search"></i></button>
     </form>
   </div> End Search Bar -->



   <nav class="header-nav ms-auto">
     <ul class="d-flex align-items-center">

       <li class="nav-item d-block d-lg-none">
         <a class="nav-link nav-icon search-bar-toggle " href="#">
           <i class="bi bi-search"></i>
         </a>
       </li><!-- End Search Icon-->

    
 <li class="nav-item dropdown me-4">

    <a class="nav-link d-flex align-items-center" href="#" data-bs-toggle="dropdown" >

        <i class="bi bi-currency-exchange fs-5 me-1 text-success" style="color:#39FF14;"></i>

        <span class="fw-bold" style="color:#39FF14;">
            TC:
            <asp:Label ID="lblTipoCambio" runat="server" Text="0.00"></asp:Label>
        </span>

    </a>

    <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">

        <li class="dropdown-header">
            Tipo de Cambio
        </li>

        <li><hr class="dropdown-divider" /></li>

        <li class="px-3 py-2">
            <strong>Oficial:</strong>
            <asp:Label ID="lblOficial" runat="server"></asp:Label>
        </li>

        <li class="px-3 py-2">
            <strong>Blue:</strong>
            <asp:Label ID="lblBlue" runat="server"></asp:Label>
        </li>

        <li class="px-3 py-2">
            <strong>UFV:</strong>
            <asp:Label ID="lblUFV" runat="server"></asp:Label>
        </li>

        <li class="px-3 py-2 text-muted">
            Actualizado:
            <asp:Label ID="lblFechaTC" runat="server"></asp:Label>
        </li>

        <li><hr class="dropdown-divider" /></li>

<li class="px-3 py-2 text-center">
    <asp:LinkButton 
        ID="btnActualizarTC" 
        runat="server" 
        CssClass="btn btn-sm btn-primary w-100"
        PostBackUrl="~/Presentacion/FComercial_TipoCambioDolar.aspx">
        Actualizar Tipo de Cambio
    </asp:LinkButton>
</li>

    </ul>

</li>

       <li class="nav-item">
    <div class="vr mx-2"></div>
</li>
 

       <li class="nav-item dropdown pe-3">

         <a class="nav-link nav-profile d-flex align-items-center pe-0" href="#" data-bs-toggle="dropdown">
           <img src="../NegocioPlantilla/assets/img/naxsnax.png" alt="Profile" class="rounded-circle">
           <span class="d-none d-md-block dropdown-toggle ps-2">Corpal</span>
         </a><!-- End Profile Iamge Icon -->

         <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow profile">
           <li class="dropdown-header">
             <h6>NaxSnax</h6>
             <span>Web Designer</span>
           </li>
           <li>
             <hr class="dropdown-divider">
           </li>

           <li>
             <a class="dropdown-item d-flex align-items-center" href="users-profile.html">
               <i class="bi bi-person"></i>
               <span>My Profile</span>
             </a>
           </li>
           <li>
             <hr class="dropdown-divider">
           </li>

           <li>
             <a class="dropdown-item d-flex align-items-center" href="users-profile.html">
               <i class="bi bi-gear"></i>
               <span>Account Settings</span>
             </a>
           </li>
           <li>
             <hr class="dropdown-divider">
           </li>

           <li>
             <a class="dropdown-item d-flex align-items-center" href="pages-faq.html">
               <i class="bi bi-question-circle"></i>
               <span>Need Help?</span>
             </a>
           </li>
           <li>
             <hr class="dropdown-divider">
           </li>

           <li>
             <a class="dropdown-item d-flex align-items-center" href="#">
               <i class="bi bi-box-arrow-right"></i>
               <span>Sign Out</span>
             </a>
           </li>

         </ul><!-- End Profile Dropdown Items -->
       </li><!-- End Profile Nav -->

     </ul>
   </nav><!-- End Icons Navigation -->

 </header><!-- End Header -->

