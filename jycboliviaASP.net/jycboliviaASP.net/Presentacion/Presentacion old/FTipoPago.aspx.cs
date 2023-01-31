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
    public partial class FTipoPago : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();


            if (tienePermisoDeIngreso(6) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            listar();            
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

        private void registrar()
        {
            NTipoPago tipoPago = new NTipoPago();
            string nombre = txtNombre.Text;
            int estado = 1;
            tipoPago.registrar(nombre, estado );

            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ha Insertado un Tipo de Pago "+ nombre );
        }

        private void listar()
        {
            NTipoPago tipoPago = new NTipoPago();
            DataSet lista = tipoPago.listar();
            GridView1.DataSource = lista;
            GridView1.DataBind();
        }

        private void modificar() 
        {
            NTipoPago tipoPago = new NTipoPago();
            string codigo = GridView1.SelectedRow.Cells[2].Text;
            string nombre = txtNombre.Text;

            tipoPago.modificar(Convert.ToInt32(codigo), nombre);

            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ha Modificado un Tipo de Pago "+ nombre);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            registrar();
            Response.Write("<script type='text/javascript'> alert('Se ha registrado correctamente') </script>");
            listar();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificar();
            Response.Write("<script type='text/javascript'> alert('Se ha modificado correctamente') </script>");
            listar();
        }

        protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            int posicion = e.RowIndex;
            NTipoPago tipoPago = new NTipoPago();
            string codigo = GridView1.DataKeys[posicion].Value.ToString();
            tipoPago.eliminar(Convert.ToInt32(codigo));
            Response.Write("<script type='text/javascript'> alert('Se ha eliminaado correctamente') </script>");
            listar();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNombre.Text = GridView1.SelectedRow.Cells[3].Text;
        }

    }
}