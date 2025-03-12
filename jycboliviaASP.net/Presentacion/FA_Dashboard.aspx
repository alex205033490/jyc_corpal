<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FA_Dashboard.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main  class="main">

    <div class="pagetitle">
      <h1>Dashboard</h1>
      <nav>
        <ol class="breadcrumb">
          <li class="breadcrumb-item"><a href="index.html">Home</a></li>
          <li class="breadcrumb-item active">Dashboard</li>
        </ol>
      </nav>
    </div><!-- End Page Title -->

    <section class="section dashboard">
      <div class="row">

        <!-- Left side columns -->
        <div class="col-lg-8">
          <div class="row">

            <!-- Sales Card -->
            <div class="col-xxl-4 col-md-6">
              <div class="card info-card sales-card">
                              
                <div class="card-body">
                  <h5 class="card-title">Objetivos Ventas </h5>

                  <div class="d-flex align-items-center">
                    <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                      <i class="bi bi-cash-coin"></i>
                    </div>
                    <div class="ps-3">
                      <h6>
                          <asp:Label ID="lb_objVentasAnual" runat="server" Text="0"></asp:Label>
                          </h6>
                      <span class="text-success small pt-1 fw-bold">12%</span> <span class="text-muted small pt-2 ps-1">increase</span>

                    </div>
                  </div>
                </div>

              </div>
            </div><!-- End Sales Card -->

               <!-- Sales Card -->
 <div class="col-xxl-4 col-md-6">
   <div class="card info-card sales-card">
                   
     <div class="card-body">
       <h5 class="card-title">Orden Produccion</h5>

       <div class="d-flex align-items-center">
         <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
           <i class="bi bi-cash-stack"></i>
         </div>
         <div class="ps-3">
           <h6>
               <asp:Label ID="lb_totalOrdenProduccion" runat="server" Text="0"></asp:Label>
               </h6>
           <span class="text-success small pt-1 fw-bold">12%</span> <span class="text-muted small pt-2 ps-1">increase</span>

         </div>
       </div>
     </div>

   </div>
 </div><!-- End Sales Card -->

            <!-- Revenue Card -->
            <div class="col-xxl-4 col-md-6">
              <div class="card info-card revenue-card">
                           
                <div class="card-body">
                  <h5 class="card-title">Ventas </h5>
                  <div class="d-flex align-items-center">
                    <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                      <i class="bi bi-currency-dollar"></i>
                    </div>
                    <div class="ps-3">
                      <h6>
                          <asp:Label ID="lb_totalSalidaAlmacen" runat="server" Text="0"></asp:Label>
                        </h6>
                      <span class="text-success small pt-1 fw-bold">8%</span> <span class="text-muted small pt-2 ps-1">increase</span>

                    </div>
                  </div>
                </div>

              </div>
            </div><!-- End Revenue Card -->
                          
            <!-- Reports -->
            <div class="col-12">
              <div class="card">

                <div class="card-body">
                  <h5 class="card-title">Reporte </h5>

                  <!-- Line Chart -->
                  <div id="reportsChart"></div>

                  <script>
                      let chart; // Variable global para el gráfico
                      document.addEventListener("DOMContentLoaded", async () => {
                          // Inicializamos el gráfico
                          initializeChart();
                          // Configuramos un intervalo para actualizar el gráfico cada 5 segundos
                          setInterval(updateChartData, 5000); // 5000 ms = 5 segundos
                      });

                      async function updateChartData() {
                          try {
                              // Hacer la solicitud al WebMethod en C#
                              const response = await fetch("FA_Dashboard.aspx/GetChartData", {
                                  method: "POST",
                                  headers: { "Content-Type": "application/json" },
                                  body: JSON.stringify({})
                              });

                              const jsonResponse = await response.json();
                              const data = JSON.parse(jsonResponse.d); // `d` contiene los datos reales

                              // Actualizar los datos del gráfico
                              chart.updateOptions({
                                  series: [
                                      { name: 'Objetivo', data: data.Sales },
                                      { name: 'Ventas', data: data.Revenue },
                                      { name: 'Produccion', data: data.Customers }
                                  ],
                                  xaxis: {
                                      categories: data.Categories // Actualizar los meses
                                  }
                              });

                          } catch (error) {
                              console.error("Error al obtener los datos:", error);
                          }
                      }

                      function initializeChart() {
                          // Crear el gráfico inicialmente con datos vacíos o predeterminados
                          chart = new ApexCharts(document.querySelector("#reportsChart"), {
                              series: [
                                  { name: 'Objetivo', data: [] },
                                  { name: 'Ventas', data: [] },
                                  { name: 'Produccion', data: [] }
                              ],
                              chart: {
                                  height: 350,
                                  type: 'area',
                                  toolbar: { show: false }
                              },
                              xaxis: {
                                  type: 'category',
                                  categories: [] // De momento sin datos
                              },
                              tooltip: {
                                  x: { formatter: (value) => value }
                              },
                              colors: ['#FF0000', '#00FF00', '#0000FF'], // Colores personalizados
                              stroke: {
                                  curve: 'smooth',
                                  width: 3
                              }
                          });

                          chart.render(); // Renderizar el gráfico
                        }
                  </script>
                  <!-- End Line Chart -->

                </div>

              </div>
            </div><!-- End Reports -->
                 
          </div>
        </div><!-- End Left side columns -->

        <!-- Right side columns -->
        <div class="col-lg-4">


          <!-- Budget Report -->
          <div class="card">
            <div class="filter">
              <a class="icon" href="#" data-bs-toggle="dropdown"><i class="bi bi-three-dots"></i></a>
              <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">
                <li class="dropdown-header text-start">
                  <h6>Filter</h6>
                </li>

                <li><a class="dropdown-item" href="#">Today</a></li>
                <li><a class="dropdown-item" href="#">This Month</a></li>
                <li><a class="dropdown-item" href="#">This Year</a></li>
              </ul>
            </div>

            <div class="card-body pb-0">
              <h5 class="card-title">Budget Report <span>| This Month</span></h5>

              <div id="budgetChart" style="min-height: 400px;" class="echart"></div>

              <script>
                document.addEventListener("DOMContentLoaded", () => {
                  var budgetChart = echarts.init(document.querySelector("#budgetChart")).setOption({
                    legend: {
                      data: ['Allocated Budget', 'Actual Spending']
                    },
                    radar: {
                      // shape: 'circle',
                      indicator: [{
                          name: 'Sales',
                          max: 6500
                        },
                        {
                          name: 'Administration',
                          max: 16000
                        },
                        {
                          name: 'Information Technology',
                          max: 30000
                        },
                        {
                          name: 'Customer Support',
                          max: 38000
                        },
                        {
                          name: 'Development',
                          max: 52000
                        },
                        {
                          name: 'Marketing',
                          max: 25000
                        }
                      ]
                    },
                    series: [{
                      name: 'Budget vs spending',
                      type: 'radar',
                      data: [{
                          value: [4200, 3000, 20000, 35000, 50000, 18000],
                          name: 'Allocated Budget'
                        },
                        {
                          value: [5000, 14000, 28000, 26000, 42000, 21000],
                          name: 'Actual Spending'
                        }
                      ]
                    }]
                  });
                });
              </script>

            </div>
          </div><!-- End Budget Report -->

          <!-- Website Traffic -->
          <div class="card">
            <div class="filter">
              <a class="icon" href="#" data-bs-toggle="dropdown"><i class="bi bi-three-dots"></i></a>
              <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">
                <li class="dropdown-header text-start">
                  <h6>Filter</h6>
                </li>

                <li><a class="dropdown-item" href="#">Today</a></li>
                <li><a class="dropdown-item" href="#">This Month</a></li>
                <li><a class="dropdown-item" href="#">This Year</a></li>
              </ul>
            </div>

            <div class="card-body pb-0">
              <h5 class="card-title">Website Traffic <span>| Today</span></h5>

              <div id="trafficChart" style="min-height: 400px;" class="echart"></div>

              <script>
                document.addEventListener("DOMContentLoaded", () => {
                  echarts.init(document.querySelector("#trafficChart")).setOption({
                    tooltip: {
                      trigger: 'item'
                    },
                    legend: {
                      top: '5%',
                      left: 'center'
                    },
                    series: [{
                      name: 'Access From',
                      type: 'pie',
                      radius: ['40%', '70%'],
                      avoidLabelOverlap: false,
                      label: {
                        show: false,
                        position: 'center'
                      },
                      emphasis: {
                        label: {
                          show: true,
                          fontSize: '18',
                          fontWeight: 'bold'
                        }
                      },
                      labelLine: {
                        show: false
                      },
                      data: [{
                          value: 1048,
                          name: 'Search Engine'
                        },
                        {
                          value: 735,
                          name: 'Direct'
                        },
                        {
                          value: 580,
                          name: 'Email'
                        },
                        {
                          value: 484,
                          name: 'Union Ads'
                        },
                        {
                          value: 300,
                          name: 'Video Ads'
                        }
                      ]
                    }]
                  });
                });
              </script>

            </div>
          </div><!-- End Website Traffic -->

      
        </div><!-- End Right side columns -->

      </div>
    </section>

  </main><!-- End #main -->

</asp:Content>
