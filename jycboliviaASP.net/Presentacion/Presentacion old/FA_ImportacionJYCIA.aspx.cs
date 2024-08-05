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
using System.Drawing;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ImportacionJYCIA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(65) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if (!IsPostBack)
            {
                Limpiar_Datos();                
                Buscar_Datos("","","","","","","","","","","");
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

     
        private void Buscar_Datos(string Edificio, string exbo, string nrofactura, string fechafactura, string montofactura, string fechagiro, string montogiro1, string montogiro2, string montogiro3, string montogiro4, string montogiro5)
        {
            //gv_datos.DataSource = null;
            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.Buscar_ImportacionJYCIA(Edificio,  exbo,  nrofactura,  fechafactura,  montofactura,  fechagiro,  montogiro1,  montogiro2,  montogiro3,  montogiro4,  montogiro5);
            gv_datos.DataSource = tuplaRes;
            gv_datos.DataBind();
            lb_cantidad.Text = gv_datos.Rows.Count.ToString(); 
        }

     

        private void Limpiar_Datos()
        {
            tx_edificio.Text = "";
            tx_exbo.Text = "";            
        }

        protected void bt_Limpiar_Click(object sender, EventArgs e)
        {
            Limpiar_Datos();
        }

       

        public string convertidorFecha(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'" + _fecha + "'";
            }
            else
                return "null";
        }

    
        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string proyectoNombre = tx_edificio.Text;
            string exbo = tx_exbo.Text;        
            //-------------- datos a eliminar-------------
            string nroFactura = "";
            string fechaFactura = "";
            string montoFactura = "";
            string fechaGiro = "";
            string montoGiro1 = "";
            string montoGiro2 = "";
            string montoGiro3 = "";
            string montoGiro4 = "";
            string montoGiro5 = "";
            //----------------------------------------------
            Buscar_Datos(proyectoNombre,exbo,nroFactura,fechaFactura,montoFactura,fechaGiro,montoGiro1,montoGiro2,montoGiro3,montoGiro4,montoGiro5);
        }

        protected void gv_datos_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargar_Datos();
        }

        private void cargar_Datos()
        {
            if (gv_datos.SelectedRow.Cells[2].Text != "&nbsp;")
            {
                tx_edificio.Text = HttpUtility.HtmlDecode(gv_datos.SelectedRow.Cells[2].Text);
            }
            else
                tx_edificio.Text = "";

            if (gv_datos.SelectedRow.Cells[3].Text != "&nbsp;")
            {
                tx_exbo.Text = HttpUtility.HtmlDecode(gv_datos.SelectedRow.Cells[3].Text);
            }
            else
                tx_exbo.Text = "";


        }

        protected void bt_Exportar_Click(object sender, EventArgs e)
        {
            Exportar_Excel();
        }


        public void Exportar_Excel()
        {
            string proyectoNombre = tx_edificio.Text;
            string exbo = tx_exbo.Text;

            ///----------------------- datos a eliminar
            string nroFactura = "";
            string fechaFactura = "";
            string montoFactura = "";
            string fechaGiro = "";
            string montoGiro1 = "";
            string montoGiro2 = "";
            string montoGiro3 = "";
            string montoGiro4 = "";
            string montoGiro5 = "";

            ///-----------------------datos a eliminar

            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.Buscar_ImportacionJYCIA_2(proyectoNombre, exbo, nroFactura, fechaFactura, montoFactura, fechaGiro, montoGiro1, montoGiro2, montoGiro3, montoGiro4, montoGiro5);
            

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombreDB = Session["BaseDatos"].ToString();
            string nombre = "Importacion_JYCIA " + nombreDB+" "+DateTime.Now.ToString("dd/MM/yyyy");
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = tuplaRes;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }

        }



        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            bool isUpdateVisible = false;
           // Label1.Text = string.Empty;

            //Loop through all rows in GridView
            foreach (GridViewRow row in gv_datos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                  //--------------  
                    if (isChecked)                    
                        row.RowState = DataControlRowState.Edit;

                        for (int i = 14; i < row.Cells.Count; i++)
                        {
                            row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Visible = !isChecked;
                            if (row.Cells[i].Controls.OfType<TextBox>().ToList().Count > 0)
                            {
                                row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Visible = isChecked;
                                row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().BackColor = Color.Yellow;
                            }

                            if (row.Cells[i].Controls.OfType<CheckBox>().ToList().Count > 0)
                            {
                                row.Cells[i].Controls.OfType<CheckBox>().FirstOrDefault().Visible = isChecked;
                                string dato = row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Text;
                                if (dato.Equals("True"))
                                    row.Cells[i].Controls.OfType<CheckBox>().FirstOrDefault().Checked = true;
                                else
                                    row.Cells[i].Controls.OfType<CheckBox>().FirstOrDefault().Checked = false;

                            }
                        
                            if (isChecked && !isUpdateVisible)
                            {
                                isUpdateVisible = true;
                            }
                        }
                    
                    //--------------
                }
            }
           bt_Update.Visible = isUpdateVisible;
        }

        protected void bt_Update_Click(object sender, EventArgs e)
        {
            actualizarTodo();
            string proyectoNombre = tx_edificio.Text;
            string exbo = tx_exbo.Text;
            ///----------------  datos a eliminar ------------------
            string nroFactura = "";
            string fechaFactura = "";
            string montoFactura = "";
            string fechaGiro = "";
            string montoGiro1 = "";
            string montoGiro2 = "";
            string montoGiro3 = "";
            string montoGiro4 = "";
            string montoGiro5 = "";
            //---------------------------------------------------
            Buscar_Datos(proyectoNombre, exbo, nroFactura, fechaFactura, montoFactura, fechaGiro, montoGiro1, montoGiro2, montoGiro3, montoGiro4, montoGiro5);
        }

        private void actualizarTodo()
        {
            foreach (GridViewRow row in gv_datos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked)
                    {
                        int codigo = Convert.ToInt32(row.Cells[1].Text);
                        string proyectoNombre = row.Cells[2].Text;
                        string exbo = row.Cells[3].Text;
                        string nroFactura = row.Cells[14].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        string fechaFactura = convertidorFecha(row.Cells[15].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        float montoFactura = 0;
                        bool esnumero = Single.TryParse(row.Cells[16].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out montoFactura);

                        string fechaGiro1 = convertidorFecha(row.Cells[17].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        float montoGiro1 = 0;
                        esnumero = Single.TryParse(row.Cells[18].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out montoGiro1);

                        string fechagiro2 = convertidorFecha(row.Cells[19].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        float montoGiro2 = 0;
                        esnumero = Single.TryParse(row.Cells[20].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out montoGiro2);

                        string fechagiro3 = convertidorFecha(row.Cells[21].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        float montoGiro3 = 0;
                        esnumero = Single.TryParse(row.Cells[22].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out montoGiro3);

                        string fechagiro4 = convertidorFecha(row.Cells[23].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        float montoGiro4 = 0;
                        esnumero = Single.TryParse(row.Cells[24].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out montoGiro4);

                        string fechagiro5 = convertidorFecha(row.Cells[25].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        float montoGiro5 = 0;
                        esnumero = Single.TryParse(row.Cells[26].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out montoGiro5 );

                        float valorFob = 0;
                        esnumero = Single.TryParse(row.Cells[27].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out valorFob);

                        float valortransportemaritimo2 = 0;
                        esnumero = Single.TryParse(row.Cells[28].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out valortransportemaritimo2);
                        string nrocontenedor = row.Cells[29].Controls.OfType<TextBox>().FirstOrDefault().Text;
                                                
                        bool pago1 = row.Cells[30].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                        bool pago2 = row.Cells[31].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                        bool pago3 = row.Cells[32].Controls.OfType<CheckBox>().FirstOrDefault().Checked;

                        NEquipo equipo = new NEquipo();
                        bool actualizado = equipo.actualizar_importacionJYCIA(codigo, nroFactura, fechaFactura, montoFactura, fechaGiro1, montoGiro1, montoGiro2, montoGiro3, montoGiro4, montoGiro5, valorFob, valortransportemaritimo2, nrocontenedor, fechagiro2, fechagiro3, fechagiro4, fechagiro5, pago1, pago2, pago3);
                        if (actualizado == true)
                            row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = false;
                    }
                }
            }
            bt_Update.Visible = false;
            // this.BindGrid();
        }

        protected void dd_BaseDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dpto = dd_BaseDatos.SelectedValue.ToString();            
            cambiarBaseDeDatos(dpto);
            Response.Redirect("../Presentacion/FA_ImportacionJYCIA.aspx"); 
        }

        public void cambiarBaseDeDatos(string departamento)
        {
            string BaseDatos = departamento;
            switch (BaseDatos)
            {
                case "Prueba":
                    Session["NombreBaseDatos"] = "db_prueba";
                    Session["BaseDatos"] = "Prueba";
                    break;
                case "Santa Cruz":
                    Session["NombreBaseDatos"] = "db_SantaCruz";
                    Session["BaseDatos"] = "Santa Cruz";
                    break;
                case "La Paz":
                    Session["NombreBaseDatos"] = "db_LaPaz";
                    Session["BaseDatos"] = "La Paz";
                    break;
                case "Cochabamba":
                    Session["NombreBaseDatos"] = "db_Cochabamba";
                    Session["BaseDatos"] = "Cochabamba";
                    break;
                case "Sucre":
                    Session["NombreBaseDatos"] = "db_Sucre";
                    Session["BaseDatos"] = "Sucre";
                    break;
                case "Oruro":
                    Session["NombreBaseDatos"] = "db_Oruro";
                    Session["BaseDatos"] = "Oruro";
                    break;
                case "Potosi":
                    Session["NombreBaseDatos"] = "db_Potosi";
                    Session["BaseDatos"] = "Potosi";
                    break;
                case "Tarija":
                    Session["NombreBaseDatos"] = "db_Tarija";
                    Session["BaseDatos"] = "Tarija";
                    break;
                case "Yacuiba":
                    Session["NombreBaseDatos"] = "db_Yacuiba";
                    Session["BaseDatos"] = "Yacuiba";
                    break;
                case "Villamontes":
                    Session["NombreBaseDatos"] = "db_Villamontes";
                    Session["BaseDatos"] = "Villamontes";
                    break;
                case "Asuncion-Paraguay":
                    Session["NombreBaseDatos"] = "db_Paraguay";
                    Session["BaseDatos"] = "Asuncion-Paraguay";
                    break;
                case "Beni":
                    Session["NombreBaseDatos"] = "db_beni";
                    Session["BaseDatos"] = "Beni";
                    Session["DB"] = "db_seguimientobeni_jyc";
                    break;
                case "Pando":
                    Session["NombreBaseDatos"] = "db_pando";
                    Session["BaseDatos"] = "Pando";
                    Session["DB"] = "db_seguimientopando_jyc";
                    break;

                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
      

    }
}