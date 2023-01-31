using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_SeguimientosMorosos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            
            if (tienePermisoDeIngreso(29) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            lbTitulo.Text = "Mantenimientos Morosos " + Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                cargarYearSeguimiento();
                PermisosVista();
            }            
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

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProyectos3(string prefixText, int count)
        {
            string nombreProyecto = prefixText;

            NProyecto proyectoN = new NProyecto();
            DataSet tuplas = proyectoN.buscadorCallCenter(nombreProyecto);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }


        private void cargarYearSeguimiento()
        {
            NA_Seguimiento Nsegui = new NA_Seguimiento();
            DataSet datosCargar = Nsegui.getAllyearSeguimiento();
            dd_anio.DataSource = datosCargar;
            dd_anio.DataValueField = "codigo";
            dd_anio.DataTextField = "years";
            dd_anio.Items.Add(new ListItem("", "-1"));
            dd_anio.AppendDataBoundItems = true;
            dd_anio.SelectedIndex = -1;
            dd_anio.DataBind();
        }
        
         public void PermisosVista() {
                NA_Responsables Nresp = new NA_Responsables();
                int codUser = Nresp.getCodUsuario(Session["NameUser"].ToString(), Session["passworuser"].ToString());                
                lb_BaseDatos.Visible = false;
                dd_BaseDatos.Visible = false;

                if (codUser != -1)
                {
                   
                    bool tienepermiso30 = Nresp.tienePermiso(codUser, 30);
                    bool tienepermiso31 = Nresp.tienePermiso(codUser, 31);

                    if (tienepermiso30)
                    {
                        dd_anio.Items.Clear();
                        NA_Seguimiento Nsegui = new NA_Seguimiento();
                        DataSet datosCargar = Nsegui.getAllyearSeguimiento();
                        dd_anio.DataSource = datosCargar;
                        dd_anio.DataValueField = "codigo";
                        dd_anio.DataTextField = "years";
                        dd_anio.Items.Add(new ListItem("Todos", "-1"));
                        dd_anio.AppendDataBoundItems = true;
                        dd_anio.SelectedIndex = -1;
                        dd_anio.DataBind();
                    }

                    if (tienepermiso31)
                    {
                        lb_BaseDatos.Visible = true;
                        dd_BaseDatos.Visible = true;
                    }
                }
            }


        public void buscarParecidos() {
            gv_seguimientoMoroso.DataSource = null;
            gv_seguimientoMoroso.DataBind();

            gv_seguimientoMoroso.SelectedIndex = -1;
            string year = dd_anio.SelectedItem.Text;
            string edificio = tx_edificio.Text;
            NProyecto nproy = new NProyecto();
            
            if(nproy.existeProyectoParecido(edificio)){
                                if (year != "" && year != "Todos")
                                {
                                    int anio = Convert.ToInt32(year);
                                    string exbo = tx_exbo.Text;
                                    NA_Seguimiento segn = new NA_Seguimiento();
                                    int meseslimiteAtrazadoPermitido = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
                                    DataSet tuplaRes = segn.getEquiposMantenimientoMorosos(exbo, edificio, meseslimiteAtrazadoPermitido, anio);
                                    gv_seguimientoMoroso.DataSource = tuplaRes;
                                    gv_seguimientoMoroso.DataBind();
                                    tx_cantidadEquipos.Text = gv_seguimientoMoroso.Rows.Count.ToString();
                                    if (tuplaRes.Tables[0].Rows.Count == 0)
                                    {
                                        // Response.Write("<script type='text/javascript'> alert('No tiene Deudas') </script>");
                                        lb_deudas.Text = "No tiene Deudas";
                                    }
                                }
                                else
                                {
                                    if (year == "Todos")
                                    {
                                        string exbo = tx_exbo.Text;
                                        NA_Seguimiento segn = new NA_Seguimiento();
                                        int meseslimiteAtrazadoPermitido = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
                                        DataSet tuplaRes = segn.getTodosEquiposMantenimientoMorosos(exbo, edificio,meseslimiteAtrazadoPermitido);
                                        gv_seguimientoMoroso.DataSource = tuplaRes;
                                        gv_seguimientoMoroso.DataBind();
                                        tx_cantidadEquipos.Text = gv_seguimientoMoroso.Rows.Count.ToString();
                                        if (tuplaRes.Tables[0].Rows.Count == 0)
                                        {
                                            //Response.Write("<script type='text/javascript'> alert('No tiene Deudas') </script>");
                                            lb_deudas.Text = "No tiene Deudas";
                                        }
                                    }
                                }
            }else
                lb_deudas.Text = "El edificio no Existe";            
        }


        public void buscarEspecifico()
        {
            gv_seguimientoMoroso.DataSource = null;
            gv_seguimientoMoroso.DataBind();

            gv_seguimientoMoroso.SelectedIndex = -1;
            string year = dd_anio.SelectedItem.Text;
            string edificio = tx_edificio.Text;
            NProyecto nproy = new NProyecto();

            if (nproy.existeProyectoEspecifico(edificio))
            {
                if (year != "" && year != "Todos")
                {
                    int anio = Convert.ToInt32(year);
                    string exbo = tx_exbo.Text;
                    NA_Seguimiento segn = new NA_Seguimiento();
                    int meseslimiteAtrazadoPermitido = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
                    DataSet tuplaRes = segn.getEquiposMantenimientoMorososEspecifico(exbo, edificio, meseslimiteAtrazadoPermitido, anio);
                    gv_seguimientoMoroso.DataSource = tuplaRes;
                    gv_seguimientoMoroso.DataBind();
                    tx_cantidadEquipos.Text = gv_seguimientoMoroso.Rows.Count.ToString();
                    if (tuplaRes.Tables[0].Rows.Count == 0)
                    {
                        // Response.Write("<script type='text/javascript'> alert('No tiene Deudas') </script>");
                        lb_deudas.Text = "No tiene Deudas";
                    }
                }
                else
                {
                    if (year == "Todos")
                    {
                        string exbo = tx_exbo.Text;
                        NA_Seguimiento segn = new NA_Seguimiento();
                        int meseslimiteAtrazadoPermitido = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
                        DataSet tuplaRes = segn.getTodosEquiposMantenimientoMorososEspecifico(exbo, edificio, meseslimiteAtrazadoPermitido);
                        gv_seguimientoMoroso.DataSource = tuplaRes;
                        gv_seguimientoMoroso.DataBind();
                        tx_cantidadEquipos.Text = gv_seguimientoMoroso.Rows.Count.ToString();
                        if (tuplaRes.Tables[0].Rows.Count == 0)
                        {
                            //Response.Write("<script type='text/javascript'> alert('No tiene Deudas') </script>");
                            lb_deudas.Text = "No tiene Deudas";
                        }
                    }
                }
            }
            else
                lb_deudas.Text = "El edificio no Existe";

        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
          //  lb_deudas.Text = "";
          //  string edificio = tx_edificio.Text;
           // if (edificio.Equals(""))
           // {
                buscarParecidos();
           // }
           // else
              //  buscarEspecifico();
            
        }

        protected void bt_Excel_Click(object sender, EventArgs e)
        {
            lb_deudas.Text = "";
            gv_seguimientoMoroso.SelectedIndex = -1;
            string year = dd_anio.SelectedItem.Text;            
                
                string edificio = tx_edificio.Text;
                string exbo = tx_exbo.Text;
                NA_Seguimiento segn = new NA_Seguimiento();
                DataSet tuplaRes = null;

                if (edificio.Equals(""))
                {
                    if (year != "" && year != "Todos")
                    {
                        int anio = Convert.ToInt32(year);
                        int meseslimiteAtrazadoPermitido = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
                         tuplaRes = segn.getEquiposMantenimientoMorosos(exbo, edificio, meseslimiteAtrazadoPermitido, anio);                     
                    }
                    else
                    {
                        if (year == "Todos")
                        {
                            int meseslimiteAtrazadoPermitido = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
                             tuplaRes = segn.getTodosEquiposMantenimientoMorosos(exbo, edificio, meseslimiteAtrazadoPermitido);                           
                        }
                    }
                }
                else 
                {
                    if (year != "" && year != "Todos")
                    {
                        int anio = Convert.ToInt32(year);
                        int meseslimiteAtrazadoPermitido = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
                        tuplaRes = segn.getEquiposMantenimientoMorososEspecifico(exbo, edificio, meseslimiteAtrazadoPermitido, anio);
                    }
                    else
                    {
                        if (year == "Todos")
                        {
                            int meseslimiteAtrazadoPermitido = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
                            tuplaRes = segn.getTodosEquiposMantenimientoMorososEspecifico(exbo, edificio, meseslimiteAtrazadoPermitido);                            
                        }
                    }
                }                    

            
                //// Creacion del Excel
                HttpResponse response = HttpContext.Current.Response;
                // first let's clean up the response.object
                response.Clear();
                response.Charset = "";
                // set the response mime type for excel
                response.ContentType = "application/vnd.ms-excel";
                string nombreDB = Session["BaseDatos"].ToString();
                string nombre = "Cobros Morosos " + nombreDB+"_"+year;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre+ ".xls" + "\"");

                // create a string writer
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        //instantiate a datagrid
                        DataGrid dg = new DataGrid();
                        dg.DataSource = tuplaRes;
                        dg.DataBind();
                        dg.RenderControl(htw);                        
                        response.Write(sw.ToString());
                        response.End();
                    }

            }

        }


        public void cambiarBaseDeDatos(string departamento)
        {
            string BaseDatos = departamento;
            switch (BaseDatos)
            {
                case "Prueba":
                    Session["NombreBaseDatos"] = "db_prueba";
                    Session["BaseDatos"] = "Prueba";
                    break;
                case "Santa Cruz":
                    Session["NombreBaseDatos"] = "db_SantaCruz";
                    Session["BaseDatos"] = "Santa Cruz";
                    break;
                case "La Paz":
                    Session["NombreBaseDatos"] = "db_LaPaz";
                    Session["BaseDatos"] = "La Paz";
                    break;
                case "Cochabamba":
                    Session["NombreBaseDatos"] = "db_Cochabamba";
                    Session["BaseDatos"] = "Cochabamba";
                    break;
                case "Sucre":
                    Session["NombreBaseDatos"] = "db_Sucre";
                    Session["BaseDatos"] = "Sucre";
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        protected void dd_BaseDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dpto = dd_BaseDatos.SelectedValue.ToString();            
            cambiarBaseDeDatos(dpto);
            Response.Redirect("../Presentacion/FA_SeguimientosMorosos.aspx"); 
        }
    }
}