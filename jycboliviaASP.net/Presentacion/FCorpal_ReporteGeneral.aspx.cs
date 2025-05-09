using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using jycboliviaASP.net.Datos;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ReporteGeneral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                mostrarReporteGeneral();
            }
        }

        private void mostrarReporteGeneral()
        {
            string reporte = Session["ReporteGeneral"].ToString();
            switch (reporte)
            {
                case "Reporte_QRActivos":
                    qr_activos();
                    break;
                case "Reporte_Entrega_Produccion":
                    entregaProduccion();
                    break;
                case "Reporte_AsignacionProductoCamion":
                    AsignacionProductoCamion();
                    break;
                case "Reporte_ProductoCamionEntrega":
                    ReporteProductoCamionEntrega();
                    break;
                case "Reporte_SolicitudMaterialInsumos":
                    reporteSolicitudMaterialInsumos();
                    break;
                case "Reporte_CompraMaterialInsumos":
                    reporteCompraMaterialInsumos();
                    break;
                case "Reporte_RecibidoMaterialInsumos":
                    reporteRecibidoMaterialInsumos();
                    break;
                case "CalcularInsumosPorTurnoDia":
                    CalcularInsumosPorTurnoDia();
                    break;
                case "Reporte_DespachoProductoCamionEntrega":
                    ReporteDespachoProductoCamionEntrega();
                    break;
                case "Report_DespachoBoletasProdEntrega":
                    ReportDespachoBoletasProdEntrega();
                    break;

                default:
                    //number = "Error";
                    break;
            }
        }

        private void ReportDespachoBoletasProdEntrega()
        {
            

            int codigoDespacho;
            int.TryParse(Session["codigoDespacho"].ToString(), out codigoDespacho);

            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            DataSet datoResult = negocio.get_DespachoBoletasProdEntrega(codigoDespacho);
            DataTable DS_EntregaProductosCamiones = datoResult.Tables[0];
            ReportDataSource DSEntregaProductosCamiones = new ReportDataSource("DS_BoletasProductosEntrega_porDespacho", DS_EntregaProductosCamiones);

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_DespachoBoletasProdEntrega"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      
            ReportViewer1.LocalReport.DataSources.Add(DSEntregaProductosCamiones);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void ReporteDespachoProductoCamionEntrega()
        {
            int codigoDespacho;
            int.TryParse(Session["codigoDespacho"].ToString(), out codigoDespacho);

            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            DataSet datoResult = negocio.get_DespachoProductoaCamion(codigoDespacho);
            DataTable DS_EntregaProductosCamiones = datoResult.Tables[0];
            ReportDataSource DSEntregaProductosCamiones = new ReportDataSource("DS_DespachoProdVehiculo", DS_EntregaProductosCamiones);

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_DespachoProductosCamion"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      
            ReportViewer1.LocalReport.DataSources.Add(DSEntregaProductosCamiones);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void ReporteProductoCamionEntrega()
        {
            int codigoCamion;  
            int.TryParse(Session["codigoCamion"].ToString(), out codigoCamion);
            
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();            
            DataSet datoResult = negocio.get_EntregasProductoaCamion(codigoCamion);
            DataTable DS_EntregaProductosCamiones = datoResult.Tables[0];
            ReportDataSource DSEntregaProductosCamiones = new ReportDataSource("DS_EntregaProductosCamiones", DS_EntregaProductosCamiones);

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_EntregaProductosCamion"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      
            ReportViewer1.LocalReport.DataSources.Add(DSEntregaProductosCamiones);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void AsignacionProductoCamion()
        {
            
            int codigoCamion = int.Parse(Session["codigoCamion"].ToString());
            NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();
            DataSet datoResult = negocio.get_AsignacionProductoaCamion(codigoCamion);
            DataTable DSasignacionProductoCamion = datoResult.Tables[0];            
            ReportDataSource DS_AsignacionProductoCamiones = new ReportDataSource("DS_AsignacionProductoCamiones", DSasignacionProductoCamion);

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_AsignacionProductosCamion"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      
            ReportViewer1.LocalReport.DataSources.Add(DS_AsignacionProductoCamiones);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void CalcularInsumosPorTurnoDia()
        {
            int codigoOrdenProduccion = int.Parse(Session["codigoOrdenProduccion"].ToString());
            NCorpal_Produccion npro = new NCorpal_Produccion();
            DataSet datoGeneral = npro.get_datosOrdenProduccion(codigoOrdenProduccion);

            string fecha = datoGeneral.Tables[0].Rows[0][1].ToString();
            string hora = datoGeneral.Tables[0].Rows[0][2].ToString();
            string producto = datoGeneral.Tables[0].Rows[0][3].ToString();
            decimal cantcajas;
            decimal.TryParse(datoGeneral.Tables[0].Rows[0][4].ToString().Replace('.', ','), out cantcajas);
            string medida = datoGeneral.Tables[0].Rows[0][5].ToString();
            int codNax;
            int.TryParse(datoGeneral.Tables[0].Rows[0][11].ToString().Replace('.', ','), out codNax);

            decimal cantTurnoDia;
            decimal.TryParse(datoGeneral.Tables[0].Rows[0][8].ToString().Replace(".", ","), out cantTurnoDia);
            decimal cantTurnoTarde;
            decimal.TryParse(datoGeneral.Tables[0].Rows[0][9].ToString().Replace(".", ","), out cantTurnoTarde);
            decimal cantTurnoNoche;
            decimal.TryParse(datoGeneral.Tables[0].Rows[0][10].ToString().Replace(".", ","), out cantTurnoNoche);
            decimal canttotal = cantTurnoDia + cantTurnoTarde + cantTurnoNoche;
                        
            DataSet tuplaDia = npro.get_insumosporProductoNormal(codNax, producto, cantTurnoDia);
            DataSet tuplaTarde = npro.get_insumosporProductoNormal(codNax, producto, cantTurnoTarde);
            DataSet tuplaNoche = npro.get_insumosporProductoNormal(codNax, producto, cantTurnoNoche);
            DataSet tuplaTOTAL = npro.get_insumosporProductoNormal(codNax, producto, canttotal);

            DataSet tuplaDiaCreado = npro.get_insumosCreadosporProducto(codNax, cantTurnoDia);
            DataSet tuplaTardeCreado = npro.get_insumosCreadosporProducto(codNax, cantTurnoTarde);
            DataSet tuplaNocheCreado = npro.get_insumosCreadosporProducto(codNax, cantTurnoNoche);
            DataSet tuplaCreadoTOTAL = npro.get_insumosCreadosporProducto(codNax, canttotal);

            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_ConsultaListadeInsumosTurnos.rdlc";

            DataTable DS_dia = tuplaDia.Tables[0];
            DataTable DS_tarde = tuplaTarde.Tables[0];
            DataTable DS_noche = tuplaNoche.Tables[0];
            DataTable DS_TOTAL = tuplaTOTAL.Tables[0];

            DataTable DS_diaCreadp = tuplaDiaCreado.Tables[0];
            DataTable DS_tardeCreado = tuplaTardeCreado.Tables[0];
            DataTable DS_nocheCreado = tuplaNocheCreado.Tables[0];
            DataTable DS_CreadoTOTAL = tuplaCreadoTOTAL.Tables[0];

            ReportParameter p_fechaproduccion = new ReportParameter("p_fechaproduccion", fecha);
            ReportParameter p_horaproduccion = new ReportParameter("p_horaproduccion", hora);
            ReportParameter p_producto = new ReportParameter("p_producto", producto);
            ReportParameter p_medida = new ReportParameter("p_medida", medida);
            ReportParameter p_cantDia = new ReportParameter("p_cantDia", cantTurnoDia.ToString().Replace(',', '.'));
            ReportParameter p_canttarde = new ReportParameter("p_canttarde", cantTurnoTarde.ToString().Replace(',', '.'));
            ReportParameter p_cantnoche = new ReportParameter("p_cantnoche", cantTurnoNoche.ToString().Replace(',', '.'));

            ReportParameter p_canttotal = new ReportParameter("p_canttotal", canttotal.ToString().Replace(',', '.'));

            ReportDataSource DS_ListaInsumosTurnoDia = new ReportDataSource("DS_ListaInsumosTurnoDia", DS_dia);
            ReportDataSource DS_ListaInsumosTurnoTarde = new ReportDataSource("DS_ListaInsumosTurnoTarde", DS_tarde);
            ReportDataSource DS_ListaInsumosTurnoNoche = new ReportDataSource("DS_ListaInsumosTurnoNoche", DS_noche);
            ReportDataSource DS_ListaInsumosTurnoTOTAL = new ReportDataSource("DS_ListaInsumosTurnoTotal", DS_TOTAL);

            ReportDataSource DS_ListaInsumosCreadosDia = new ReportDataSource("DS_ListaInsumosCreadosDia", DS_diaCreadp);
            ReportDataSource DS_ListaInsumosCreadosTarde = new ReportDataSource("DS_ListaInsumosCreadosTarde", DS_tardeCreado);
            ReportDataSource DS_ListaInsumosCreadosNoche = new ReportDataSource("DS_ListaInsumosCreadosNoche", DS_nocheCreado);
            ReportDataSource DS_ListaInsumosCreadosTOTAL = new ReportDataSource("DS_ListaInsumosCreadosTotal", DS_CreadoTOTAL);

            ReportViewer1.LocalReport.SetParameters(p_fechaproduccion);
            ReportViewer1.LocalReport.SetParameters(p_horaproduccion);
            ReportViewer1.LocalReport.SetParameters(p_producto);
            ReportViewer1.LocalReport.SetParameters(p_medida);
            ReportViewer1.LocalReport.SetParameters(p_cantDia);
            ReportViewer1.LocalReport.SetParameters(p_canttarde);
            ReportViewer1.LocalReport.SetParameters(p_cantnoche);
            ReportViewer1.LocalReport.SetParameters(p_canttotal);

            ReportViewer1.LocalReport.DataSources.Add(DS_ListaInsumosTurnoDia);
            ReportViewer1.LocalReport.DataSources.Add(DS_ListaInsumosTurnoTarde);
            ReportViewer1.LocalReport.DataSources.Add(DS_ListaInsumosTurnoNoche);
            ReportViewer1.LocalReport.DataSources.Add(DS_ListaInsumosTurnoTOTAL);

            ReportViewer1.LocalReport.DataSources.Add(DS_ListaInsumosCreadosDia);
            ReportViewer1.LocalReport.DataSources.Add(DS_ListaInsumosCreadosTarde);
            ReportViewer1.LocalReport.DataSources.Add(DS_ListaInsumosCreadosNoche);
            ReportViewer1.LocalReport.DataSources.Add(DS_ListaInsumosCreadosTOTAL);

            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();

            /*-------------------------------------------*/
            bool envioCorreoOK = Convert.ToBoolean(Session["EnvioCorreoCalculoInsumo"].ToString());
            if (envioCorreoOK == true) {
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                string NombreUsuario = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                byte[] bytes = ReportViewer1.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string rutaGuardar = ConfigurationManager.AppSettings["Guardar_RecetaInsumo"];
                if (!Directory.Exists(rutaGuardar))
                    Directory.CreateDirectory(rutaGuardar);

                string nombreArchivo = "Report_RecetaInsumo_Produccion_Nro_" + codigoOrdenProduccion;
                string direccionGuardar = rutaGuardar + nombreArchivo;

                using (FileStream fs = new FileStream(direccionGuardar + "." + extension, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

                NA_EnvioCorreo ncorreo = new NA_EnvioCorreo();
                string Asunto = "(Corpal) Orden de Produccion Nro." + codigoOrdenProduccion;
                string CuerpoMensaje = "Se ha creado una Orden de Produccion con numero " + codigoOrdenProduccion + "<br>" +
                               "Del producto " + producto + ", <br>" +
                               "Realizado por el Usuario " + NombreUsuario;
                bool banderaOk = ncorreo.Enviar_CorreoRecetaInsumo(Asunto, CuerpoMensaje, rutaGuardar, nombreArchivo);
                if (banderaOk==true) {
                    Session["EnvioCorreoCalculoInsumo"] = false;
                }
            }
        }
    

        private void qr_activos()
        {
            string baseDatos = Session["BaseDatos"].ToString();
            NA_ActivosJyC aa = new NA_ActivosJyC();
            string ruta = ConfigurationManager.AppSettings["qr_codeActivo"] + Session["BaseDatos"].ToString() + "/";
            DataSet tuplas = aa.get_direccionQRActivos(ruta);

            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_QRActivos.rdlc";
            DataTable DSconsulta = tuplas.Tables[0];

            string rutaLogo = ConfigurationManager.AppSettings["image_logo"];
            string nombreImagen = "jyc";


            if (baseDatos.Equals("La Paz"))
            {
                nombreImagen = "elevamerica";
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    nombreImagen = "melevar";
                }
                else
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        nombreImagen = "interlogy";
                    }

            string direccionImagen = rutaLogo + nombreImagen;

            ReportParameter imagen = new ReportParameter("p_logo", @"file:\" + direccionImagen + ".jpg");

            ReportDataSource DS_Activos_QR = new ReportDataSource("DS_Activos", DSconsulta);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.SetParameters(imagen);
            ReportViewer1.LocalReport.DataSources.Add(DS_Activos_QR);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void reporteRecibidoMaterialInsumos()
        {
            int codigoSMI = int.Parse(Session["codigoRecibidoMaterialeInsumos"].ToString());
            NCorpal_PedidoMaterialeInsumos npp = new NCorpal_PedidoMaterialeInsumos();
            DataSet datoResult = npp.get_DatosSolicitudMaterialeInsumos(codigoSMI);
            
            string codigo = datoResult.Tables[0].Rows[0][0].ToString();
            string fechaSolicitud = datoResult.Tables[0].Rows[0][1].ToString();
            string fechaEstimadaEntrega = datoResult.Tables[0].Rows[0][2].ToString();
            string PersonalSolicitud = datoResult.Tables[0].Rows[0][3].ToString();
            string PersonalCompra = datoResult.Tables[0].Rows[0][6].ToString();


            ReportParameter p_numero = new ReportParameter("p_numero", "Nro. " + codigo.ToString());
            ReportParameter p_solicitante = new ReportParameter("p_solicitante", PersonalSolicitud);
            ReportParameter p_fechasolicitud = new ReportParameter("p_fechasolicitud", fechaSolicitud);
            ReportParameter p_fechaestimadaentrega = new ReportParameter("p_fechaestimadaentrega", fechaEstimadaEntrega);
            ReportParameter p_personalcomprado = new ReportParameter("p_personalcomprado", PersonalCompra);
            

            DataSet tuplasFilas = npp.get_todosItemInsumosComprados(codigoSMI);
            DataTable DSMaterialeInsumosSolicitados = tuplasFilas.Tables[0];
            ReportDataSource DSRecibidoMaterialInsumos = new ReportDataSource("DSRecibidoMaterialInsumos", DSMaterialeInsumosSolicitados);

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_RecibidoMaterialeInsumos"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_numero);
            ReportViewer1.LocalReport.SetParameters(p_fechasolicitud);
            ReportViewer1.LocalReport.SetParameters(p_solicitante);
            ReportViewer1.LocalReport.SetParameters(p_fechaestimadaentrega);
            ReportViewer1.LocalReport.SetParameters(p_personalcomprado);
            ReportViewer1.LocalReport.DataSources.Add(DSRecibidoMaterialInsumos);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void reporteCompraMaterialInsumos()
        {
            int codigoSMI = int.Parse(Session["codigoCompraMaterialeInsumos"].ToString());
            NCorpal_PedidoMaterialeInsumos npp = new NCorpal_PedidoMaterialeInsumos();
            DataSet datoResult = npp.get_DatosSolicitudMaterialeInsumos(codigoSMI);

            string ciudad = Session["BaseDatos"].ToString();

            string codigo = datoResult.Tables[0].Rows[0][0].ToString();
            string fechaSolicitud = datoResult.Tables[0].Rows[0][1].ToString();
            string fechaEstimadaEntrega = datoResult.Tables[0].Rows[0][2].ToString();
            string PersonalSolicitud = datoResult.Tables[0].Rows[0][3].ToString();


            ReportParameter p_numero = new ReportParameter("p_numero", "Nro. " + codigo.ToString());
            ReportParameter p_solicitante = new ReportParameter("p_solicitante", PersonalSolicitud);
            ReportParameter p_fechasolicitud = new ReportParameter("p_fechasolicitud", fechaSolicitud);
            ReportParameter p_fechaestimadaentrega = new ReportParameter("p_fechaestimadaentrega", fechaEstimadaEntrega);

            DataSet tuplasFilas = npp.get_todosItemInsumosPedidos(codigoSMI);
            DataTable DSMaterialeInsumosSolicitados = tuplasFilas.Tables[0];
            ReportDataSource DSCompraMaterialeInsumos = new ReportDataSource("DSCompraMaterialeInsumos", DSMaterialeInsumosSolicitados);

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_CompradoMaterialeInsumos"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_numero);
            ReportViewer1.LocalReport.SetParameters(p_fechasolicitud);
            ReportViewer1.LocalReport.SetParameters(p_solicitante);
            ReportViewer1.LocalReport.SetParameters(p_fechaestimadaentrega);
            ReportViewer1.LocalReport.DataSources.Add(DSCompraMaterialeInsumos);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void reporteSolicitudMaterialInsumos()
        {
            int codigoSMI = int.Parse(Session["codigoSolicitudMaterialeInsumos"].ToString());
            NCorpal_PedidoMaterialeInsumos npp = new NCorpal_PedidoMaterialeInsumos();
            DataSet datoResult = npp.get_DatosSolicitudMaterialeInsumos(codigoSMI);

            string ciudad = Session["BaseDatos"].ToString();

            string codigo = datoResult.Tables[0].Rows[0][0].ToString();
            string fechaSolicitud = datoResult.Tables[0].Rows[0][1].ToString();
            string fechaEstimadaEntrega = datoResult.Tables[0].Rows[0][2].ToString();
            string PersonalSolicitud = datoResult.Tables[0].Rows[0][3].ToString();


            ReportParameter p_numero = new ReportParameter("p_numero", "Nro. "+codigo.ToString());
            ReportParameter p_solicitante = new ReportParameter("p_solicitante", PersonalSolicitud);
            ReportParameter p_fechasolicitud = new ReportParameter("p_fechasolicitud", fechaSolicitud);
            ReportParameter p_fechaestimadaentrega = new ReportParameter("p_fechaestimadaentrega", fechaEstimadaEntrega);

            DataSet tuplasFilas = npp.get_todosItemInsumosPedidos(codigoSMI);
            DataTable DSMaterialeInsumosSolicitados = tuplasFilas.Tables[0];
            ReportDataSource DS_MaterialeInsumosSolicitados = new ReportDataSource("DSMaterialeInsumosSolicitados", DSMaterialeInsumosSolicitados);

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_SolicitudMaterialeInsumos"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_numero);
            ReportViewer1.LocalReport.SetParameters(p_fechasolicitud);
            ReportViewer1.LocalReport.SetParameters(p_solicitante);
            ReportViewer1.LocalReport.SetParameters(p_fechaestimadaentrega);
            ReportViewer1.LocalReport.DataSources.Add(DS_MaterialeInsumosSolicitados);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void entregaProduccion()
        {
            int codigoEntregaProduccion = int.Parse(Session["codigoEntregaProduccion"].ToString());
            NCorpal_Produccion npro = new NCorpal_Produccion();
            DataSet datoResult = npro.get_DatosEntregaProduccion(codigoEntregaProduccion);

            string ciudad = Session["BaseDatos"].ToString();


            string codigo = datoResult.Tables[0].Rows[0][0].ToString();
            string fecha = datoResult.Tables[0].Rows[0][1].ToString();
            string hora = datoResult.Tables[0].Rows[0][2].ToString();
            string turno = datoResult.Tables[0].Rows[0][3].ToString();
            string resp_entrega = datoResult.Tables[0].Rows[0][4].ToString();
            string resp_recepcion = datoResult.Tables[0].Rows[0][5].ToString();
            string nroorden = datoResult.Tables[0].Rows[0][6].ToString();
            string productoNax = datoResult.Tables[0].Rows[0][7].ToString();
            string cantcajas = datoResult.Tables[0].Rows[0][8].ToString();

            N_numLetra converN = new N_numLetra();
            string cantcajasLetras = "";
            cantcajasLetras = converN.Convertir(cantcajas, false, "Cajas");

            string unidadsuelta = datoResult.Tables[0].Rows[0][9].ToString();
            string kgrdesperdicio = datoResult.Tables[0].Rows[0][10].ToString();
            string kgrparamix = datoResult.Tables[0].Rows[0][11].ToString();
            string codresprecepcion = datoResult.Tables[0].Rows[0][12].ToString();
            string medidaentregada = datoResult.Tables[0].Rows[0][13].ToString();
            string kgrdesperdicio_conaceite = datoResult.Tables[0].Rows[0][14].ToString();            
            string kgrdesperdicio_sinaceite = datoResult.Tables[0].Rows[0][15].ToString();
            string pack_ferial = datoResult.Tables[0].Rows[0][16].ToString();


            ReportParameter p_nroorden = new ReportParameter("p_nroorden", codigo);
            ReportParameter p_fecha = new ReportParameter("p_fecha", fecha);
            ReportParameter p_hora = new ReportParameter("p_hora", hora);
            ReportParameter p_turno = new ReportParameter("p_turno", turno);

            ReportParameter p_entregaproduccion = new ReportParameter("p_entregaproduccion", resp_entrega);
            ReportParameter p_recepcionproduccion = new ReportParameter("p_recepcionproduccion", resp_recepcion);
            ReportParameter p_produccionnax = new ReportParameter("p_produccionnax", productoNax);
            ReportParameter p_cantcajas = new ReportParameter("p_cantcajas", cantcajas);
            ReportParameter p_unidadsuelta = new ReportParameter("p_unidadsuelta", unidadsuelta);
            ReportParameter p_kgrparamix = new ReportParameter("p_kgrparamix", kgrparamix);
            ReportParameter p_kgrdesperdicio = new ReportParameter("p_kgrdesperdicio", kgrdesperdicio);

            ReportParameter p_medidaentregada = new ReportParameter("p_medidaCajas", medidaentregada);
            ReportParameter p_kgrdesperdicio_conaceite = new ReportParameter("p_kgrDesperdicioConAceite", kgrdesperdicio_conaceite);
            ReportParameter p_kgrdesperdicio_sinaceite = new ReportParameter("p_kgrDesperdicioSinAceite", kgrdesperdicio_sinaceite);
            ReportParameter p_pack_ferial = new ReportParameter("p_PackFerial", pack_ferial);

            ReportParameter p_cantcajasLetras = new ReportParameter("p_cantcajasLetras", cantcajasLetras);

            //ReportParameter p_edificio = new ReportParameter("p_edificio", HttpUtility.HtmlDecode(datoResult.Tables[0].Rows[0][3].ToString()));
            /*
            string ruta = ConfigurationManager.AppSettings["image_logo"];
            string nombreImagen = "jyc";
            string baseDatos = Session["BaseDatos"].ToString();

            if (baseDatos.Equals("La Paz"))
            {
                nombreImagen = "elevamerica";
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    nombreImagen = "melevar";
                }
                else
                    if (baseDatos.Equals("Santa Cruz") || baseDatos.Equals("Beni") || baseDatos.Equals("Pando") || baseDatos.Equals("Yacuiba"))
                    {
                        nombreImagen = "interlogy";
                    }

            string direccionImagen = ruta + nombreImagen;

            ReportParameter imagen = new ReportParameter("p_logo", @"file:\" + direccionImagen + ".jpg");
            //  ReportParameter imagen = new ReportParameter("p_logo", "d:/temp/alex.jpg");
            */

            string rutaReciboIngreso = ConfigurationManager.AppSettings["repo_ReciboEntregaProduccion"];

            ReportViewer1.LocalReport.ReportPath = rutaReciboIngreso;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_nroorden);
            ReportViewer1.LocalReport.SetParameters(p_fecha);
            ReportViewer1.LocalReport.SetParameters(p_hora);
            ReportViewer1.LocalReport.SetParameters(p_turno);
            ReportViewer1.LocalReport.SetParameters(p_entregaproduccion);
            ReportViewer1.LocalReport.SetParameters(p_recepcionproduccion);
            ReportViewer1.LocalReport.SetParameters(p_produccionnax);

            ReportViewer1.LocalReport.SetParameters(p_cantcajas);
            ReportViewer1.LocalReport.SetParameters(p_unidadsuelta);
            ReportViewer1.LocalReport.SetParameters(p_kgrparamix);
            ReportViewer1.LocalReport.SetParameters(p_kgrdesperdicio);

            ReportViewer1.LocalReport.SetParameters(p_medidaentregada);
            ReportViewer1.LocalReport.SetParameters(p_kgrdesperdicio_conaceite);
            ReportViewer1.LocalReport.SetParameters(p_kgrdesperdicio_sinaceite);
            ReportViewer1.LocalReport.SetParameters(p_pack_ferial);

            ReportViewer1.LocalReport.SetParameters(p_cantcajasLetras);

            ReportViewer1.LocalReport.Refresh();

            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
            /*
                        Warning[] warnings;
                        string[] streamIds;
                        string mimeType = string.Empty;
                        string encoding = string.Empty;
                        // string encoding = System.Text.Encoding.Default.ToString();
                        string extension = string.Empty;

                        byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                        //  byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.          
                        // System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        //----------------------- enviar al cliente el archivo -----------------------
                        /* Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("content-disposition", "attachment; filename= filename" + "." + extension);
                        Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
                        Response.Flush(); // send it to the client to download  
                        Response.End();*/
            //------------------------------------------------------------------------------

            /*     string rutaGuardarR144 = ConfigurationManager.AppSettings["guardar_r144"];
                 if (!Directory.Exists(rutaGuardarR144))
                     Directory.CreateDirectory(rutaGuardarR144);

                 int codigoCoti = Convert.ToInt32(Session["codcotiRepuesto"].ToString());
                 string Edificio = Session["EdificioRepuesto"].ToString();
                 string nombreArchivo = "R-144_coti" + codigoCoti + "_" + Edificio;
                 string direccionGuardarR144 = rutaGuardarR144 + nombreArchivo;

                 using (FileStream fs = new FileStream(@direccionGuardarR144 + "." + extension, FileMode.Create))
                 {
                     fs.Write(bytes, 0, bytes.Length);
                 }
                 */
        }
    }
}