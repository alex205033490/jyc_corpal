using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(4) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }  


            if(!IsPostBack){
                cargarFormularios();
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha Ingresado a Gestionar Formulario");
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

        private void cargarFormularios() {
            NA_Formularios Nfor = new NA_Formularios();
            DataSet datoTabla = Nfor.mostrarAllDatos();
            gv_formulario.DataSource = datoTabla;
            gv_formulario.DataBind();        
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string nombre = tx_nombreF.Text;
            NA_Formularios Nfor = new NA_Formularios();
            Nfor.insertar(nombre, 1);

            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ha Insertado el formulario "+nombre);

            DataSet datoTabla = Nfor.mostrarAllDatos();
            gv_formulario.DataSource = datoTabla;
            gv_formulario.DataBind();        
        }

        protected void modificarDatos()
        {
            int index = gv_formulario.SelectedIndex;
            if (index >= 0)
            {
                int codigo = Convert.ToInt32(gv_formulario.SelectedRow.Cells[2].Text);
                string nombre = tx_nombreF.Text;
               
                NA_Formularios Nfor = new NA_Formularios();
                Nfor.modificar(codigo,nombre,1);

                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha Modificado el formulario por el nombre "+nombre);

                DataSet datoTabla = Nfor.mostrarAllDatos();
                gv_formulario.DataSource = datoTabla;
                gv_formulario.DataBind();
            }
            else
            {
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione una Fila de la Tabla') </script>");
                // bt_modificar.Attributes.Add("onclick", "javascript:alert('" + "Error: Seleccione una Fila de la Tabla" + "');");
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            modificarDatos();
        }

        protected void cargarDatosSeleccionado()
        {
            tx_nombreF.Text = gv_formulario.SelectedRow.Cells[3].Text;         
        }

        
        protected void eliminarSeleccionado()
        {

            NA_Formularios Nfor = new NA_Formularios();
            CheckBox cb = null;
            for (int i = 0; i < gv_formulario.Rows.Count; i++)
            {
                cb = (CheckBox)gv_formulario.Rows[i].Cells[1].FindControl("CheckBox1");
                if (cb != null && cb.Checked)
                {
                    int codigo = Convert.ToInt32(gv_formulario.Rows[i].Cells[2].Text);
                    Nfor.eliminar(codigo);
                }
            }
            cargarFormularios();

        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarSeleccionado();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string nombre = tx_nombreF.Text;
            NA_Formularios Nfor = new NA_Formularios();
            DataSet datoTabla = Nfor.buscarFormularios(nombre);
            gv_formulario.DataSource = datoTabla;
            gv_formulario.DataBind();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            eliminarSeleccionado();
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            tx_nombreF.Text = "";
        }

        protected void gv_formulario_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatosSeleccionado();
        }
    }
}