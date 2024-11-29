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
using System.Configuration;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_GestionarProyEncargadoPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

          /*  if (tienePermisoDeIngreso(25) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 
            */
            if(!IsPostBack){
                listarTiendas("");
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

        [WebMethod]
        [ScriptMethod]
        public static string[] getListaPropietario(string prefixText, int count)
        {
            string nombre = prefixText;
            NCorpal_Cliente prop = new NCorpal_Cliente();
            DataSet tuplas = prop.buscarPropietario(nombre);
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
        public static string[] getListaTienda(string prefixText, int count)
        {
            string nombre = prefixText;
            NCorpal_Cliente prop = new NCorpal_Cliente();
            DataSet tuplas = prop.listarTiendas(nombre);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }
            return lista;
        }


        private void listarTiendas(string nombreTienda)
        {
            NCorpal_Cliente Nproy = new NCorpal_Cliente();
            DataSet lista = Nproy.listarTiendas(nombreTienda);
            gv_tablaTienda.DataSource = lista;
            gv_tablaTienda.DataBind();
        }

     

      



        private void cargarDatosSeleccionTienda()
        {

            int codigo = Convert.ToInt32(gv_tablaTienda.SelectedRow.Cells[1].Text);
            NCorpal_Cliente tienda = new NCorpal_Cliente();
            DataSet tupla = tienda.get_tienda(codigo);
            
            tx_nombreTienda.Text = tupla.Tables[0].Rows[0][1].ToString().Replace("&nbsp;","");
            tx_direcciontienda.Text = tupla.Tables[0].Rows[0][2].ToString().Replace("&nbsp;", "");
            tx_telefonoTienda.Text = tupla.Tables[0].Rows[0][3].ToString().Replace("&nbsp;", "");
            dd_departamentoTienda.SelectedValue = tupla.Tables[0].Rows[0][4].ToString().Replace("&nbsp;", "");
            tx_zonaTienda.Text = tupla.Tables[0].Rows[0][5].ToString().Replace("&nbsp;", "");
            tx_nombrePropietario.Text = tupla.Tables[0].Rows[0][6].ToString().Replace("&nbsp;", "");
            tx_ciPropietario.Text = tupla.Tables[0].Rows[0][7].ToString().Replace("&nbsp;", "");
            tx_direccionPropietario.Text = tupla.Tables[0].Rows[0][8].ToString().Replace("&nbsp;", "");
            tx_celularPropietario.Text = tupla.Tables[0].Rows[0][9].ToString().Replace("&nbsp;", "");
            tx_nitPropietario.Text = tupla.Tables[0].Rows[0][10].ToString().Replace("&nbsp;", "");
            tx_correoPropietario.Text = tupla.Tables[0].Rows[0][11].ToString().Replace("&nbsp;", "");
            tx_FacturarA.Text = tupla.Tables[0].Rows[0][12].ToString().Replace("&nbsp;", "");
            tx_facturarNit.Text = tupla.Tables[0].Rows[0][13].ToString().Replace("&nbsp;", "");
            tx_facturarCorreo.Text = tupla.Tables[0].Rows[0][14].ToString().Replace("&nbsp;", "");
            tx_observacionTienda.Text = tupla.Tables[0].Rows[0][15].ToString().Replace("&nbsp;", "");

        }
              

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
           if(gv_tablaTienda.SelectedIndex > -1){
            modificarTienda();                      
           }else
               Response.Write("<script type='text/javascript'> alert('Error: Seleccione Proyecto') </script>");
        }

        private void modificarTienda()
        {

            int codigo = int.Parse(gv_tablaTienda.SelectedRow.Cells[1].Text);
            string tiendaname = tx_nombreTienda.Text;
            string tiendadir = tx_direcciontienda.Text;
            string tiendatelefono = tx_telefonoTienda.Text;
            string tiendadepartamento = dd_departamentoTienda.SelectedItem.Text;
            string tiendazona = tx_zonaTienda.Text;
            string propietarioname = tx_nombrePropietario.Text;
            string propietarioci = tx_ciPropietario.Text;
            string propietariodir = tx_direccionPropietario.Text;
            string propietariocelular = tx_celularPropietario.Text;
            string propietarionit = tx_nitPropietario.Text;
            string propietariocorreo = tx_correoPropietario.Text;
            string facturar_a = tx_FacturarA.Text;
            string facturar_nit = tx_facturarNit.Text;
            string facturar_correo = tx_facturarCorreo.Text;
            string observacion = tx_observacionTienda.Text;

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUserGra = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NCorpal_Cliente ncorpal = new NCorpal_Cliente();
            bool bandera = ncorpal.updateDatosTienda(codigo, tiendaname, tiendadir, tiendatelefono,
                                  tiendadepartamento, tiendazona, propietarioname,
                                  propietarioci, propietariodir, propietariocelular,
                                  propietarionit, propietariocorreo, facturar_a,
                                  facturar_nit, facturar_correo, observacion,codUserGra);
            if (bandera)
            {
                listarTiendas("");
                gv_tablaTienda.SelectedIndex = -1;
                Response.Write("<script type='text/javascript'> alert('Modificado OK') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error') </script>");
         
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatosTienda();
        }

        private void limpiarDatosTienda()
        {
            tx_nombreTienda.Text = "";
            tx_telefonoTienda.Text = "";
            tx_direcciontienda.Text = "";
            dd_departamentoTienda.SelectedIndex = 0;
            tx_zonaTienda.Text = "";

            tx_nombrePropietario.Text = "";
            tx_direccionPropietario.Text = "";
            tx_ciPropietario.Text = "";
            tx_celularPropietario.Text = "";
            tx_correoPropietario.Text = "";
            tx_nitPropietario.Text = "";

            tx_facturarCorreo.Text = "";
            tx_facturarNit.Text = "";
            tx_FacturarA.Text = "";
            tx_facturarNit.Text = "";
            
            tx_observacionTienda.Text = "";
            
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            listarTiendas(tx_nombreTienda.Text);
        }

    
    
      

      

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Exportar_Excel();
        }


        protected void Exportar_Excel()
        {
            NCorpal_Cliente Ncorpal = new NCorpal_Cliente();
            DataSet lista = Ncorpal.listarTiendas(tx_nombreTienda.Text);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombreDB = Session["BaseDatos"].ToString();
            string nombre = "Lista de Edificios y Encargados de Pago " + nombreDB;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = lista;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }

        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarTienda();
        }

        private void eliminarTienda()
        {
            if (gv_tablaTienda.SelectedIndex > -1)
            {
                int codigo = int.Parse(gv_tablaTienda.SelectedRow.Cells[1].Text);
                NCorpal_Cliente ncorp = new NCorpal_Cliente();
                bool bandera = ncorp.eliminarTienda(codigo);
                if (bandera)
                {
                    listarTiendas("");
                    gv_tablaTienda.SelectedIndex = -1;

                    Response.Write("<script type='text/javascript'> alert('Eliminado OK') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error') </script>");
            }
        }

        protected void bt_guardar_Click(object sender, EventArgs e)
        {   
            GuardarTienda();
        }

        private void GuardarTienda()
        {
          
         string tiendaname = tx_nombreTienda.Text;
         string tiendadir  = tx_direcciontienda.Text;
         string tiendatelefono  = tx_telefonoTienda.Text;
         string tiendadepartamento  = dd_departamentoTienda.SelectedItem.Text;
         string tiendazona  = tx_zonaTienda.Text;       
         string propietarioname = tx_nombrePropietario.Text;   
         string propietarioci = tx_ciPropietario.Text;     
         string propietariodir = tx_direccionPropietario.Text;    
         string propietariocelular = tx_celularPropietario.Text;
         string propietarionit   = tx_nitPropietario.Text; 
         string propietariocorreo  = tx_correoPropietario.Text;
         string facturar_a  = tx_FacturarA.Text;      
         string facturar_nit  = tx_facturarNit.Text;    
         string facturar_correo  = tx_facturarCorreo.Text;  
         string observacion    = tx_observacionTienda.Text;

         NA_Responsables Nresp = new NA_Responsables();
         string usuarioAux = Session["NameUser"].ToString();
         string passwordAux = Session["passworuser"].ToString();
         int codUserGra = Nresp.getCodUsuario(usuarioAux, passwordAux);

         
                              
         NCorpal_Cliente ncorpal = new NCorpal_Cliente();
         bool bandera = ncorpal.guardarDatosTienda(tiendaname, tiendadir, tiendatelefono,
                              tiendadepartamento, tiendazona, propietarioname,
                              propietarioci, propietariodir, propietariocelular,
                              propietarionit, propietariocorreo, facturar_a,
                              facturar_nit, facturar_correo, observacion, codUserGra);
            if(bandera){
                limpiarDatosTienda();
                listarTiendas("");
                Response.Write("<script type='text/javascript'> alert('Guardado OK') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error') </script>");
        }

        protected void gv_tablaProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatosSeleccionTienda();
        }

      
    }
}