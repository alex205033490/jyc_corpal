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
    public partial class FA_AgendaTelefonica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(40) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            
            if(!IsPostBack){
            buscarDatos("");            
            }
            desactivarDatos();

            if (tienePermisoDeIngreso(93) == true)
            {
                activarBotones();
            }
        }

        private void activarBotones()
        {
            lb_botondescargar.Visible = true;
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

        private void desactivarDatos()
        {
            tx_codigo.Enabled = false;
            lb_botondescargar.Visible = false;
        }


        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProyectosAgenda(string prefixText, int count)
        {
            string nombreProyecto = prefixText;

            NA_AgendaTelefonica agenda = new NA_AgendaTelefonica();
            DataSet datos = agenda.mostrarDatos2(nombreProyecto);
            string[] lista = new string[datos.Tables[0].Rows.Count];
            int fin = datos.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = datos.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }


        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            buscarDatos(tx_nombrebusqueda.Text);
        }

        public void buscarDatos(string nombreBusqueda) {
            NA_AgendaTelefonica agenda = new NA_AgendaTelefonica();
            DataSet datos = agenda.mostrarDatos(nombreBusqueda);
            gv_datos.DataSource = datos;
            gv_datos.DataBind();         
        }

        protected void bt_adicionar_Click(object sender, EventArgs e)
        {
            insertarDatos();            
        }

        private void insertarDatos()
        {
           //int codigo = Convert.ToInt32(tx_codigo.Text);
           string nombre = tx_nombreCliente.Text;
           string direccion = tx_direccion.Text;
           string telefono = tx_telefono.Text;
           string celular1 = tx_celular1.Text;
           string celular2 = tx_celular2.Text;
           string celular3 = tx_celular3.Text;
           string celular4 = tx_celular4.Text;
           string email1 = tx_email1.Text;
           string email2 = tx_email2.Text;
           string email3 = tx_mail3.Text;
           string email4 = tx_email4.Text;
           string fax = tx_fax.Text;
           string nota = tx_nota.Text;

           NA_AgendaTelefonica agenda = new NA_AgendaTelefonica();
           agenda.insertar(nombre, direccion, telefono, celular1, celular2, celular3, celular4, email1, email2, email3, email4, fax, nota);
           //-----------------------historial
           int codUser = Convert.ToInt32(Session["coduser"].ToString());
           NA_Historial nhistorial = new NA_Historial();
           nhistorial.insertar(codUser, "Ha adicionado un nuevo registro "+nombre);
           //----------------- historial
           buscarDatos(nombre); 
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            modificarDatos();
        }

        private void modificarDatos()
        {
          if(gv_datos.SelectedIndex > -1){
            int codigo = Convert.ToInt32(tx_codigo.Text);
            string nombre = tx_nombreCliente.Text;
            string direccion = tx_direccion.Text;
            string telefono = tx_telefono.Text;
            string celular1 = tx_celular1.Text;
            string celular2 = tx_celular2.Text;
            string celular3 = tx_celular3.Text;
            string celular4 = tx_celular4.Text;
            string email1 = tx_email1.Text;
            string email2 = tx_email2.Text;
            string email3 = tx_mail3.Text;
            string email4 = tx_email4.Text;
            string fax = tx_fax.Text;
            string nota = tx_nota.Text;

            NA_AgendaTelefonica agenda = new NA_AgendaTelefonica();
            agenda.modificar(codigo,nombre, direccion, telefono, celular1, celular2, celular3, celular4, email1, email2, email3, email4, fax, nota);
            //-----------------------historial
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            NA_Historial nhistorial = new NA_Historial();
            nhistorial.insertar(codUser, "Ha modificado la agenda Telefonica del codigo = " +codigo+" con el nombre "+nombre);
            //----------------- historial
            buscarDatos(nombre);           
          }else
              Response.Write("<script type='text/javascript'> alert('Error: No ha seleccionado Dato') </script>");
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarDato();
        }

        private void eliminarDato()
        {
            int codigo = Convert.ToInt32(tx_codigo.Text);
            NA_AgendaTelefonica agenda = new NA_AgendaTelefonica();
            agenda.eliminar(codigo);
            //-----------------------historial
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            NA_Historial nhistorial = new NA_Historial();
            nhistorial.insertar(codUser, "Ha eliminado un registro el codigo " + codigo);
            //----------------- historial
            buscarDatos(""); 
        }

        protected void gv_datos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_datos.SelectedRow.RowIndex;
            if(index > -1){
                tx_codigo.Text = gv_datos.SelectedRow.Cells[1].Text;
                
                if (gv_datos.SelectedRow.Cells[2].Text != "&nbsp;")
                    tx_nombreCliente.Text = gv_datos.SelectedRow.Cells[2].Text;
                else
                    tx_nombreCliente.Text = "";

                if (gv_datos.SelectedRow.Cells[3].Text != "&nbsp;")                
                tx_direccion.Text = gv_datos.SelectedRow.Cells[3].Text;
                else
                tx_direccion.Text = "";

                if (gv_datos.SelectedRow.Cells[4].Text != "&nbsp;")
                    tx_telefono.Text = gv_datos.SelectedRow.Cells[4].Text;
                else
                    tx_telefono.Text = "";

                if (gv_datos.SelectedRow.Cells[5].Text != "&nbsp;")
                    tx_celular1.Text = gv_datos.SelectedRow.Cells[5].Text;
                else
                    tx_celular1.Text = "";

                if (gv_datos.SelectedRow.Cells[6].Text != "&nbsp;")
                    tx_celular2.Text = gv_datos.SelectedRow.Cells[6].Text;
                else
                    tx_celular2.Text = "";

                if (gv_datos.SelectedRow.Cells[7].Text != "&nbsp;")
                    tx_celular3.Text = gv_datos.SelectedRow.Cells[7].Text;
                else
                    tx_celular3.Text = "";

                if (gv_datos.SelectedRow.Cells[8].Text != "&nbsp;")
                    tx_celular4.Text = gv_datos.SelectedRow.Cells[8].Text;
                else
                    tx_celular4.Text = "";


                if (gv_datos.SelectedRow.Cells[9].Text != "&nbsp;")
                    tx_email1.Text = gv_datos.SelectedRow.Cells[9].Text;
                else
                    tx_email1.Text = "";

                if (gv_datos.SelectedRow.Cells[10].Text != "&nbsp;")
                    tx_email2.Text = gv_datos.SelectedRow.Cells[10].Text;
                else
                    tx_email2.Text = "";

                if (gv_datos.SelectedRow.Cells[11].Text != "&nbsp;")
                    tx_mail3.Text = gv_datos.SelectedRow.Cells[11].Text;
                else
                    tx_mail3.Text = "";

                if (gv_datos.SelectedRow.Cells[12].Text != "&nbsp;")
                    tx_email4.Text = gv_datos.SelectedRow.Cells[12].Text;
                else
                    tx_email4.Text = "";

                if (gv_datos.SelectedRow.Cells[13].Text != "&nbsp;")
                    tx_fax.Text = gv_datos.SelectedRow.Cells[13].Text;
                else
                    tx_fax.Text = "";

                if (gv_datos.SelectedRow.Cells[14].Text != "&nbsp;")
                    tx_nota.Text = gv_datos.SelectedRow.Cells[14].Text;
                else
                    tx_nota.Text = "";

            }
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiartodo();
        }

        private void limpiartodo()
        {
            tx_celular1.Text = "";
            tx_celular2.Text = "";
            tx_celular3.Text = "";
            tx_celular4.Text = "";
            tx_codigo.Text = "";
            tx_direccion.Text = "";
            tx_email1.Text = "";
            tx_email2.Text = "";
            tx_email4.Text = "";
            tx_fax.Text = "";
            tx_mail3.Text = "";
            tx_nombreCliente.Text = "";
            tx_nota.Text = "";
            tx_telefono.Text = "";         

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            excel_download();
        }


        protected void excel_download()
        {

            string detalleAbuscar = tx_nombrebusqueda.Text;

            NA_AgendaTelefonica agenda = new NA_AgendaTelefonica();
            DataSet datos = agenda.mostrarDatos(detalleAbuscar);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Agenda Telefonica - " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = datos;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }
    

    }
}