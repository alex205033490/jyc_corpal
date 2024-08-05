using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using MySql.Data;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net

{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(1) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }  

           if(!IsPostBack){
            cargarCargo();
            cargarResponsables();

            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ha ingresado a Gestionar Responsable");
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

        protected void cargarResponsables() {
            NA_Responsables Nresp = new NA_Responsables();
            DataSet datoTabla = Nresp.mostrarTodosDatos();
            GridView1.DataSource = datoTabla;
            GridView1.DataBind();
        
        }

        protected void cargarCargo() {           
               NA_Cargo Ncargo = new NA_Cargo();
               DataSet datosCargo = Ncargo.mostrarAllDatos();
               cb_cargo.DataSource = datosCargo;
               cb_cargo.DataValueField = "codigo";
               cb_cargo.DataTextField = "nombre";
               cb_cargo.Items.Add(new ListItem("", "-1"));
               cb_cargo.AppendDataBoundItems = true;
               cb_cargo.SelectedIndex = -1;
               cb_cargo.DataBind();            
        }

        protected void limpiarDato() {
            tx_celular.Text = "";
            tx_ciudad.Text = "";
            tx_direccion.Text = "";
            tx_email.Text = "";
            tx_nombre.Text = "";
            tx_telefono.Text = "";
            tx_usuario.Text = "";
            cb_cargo.SelectedIndex = -1;
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
          
            string nombre = tx_nombre.Text;
            string direccion = tx_direccion.Text;
            string telefono = tx_telefono.Text;
            string celular = tx_celular.Text;
            string email = tx_email.Text;
            string dpto = cb_dpto.Text;
            string ciudad = tx_ciudad.Text;
            int sueldo = 0;
            string usuario = tx_usuario.Text;
            string password = tx_Password.Value;
            int cargo = Convert.ToInt32(cb_cargo.SelectedValue);
            NA_Responsables Nresp = new NA_Responsables();
            if (!Nresp.ExisteUsuario(usuario))
            {

                Nresp.insertarResponsable(nombre, direccion, telefono, celular, email, dpto, ciudad, sueldo, usuario, password, cargo, 1);
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha ingresado el Responsable " + nombre);
                DataSet datoTabla = Nresp.mostrarTodosDatos();
                GridView1.DataSource = datoTabla;
                GridView1.DataBind();
                limpiarDato();

            }
            else
            {
                Response.Write("<script type='text/javascript'> alert('ERROR : El Usuario ya Existe') </script>");
            }
           
        }

        protected void modificarDatos() {
            int index = GridView1.SelectedIndex;
            if (index >= 0)
            {
                int codigo = Convert.ToInt32(GridView1.SelectedRow.Cells[2].Text);
                string nombre = tx_nombre.Text;
                string direccion = tx_direccion.Text;
                string telefono = tx_telefono.Text;
                string celular = tx_celular.Text;
                string email = tx_email.Text;
                string dpto = cb_dpto.Text;
                string ciudad = tx_ciudad.Text;
                int sueldo = 0;
                string usuario = tx_usuario.Text;
                string password = tx_Password.Value;
                int cargo = Convert.ToInt32(cb_cargo.SelectedValue);

                NA_Responsables Nresp = new NA_Responsables();
                Nresp.modificarResponsable(codigo, nombre, direccion, telefono, celular, email, dpto, ciudad, sueldo, usuario, password, cargo, 1);

                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha modificado el responsable con codigo "+codigo);
                
                DataSet datoTabla = Nresp.mostrarTodosDatos();
                GridView1.DataSource = datoTabla;
                GridView1.DataBind();
            }
            else
            {
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione una Fila de la Tabla') </script>");
               // bt_modificar.Attributes.Add("onclick", "javascript:alert('" + "Error: Seleccione una Fila de la Tabla" + "');");
            } 
                 
        }



        protected void cargarDatosSeleccionado() {
            int indexSelect = Convert.ToInt32(GridView1.SelectedRow.Cells[2].Text);  
            NA_Responsables Nresp = new NA_Responsables();
            DataSet tuplaResp = Nresp.get_responsable(indexSelect);

          tx_nombre.Text = tuplaResp.Tables[0].Rows[0][1].ToString();
          tx_direccion.Text = tuplaResp.Tables[0].Rows[0][2].ToString();
          tx_telefono.Text = tuplaResp.Tables[0].Rows[0][3].ToString();
          tx_celular.Text = tuplaResp.Tables[0].Rows[0][4].ToString();
          tx_email.Text = tuplaResp.Tables[0].Rows[0][5].ToString();
          cb_dpto.SelectedValue = tuplaResp.Tables[0].Rows[0][6].ToString();
          tx_ciudad.Text = tuplaResp.Tables[0].Rows[0][7].ToString();
          cb_cargo.SelectedValue = tuplaResp.Tables[0].Rows[0][11].ToString();
          tx_usuario.Text = tuplaResp.Tables[0].Rows[0][9].ToString();
        }

        protected void eliminarSeleccionado() {
            
                NA_Responsables Nresp = new NA_Responsables();
                CheckBox cb = null;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    cb = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("CheckBox1");
                    if (cb != null && cb.Checked)
                    {
                        int codigo = Convert.ToInt32(GridView1.Rows[i].Cells[2].Text);
                        Nresp.eliminarResponsable(codigo);
                    }
                }
                cargarResponsables();

        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatosSeleccionado();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            modificarDatos();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            eliminarSeleccionado();
        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {            
            cargarDatosSeleccionado();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            limpiarDato();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {            
            string nombre = tx_nombre.Text;
            string direccion = tx_direccion.Text;
            string telefono = tx_telefono.Text;
            string celular = tx_celular.Text;
            string email = tx_email.Text;
            string dpto = cb_dpto.Text;
            string ciudad = tx_ciudad.Text;
            int sueldo = 0;
            string usuario = tx_usuario.Text;
            string password = tx_Password.Value;
            int cargo = Convert.ToInt32(cb_cargo.SelectedValue);

            NA_Responsables Nresp = new NA_Responsables();
            DataSet datoTabla = Nresp.buscar_responsable(nombre,direccion,telefono,celular,email,dpto,ciudad,sueldo,usuario,password,cargo);
            GridView1.DataSource = datoTabla;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            cargarResponsables();
        }

       
    

     
    }
}