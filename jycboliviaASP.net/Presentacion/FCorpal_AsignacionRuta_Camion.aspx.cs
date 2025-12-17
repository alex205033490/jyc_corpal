using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

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

        // Cargar Clientes 
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


        // Cargar datos Vehiculo DD
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

        protected void dd_listVehiculo_selectedIndexChanged(object sender, EventArgs e)
        {
            int codCar = int.Parse(dd_listVehiculo.SelectedValue);

            if (codCar >= 1)
            {
                CargarRutasVehiculo(codCar);
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

        protected void btn_dibujarPuntos_Click(object sender, EventArgs e)
        {
            var puntos = new List<object>();

            foreach (GridViewRow row in gv_listaRutasDespacho.Rows)
            {
                string lat = row.Cells[2].Text;
                string lng = row.Cells[3].Text;

                puntos.Add(new
                {
                    lat = lat,
                    lng = lng
                });
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonPuntos = js.Serialize(puntos);

            ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "dibujarPuntos",
                    $"dibujarPuntosMapa({jsonPuntos})",
                    true
         );           

        }

        protected void btn_registrarNuevoPunto_Click(object sender, EventArgs e)
        {
            int codCar = int.Parse(dd_listVehiculo.SelectedValue);
            string car = dd_listVehiculo.SelectedItem.ToString();

            RegistrarNewRutaPuntoEntrega(codCar, car);
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

                foreach(DataRow rowCli in dsCli.Tables[0].Rows)
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
                showalert("Nuevo punto de entrega registrado correctamente");
                CargarRutasVehiculo(codCar);
                limpiarCampoCliente();
            }
            catch (Exception ex)
            {
                showalert("Error en el metodo registro nuevo punto. " + ex.Message);
            }



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

    }
}