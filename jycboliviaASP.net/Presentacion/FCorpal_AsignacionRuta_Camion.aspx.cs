using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Management.Instrumentation;
using Newtonsoft.Json.Linq;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_AsignacionRuta_Camion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (!IsPostBack)
            {
                cargarVehiculosDD();
            }
        }


        // ################ Cargar Clientes DD ################# //
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaClientes(string prefixText, int count)
        {
            string nombrecliente = prefixText;

            NCorpal_Cliente cc = new NCorpal_Cliente();
            DataSet tuplas = cc.get_ClienteNombre(nombrecliente);

            int fin = tuplas.Tables[0].Rows.Count;

            string[] lista = new string[fin];

            for (int i = 0; i < fin; i++)
            {
                string codigo = tuplas.Tables[0].Rows[i][0].ToString();
                string tienda = tuplas.Tables[0].Rows[i][1].ToString();

                lista[i] = codigo + " - " + tienda;
            }
            return lista;
        }


        //########### Cargar datos Vehiculo ########### //
        private void cargarVehiculosDD()
        {
            NCorpal_Vehiculos Nve = new NCorpal_Vehiculos();
            DataSet dsCar = Nve.get_showVehiculoDD();

            if (dsCar != null && dsCar.Tables.Count > 0)
            {
                dd_listVehiculo.DataSource = dsCar.Tables[0];
                dd_listVehiculo.DataTextField = "detalle";
                dd_listVehiculo.DataValueField = "codigo";

                dd_listVehiculo.DataBind();
                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Seleccione un vehiculo", "0");
                dd_listVehiculo.Items.Insert(0, li);
            }
        }


        //######### CARGAR DATOS RUTAS EN GV #########//
        protected void dd_listVehiculo_selectedIndexChanged(object sender, EventArgs e)
        {
            int codCar = int.Parse(dd_listVehiculo.SelectedValue);

            if (codCar >= 1)
            {
                CargarRutasVehiculo(codCar);

                DibujarPuntosDesdeGridView();
            }
            else
            {
                gv_listaRutasDespacho.DataSource = null;
                gv_listaRutasDespacho.DataBind();
            }
        }
        private void CargarRutasVehiculo(int codCar)
        {
            try
            {
                NCorpal_Vehiculos nVehiculo = new NCorpal_Vehiculos();
                var ds = nVehiculo.get_showRutasVehiculosDespachos(codCar);

                if (ds != null && ds.Tables.Count > 0)
                {
                    gv_listaRutasDespacho.DataSource = ds;
                    gv_listaRutasDespacho.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar ruta vehiculos. " + ex.Message);
            }
        }


        //###################   REGISTRAR NUEVO PUNTO - MAPS ################//
        protected void btn_registrarNuevoPunto_Click(object sender, EventArgs e)
        {
            try
            {
                int codCar;
                int codCli;

                if(!int.TryParse(dd_listVehiculo.SelectedValue, out codCar) || codCar <= 0)
                {
                    showalert("Debe seleccionar un vehiculo válido");
                    return;
                }
                if(string.IsNullOrEmpty(tx_newCliente.Text))
                {
                    showalert("Debes seleccionar un cliente válido");
                    return;
                }

                if(!int.TryParse(hf_codCliente.Value, out codCli) || codCli <= 0)
                {
                    showalert("Debes seleccionar un cliente válido");
                    return;
                }

                string car = dd_listVehiculo.SelectedItem.ToString();

                RegistrarNewRutaPuntoEntrega(codCar, car);
            }
            catch(Exception ex)
            {
                showalert("Error inesperado al agregar nuevo punto. "+ ex.Message);
            }
        }
        private void RegistrarNewRutaPuntoEntrega(int codCar, string car)
        {
            try
            {
                NCorpal_Vehiculos nVehiculo = new NCorpal_Vehiculos();
                NCorpal_Cliente nCli = new NCorpal_Cliente();

                int idRuta = nVehiculo.post_NewRegistroRutaEntrega_Asignacion(codCar, car);

                if (idRuta <= 0)
                {
                    showalert("No se pudo registrar la ruta.");
                    return;
                }

                int nroOrden = 0;
                int codCli = int.Parse(hf_codCliente.Value);
                string cli = tx_newCliente.Text.Trim();

                DataSet dsCli = nCli.get_ClienteCodigo(codCli);

                foreach (DataRow rowCli in dsCli.Tables[0].Rows)
                {
                    string dir_lat = rowCli["direccion_lat"].ToString();
                    string dir_lng = rowCli["direccion_lng"].ToString();

                    bool resultPuntos = nVehiculo.post_NewRegistroRutaEntregaPuntos_Asignacion
                                    (nroOrden, idRuta, codCli, cli, dir_lat, dir_lng);

                    if (!resultPuntos)
                    {
                        showalert("Error al registrar el punto del cliente. " + cli);
                        return;
                    }
                    nroOrden++;
                }
                showalert("Punto agregado. OK");
                CargarRutasVehiculo(codCar);
                limpiarCampoCliente();
            }
            catch (Exception ex)
            {
                showalert("Error en el metodo registro nuevo punto. " + ex.Message);
            }
        }



        protected void btn_dibujarPuntos_Click(object sender, EventArgs e)
        { }

        protected void btn_dibujarPuntosGV_Click(object sender, EventArgs e)
        {
            var puntos = new List<object>();

            foreach (GridViewRow row in gv_listaRutasDespacho.Rows)
            {
                TextBox txtOrden = (TextBox)row.FindControl("txtOrden");

                string orden = txtOrden.Text;
                string cli = row.Cells[1].Text;
                string lat = row.Cells[2].Text;
                string lng = row.Cells[3].Text;

                puntos.Add(new
                {
                    orden = orden,
                    cliente = cli,
                    lat = lat,
                    lng = lng
                });
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string json = js.Serialize(puntos);

            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "dibujarDesdeGV",
                    $"dibujarPuntosDesdeGv({json});",
                    true
                );
        }

        private void DibujarPuntosDesdeGridView()
        {
            var puntos = new List<object>();

            foreach (GridViewRow row in gv_listaRutasDespacho.Rows)
            {
                TextBox txtOrden = (TextBox)row.FindControl("txtOrden");

                string orden = txtOrden.Text;
                string cli = row.Cells[1].Text;
                string lat = row.Cells[2].Text;
                string lng = row.Cells[3].Text;

                puntos.Add(new
                {
                    orden = orden,
                    cliente = cli,
                    lat = lat,
                    lng = lng
                });
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string json = js.Serialize(puntos);

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "dibujarDesdeGV",
                $"dibujarPuntosDesdeGv({json});",
                true
            );
        }




        private void limpiarCampoCliente()
        {
            tx_newCliente.Text = string.Empty;
            hf_codCliente.Value = "";
        }


        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        protected void btn_guardarOrden_Click(object sender, EventArgs e)
        {
            try
            {
                actualizarOrdenRutas_asignacion();
                
            }
            catch(Exception ex)
            {
                showalert("Error al actualizar los campos. " + ex.Message);
            }
        }

        private void actualizarOrdenRutas_asignacion()
        {
            try
            {
                if (dd_listVehiculo.SelectedIndex == 0)
                {
                    showalert("Debes seleccionar un vehiculo válido");
                    return;
                }

                int codCar = Convert.ToInt32(dd_listVehiculo.SelectedValue);
                NCorpal_Vehiculos nVec = new NCorpal_Vehiculos();

                foreach (GridViewRow row in gv_listaRutasDespacho.Rows)
                {
                    if (row.RowType != DataControlRowType.DataRow)
                        continue;

                    TextBox txtOrden = (TextBox)row.FindControl("txtOrden");
                    if (txtOrden == null || string.IsNullOrWhiteSpace(txtOrden.Text))
                        continue;

                    int orden = Convert.ToInt32(txtOrden.Text);

                    int codCli = Convert.ToInt32(
                            gv_listaRutasDespacho.DataKeys[row.RowIndex].Value
                        );

                    bool result = nVec.update_ordenRutaEntrega_asignacion(codCar, orden, codCli);

                    if (result)
                    {
                        //CargarRutasVehiculo(codCar);
                        showalert("Registro actualizado correctamente.");
                    }
                }
                CargarRutasVehiculo(codCar);
            }
            catch (Exception ex)
            {
                showalert("Error en el metodo actualizar rutas. " + ex.Message);
            }
        }

        protected void btn_limpiarMaps_Click(object sender, EventArgs e)
        {

        }
    }
}