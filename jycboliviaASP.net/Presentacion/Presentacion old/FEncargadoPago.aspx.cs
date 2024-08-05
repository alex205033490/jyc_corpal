using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;

namespace jycboliviaASP.net
{
    public partial class FEncargadoPago : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(7) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if( !IsPostBack)
            {
                listar();

                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ingresado a Gestionar Encargado de Pago");
            }
            
            //deshabilitarTexBox();
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

        // --------------------  Metodos  ---------------------

        private void registrar()
        {
            NEncargadoPago encargadoPago = new NEncargadoPago();
            string nombre = tx_nombre.Text;
            string ci = tx_ci.Text;
            string ciudad = dList_Ciudad.Text;
            string telefono = tx_telefono.Text;
            string celular = tx_celular.Text;
            string direccion = tx_direccion.Text;
            string email = tx_email.Text;
            string facturar_A = tx_facturar_A.Text;
            string nit = tx_nit.Text;
            string nit_;
            if (nit == "")
            {
                nit_ = "0";
            }
            else
            {
                nit_ = nit;
            }
            
            encargadoPago.registrar(nombre, ci + ciudad, telefono, celular, direccion, email, facturar_A, nit_, 1,"", "");

            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ha registrado un nuevo encargado "+nombre);
        }

        private void modificar()
        {
            NEncargadoPago encargadoPago = new NEncargadoPago();
            string nombre = tx_nombre.Text;
            string ci = tx_ci.Text;
            string telefono = tx_telefono.Text;
            string celular = tx_celular.Text;
            string direccion = tx_direccion.Text;
            string email = tx_email.Text;
            string facturar_A = tx_facturar_A.Text;
            string nit = tx_nit.Text;
            string nit_;
            string banco = tx_banco.Text;
            string observacion = tx_Observacion.Text;

            if (nit == "")
            {
                nit_ = "0";
            }
            else
            {
                nit_ = nit;
            }

            String codigo = GridView1.SelectedRow.Cells[2].Text;
            encargadoPago.modificar(Convert.ToInt32(codigo), nombre, ci, telefono, celular, direccion, email, facturar_A, nit_ , banco, observacion);

            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ha modificado un nuevo encargado "+nombre);
        }

        private void listar()
        {
            NEncargadoPago encargadoPago = new NEncargadoPago();
            DataSet lista = encargadoPago.listar();
            GridView1.DataSource = lista;
            GridView1.DataBind();
        }

        private void limpiarDatos()
        {
            tx_nombre.Text = "";
            tx_ci.Text = "";
            tx_celular.Text = "";
            tx_telefono.Text = "";
            tx_direccion.Text = "";
            tx_email.Text = "";
            tx_facturar_A.Text = "";
            tx_nit.Text = "";
        }

        private void cargarDatosSeleccion()
        {
            tx_nombre.Text = GridView1.SelectedRow.Cells[3].Text;
            tx_ci.Text = GridView1.SelectedRow.Cells[4].Text;
            tx_telefono.Text = GridView1.SelectedRow.Cells[5].Text;
            tx_celular.Text = GridView1.SelectedRow.Cells[6].Text;
            tx_direccion.Text = GridView1.SelectedRow.Cells[7].Text;
            tx_email.Text = GridView1.SelectedRow.Cells[8].Text;
            tx_facturar_A.Text = GridView1.SelectedRow.Cells[9].Text;
            tx_nit.Text = GridView1.SelectedRow.Cells[10].Text;
        }

    /*    private void deshabilitarTexBox() 
        {
            tx_nombre.Enabled = false;
            tx_ci.Enabled = false;
            dList_Ciudad.Enabled = false;
            tx_telefono.Enabled = false;
            tx_celular.Enabled = false;
            tx_direccion.Enabled = false;
            tx_email.Enabled = false;
            tx_facturar_A.Enabled = false;
            tx_nit.Enabled = false;
        } */

        private void habilitarTexBox()
        {
            tx_nombre.Enabled = true;
            tx_ci.Enabled = true;
            dList_Ciudad.Enabled = true;
            tx_telefono.Enabled = true;
            tx_celular.Enabled = true;
            tx_direccion.Enabled = true;
            tx_email.Enabled = true;
            tx_facturar_A.Enabled = true;
            tx_nit.Enabled = true;
        }

     /*   private void deshabilitarBoton() 
        {
            btnRegistrar.Enabled = false;
            btnNuevo.Enabled = false;
            btnBuscar.Enabled = false;
            btnModificar.Enabled = false;
        }*/

        //--------------------------------------------

        protected void btn_Registrar(object sender, EventArgs e)
        {
            //Response.Write("<script type='text/javascript'> confirm('Esta seguro de Registrar') </script>");
            registrar();
            Response.Write("<script type='text/javascript'> alert('Se ha registrado correctamente') </script>");
            limpiarDatos();
            listar();
        }

        protected void btn_Modificar(object sender, EventArgs e)
        {
            modificar();
            Response.Write("<script type='text/javascript'> alert('Se ha modificado correctamente') </script>");
            listar();
            limpiarDatos();
        }

        protected void btn_Listar(object sender, EventArgs e)
        {
            listar();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatosSeleccion();
            habilitarTexBox();
            // deshabilitarBoton();
          //  btnModificar.Enabled = true;
          //  btnNuevo.Enabled = true;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            NEncargadoPago encargadoPago = new NEncargadoPago();
            int posicion = e.RowIndex;
            string codigo = GridView1.DataKeys[posicion].Value.ToString();
            encargadoPago.eliminar(Convert.ToInt32(codigo));

            listar();
        }

        protected void btn_Nuevo(object sender, EventArgs e)
        {
            habilitarTexBox();
            limpiarDatos();
            //btnRegistrar.Enabled = true;
           // btnModificar.Enabled = false;
        }

        protected void btn_Buscar(object sender, EventArgs e)
        {
            NEncargadoPago encargadoPago = new NEncargadoPago();
            //tx_ci.Enabled = true;
            string ci = tx_ci.Text;
            string nombre = tx_nombre.Text;
            DataSet lista = encargadoPago.buscar(nombre,ci);
            GridView1.DataSource = lista;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
                GridView1.PageIndex = e.NewPageIndex;
                listar();
        }

        }
    }


