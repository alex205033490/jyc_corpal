using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_RutaCobrador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
           /* if (tienePermisoDeIngreso(88) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }*/
          
            if (!IsPostBack)
            {
                DataTable datoProducto = new DataTable();
                datoProducto.Clear();
                datoProducto.Columns.Add("Producto", typeof(string));
                datoProducto.Columns.Add("Cantidad", typeof(string));
                gv_productos.DataSource = datoProducto;
                gv_productos.DataBind();
                Session["listaProducto"] = datoProducto;

                buscardatos("",""); 
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

        public void limpiar() {
            tx_tiendanombre.Text = "";
            tx_personalAsignado.Text = "";
            dd_estado.SelectedIndex = -1;            
            tx_fechaEntrega.Text = "";
            tx_horaEntrega.Text = "";            
            tx_detalleRuta.Text = "";

            DataTable datoProducto = new DataTable();
            datoProducto.Clear();            
            Session["listaProducto"] = datoProducto;
            gv_productos.DataSource = datoProducto;
            gv_productos.DataBind();
        
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
        public static string[] GetlistaTienda2(string prefixText, int count)
        {
            string nombre = prefixText;

            NCorpal_Cliente ntienda = new NCorpal_Cliente();
            DataSet tuplas = ntienda.listarTiendas(nombre);
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
        public static string[] GetlistaResponsable2(string prefixText, int count)
        {
            string nombreResponsable = prefixText;

            NA_Responsables Nrespon = new NA_Responsables();
            DataSet tuplas = Nrespon.mostrarTodos_AutoComplit(nombreResponsable);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }


        protected void bt_grabarcobro_Click(object sender, EventArgs e)
        {
            grabarCobroRuta();
        }

        private void grabarCobroRuta()
        {
            if(gv_productos.Rows.Count > 0){

                 string fechacobro = convertidorFecha(tx_fechaEntrega.Text);
                 string horacobro = tx_horaEntrega.Text;
                 string detalle = tx_detalleRuta.Text;
                 string tiendanombre = tx_tiendanombre.Text;
                 NCorpal_Cliente ntienda = new NCorpal_Cliente();
                 DataSet datoTienda = ntienda.get_ClienteNombre(tiendanombre);
                 int codtienda;
                 int.TryParse(datoTienda.Tables[0].Rows[0][0].ToString(), out codtienda);   
                 string personalasignado = tx_personalAsignado.Text;
                 NA_Responsables nresp = new NA_Responsables();                 
                 int coduserasignado = nresp.getCodigo_NombreResponsable(personalasignado);
                //------------------------------------                
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = nresp.getCodUsuario(usuarioAux, passwordAux); 
                //-------------------------------------
                 int coduserinicio = codUser;
                 string estadoEntrega = dd_estado.SelectedItem.Text;

                 NCorpal_RutaEntrega nrutac = new NCorpal_RutaEntrega();
                 bool bandera = nrutac.guardarDatosRutasEntrega(fechacobro, horacobro, detalle, tiendanombre, codtienda, coduserasignado, personalasignado, coduserinicio, estadoEntrega);
                if (bandera == true)
                {
                    int codigoRutaIngresada = nrutac.get_UltimaRutaIngresada(tiendanombre);                    
                    DataTable datoProducto = Session["listaProducto"] as DataTable;        
            
                    for (int i = 0; i < datoProducto.Rows.Count; i++)
                    {
                        DataRow fila = datoProducto.Rows[i];
                        string Producto = fila["Producto"].ToString();
                        double Cantidad = Convert.ToDouble(fila["Cantidad"].ToString());
                        bool banderaP = nrutac.insertarProductos(codigoRutaIngresada,Producto,Cantidad);
                    }
                    datoProducto.Clear();
                    Session["listaProducto"] = datoProducto;
                    gv_productos.DataSource = datoProducto;
                    gv_productos.DataBind();

                    buscardatos("", "");
                    limpiar();
                    Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Guardado') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: No tiene Productos') </script>");
            
            
        }

            

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string tienda = tx_tiendanombre.Text;
            string personalAsignado = tx_personalAsignado.Text;
            buscardatos(tienda, personalAsignado);
        }

        private void buscardatos(string tienda, string personalAsignado)
        {

            NCorpal_RutaEntrega ncorp = new NCorpal_RutaEntrega();
            DataSet tupla = ncorp.BuscarRutasEntrega(tienda, personalAsignado);
            gv_rutaEntrega.DataSource = tupla;
            gv_rutaEntrega.DataBind();

        }

      


        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiar();            
        }

        protected void bt_eliminarEntrega_Click(object sender, EventArgs e)
        {
            eliminarRuta();
        }

        private void eliminarRuta()
        {
            if(gv_rutaEntrega.SelectedIndex > -1){
                NCorpal_RutaEntrega ncorp = new NCorpal_RutaEntrega();
                int codigo = int.Parse(gv_rutaEntrega.SelectedRow.Cells[1].Text);
                bool bandera = ncorp.eliminarRutaEntrega(codigo);
                if (bandera == true)
                {
                    buscardatos("", "");
                    Response.Write("<script type='text/javascript'> alert('Eliminado: OK') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Eliminar') </script>");
            }
        }

        protected void bt_modificarEntrega_Click(object sender, EventArgs e)
        {
            modificarDatosEntrega();
        }

        private void modificarDatosEntrega()
        {
            if(gv_rutaEntrega.SelectedIndex > -1){

                int codigo = int.Parse(gv_rutaEntrega.SelectedRow.Cells[1].Text);
                 string fechacobro = convertidorFecha(tx_fechaEntrega.Text);
                 string horacobro = tx_horaEntrega.Text;
                 string detalle = tx_detalleRuta.Text;
                 string tiendanombre = tx_tiendanombre.Text;
                 NCorpal_Cliente ntienda = new NCorpal_Cliente();
                 DataSet datoTienda = ntienda.get_ClienteNombre(tiendanombre);
                 int codtienda;
                 int.TryParse(datoTienda.Tables[0].Rows[0][0].ToString(), out codtienda);   
                 string personalasignado = tx_personalAsignado.Text;
                 NA_Responsables nresp = new NA_Responsables();                 
                 int coduserasignado = nresp.getCodigo_NombreResponsable(personalasignado);
                //------------------------------------                
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = nresp.getCodUsuario(usuarioAux, passwordAux); 
                //-------------------------------------
                 int coduserinicio = codUser;
                 string estadoEntrega = dd_estado.SelectedItem.Text;

                 NCorpal_RutaEntrega nrutac = new NCorpal_RutaEntrega();
                 bool bandera = nrutac.updateDatosRutasEntrega(codigo, fechacobro, horacobro, detalle, tiendanombre, codtienda, coduserasignado, personalasignado, coduserinicio, estadoEntrega);
                if (bandera == true)
                {                                    
                    bool bandera3 = nrutac.eliminarProductosRutaEntreta(codigo);
                    DataTable datoProducto = Session["listaProducto"] as DataTable;   
                    for (int i = 0; i < datoProducto.Rows.Count; i++)
                    {
                        DataRow fila = datoProducto.Rows[i];
                        string Producto = fila["Producto"].ToString();
                        double Cantidad = Convert.ToDouble(fila["Cantidad"].ToString());
                        bool banderaP = nrutac.insertarProductos(codigo,Producto,Cantidad);
                    }
                    datoProducto.Clear();
                    Session["listaProducto"] = datoProducto;

                    buscardatos("", "");
                    limpiar();
                    Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Guardado') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Guardado') </script>");
            
        }

        protected void bt_agregarProducto_Click(object sender, EventArgs e)
        {
            agregarProductosRuta();
        }

        private void agregarProductosRuta()
        {
            DataTable datoProducto = Session["listaProducto"] as DataTable;         
            string producto = HttpUtility.HtmlDecode(tx_producto.Text);
            float Cantidad;
            float.TryParse(tx_cantidadProducto.Text.Replace('.',','), out Cantidad);
            if(Cantidad>0){
                DataRow tupla = datoProducto.NewRow();
                tupla["Producto"] = producto;
                tupla["Cantidad"] = Cantidad;
                datoProducto.Rows.Add(tupla);

                gv_productos.DataSource = datoProducto;
                gv_productos.DataBind();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Cantidad: Menor o Igual a Cero') </script>");
        }

        protected void bt_eliminarProducto_Click(object sender, EventArgs e)
        {
            eliminarProductoRutaEntrega();
        }

        private void eliminarProductoRutaEntrega()
        {
            DataTable datoProducto = Session["listaProducto"] as DataTable;
            int i= 0 ;
            foreach (GridViewRow row in gv_productos.Rows)
            {
              
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked == true)
                    {
                        
                        int index = row.RowIndex-i;                           
                        datoProducto.Rows[index].Delete();
                        datoProducto.AcceptChanges();
                        i++;
                    }
                }
            }

            Session["listaProducto"] = datoProducto;
            gv_productos.DataSource = datoProducto;
            gv_productos.DataBind();     
            
        }

        protected void gv_rutaEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarSelecciondelaRuta();
        }

        private void mostrarSelecciondelaRuta()
        {

            tx_fechaEntrega.Text = gv_rutaEntrega.SelectedRow.Cells[4].Text;
            tx_horaEntrega.Text = gv_rutaEntrega.SelectedRow.Cells[5].Text;
            tx_tiendanombre.Text = gv_rutaEntrega.SelectedRow.Cells[6].Text;
            tx_detalleRuta.Text = gv_rutaEntrega.SelectedRow.Cells[7].Text;
            tx_personalAsignado.Text = gv_rutaEntrega.SelectedRow.Cells[8].Text;
            dd_estado.SelectedValue = gv_rutaEntrega.SelectedRow.Cells[9].Text;
            int codigoRuta = int.Parse(gv_rutaEntrega.SelectedRow.Cells[1].Text);

            NCorpal_RutaEntrega nrutac = new NCorpal_RutaEntrega();
            DataSet listpro = nrutac.get_productosAdicionados(codigoRuta);

            DataTable datoProducto = Session["listaProducto"] as DataTable;
            datoProducto.Clear();

            for (int i = 0; i < listpro.Tables[0].Rows.Count; i++ )
            {
                string producto = HttpUtility.HtmlDecode(listpro.Tables[0].Rows[i][0].ToString());
                float Cantidad;
                float.TryParse(listpro.Tables[0].Rows[i][1].ToString().Replace('.', ','), out Cantidad);
                if (Cantidad > 0)
                {
                    DataRow tupla = datoProducto.NewRow();
                    tupla["Producto"] = producto;
                    tupla["Cantidad"] = Cantidad;
                    datoProducto.Rows.Add(tupla);                    
                }
            }
            Session["listaProducto"] = datoProducto;
            gv_productos.DataSource = datoProducto;
            gv_productos.DataBind();
            
        }

    }
}