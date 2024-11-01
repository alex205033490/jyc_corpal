using jycboliviaASP.net.Datos;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_Recetas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(132) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                llenarProductosNax();
                buscarRecetas("");

                DataTable datoInsumo = new DataTable();
                datoInsumo.Columns.Add("Codigo", typeof(string));
                datoInsumo.Columns.Add("Insumo", typeof(string));
                datoInsumo.Columns.Add("Cantidad", typeof(string));

                gv_Insumos.DataSource = datoInsumo;
                gv_Insumos.DataBind();
                Session["listaInsumo"] = datoInsumo;

                DataTable datoInsumoCompuesto = new DataTable();
                datoInsumoCompuesto.Columns.Add("Codigo", typeof(string));
                datoInsumoCompuesto.Columns.Add("Insumo", typeof(string));
                datoInsumoCompuesto.Columns.Add("Cantidad", typeof(string));

                gv_InsumosCompuesto.DataSource = datoInsumoCompuesto;
                gv_InsumosCompuesto.DataBind();
                Session["listaInsumoCompuesto"] = datoInsumoCompuesto;
            }

        }

        private void llenarProductosNax()
        {
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos("");

            dd_productosNax.DataSource = tuplas;
            dd_productosNax.DataValueField = "codigo";
            dd_productosNax.DataTextField = "producto";
            dd_productosNax.AppendDataBoundItems = true;
            dd_productosNax.SelectedIndex = 1;
            dd_productosNax.DataBind();
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

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaInsumos(string prefixText, int count)
        {
            string nombreInsumo = prefixText;

            NCorpal_Produccion pp = new NCorpal_Produccion();
            DataSet tuplas = pp.get_insumosNormal(nombreInsumo);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;
        }


        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaInsumosCompuesto(string prefixText, int count)
        {
            string nombreInsumo = prefixText;

            NCorpal_Produccion pp = new NCorpal_Produccion();
            DataSet tuplas = pp.get_insumosCompuesto(nombreInsumo);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string receta = tx_nameReceta.Text;
            buscarRecetas(receta);
        }

        private void buscarRecetas(string receta)
        {
            NCorpal_Produccion pp = new NCorpal_Produccion();
            DataSet tuplas = pp.buscarRecetas(receta);
            gv_recetasCreada.DataSource = tuplas;
            gv_recetasCreada.DataBind();
        }

        protected void bt_adicionarInsumos_Click(object sender, EventArgs e)
        {
            adicionarInsumosLista();
        }

        private void adicionarInsumosLista()
        {
            string insumos = tx_insumos.Text;
            NCorpal_Produccion pp = new NCorpal_Produccion();
            if (pp.existeInsumo(insumos) == true)
            {
                int codigoInsumo = pp.get_codigoInsumo(insumos);
                decimal cantidad;
                decimal.TryParse(tx_cantInsumos.Text.Replace('.', ','), out cantidad);
                DataTable datoInsumo = Session["listaInsumo"] as DataTable;
                DataRow tupla = datoInsumo.NewRow();
                tupla["Codigo"] = codigoInsumo;
                tupla["Insumo"] = insumos;
                tupla["Cantidad"] = cantidad;
                datoInsumo.Rows.Add(tupla);

                Session["listaInsumo"] = datoInsumo;

                gv_Insumos.DataSource = datoInsumo;
                gv_Insumos.DataBind();

                tx_insumos.Text = "";
                tx_cantInsumos.Text = "";
            }
        }

        protected void bt_adicionarInsumosCompuesto_Click(object sender, EventArgs e)
        {
            adicionarInsumosCompuesto();
        }

        private void adicionarInsumosCompuesto()
        {
            string insumosCompuesto = tx_insumosCompuesto.Text;
            NCorpal_Produccion pp = new NCorpal_Produccion();
            if (pp.existeInsumoCompuesto(insumosCompuesto) == true)
            {

                int codigoInsumo = pp.get_codigoInsumoCompuesto(insumosCompuesto);
                decimal cantidad;
                decimal.TryParse(tx_cantInsumosCompuesto.Text.Replace('.', ','), out cantidad);
                DataTable datoInsumo = Session["listaInsumoCompuesto"] as DataTable;
                DataRow tupla = datoInsumo.NewRow();
                tupla["Codigo"] = codigoInsumo;
                tupla["Insumo"] = insumosCompuesto;
                tupla["Cantidad"] = cantidad;
                datoInsumo.Rows.Add(tupla);

                Session["listaInsumoCompuesto"] = datoInsumo;

                gv_InsumosCompuesto.DataSource = datoInsumo;
                gv_InsumosCompuesto.DataBind();
                tx_insumosCompuesto.Text = "";
                tx_cantInsumosCompuesto.Text = "";
            }
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_nameReceta.Text = "";
            tx_insumos.Text = "";
            tx_insumosCompuesto.Text = "";
            tx_cantInsumos.Text = "";
            tx_cantInsumosCompuesto.Text = "";
            dd_productosNax.SelectedIndex = 0;

            gv_Insumos.DataSource = null;
            gv_Insumos.DataBind();
            gv_InsumosCompuesto.DataSource = null;
            gv_InsumosCompuesto.DataBind();
        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            insertarRecetaNueva();
        }

        private void insertarRecetaNueva()
        {
            int codigoProducto;
            int.TryParse(dd_productosNax.SelectedValue.ToString(), out codigoProducto);
            NCorpal_Produccion pp = new NCorpal_Produccion();
            if (pp.existeProductoAsignadoaReceta(codigoProducto) == false) {
                /*-----------------------------------------*/
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                /*---------------------------------------------*/
                string producto = dd_productosNax.SelectedItem.Text;
                string Receta = tx_nameReceta.Text;
                decimal cantidadPorDia;
                decimal.TryParse(tx_cantidadPorDia.Text.Replace(".","."), out cantidadPorDia);
                bool consicionante = cbx_condicionante.Checked;

                bool bandera = pp.insertarReceta(Receta, codigoProducto, codUser, cantidadPorDia, consicionante);
                int codigoReceta = pp.get_codigoRecetaUltima(Receta, codigoProducto);
                if (bandera) {
                    DataTable datoInsumo = Session["listaInsumo"] as DataTable;
                    for (int i = 0; i < datoInsumo.Rows.Count; i++) {
                        int CodigoInsumo;
                        int.TryParse(datoInsumo.Rows[i][0].ToString(), out CodigoInsumo);
                        decimal Cantidad;
                        decimal.TryParse(datoInsumo.Rows[i][2].ToString().Replace(".",","), out Cantidad);
                        bool banderaInsumo = pp.insertarInsumoNormalReceta(codigoReceta, CodigoInsumo, Cantidad, codUser);
                    }

                    DataTable datoInsumoCompuesto = Session["listaInsumoCompuesto"] as DataTable;
                    for (int i = 0; i < datoInsumoCompuesto.Rows.Count; i++) {
                        int CodigoInsumoCompuesto;
                        int.TryParse(datoInsumoCompuesto.Rows[i][0].ToString(), out CodigoInsumoCompuesto);
                        decimal Cantidad;
                        decimal.TryParse(datoInsumoCompuesto.Rows[i][2].ToString().Replace(".", ","), out Cantidad);
                        bool banderaInsumoCompuesto = pp.insertarInsumoCompuestoReceta(codigoReceta, CodigoInsumoCompuesto, Cantidad, codUser);
                    }
                    Response.Write("<script type='text/javascript'> alert('Guardado: Ok!!') </script>");
                    buscarRecetas("");
                    limpiarDatos();

                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Guardar Receta') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: El Producto ya tiene Receta') </script>");
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            modificarDatos();
        }

        private void modificarDatos()
        {
            if (gv_recetasCreada.SelectedIndex >= 0) {
                int codigoProducto;
                int.TryParse(dd_productosNax.SelectedValue.ToString(), out codigoProducto);
                int codigoReceta;
                int.TryParse(gv_recetasCreada.SelectedRow.Cells[1].Text, out codigoReceta);

                NCorpal_Produccion pp = new NCorpal_Produccion();               
                    /*-----------------------------------------*/
                    NA_Responsables Nresp = new NA_Responsables();
                    string usuarioAux = Session["NameUser"].ToString();
                    string passwordAux = Session["passworuser"].ToString();
                    int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                    /*---------------------------------------------*/
                    string producto = dd_productosNax.SelectedItem.Text;
                    string Receta = tx_nameReceta.Text;
                    decimal cantidadPorDia;
                    decimal.TryParse(tx_cantidadPorDia.Text.Replace(".", "."), out cantidadPorDia);
                    bool consicionante = cbx_condicionante.Checked;

                    bool banderaUpdateReceta = pp.update_recetaProducto(codigoReceta, Receta, cantidadPorDia, consicionante);
                if (banderaUpdateReceta == true) {     
                    bool banderaEliminar1 = pp.eliminarInsumosNormalesAgregados(codigoReceta);
                    bool banderaEliminar2 = pp.eliminarInsumosCompuestosAgregados(codigoReceta);

                    if (banderaEliminar1 == true)
                    {
                        DataTable datoInsumo = Session["listaInsumo"] as DataTable;
                        for (int i = 0; i < datoInsumo.Rows.Count; i++)
                        {
                            int CodigoInsumo;
                            int.TryParse(datoInsumo.Rows[i][0].ToString(), out CodigoInsumo);
                            decimal Cantidad;
                            decimal.TryParse(datoInsumo.Rows[i][2].ToString().Replace(".", ","), out Cantidad);
                            bool banderaInsumo = pp.insertarInsumoNormalReceta(codigoReceta, CodigoInsumo, Cantidad, codUser);
                        }
                    }

                    if (banderaEliminar2 == true)
                    {
                        DataTable datoInsumoCompuesto = Session["listaInsumoCompuesto"] as DataTable;
                        for (int i = 0; i < datoInsumoCompuesto.Rows.Count ; i++)
                        {
                            int CodigoInsumoCompuesto;
                            int.TryParse(datoInsumoCompuesto.Rows[i][0].ToString(), out CodigoInsumoCompuesto);
                            decimal Cantidad;
                            decimal.TryParse(datoInsumoCompuesto.Rows[i][2].ToString().Replace(".", ","), out Cantidad);
                            bool banderaInsumoCompuesto = pp.insertarInsumoCompuestoReceta(codigoReceta, CodigoInsumoCompuesto, Cantidad, codUser);
                        }
                    }

                    Response.Write("<script type='text/javascript'> alert('Guardar: OK!') </script>");
                    buscarRecetas("");
                }else
                    Response.Write("<script type='text/javascript'> alert('Error: Guardar Receta') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione Receta') </script>");
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarRecetaDatos();
        }

        private void eliminarRecetaDatos()
        {
            NCorpal_Produccion pp = new NCorpal_Produccion();
            if (gv_recetasCreada.SelectedIndex >= 0) {
                int codigoReceta;
                int.TryParse(gv_recetasCreada.SelectedRow.Cells[1].Text, out codigoReceta);
               /* bool banderaEliminar1 = pp.eliminarInsumosNormalesAgregados(codigoReceta);
                bool banderaEliminar2 = pp.eliminarInsumosCompuestosAgregados(codigoReceta);
                if (banderaEliminar1 == banderaEliminar2 == true) { */
                    /*-----------------------------------------*/
                    NA_Responsables Nresp = new NA_Responsables();
                    string usuarioAux = Session["NameUser"].ToString();
                    string passwordAux = Session["passworuser"].ToString();
                    int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                    /*---------------------------------------------*/                                        
                    bool banderaEliminarReceta = pp.eliminar_Receta(codigoReceta, codUser);
                    if (banderaEliminarReceta == true) {
                    buscarRecetas("");
                    limpiarDatos();
                        Response.Write("<script type='text/javascript'> alert('Eliminado: OK!') </script>");
                    }else
                        Response.Write("<script type='text/javascript'> alert('Error: Eliminado!') </script>");
             }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Seleccionar Receta!') </script>");
           // }
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {

        }

        protected void gv_reciboIngresoEgreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarRecetaConTodoAsignado();
        }

        private void seleccionarRecetaConTodoAsignado()
        {
            int codigoReceta;
            int.TryParse(gv_recetasCreada.SelectedRow.Cells[1].Text, out codigoReceta);
            string nombreReceta = gv_recetasCreada.SelectedRow.Cells[2].Text;
            int codigoProductoAsignado;
            int.TryParse(gv_recetasCreada.SelectedRow.Cells[3].Text, out codigoProductoAsignado);
            string nombreProductoAsignado = gv_recetasCreada.SelectedRow.Cells[4].Text;
            decimal candidaPorDia;
            decimal.TryParse(gv_recetasCreada.SelectedRow.Cells[5].Text.Replace(".",","), out candidaPorDia);
            tx_cantidadPorDia.Text = candidaPorDia.ToString();
            
            NCorpal_Produccion pp = new NCorpal_Produccion();
            DataSet ddss = pp.get_Receta(codigoReceta);
            string prueba = ddss.Tables[0].Rows[0][5].ToString();
            bool banderaPP;
            bool.TryParse(prueba, out banderaPP);
            cbx_condicionante.Checked = banderaPP;

            tx_nameReceta.Text = nombreReceta;
            dd_productosNax.SelectedValue = codigoProductoAsignado.ToString();
                        
            DataSet tuplasInsumos = pp.get_insumosdeReceta(codigoReceta);
            DataTable datoInsumo = Session["listaInsumo"] as DataTable;
            datoInsumo = tuplasInsumos.Tables[0];
            Session["listaInsumo"] = datoInsumo;
            gv_Insumos.DataSource = datoInsumo;
            gv_Insumos.DataBind();

            DataSet tuplasInsumosCompuesto = pp.get_insumosCompuestodeReceta(codigoReceta);
            DataTable datoInsumoCompuesto = Session["listaInsumoCompuesto"] as DataTable;
            datoInsumoCompuesto = tuplasInsumosCompuesto.Tables[0];
            Session["listaInsumoCompuesto"] = datoInsumoCompuesto;
            gv_InsumosCompuesto.DataSource = datoInsumoCompuesto;
            gv_InsumosCompuesto.DataBind();

        }

        protected void gv_Insumos_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionInsmosReceta();
        }


        private void seleccionInsmosReceta()
        {
            int codigoI;
            int.TryParse(gv_Insumos.SelectedRow.Cells[2].Text, out codigoI);
            string insumo = gv_Insumos.SelectedRow.Cells[3].Text;
            decimal cantidad;
            decimal.TryParse(gv_Insumos.SelectedRow.Cells[4].Text.Replace(".", ","), out cantidad);

            tx_insumos.Text = insumo;
            tx_cantInsumos.Text = cantidad.ToString();
        }

        protected void gv_InsumosCompuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codigoI;
            int.TryParse(gv_InsumosCompuesto.SelectedRow.Cells[2].Text, out codigoI);
            string insumo = gv_InsumosCompuesto.SelectedRow.Cells[3].Text;
            decimal cantidad;
            decimal.TryParse(gv_InsumosCompuesto.SelectedRow.Cells[4].Text.Replace(".", ","), out cantidad);

            tx_insumosCompuesto.Text = insumo;
            tx_cantInsumosCompuesto.Text = cantidad.ToString();
        }

        protected void gv_Insumos_Deleted(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;
            eliminado_DatosInsumos(index);
        }



        private void eliminado_DatosInsumos(int index_Seleccionado)
        {
            /*   NCorpal_Produccion pp = new NCorpal_Produccion();            
               if (gv_recetasCreada.SelectedIndex >= 0)
               {
                   int codigoInsumo;
                   int.TryParse(gv_Insumos.Rows[index_Seleccionado].Cells[2].Text, out codigoInsumo);

                   int codigoReceta;
                   int.TryParse(gv_recetasCreada.SelectedRow.Cells[1].Text, out codigoReceta);
                   bool bandera = pp.eliminar_insumoReceta(codigoReceta, codigoInsumo);

                   if (bandera == true)
                   {
                       DataSet tuplasInsumos = pp.get_insumosdeReceta(codigoReceta);
                       DataTable datoInsumo = Session["listaInsumo"] as DataTable;
                       datoInsumo = tuplasInsumos.Tables[0];
                       Session["listaInsumo"] = datoInsumo;
                       gv_Insumos.DataSource = datoInsumo;
                       gv_Insumos.DataBind();
                   }
               }
               else
               {   */
                int index = index_Seleccionado;
                DataTable datoInsumo = Session["listaInsumo"] as DataTable;
                datoInsumo.Rows[index].Delete();
                datoInsumo.AcceptChanges();

                gv_Insumos.EditIndex = -1;

                Session["listaInsumo"] = datoInsumo;
                gv_Insumos.DataSource = datoInsumo;
                gv_Insumos.DataBind();
           // }
        }

        protected void gv_InsumosCompuesto_Deleted(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;
            eliminado_DatosInsumosCompuesto(index);
        }

        private void eliminado_DatosInsumosCompuesto(int index_seleccionado)
        {
           /* NCorpal_Produccion pp = new NCorpal_Produccion();            
            if (gv_recetasCreada.SelectedIndex >= 0)
            {
                int codigoInsumo;
                int.TryParse(gv_InsumosCompuesto.Rows[index_seleccionado].Cells[2].Text, out codigoInsumo);

                int codigoReceta;
                int.TryParse(gv_recetasCreada.SelectedRow.Cells[1].Text, out codigoReceta);
                bool bandera = pp.eliminar_insumoCompuestoReceta(codigoReceta, codigoInsumo);

                if (!bandera)
                {
                    DataSet tuplasInsumos = pp.get_insumosCompuestodeReceta(codigoReceta);
                    DataTable datoInsumo = Session["listaInsumoCompuesto"] as DataTable;
                    datoInsumo = tuplasInsumos.Tables[0];
                    Session["listaInsumoCompuesto"] = datoInsumo;
                    gv_InsumosCompuesto.DataSource = datoInsumo;
                    gv_InsumosCompuesto.DataBind();
                }
            }
            else
            {   */
                int index = index_seleccionado;
                DataTable datoInsumo = Session["listaInsumoCompuesto"] as DataTable;
                datoInsumo.Rows[index].Delete();
                datoInsumo.AcceptChanges();

                gv_InsumosCompuesto.EditIndex = -1;

                Session["listaInsumoCompuesto"] = datoInsumo;
                gv_InsumosCompuesto.DataSource = datoInsumo;
                gv_InsumosCompuesto.DataBind();
          //  }
        }

        protected void bt_updateInsumos_Click(object sender, EventArgs e)
        {
            actualizarInsumoDato();
        }

        private void actualizarInsumoDato()
        {
            if (gv_Insumos.SelectedIndex >= 0) {
                string insumos = gv_Insumos.SelectedRow.Cells[3].Text;
                int codigoInsumo;
                int.TryParse(gv_Insumos.SelectedRow.Cells[2].Text, out codigoInsumo);
                decimal cantidad;
                decimal.TryParse(tx_cantInsumos.Text.Replace('.', ','), out cantidad);

                int index = gv_Insumos.SelectedIndex;
                DataTable datoInsumo = Session["listaInsumo"] as DataTable;
                DataRow tupla = datoInsumo.Rows[index];
                tupla["Codigo"] = codigoInsumo;
                tupla["Insumo"] = insumos;
                tupla["Cantidad"] = cantidad;
                datoInsumo.AcceptChanges();

                Session["listaInsumo"] = datoInsumo;
                gv_Insumos.DataSource = datoInsumo;
                gv_Insumos.DataBind();
                tx_insumos.Text = "";
                tx_cantInsumos.Text = "";
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccionar Insumo!') </script>");
        }

        protected void bt_InsumosCompuesto_Click(object sender, EventArgs e)
        {
            actualizarInsumoCompuestoDato();
        }

        private void actualizarInsumoCompuestoDato()
        {
            if (gv_InsumosCompuesto.SelectedIndex >= 0)
            {
                string insumos = gv_InsumosCompuesto.SelectedRow.Cells[3].Text;
                int codigoInsumo;
                int.TryParse(gv_InsumosCompuesto.SelectedRow.Cells[2].Text, out codigoInsumo);
                decimal cantidad;
                decimal.TryParse(tx_cantInsumosCompuesto.Text.Replace('.', ','), out cantidad);

                int index = gv_InsumosCompuesto.SelectedIndex;
                DataTable datoInsumo = Session["listaInsumoCompuesto"] as DataTable;
                DataRow tupla = datoInsumo.Rows[index];
                tupla["Codigo"] = codigoInsumo;
                tupla["Insumo"] = insumos;
                tupla["Cantidad"] = cantidad;
                datoInsumo.AcceptChanges();

                Session["listaInsumoCompuesto"] = datoInsumo;
                gv_InsumosCompuesto.DataSource = datoInsumo;
                gv_InsumosCompuesto.DataBind();
                tx_insumos.Text = "";
                tx_cantInsumos.Text = "";
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccionar Insumo!') </script>");
        }
    }
}