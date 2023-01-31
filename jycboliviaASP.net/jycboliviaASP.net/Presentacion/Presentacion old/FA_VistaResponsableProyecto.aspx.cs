using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;
using System.Configuration;


namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_VistaResponsableProyecto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(32) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if (!IsPostBack) {
                Buscar("","","","","",false,false,false,"");
                cargarResponsableEdificio();
                cargarTecnicoInstalador();
                cargarddlEstadoEquipo();

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

        private void cargarddlEstadoEquipo()
        {
            NEstadoEquipo estadoEquipo = new NEstadoEquipo();

            ddlEstadoEquipo.DataSource = estadoEquipo.listar();
            ddlEstadoEquipo.DataValueField = "codigo";
            ddlEstadoEquipo.DataTextField = "nombre";
            ddlEstadoEquipo.Items.Add(new ListItem("", "-1"));
            ddlEstadoEquipo.AppendDataBoundItems = true;
            ddlEstadoEquipo.SelectedIndex = -1;
            ddlEstadoEquipo.DataBind();
        }

        private void cargarTecnicoInstalador()
        {
          //  NA_Equipo Nequipo = new NA_Equipo();
          //  DataSet datosCargar = Nequipo.getAllResponsableProyecto_TecnicoInstalador();
            NEquipo equipo = new NEquipo();
            dd_TecnicoInstalador.DataSource = equipo.listaTecnicoInstalador();
            // dd_TecnicoInstalador.DataValueField = "codigo";
            dd_TecnicoInstalador.DataTextField = "nombre";
            dd_TecnicoInstalador.Items.Add(new ListItem("", "-1"));
            dd_TecnicoInstalador.AppendDataBoundItems = true;
            dd_TecnicoInstalador.SelectedIndex = -1;
            dd_TecnicoInstalador.DataBind();
        }

        private void cargarResponsableEdificio()
        {
            NA_Responsables NResponsables = new NA_Responsables();
            DataSet datosCargar = NResponsables.get_AllFiscalesProy();
            dd_RespProyecto.DataSource = datosCargar;
           // dd_RespProyecto.DataValueField = "codigo";
            dd_RespProyecto.DataTextField = "nombre";
            dd_RespProyecto.Items.Add(new ListItem("", "-1"));
            dd_RespProyecto.AppendDataBoundItems = true;
            dd_RespProyecto.SelectedIndex = -1;
            dd_RespProyecto.DataBind();
        }



        public void Buscar(string exbo, string edificio, string nombreRespEdificio, string nombreTecnicoInstalador, string NombreEstado, bool polizaSeguro, bool boletaBancaria, bool letraCambio, string codvariablesimec)
        {            
            NA_Equipo nequipo = new NA_Equipo();
            DataSet datos = nequipo.getAllResponsableProyecto(exbo, edificio, nombreRespEdificio, nombreTecnicoInstalador, NombreEstado, polizaSeguro, boletaBancaria, letraCambio, false, codvariablesimec);
            int cant = nequipo.get_Cant_AllResponsableProyecto(exbo, edificio, nombreRespEdificio, nombreTecnicoInstalador, NombreEstado, polizaSeguro, boletaBancaria, letraCambio);
          //  int cant;
           // bool isnumeric = int.TryParse(datos.Tables[0].Rows.Count.ToString(), out cant);
            gv_tabla.DataSource = datos;
            gv_tabla.DataBind();
          //  if (isnumeric)
          //  {
                tx_cantidadEquipo.Text = cant.ToString();
           /* }
            else
                tx_cantidadEquipo.Text = "0";
            */

        }

        protected void bt_Buscar_Click1(object sender, EventArgs e)
        {
            Buscar(tx_Exbo.Text, tx_Edificio.Text, dd_RespProyecto.SelectedItem.Text, dd_TecnicoInstalador.SelectedItem.Text, ddlEstadoEquipo.SelectedItem.Text,cb_polizaSeguro.Checked,cb_boletaBancaria.Checked,cb_letraCambio.Checked, tx_variableSimec.Text);
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string nombreArchivo = "ResponsablesProyecto " + Session["BaseDatos"].ToString() + " " + DateTime.Now;
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombreArchivo + ".xls" + "\"");


            NA_Equipo nequipo = new NA_Equipo();
            DataSet tuplas = nequipo.getAllResponsableProyecto(tx_Exbo.Text, tx_Edificio.Text, dd_RespProyecto.SelectedItem.Text, dd_TecnicoInstalador.SelectedItem.Text, ddlEstadoEquipo.SelectedItem.Text,cb_polizaSeguro.Checked,cb_boletaBancaria.Checked,cb_letraCambio.Checked, true, tx_variableSimec.Text);
            DataGrid dg = new DataGrid();
            dg.DataSource = tuplas;
            dg.DataBind();                    
            dg.RenderControl(htmltextwrtter);

            Response.Write(strwritter.ToString());
            Response.End(); 
        }
    }
}