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
    public partial class FProyecto : System.Web.UI.Page
    {

        

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(8) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            //lbFechaActual.Text = proyecto.obtenerFechaActual();
            if (!IsPostBack)
            {
                listar();
              cargarddlEncargadoPago();
              cargarZona();
              
            }

          //  desabilitar();
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

        //----------------- METODOS----------------------

        private void listar()
        {
            NProyecto proyecto = new NProyecto();
            
            DataSet lista = proyecto.listar();
            gv_Proyecto.DataSource = lista;
            gv_Proyecto.DataBind();
        }

        private void limpiar() 
        {            
            txNombre.Text = "";
            txDireccion.Text = "";
            ddlEncargadoPago.SelectedIndex = -1;
            dd_Zona.SelectedIndex = -1;
              
        }

        private void cargarddlEncargadoPago()
        {
            
            NEncargadoPago encargadoPago = new NEncargadoPago();
            ddlEncargadoPago.DataSource = encargadoPago.listar();
            ddlEncargadoPago.DataValueField = "codigo";
            ddlEncargadoPago.DataTextField = "nombre";
            ddlEncargadoPago.Items.Add(new ListItem("", "-1"));
            ddlEncargadoPago.AppendDataBoundItems = true;
            ddlEncargadoPago.SelectedIndex = -1;
            ddlEncargadoPago.DataBind();
        }

        private void cargarZona()
        {
            NA_Zona Nzona = new NA_Zona();
            dd_Zona.DataSource = Nzona.mostrarAllDatos();
            dd_Zona.DataValueField = "codigo";
            dd_Zona.DataTextField = "nombre";
            dd_Zona.Items.Add(new ListItem("", "-1"));
            dd_Zona.AppendDataBoundItems = true;
            dd_Zona.SelectedIndex = -1;
            dd_Zona.DataBind();
        }

        
        private void habilitar()
        {
            txNombre.Enabled = true;
            dd_Zona.Enabled = true;
            txDireccion.Enabled = true;            
            ddlEncargadoPago.Enabled = true;
            
        }

      /*  private void desabilitar()
        {
            txNombre.Enabled = false;
            dd_Zona.Enabled = false;
            txDireccion.Enabled = false;
            ddlEncargadoPago.Enabled = false;
            
        }  */

        private void cargarDatosSeleccion()
        {
            NProyecto proyecto = new NProyecto();
            NEncargadoPago encargadoPago = new NEncargadoPago();
            int codigo = Convert.ToInt32(gv_Proyecto.SelectedRow.Cells[2].Text);
            DataSet tuplaResp = proyecto.getProyect(codigo);

            txNombre.Text = tuplaResp.Tables[0].Rows[0][1].ToString();
            txDireccion.Text = tuplaResp.Tables[0].Rows[0][3].ToString();

            string codigoEncargado = tuplaResp.Tables[0].Rows[0][5].ToString();
            string codigoZona = tuplaResp.Tables[0].Rows[0][8].ToString();
            string Departamento = tuplaResp.Tables[0].Rows[0][9].ToString();

            if(codigoEncargado != ""){
            ddlEncargadoPago.SelectedValue = codigoEncargado;
            }else
                ddlEncargadoPago.SelectedValue = "-1";


            if (codigoZona != "")
            {
                dd_Zona.SelectedValue = codigoZona;
            }
            else
                dd_Zona.SelectedValue = "-1";


            if (Departamento != "")
            {
                dd_departamento.SelectedValue = Departamento;
            }
         
        }

        private void registrarProyecto()
        {
            NProyecto proyecto = new NProyecto();
            

            string nombre = txNombre.Text;
            int Codzona = Convert.ToInt32(dd_Zona.SelectedValue);
            string fecha = proyecto.obtenerFechaActual();
            int CodencargadoPago = Convert.ToInt32(ddlEncargadoPago.SelectedValue);
            string direccion = txDireccion.Text;
            string departamento = dd_departamento.SelectedValue;


            //DateTime fecha_ = Convert.ToDateTime(fecha);
            //int dia = fecha_.Day;
            //int mes = fecha_.Month;
            //int anio = fecha_.Year;
            //string _fecha = anio + "/" + mes + "/" + dia;
            
            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ha Insertado un Proyecto " + nombre);

            proyecto.registrar(nombre,nombre,direccion,fecha,CodencargadoPago,codUser,1,Codzona, departamento);
        }

        private void modificarProyecto()
        {
            habilitar();
            NProyecto proyecto = new NProyecto();
            
            string nombreAnterior = gv_Proyecto.SelectedRow.Cells[3].Text;
            int codigoProyecto = Convert.ToInt32(gv_Proyecto.SelectedRow.Cells[2].Text);
            string nombre = txNombre.Text;
            int codzona = Convert.ToInt32(dd_Zona.SelectedValue);
            int codencargadoPago = Convert.ToInt32(ddlEncargadoPago.SelectedValue);
            string direccion = txDireccion.Text;
            string departamento = dd_departamento.SelectedValue;

            //DateTime fecha_ = Convert.ToDateTime(fecha);
            //int dia = fecha_.Day;
            //int mes = fecha_.Month;
            //int anio = fecha_.Year;
            //string _fecha = anio + "/" + mes + "/" + dia;
                        
            int codUser = Convert.ToInt32(Session["coduser"].ToString());                       
            proyecto.modificarPP(codigoProyecto, nombre, direccion,  codencargadoPago, codUser, 1,codzona, departamento);

            NA_Historial nhistorial = new NA_Historial();           
            nhistorial.insertar(codUser, "Modificar Proyecto");
        }

        private void eliminar()
        {
            NProyecto proyecto = new NProyecto();
            
            string codigo = gv_Proyecto.SelectedRow.Cells[2].Text;
            proyecto.eliminar(Convert.ToInt32(codigo));
        }
        //-------------------------------------------------------
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            habilitar();
            limpiar();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            registrarProyecto();
            Response.Write("<script type='text/javascript'> alert('Se ha registrado correctamente') </script>");
            listar();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificarProyecto();
            Response.Write("<script type='text/javascript'> alert('Datos Actualizado') </script>");
            listar();
        }

     
   
        private void buscar(string nombre){
            NProyecto proyecto = new NProyecto();
            
            DataSet resultadoR = proyecto.buscar(nombre);
            gv_Proyecto.DataSource = resultadoR;
            gv_Proyecto.DataBind();
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            string nombreaBuscar = txNombre.Text;
            buscar(nombreaBuscar);
        }

      
        protected void gv_Proyecto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int posicion = e.RowIndex;
            NProyecto proyecto = new NProyecto();
            
            string codigo = gv_Proyecto.DataKeys[posicion].Value.ToString();
            proyecto.eliminar(Convert.ToInt32(codigo));
            Response.Write("<script type='text/javascript'> alert('Se ha eliminado correctamente') </script>");
            listar();
        }

        protected void gv_Proyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatosSeleccion();
            habilitar();
        }
    }
}