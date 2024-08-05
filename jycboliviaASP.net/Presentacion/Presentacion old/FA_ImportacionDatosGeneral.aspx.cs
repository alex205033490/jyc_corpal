using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ImportacionDatosGeneral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(87) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                Limpiar_Datos();
                Buscar_Datos("", "","","","");
                cargarddlEstadoEquipo();
                
            }
        }

        private void cargarddlEstadoEquipo()
        {
            NEstadoEquipo estadoEquipo = new NEstadoEquipo();
            dd_estado.DataSource = estadoEquipo.listar();
            dd_estado.DataValueField = "codigo";
            dd_estado.DataTextField = "nombre";
            dd_estado.Items.Add(new ListItem("", "-1"));
            dd_estado.AppendDataBoundItems = true;
            dd_estado.SelectedIndex = -1;
            dd_estado.DataBind();
        }

        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }

        public string convertidorFecha(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'" + _fecha + "'";
            }
            else
                return "null";
        }

        private void Buscar_Datos(string Edificio, string exbo, string estado, string dui, string contenedor)
        {
            //gv_datos.DataSource = null;
            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.buscar_ImportacionDatosGeneral(Edificio, exbo, estado, dui, contenedor, false);
            gv_datos.DataSource = tuplaRes;
            gv_datos.DataBind();
            lb_cantidad.Text = equipo1.cant_ImportacionDatosGeneral(Edificio, exbo, estado, dui, contenedor).ToString();
          //  OnCheckedChanged_seleccionTodo();
        }

        private void Limpiar_Datos()
        {
            tx_edificio.Text = "";
            tx_exbo.Text = "";
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string edificio = tx_edificio.Text;
            string exbo = tx_exbo.Text;
            string estado = dd_estado.SelectedItem.Text;
            string Dui = tx_dui.Text;
            string contenedor = tx_contenedor.Text;
            Buscar_Datos(edificio, exbo, estado, Dui, contenedor);
        }

        protected void bt_Limpiar_Click(object sender, EventArgs e)
        {
            Limpiar_Datos();
        }


      /*  protected void OnCheckedChanged_seleccionTodo()
        {
            //Loop through all rows in GridView
            foreach (GridViewRow row in gv_datos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    */
                  /*  row.RowState = DataControlRowState.Edit;
                    row.Cells[8].Controls.OfType<Label>().FirstOrDefault().Visible = true;
                    row.Cells[8].Controls.OfType<TextBox>().FirstOrDefault().Visible = true;
                    string estado = row.Cells[8].Controls.OfType<Label>().FirstOrDefault().Text;                    
                    row.Cells[8].Controls.OfType<DropDownList>().FirstOrDefault().Visible = true;
                    row.Cells[8].Controls.OfType<DropDownList>().FirstOrDefault().BackColor = Color.Yellow;
                    NEstadoEquipo estadoEquipo = new NEstadoEquipo();
                    DataSet resultado = estadoEquipo.listar();

                    row.Cells[8].Controls.OfType<DropDownList>().FirstOrDefault().DataValueField = "codigo";
                    row.Cells[8].Controls.OfType<DropDownList>().FirstOrDefault().DataTextField = "nombre";

                    row.Cells[8].Controls.OfType<DropDownList>().FirstOrDefault().DataSource = resultado;
                    row.Cells[8].Controls.OfType<DropDownList>().FirstOrDefault().DataBind();
                    int codigoEstado = estadoEquipo.getCodigoEstadoEquipo(estado);
                    row.Cells[8].Controls.OfType<DropDownList>().FirstOrDefault().SelectedValue = codigoEstado.ToString();
                   // row.Cells[8].Controls.OfType<Label>().FirstOrDefault().Visible = false;
                   // row.Cells[8].Controls.OfType<TextBox>().FirstOrDefault().Visible = false;
                  */
                 /*   for (int i = 16; i < row.Cells.Count; i++)
                    {
                        row.RowState = DataControlRowState.Edit;
                        row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Visible = false;
                        string monto = row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Text;
                        row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Visible = true;
                        row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().BackColor = Color.Yellow;
                        row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Text = monto;
                    }
                }
            }
        }
        */

        protected void gv_datos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            DropDownList listaDesplegable = (DropDownList)e.Row.Cells[8].Controls.OfType<DropDownList>().FirstOrDefault();
                if (listaDesplegable != null)
                {

                    listaDesplegable.DataValueField = "codigo";
                    listaDesplegable.DataTextField = "nombre";

                    NEstadoEquipo estadoEquipo = new NEstadoEquipo();
                    DataSet resultado = estadoEquipo.listar();
                    listaDesplegable.DataSource = resultado;

                    string datoR = ((Label)e.Row.Cells[8].Controls.OfType<Label>().FirstOrDefault()).Text;
                     int codigoEstado = estadoEquipo.getCodigoEstadoEquipo(datoR);
                     NEquipo nequipo1 = new NEquipo();
                     // if(nequipo1.estaPermitidoEstadoImportacion(codigoEstado)){
                     listaDesplegable.SelectedValue = codigoEstado.ToString();
                     listaDesplegable.BackColor = Color.Yellow;
                     // }

                     ((Label)e.Row.Cells[8].Controls.OfType<Label>().FirstOrDefault()).Visible = false;
                     ((TextBox)e.Row.Cells[8].Controls.OfType<TextBox>().FirstOrDefault()).Visible = false;

                     ((Label)e.Row.Cells[9].Controls.OfType<Label>().FirstOrDefault()).Visible = false;
                     ((TextBox)e.Row.Cells[9].Controls.OfType<TextBox>().FirstOrDefault()).BackColor = Color.Yellow;
                     ((Label)e.Row.Cells[10].Controls.OfType<Label>().FirstOrDefault()).Visible = false;
                     ((TextBox)e.Row.Cells[10].Controls.OfType<TextBox>().FirstOrDefault()).BackColor = Color.Yellow;

                     for (int i = 16; i < e.Row.Cells.Count; i++)
                     {
                         e.Row.RowState = DataControlRowState.Edit;
                         e.Row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Visible = false;
                         string monto = e.Row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Text;
                         e.Row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Visible = true;
                         e.Row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().BackColor = Color.Yellow;
                         e.Row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Text = monto;
                     }


                     ((Label)e.Row.Cells[17].Controls.OfType<Label>().FirstOrDefault()).Visible = true;
                     ((TextBox)e.Row.Cells[17].Controls.OfType<TextBox>().FirstOrDefault()).Visible = false;

                     ((Label)e.Row.Cells[18].Controls.OfType<Label>().FirstOrDefault()).Visible = true;
                     ((TextBox)e.Row.Cells[18].Controls.OfType<TextBox>().FirstOrDefault()).Visible = false;

                     string colorcanal = e.Row.Cells[35].Controls.OfType<Label>().FirstOrDefault().Text;
                     ((Label)e.Row.Cells[35].Controls.OfType<Label>().FirstOrDefault()).Visible = false;
                     ((TextBox)e.Row.Cells[35].Controls.OfType<TextBox>().FirstOrDefault()).Visible = false;
                     ((DropDownList)e.Row.Cells[35].Controls.OfType<DropDownList>().FirstOrDefault()).BackColor = Color.Yellow;
                     ((DropDownList)e.Row.Cells[35].Controls.OfType<DropDownList>().FirstOrDefault()).SelectedValue = colorcanal;
                    if(colorcanal.Equals("Rojo")){
                        e.Row.BackColor = Color.Red;                        
                    }else
                        if (colorcanal.Equals("Amarillo"))
                        {
                            e.Row.BackColor = Color.Yellow;
                        }else
                            if (colorcanal.Equals("Verde"))
                            {
                                e.Row.BackColor = Color.Green;
                            }


                    listaDesplegable.AppendDataBoundItems = true;
                    
                    listaDesplegable.DataBind();
                }
            

        }

        protected void bt_Update_Click(object sender, EventArgs e)
        {
            actualizarDatosgenerales();
            string edificio = tx_edificio.Text;
            string exbo = tx_exbo.Text;
            string estado = dd_estado.SelectedItem.Text;
            string Dui = tx_dui.Text;
            string contenedor = tx_contenedor.Text;
            Buscar_Datos(edificio, exbo, estado, Dui, contenedor);
        }

        private void actualizarDatosgenerales()
        {
            foreach (GridViewRow row in gv_datos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked)
                    {
                        int codigoEquipo = Convert.ToInt32(row.Cells[1].Text);
                        DropDownList listaDesplegable = (DropDownList)row.Cells[8].Controls.OfType<DropDownList>().FirstOrDefault();

                        int codigoEstado = int.Parse(listaDesplegable.SelectedValue.ToString());

                        string Ciudad = row.Cells[7].Text;
                        string fechaAprobacionlimiteplanos = convertidorFecha(row.Cells[9].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        string fechaArriboapuerto = convertidorFecha(row.Cells[10].Controls.OfType<TextBox>().FirstOrDefault().Text);

                        float ValorTransMaritimoPagado;
                        float.TryParse(row.Cells[16].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out ValorTransMaritimoPagado);
                        
                        float valorGiradoaProovedorDolares;
                        float.TryParse(row.Cells[17].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out valorGiradoaProovedorDolares);
                        float valorGiradoaProovedorEuros;
                        float.TryParse(row.Cells[18].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out valorGiradoaProovedorEuros);
                        
                        string NroDui = row.Cells[19].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        string Contenedor = row.Cells[20].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        string Consignatario = row.Cells[21].Controls.OfType<TextBox>().FirstOrDefault().Text;

                        string fechafacturaProveedor = convertidorFecha(row.Cells[22].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        string NroFacturaProveedor = row.Cells[23].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        float MontoFacturaProveedor;
                        float.TryParse(row.Cells[24].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out MontoFacturaProveedor);
                                                
                        string FechaFactura = convertidorFecha(row.Cells[25].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        string NIT = row.Cells[26].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        string NroFactura = row.Cells[27].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        float MontoSeguro;
                        float.TryParse(row.Cells[28].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out MontoSeguro);



                        string nroaplicacionseguro = row.Cells[29].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        float valorCostoPrima;
                        float.TryParse(row.Cells[30].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out valorCostoPrima);
                        string FechaArriboApuerto = convertidorFecha(row.Cells[31].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        string FechaArriboAduanero = convertidorFecha(row.Cells[32].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        string FechaArriboObra = convertidorFecha(row.Cells[33].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        string FechaCruceFrontera = convertidorFecha(row.Cells[34].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        string colorCanal = row.Cells[35].Controls.OfType<DropDownList>().FirstOrDefault().SelectedValue.ToString();
                        string NroBl = row.Cells[36].Controls.OfType<TextBox>().FirstOrDefault().Text;

                        //-------------------cambio de base de datos------------------
                        string _nombreDBOriginal = Session["NombreBaseDatos"].ToString();                    
                        NA_VariablesGlobales nvar = new NA_VariablesGlobales();
                        string baseDeDatosTemporal = nvar.getBasedeDatosTemporal(Ciudad);
                        Session["NombreBaseDatos"] = baseDeDatosTemporal;
                        
                        //-------------------fin de cambio de datos-------------------

                            NEquipo equipo = new NEquipo();
                            bool actualizado = equipo.actualizar_importacionJYCIA_General(codigoEquipo, Ciudad, ValorTransMaritimoPagado, NroDui, Contenedor, Consignatario, FechaFactura, NIT, NroFactura, MontoSeguro,
                            fechaAprobacionlimiteplanos, fechaArriboapuerto, valorCostoPrima,
                            FechaArriboApuerto,  FechaArriboAduanero,  FechaArriboObra,  FechaCruceFrontera,
                            colorCanal, nroaplicacionseguro, valorGiradoaProovedorDolares, valorGiradoaProovedorEuros, NroBl,
                            fechafacturaProveedor, NroFacturaProveedor, MontoFacturaProveedor);
                        ///---------------modificar Estado pero ay k cambiar la base de datos-----------------------------------------------------                                                                       
                        
                        NA_Responsables Nresp = new NA_Responsables();
                        string usuarioAux = Session["NameUser"].ToString();
                        string passwordAux = Session["passworuser"].ToString();
                        int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                        int codEstadoActual = equipo.getCodigoEstadoActual(codigoEquipo);
                        bool permisodeImportacion = equipo.estaPermitidoEstadoImportacion(codigoEstado);
                        bool permisodeProyectoEstadoNuevo = equipo.estaPermitidoEstadoProyecto(codigoEstado);
                        bool permisodeProyectoEstadoActual = equipo.estaPermitidoEstadoProyecto(codEstadoActual);
                        if (
                            (permisodeImportacion == true &&
                            permisodeProyectoEstadoNuevo == false &&
                            permisodeProyectoEstadoActual == false)
                            ||
                            (codigoEstado == 4 &&
                            permisodeProyectoEstadoActual == false)
                            )
                        {

                            NA_Equipo nequipo = new NA_Equipo();
                            int codFechaEstadoUltimoInsertado = nequipo.getCodigoFechaEstadoEquipo(codigoEquipo);

                            if (codEstadoActual != codigoEstado)
                            {
                                NA_FechaEstadoEquipo fechaEstadoEq = new NA_FechaEstadoEquipo();
                                fechaEstadoEq.insertar(codigoEquipo, codigoEstado, codUser);
                                codFechaEstadoUltimoInsertado = fechaEstadoEq.ultimoinsertado();
                            }

                            equipo.ModificarFechaEstadoEquipo(codigoEquipo, codFechaEstadoUltimoInsertado);
                            ///--------------------------------------------
                            NA_Historial nhistorial = new NA_Historial();
                            // int codUser = Convert.ToInt32(Session["coduser"].ToString());
                            nhistorial.insertar(codUser, "Ha Modificado el equipo " + codigoEquipo + " del Exbo " + tx_exbo.Text);
                            ///--------------------------------------------
                            ///----------------------------------fin de modificar estado-----------------------------------------------------
                            //-------------------volver la base dedatos base de datos------------------                            
                            Session["NombreBaseDatos"] = _nombreDBOriginal;
                            //-------------------fin de volver base de datos-------------------

                        }
                    }
                }

            }
        }


        public void Exportar_Excel()
        {
            string edificio = tx_edificio.Text;
            string exbo = tx_exbo.Text;
            string estado = dd_estado.SelectedItem.Text;
            string Dui = tx_dui.Text;
            string contenedor = tx_contenedor.Text;

            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.buscar_ImportacionDatosGeneral(edificio, exbo, estado, Dui, contenedor, true);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombreDB = Session["BaseDatos"].ToString();
            string nombre = "Datos de Importacion General " + nombreDB + " " + DateTime.Now.ToString("dd/MM/yyyy");
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = tuplaRes;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }

        }

        protected void bt_Exportar_Click(object sender, EventArgs e)
        {
            Exportar_Excel();
        }

      

   
    }
}