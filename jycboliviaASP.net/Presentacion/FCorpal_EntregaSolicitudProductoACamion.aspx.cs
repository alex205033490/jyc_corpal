using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_EntregaSolicitudProductoACamion_ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mostrarRegistrosSolicitudProductos("","","Abierto");
                cargarVehiculos();
            }
            NA_Responsables resp = new NA_Responsables();
            string usu = Session["NameUser"].ToString();
            string pass = Session["passworuser"].ToString();

            int codUser = resp.getCodUsuario(usu, pass);
            txt_entregoProducto.Text = resp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
        }




        /* MOSTRAR Registros GV*/
        private void mostrarRegistrosSolicitudProductos(string nroSolicitud, string solicitante, string estadoSolicitud)
        {
            NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();
            DataSet datos = negocio.get_RegistrosSolicitudPedidos(nroSolicitud, solicitante, estadoSolicitud);
            gv_listRegistros.DataSource = datos;
            gv_listRegistros.DataBind();
        }

        // SELECTED GV registros
        protected void gv_listRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        // metodo mostrar 1 Registro
        /*private void seleccionarRegistro()
        {
            try
            {
                if(gv_listRegistros.SelectedIndex > -1)
                {
                    int codigoSolicitud = int.Parse(gv_listRegistros.SelectedRow.Cells[1].Text);
                    string nroboleta = gv_listRegistros.SelectedRow.Cells[2].Text;
                    string fechaEntrega = gv_listRegistros.SelectedRow.Cells[4].Text;
                    string personalSolicitud = gv_listRegistros.SelectedRow.Cells[5].Text;

                    dd_estadoCierre.SelectedValue = gv_listRegistros.SelectedRow.Cells[6].Text;

                    txt_nroSolicitud.Text = nroboleta;
                    txt_SolicitanteProducto.Text = personalSolicitud;
                    txt_fechaEntrega.Text = fechaEntrega;
                    
                    NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();
                    DataSet datos = negocio.get_DetSolicitudesRealizadas(codigoSolicitud);
                    gv_detListRegistros.DataSource = datos;
                    gv_detListRegistros.DataBind();
                }
            }
            catch (Exception ex)
            {
                showalert($"Error al seleccionar el registro. {ex.Message}");
            }
        }*/

        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        // BTN buscar registro
        protected void btn_buscarRegistro_Click(object sender, EventArgs e)
        {
            string nroSolicitud = txt_nroSolicitud.Text;
            string solicitante = txt_SolicitanteProducto.Text;
            string estado = "Abierto";
            buscarDatosRegistro(nroSolicitud, solicitante, estado);
        }
        private void buscarDatosRegistro(string nroSolicitud, string solicitante, string estadoSolicitud)
        {
            NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();
            DataSet datos = negocio.get_RegistrosSolicitudPedidos(nroSolicitud, solicitante, estadoSolicitud);
            gv_listRegistros.DataSource = datos;
            gv_listRegistros.DataBind();

        }
        // AUTOCOMPLETE
        [WebMethod]
        [ScriptMethod]
        public static string[] getListNroBoletas (string prefixText, int count)
        {
            string nroBoleta = prefixText;

            NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();
            DataSet tuplas = negocio.get_showNroBoleta(nroBoleta);

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
        public static string[] getListPersonalSolicitante(string prefixText, int count)
        {
            string nombre = prefixText;
            NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();
            DataSet tuplas = negocio.get_showPersonalSolicitante(nombre);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for(int i =0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();

            }
            return lista;
        }











        // Cargar Vehiculos 
        private void cargarVehiculos()
        {
            NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();

            DataSet dsVehiculos = negocio.get_ShowVehiculos();

            if(dsVehiculos != null && dsVehiculos.Tables.Count > 0)
            {
                dd_vehiculos.DataSource = dsVehiculos.Tables[0];
                dd_vehiculos.DataTextField = "detalle";
                dd_vehiculos.DataValueField = "codigo";

                dd_vehiculos.DataBind();

                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Seleccione un Vehiculo", "0");
                dd_vehiculos.Items.Insert(0, li);
            }
        }

       


        // DD VEHICULO


        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txt_nroSolicitud.Text = string.Empty;
            txt_SolicitanteProducto.Text = string.Empty;
            dd_vehiculos.SelectedIndex = 0;

            mostrarRegistrosSolicitudProductos("", "", "Abierto");
        }

        

        // UPDATE agregar vehiculo a pedido
        protected void btn_registrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ProductosSeleccionados())
                {
                    showalert("Debe seleccionar al menos un pedido.");
                    return;
                }

                if (!IsVehiculoSeleccionado())
                {
                    showalert("Debe seleccionar 1 vehiculo.");
                    return;
                }
                bool resultadoGeneral = RegistrarProductosConVehiculo();

                if (resultadoGeneral)
                {
                    showalert("Registro insertado exitosamente.");
                    mostrarRegistrosSolicitudProductos("","","Abierto");
                    LimpiarCampos();
                } else
                {
                    showalert("Hubo un error al insertar el registro.");
                }
            }
            catch(Exception ex)
            {
                showalert($"Error: {ex.Message}");
            }
        }


        private int obtenerCodResponsable()
        {
            try
            {
                NA_Responsables negocio = new NA_Responsables();
                string usu = Session["NameUser"].ToString();
                string pass = Session["passworuser"].ToString();
                return negocio.getCodUsuario(usu, pass);
            }
            catch(Exception ex)
            {
                showalert($"Error al obtener el codigo del responsable. {ex.Message}");
                return 0;
            }
        }
        private bool RegistrarProductosConVehiculo()
        {
            bool resultadoGeneral = true;
            NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();

            foreach(GridViewRow row in gv_listRegistros.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.Cells[0].FindControl("chkSelect");

                if(chkSelect != null && chkSelect.Checked)
                {
                    int codSolicitud = Convert.ToInt32(row.Cells[1].Text);
                    int codProducto = Convert.ToInt32(row.Cells[7].Text);
                    int codResponsable = obtenerCodResponsable();
                    int codCar = int.Parse(dd_vehiculos.SelectedValue);

                    bool resultado = negocio.UpdateADDVehiculoAPedido(codCar, codResponsable, codSolicitud, codProducto);

                    if (!resultado)
                    {
                        resultadoGeneral = false;
                        break;
                    }
                }

            }
            return resultadoGeneral;
        }
        private bool IsVehiculoSeleccionado()
        {
            return dd_vehiculos.SelectedIndex != 0;
        }
        private bool ProductosSeleccionados()
        {
            foreach(GridViewRow row in gv_listRegistros.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.Cells[0].FindControl("chkSelect");
                if(chkSelect != null && chkSelect.Checked)
                {
                    return true;
                }
            }
            return false;
        }

        private void MostrarDetVehiculos()
        {
            NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();
            DataSet datos = negocio.get_ShowVehiculos();
            gv_detCar.DataSource = datos;
            gv_detCar.DataBind();
        }

        protected void dd_vehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int codigo = int.Parse(dd_vehiculos.SelectedValue);
                NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();

                DataSet vehiculoData = negocio.get_detVehiculo(codigo);

                if (vehiculoData.Tables[0].Rows.Count > 0)
                {
                    gv_detCar.DataSource = vehiculoData;
                    gv_detCar.DataBind();
                    gv_detCar.Visible = true;

                } else
                {
                    gv_detCar.Visible = false; 
                }
            }
            catch(Exception ex)
            {
                showalert($"Error al cargar los datos. {ex.Message}");
            }

        }
    }
}