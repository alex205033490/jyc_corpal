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
    public partial class FActualizacion : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(3) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }  

            listar();

            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ingresado a Actualizacion");
            //deshabilitar();
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

        private void listar()
        {
            NActualizacion actualizacion = new NActualizacion();
            DataSet lista = actualizacion.listar();
            gvActualizacion.DataSource = lista;
            gvActualizacion.DataBind();
        }

        private void registrar() 
        {
            NActualizacion actualizacion = new NActualizacion();
            string nombre = txtNombre.Text;
            actualizacion.registrar(nombre, 1);

            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ingresado un nuevo item "+ nombre);
        }

        private void modificar() 
        {
            NActualizacion actualizacion = new NActualizacion();
            String codigo = gvActualizacion.SelectedRow.Cells[2].Text;
            string nombre = txtNombre.Text;
            actualizacion.modificar(Convert.ToInt32(codigo), nombre);
            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Modifico un Item "+nombre);
        }

        private void buscar() 
        {
            habilitar();
            NActualizacion actualizacion = new NActualizacion();
            string nombre = txtNombre.Text;
            DataSet lista = actualizacion.buscar(nombre);
            gvActualizacion.DataSource = lista;
            gvActualizacion.DataBind();
        }

        private void limpiar() 
        {
            txtNombre.Text = "";
        }

        private void habilitar() 
        {
            txtNombre.Enabled = true;
        }

        private void deshabilitar() 
        {
            txtNombre.Enabled = false;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            NActualizacion actualizacion = new NActualizacion();
            int posicion = e.RowIndex;
            string codigo = gvActualizacion.DataKeys[posicion].Value.ToString();
            actualizacion.eliminar(Convert.ToInt32(codigo));

            listar();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            registrar();
            Response.Write("<script type='text/javascript'> alert('Se ha registrado correctamente') </script>");
            habilitar();
            limpiar();
            listar();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificar();
            Response.Write("<script type='text/javascript'> alert('Dato Actualizado correctamente') </script>");
            habilitar();
            limpiar();
            listar();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //habilitar();
            txtNombre.Text = gvActualizacion.SelectedRow.Cells[3].Text;
            btnRegistrar.Enabled = false;
            btnBuscar.Enabled = true;
            btnModificar.Enabled = true;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
           // habilitar();
            limpiar();
            btnModificar.Enabled = false;
            btnRegistrar.Enabled = true;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActualizacion.PageIndex = e.NewPageIndex;
            listar();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }
    }
}