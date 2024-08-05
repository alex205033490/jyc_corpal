using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_AdicionarCotizacionRepuesto : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (!IsPostBack) {
                DataTable datoRepuesto = new DataTable();
                datoRepuesto.Columns.Add("Codigo", typeof(string));
                datoRepuesto.Columns.Add("numeracion", typeof(string));
                datoRepuesto.Columns.Add("Detalle", typeof(string));
                datoRepuesto.Columns.Add("Precio", typeof(string));
                datoRepuesto.Columns.Add("Cantidad", typeof(string));
                datoRepuesto.Columns.Add("PrecioTotal", typeof(string));

                gv_adicionados.DataSource = datoRepuesto;
                gv_adicionados.DataBind();
                Session["listaRepuesto"] = datoRepuesto;

                cb_el.Checked = true;
              //  mostrarRepuesto("", "");
                mostarSiguieteCotizacion();
                habilitar_permisos();
                //------------------------nuevo--------------------
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                bool hacercotidesdeTarea = Convert.ToBoolean(Session["banderatarea"].ToString());
                bool hacercotidesdeEvento = Convert.ToBoolean(Session["banderaEvento"].ToString());
                  if (codUser != -1)
                  {
                      if (hacercotidesdeTarea)
                      {
                          tx_edificio.Text = HttpUtility.HtmlDecode(Session["nombreEdificioTarea"].ToString());
                      }

                      if (hacercotidesdeEvento) {
                          tx_edificio.Text = HttpUtility.HtmlDecode(Session["nombreEdificioEvento"].ToString());
                          
                      }


                  }

            }
            
        }

        private void mostarSiguieteCotizacion()
        {
            NA_Repuesto nrepuesto = new NA_Repuesto();
            int cotiSiguiente = nrepuesto.getultimaCotizacionRepuestoSiguiente();
            tx_codigoCotizacion.Text = cotiSiguiente.ToString();
            DateTime thisDay = DateTime.Today;
            int anio = thisDay.Year;
            tx_numeroCoti.Text = cotiSiguiente.ToString() + "/" + anio.ToString();
        }


        public void mostrarRepuesto(string numeracion, string detalle) {
            NA_Repuesto repuesto = new NA_Repuesto();
            DataSet tuplaRes = repuesto.mostrarRepuestos(numeracion, detalle);
            gv_repuesto.DataSource = tuplaRes;
            gv_repuesto.DataBind();
            tx_cantRepuesto.Text = gv_repuesto.Rows.Count.ToString(); 
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            mostrarRepuesto(tx_codigo.Text, tx_nameRepuesto.Text);
        }

        protected void bt_adicionar_Click(object sender, EventArgs e)
        {
            adicionar_repuestos();
        }

        private void adicionar_repuestos()
        {
            DataTable datoRepuesto = Session["listaRepuesto"] as DataTable;
            
            CheckBox cb = null;
            for (int i = 0; i < gv_repuesto.Rows.Count; i++)
            {
                cb = (CheckBox)gv_repuesto.Rows[i].Cells[1].FindControl("CheckBox1");
                if (cb != null && cb.Checked)
                {
                    string codigo = HttpUtility.HtmlDecode(gv_repuesto.Rows[i].Cells[1].Text);
                    string numeracion = gv_repuesto.Rows[i].Cells[2].Text;
                    string detalle = HttpUtility.HtmlDecode(gv_repuesto.Rows[i].Cells[3].Text);
                    string precio = gv_repuesto.Rows[i].Cells[4].Text;
                    string cantidad = "1";
                    DataRow tupla = datoRepuesto.NewRow();
                    tupla["Codigo"] = codigo;
                    tupla["numeracion"] = numeracion;
                    tupla["Detalle"] = detalle;
                    tupla["Precio"] = precio;
                    tupla["Cantidad"] = cantidad;
                    tupla["PrecioTotal"] = precio;                    
                    datoRepuesto.Rows.Add(tupla);               
                    }
            }

            
            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();
            total_Precio();
           // mostrarRepuesto("", "");
        }

        private void total_Precio()
        {
            DataTable datoRepuesto = Session["listaRepuesto"] as DataTable;
            double total = 0;
            for (int i = 0; i < datoRepuesto.Rows.Count; i++)
            {
                DataRow fila = datoRepuesto.Rows[i];
                double precio = Convert.ToDouble(fila["precio"].ToString());
                double cantidad = Convert.ToDouble(fila["Cantidad"].ToString());
                double preciototal = (precio * cantidad);                
                datoRepuesto.Rows[i][5] = preciototal;                

                total = total + preciototal;
            }
            Session["listaRepuesto"] = datoRepuesto;
            tx_precioTotal.Text = total.ToString();
        }


        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProyectos(string prefixText, int count)
        {
            string nombreProyecto = prefixText;

            NProyecto proyectoN = new NProyecto();
            DataSet tuplas = proyectoN.buscador2(nombreProyecto);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }



        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaCodigosRepuesto(string prefixText, int count)
        {
            string numeracion = prefixText;

            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet tuplas = nrepuesto.getNumeracionRepuesto(numeracion);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }


        [WebMethod]
        [ScriptMethod]
        public static string[] getListaEquipo(string prefixText, int count)
        {
            string nombreEquipo = prefixText;

            NEquipo equipoN = new NEquipo();
            DataSet tuplas = equipoN.buscadorEquipo(nombreEquipo);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }
            return lista;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["EdificioRepuesto"] = HttpUtility.HtmlDecode(tx_edificio.Text);
            //Session["numeroCotiRepuesto"] = tx_numeroCoti.Text + "/" + DateTime.Now.ToString("yyyy");
            Session["numeroCotiRepuesto"] = tx_numeroCoti.Text ;
            Session["fechaRepuesto"] = Session["BaseDatos"].ToString() + ", " + DateTime.Now.ToString("dd") + " de " + DateTime.Now.ToString("MMMM") + " de " + DateTime.Now.ToString("yyyy");
            Session["TotalRepuesto"] = tx_precioTotal.Text;
            N_numLetra nl = new N_numLetra();
            
            Session["PrecioLetras"] ="Son : "+ nl.Convertir(tx_precioTotal.Text,true,"Dólares Americanos");

            string ascensores = "";
            if (cb_el.Checked)
            {
                Session["ellosRepuesto"] = "el ascensor";
                Session["instalados"] = "instalado";
                ascensores = "ascensor";
            }
            else
            {
                Session["ellosRepuesto"] = "los ascensores";
                Session["instalados"] = "instalados";
                ascensores = "ascensores";
            }

            string monto = tx_precioTotal.Text.Replace(",",".");
            string edificio = HttpUtility.HtmlDecode(tx_edificio.Text);
            string cite = tx_numeroCoti.Text;
            int codEquipo = Convert.ToInt32(dd_exbos.SelectedValue.ToString());
            int codEvento = -1;
            
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);


            NA_Repuesto nrepuesto = new NA_Repuesto();  
            if(nrepuesto.insertarCotizacionRepuesto(monto,edificio,cite,ascensores,CodUser,codEvento,codEquipo)){
                int ultimoinsertado = nrepuesto.getultimaCotizacionRepuestoInsertado();
                DataTable datoRepuesto = Session["listaRepuesto"] as DataTable;

                for (int i = 0; i < datoRepuesto.Rows.Count; i++)
                {
                    int codRepuestoAux = Convert.ToInt32(datoRepuesto.Rows[i]["codigo"].ToString());
                    double cantidad = Convert.ToDouble(datoRepuesto.Rows[i]["cantidad"].ToString());
                    double preciocompra = Convert.ToDouble(datoRepuesto.Rows[i]["Precio"].ToString());
                 
                    nrepuesto.insertarDetalleCotizacionRepuesto(ultimoinsertado, codRepuestoAux, cantidad, preciocompra);
                }

                Session["codcotiRepuesto"] = ultimoinsertado;

                //---------------------- si no tiene premiso no ingresa a crear la carta
                   
                    bool hacercotidesdeTarea = Convert.ToBoolean(Session["banderatarea"]);
                    bool hacercotidesdeEvento = Convert.ToBoolean(Session["banderaEvento"].ToString());

                    if (!hacercotidesdeTarea && !hacercotidesdeEvento)
                        {
                        // ---------- hace cotizacion solo
                           // Response.Write("<script type='text/javascript'> alert('Cotizacion Creada: ok') </script>");
                           // Response.Redirect("../Presentacion/FA_ReporteCotizacionRepuesto.aspx");
                        //--------modificado   
                            Session["CodigoR144"] = ultimoinsertado;
                            Response.Redirect("../Presentacion/FA_Reporte_R144.aspx");
                        }
                        else
                        {
                            if (hacercotidesdeTarea)
                            {
                                int codTarea = Convert.ToInt32(Session["codTarea"].ToString());
                                NA_tareasTecnico ntarea = new NA_tareasTecnico();
                                ntarea.updateDetalleTareaCoti(codTarea, ultimoinsertado);
                                crearR144();
                                Session["nombreEdificioTarea"] = "";
                                Session["banderatarea"] = false; 
                                Response.Redirect("../Presentacion/FA_GActividades.aspx");
                             }else
                                if (hacercotidesdeEvento) {
                                    codEvento = Convert.ToInt32(Session["codEvento"].ToString());
                                    NA_Evento nevento = new NA_Evento();
                                    DataSet tuplas = nevento.getEvento(codEvento);
                                    int codPrioridad = Convert.ToInt32(tuplas.Tables[0].Rows[0][23].ToString());
                                    
                                    nevento.updateEventoCallcenter_CotiRepuesto(codEvento, ultimoinsertado, codPrioridad);
                                    crearR144();
                                    Session["nombreEdificioEvento"] = "";
                                    Session["banderaEvento"] = false;
                                    Response.Redirect("../Presentacion/FA_CallCenterVacio.aspx");                                
                                }
                        
                        }
                   

                //---------------------------------------------------------------------


            }else
                Response.Write("<script type='text/javascript'> alert('Error: No se pudo realizar la Cotizacion') </script>");
            
        }

      

        protected void gv_adicionados_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_adicionados.EditIndex = e.NewEditIndex;
            int index = gv_adicionados.EditIndex;

            DataTable datoRepuesto = Session["listaRepuesto"] as DataTable;
            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();

            GridViewRow row = gv_adicionados.Rows[index];
            TextBox auxtexto = (TextBox)row.Cells[2].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto = (TextBox)row.Cells[3].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto = (TextBox)row.Cells[4].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto = (TextBox)row.Cells[5].Controls[0];
            auxtexto.ReadOnly = true;

            TextBox MontoPago = (TextBox)row.Cells[6].Controls[0];
            MontoPago.BackColor = Color.Yellow;
            MontoPago.Text = "1";

            auxtexto = (TextBox)row.Cells[7].Controls[0];
            auxtexto.ReadOnly = true;

        }

        protected void gv_adicionados_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable datoRepuesto = Session["listaRepuesto"] as DataTable;
            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();
        }

        protected void gv_adicionados_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = gv_adicionados.EditIndex;
            GridViewRow row = gv_adicionados.Rows[index];
            TextBox CantidadModificar = (TextBox)row.Cells[6].Controls[0];

            DataTable datoRepuesto = Session["listaRepuesto"] as DataTable;
            datoRepuesto.Rows[index][4] = CantidadModificar.Text.Replace(".", ",");
            double precio = Convert.ToDouble(datoRepuesto.Rows[index][3].ToString());
            double cantidad = Convert.ToDouble(datoRepuesto.Rows[index][4].ToString());
            double preciototal = (precio * cantidad);
            datoRepuesto.Rows[index][5] = preciototal;

            gv_adicionados.EditIndex = -1;

            Session["listaRepuesto"] = datoRepuesto;
            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();
            total_Precio();

        }

        protected void gv_adicionados_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_adicionados.EditIndex = -1;
            DataTable datoRepuesto = Session["listaRepuesto"] as DataTable;
            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();
            total_Precio();
        }

        protected void gv_adicionados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;

            DataTable datoRepuesto = Session["listaRepuesto"] as DataTable;
            datoRepuesto.Rows[index].Delete();
            datoRepuesto.AcceptChanges();
            
            gv_adicionados.EditIndex = -1;

            Session["listaRepuesto"] = datoRepuesto;
            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();
            total_Precio();
        }

        protected void cb_el_CheckedChanged(object sender, EventArgs e)
        {
            cb_ellos.Checked = false;
        }

        protected void cb_ellos_CheckedChanged(object sender, EventArgs e)
        {
            cb_el.Checked = false;
        }




        private void habilitar_permisos()
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            bool hacercotidesdeTarea = Convert.ToBoolean(Session["banderatarea"]);
            bool hacercotidesdeEvento = Convert.ToBoolean(Session["banderaEvento"].ToString());

            if (codUser != -1)
            {
                if (hacercotidesdeTarea||hacercotidesdeEvento)
                {
                    tx_numeroCoti.Enabled = false;
                    tx_edificio.Enabled = false;
               }
            }
        }


        private void crearR144()
        {
            string fechaAux = DateTime.Now.ToString("dd/MM/yyyy");          
            string unidadNegocioAux = Session["BaseDatos"].ToString();

            DataTable datoRepuesto = Session["listaRepuesto"] as DataTable;

            DataRow tupla = datoRepuesto.NewRow();
            tupla["Codigo"] = "";
            tupla["Detalle"] = "";
            tupla["Cantidad"] = "";
            tupla["Precio"] = "Total";
            tupla["PrecioTotal"] = Session["TotalRepuesto"].ToString();
            datoRepuesto.Rows.Add(tupla);
            string montoTotalRepuestos = Session["TotalRepuesto"].ToString();

            string exboEquipo = dd_exbos.SelectedItem.Text;

            ReportParameter pexboEq = new ReportParameter("p_exbo",exboEquipo);
            ReportParameter fechacoti = new ReportParameter("p_fecha", fechaAux);
            ReportParameter unidadNegocio = new ReportParameter("p_unidadnegocio", unidadNegocioAux);
            //----------------------------------------------
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            DataSet datoResponsable = Nresp.get_responsable(CodUser);
            //------------------------------------------------------------
            ReportParameter Solicitante = new ReportParameter("p_solicitante", datoResponsable.Tables[0].Rows[0][1].ToString());
            ReportDataSource DSRepuesto = new ReportDataSource("DSR144", datoRepuesto);
            ReportParameter p_edificio = new ReportParameter("p_edificio", HttpUtility.HtmlDecode(tx_edificio.Text));



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

            string ruta144 = ConfigurationManager.AppSettings["repo_r144"];
            
            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.ReportPath = ruta144;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();            
            viewer.LocalReport.SetParameters(fechacoti);
            viewer.LocalReport.SetParameters(unidadNegocio);
            viewer.LocalReport.SetParameters(Solicitante);
            viewer.LocalReport.SetParameters(p_edificio);
            viewer.LocalReport.SetParameters(pexboEq);
            viewer.LocalReport.SetParameters(imagen);
            viewer.LocalReport.DataSources.Add(DSRepuesto);
            viewer.LocalReport.Refresh();            

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

            string rutaGuardarR144 = ConfigurationManager.AppSettings["guardar_r144"];
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

            string cuerpoDeMensaje = "Se ha creado el R-144 "+ nombreArchivo ;
            NA_EnvioCorreo nenvio = new NA_EnvioCorreo();

            bool CorreoOK = nenvio.EnvioCorreoAdicionarCotiRepuesto(codigoCoti, Edificio, cuerpoDeMensaje, baseDatos);
            if(CorreoOK == false){
                Response.Write("<script type='text/javascript'> alert('Error: Envio de correo') </script>");
            }  

        }

        protected void bt_verificar_Click(object sender, EventArgs e)
        {
            string edificio = tx_edificio.Text;
            cargarExbosEdificio(edificio);
        }

        private void cargarExbosEdificio(string edificio)
        {
            dd_exbos.Items.Clear();            

            NEquipo neq = new NEquipo();
            DataSet dato = neq.buscadorNombreExbo2(edificio);
            dd_exbos.DataSource = dato;
            dd_exbos.DataValueField = "codigo";
            dd_exbos.DataTextField = "exbo";
            dd_exbos.Items.Add(new ListItem("--Ninguno--", "-1"));
            dd_exbos.AppendDataBoundItems = true;
            dd_exbos.SelectedIndex = -1;
            dd_exbos.DataBind();
        }

       

        

       

      

    }
}