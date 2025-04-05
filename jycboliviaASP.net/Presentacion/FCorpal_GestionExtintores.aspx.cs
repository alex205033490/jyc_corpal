using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.EntitySql;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_GestionExtintores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                mostarRegistros("");
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

        /* -----    BTN GUARDAR     -----*/
        protected void btn_guardarForm_Click(object sender, EventArgs e)
        {
            bool isValid = validarFormRegistro();

            if (isValid)
            {
                insertarRegistroExtintor();
                LimpiarForm();
            }
        }
        private void insertarRegistroExtintor()
        {
            try
            {
                string detalle = txt_detalle.Text.Trim();
                string area = txt_area.Text.Trim();
                string agenteExterior = txt_agenteExtintor.Text.Trim();
                string marca = txt_marca.Text.Trim();
                float capacidad = float.Parse(txt_capacidad.Text.Trim().Replace(',','.'), CultureInfo.InvariantCulture);
                string codSistema = txt_codSistema.Text.Trim();
                string fechaCarga = string.IsNullOrWhiteSpace(txt_fechadCarga.Text) ? null : ConvertidorFecha(txt_fechadCarga.Text.Trim());
                string fechaProximaCarga = string.IsNullOrWhiteSpace(txt_fechaProximaCarga.Text) ? null : ConvertidorFecha(txt_fechaProximaCarga.Text.Trim());
                string estadoExtintor = txt_estadoExtintor.Text.Trim();
                int añoProximaPrueba = int.Parse(txt_fechaProximaPrueba.Text.Trim());
                int codRes = 55;
                string nombreRes = "carlos to";

                NCorpal_Extintor negocio = new NCorpal_Extintor();
                bool resultado = negocio.Insert_RegistroExtintor(detalle, area, agenteExterior, marca, capacidad,
                    codSistema, fechaCarga, fechaProximaCarga, estadoExtintor, añoProximaPrueba, codRes, nombreRes);
                
                if (resultado)
                {
                    showalert($"Registro guardado exitosamente!");
                    //LimpiarForm();
                    mostarRegistros("");
                }
                else
                {
                    showalert($"Hubo un error al guardar el registro. {detalle}{area}{agenteExterior}{marca}{capacidad}{codSistema}{fechaCarga}{fechaProximaCarga}{estadoExtintor}{añoProximaPrueba}{codRes}{nombreRes}");
                }
            }
            catch(Exception ex)
            {
                showalert($"Ocurrio un error inesperado. {ex.Message}");
            }
        }

        /* METODO MOSTRAR REGISTROS */
        private void mostarRegistros(string area)
        {
            NCorpal_Extintor negocio = new NCorpal_Extintor();
            DataSet datos = negocio.mostrar_registrosExtintores(area);
            gv_registrosExtintores.DataSource = datos;
            gv_registrosExtintores.DataBind();
        }

        /*  -----   BTN ANULAR  ----    */
        protected void btn_anularRegistro_Click(object sender, EventArgs e)
        {
            NCorpal_Extintor negocio = new NCorpal_Extintor();

            List<int> selectCodigo = new List<int>();

            foreach(GridViewRow row in gv_registrosExtintores.Rows)
            {
                CheckBox chkAnular = (CheckBox)row.FindControl("chkSelect");

                if(chkAnular != null && chkAnular.Checked)
                {
                    int codigo = Convert.ToInt32(gv_registrosExtintores.DataKeys[row.RowIndex].Value);
                    selectCodigo.Add(codigo);
                }
            }
            if(selectCodigo.Count > 0)
            {
                bool exito = negocio.anular_Registro(selectCodigo);
                if (exito)
                {
                    string codigoStr = string.Join(", ", selectCodigo);
                    showalert($"Se ha anulado el registro Nro: {codigoStr} exitosamente. ");
                    mostarRegistros("");
                }
                else
                {
                    string codigoStr = string.Join(",", selectCodigo);
                    showalert($"Hubo un error al anular el registro: {codigoStr}");
                }
            }
            else
            {
                showalert("Por favor seleccione al menos 1 registro");
            }
        }


        public string ConvertidorFecha(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "-" + mes + "-" + dia;
                return   _fecha ;
            }
            else
                return "null";
        }
        private bool validarFormRegistro()
        {
            /*if (string.IsNullOrEmpty(txt_agenteExtintor.Text.Trim()))
            {
                showalert("El campo agente extintor es obligatorio.");
                return false;
            }
            else if (string.IsNullOrEmpty(txt_marca.Text.Trim()))
            {
                showalert("El campo marca es obligatorio.");
                return false;
            } */
            if (string.IsNullOrEmpty(txt_capacidad.Text.Trim()) || !float.TryParse(txt_capacidad.Text.Trim(), out _))
            {
                showalert("La capacidad debe ser un número válido.");
                return false;
            }
            /*else if (string.IsNullOrEmpty(txt_fechadCarga.Text.Trim()) || !DateTime.TryParse(txt_fechadCarga.Text.Trim(), out _))
            {
                showalert("La fecha de carga no es valida.");
                return false;
            }
            else if (string.IsNullOrEmpty(txt_fechaProximaCarga.Text.Trim()) || !DateTime.TryParse(txt_fechaProximaCarga.Text.Trim(), out _))
            {
                showalert("La fecha proxima carga no es válida.");
                return false;
            }*/
            /*else if (string.IsNullOrEmpty(txt_fechaProximaPrueba.Text.Trim()) || !int.TryParse(txt_fechaProximaPrueba.Text.Trim(), out _))
            {
                showalert("El año de próxima prueba hidrostática debe ser un número válido.");
                return false;
            }*/
            else if (string.IsNullOrEmpty(txt_area.Text.Trim()))
            {
                showalert("Por favor, ingrese una area valida");
                return false;
            }
            
            return true;
        }

        private void LimpiarForm()
        {
            txt_area.Text = string.Empty;
            txt_marca.Text = string.Empty;
            txt_fechadCarga.Text = string.Empty;
            txt_fechaProximaPrueba.Text = string.Empty;
            txt_detalle.Text = string.Empty;
            txt_capacidad.Text = string.Empty;
            txt_fechaProximaCarga.Text = string.Empty;
            txt_agenteExtintor.Text = string.Empty;
            txt_codSistema.Text = string.Empty;
            txt_estadoExtintor.Text = string.Empty;
        }

        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
        /*---   CARGAR DATOS AL GV    ---*/
        protected void gv_registrosExtintores_row_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string proximaPruebaHidrostatica = DataBinder.Eval(e.Row.DataItem, "anioproximapruebahidrostatica").ToString();
                string fechaProximaCarga = ConvertidorFecha(DataBinder.Eval(e.Row.DataItem, "fechaproximacarga").ToString());
                string fechadecarga = ConvertidorFecha(DataBinder.Eval(e.Row.DataItem, "fechadecarga").ToString());
                string codSistema = DataBinder.Eval(e.Row.DataItem, "codSistema").ToString();
                string capacidad = DataBinder.Eval(e.Row.DataItem, "capacidad").ToString();
                string marca = DataBinder.Eval(e.Row.DataItem, "marca").ToString();
                string agenteextintor = DataBinder.Eval(e.Row.DataItem, "agenteextintor").ToString();
                string detalle = DataBinder.Eval(e.Row.DataItem, "detalle").ToString();
                string estadoextintor = DataBinder.Eval(e.Row.DataItem, "estadoextintor").ToString();

                string estadoextintor2 = DataBinder.Eval(e.Row.DataItem, "estadoextintor").ToString();
                DropDownList dd_eextintor2 = (DropDownList)e.Row.FindControl("dd_eextintor2");
                if(dd_eextintor2 != null)
                {
                    dd_eextintor2.Text = estadoextintor2;
                }

                string area2 = DataBinder.Eval(e.Row.DataItem, "area").ToString();
                DropDownList dd_area2 = (DropDownList)e.Row.FindControl("dd_area2");
                if(dd_area2 != null)
                {
                    dd_area2.Text = area2;
                }

                TextBox txt_pphidrostatica = (TextBox)e.Row.FindControl("txt_pphidrostatica");
                TextBox txt_fechaproximacarga = (TextBox)e.Row.FindControl("txt_fproximacarga");
                TextBox txt_fechadcarga = (TextBox)e.Row.FindControl("txt_fdcarga");
                TextBox txt_codSistema = (TextBox)e.Row.FindControl("txt_codsistema");
                TextBox txt_capacidad = (TextBox)e.Row.FindControl("txt_capacidad");
                TextBox txt_marca = (TextBox)e.Row.FindControl("txt_marca");
                TextBox txt_agenteextintor = (TextBox)e.Row.FindControl("txt_aextintor");
                TextBox txt_detalle = (TextBox)e.Row.FindControl("txt_detalle");
                TextBox txt_estadoextintor = (TextBox)e.Row.FindControl("txt_estadoextintor");

                if (txt_pphidrostatica != null)
                {
                    txt_pphidrostatica.Text = proximaPruebaHidrostatica;
                }
                if (txt_fechaproximacarga != null)
                {
                    txt_fechaproximacarga.Text = fechaProximaCarga;
                }
                if (txt_fechadcarga != null)
                {
                    txt_fechadcarga.Text = fechadecarga;
                }
                if (txt_codSistema != null)
                {
                    txt_codSistema.Text = codSistema;
                }
                if (txt_capacidad != null)
                {
                    txt_capacidad.Text = capacidad;
                }
                if (txt_marca != null)
                {
                    txt_marca.Text = marca;
                }
                if (txt_agenteextintor != null)
                {
                    txt_agenteextintor.Text = agenteextintor;
                }
                if (txt_detalle != null)
                {
                    txt_detalle.Text = detalle;
                }
                if (txt_estadoextintor != null)
                {
                    txt_estadoextintor.Text = estadoextintor;
                }
                

            }
        }


        /*---   BOTON ACTUALIZAR   ---*/
        protected void btn_updateRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> codigosActualizados = new List<int>();
                foreach(GridViewRow row in gv_registrosExtintores.Rows)
                {
                    CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;
                    if(chkSelect != null && chkSelect.Checked)
                    {
                        if (!ValidarDropDowns(row))
                        {
                            return;
                        }
                        int codigo = Convert.ToInt32(row.Cells[1].Text);
                        ActualizarRegistros(row, codigo);
                        codigosActualizados.Add(codigo);
                    }
                }
                if(codigosActualizados.Count > 0)
                {
                    showalert($"Registros actualizados correctamente: {string.Join(", ", codigosActualizados)}");
                    mostarRegistros("");
                }
                else
                {
                    showalert("No se seleccionaron registros para actualizar.");
                }
            }
            catch (Exception ex)
            {
                showalert($"Error inesperado: {ex.Message}");
            }

            /*
            try
            {
                foreach(GridViewRow row in gv_registrosExtintores.Rows)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    if(chkSelect != null && chkSelect.Checked)
                    {
                        int codigo = Convert.ToInt32(row.Cells[1].Text);

                        TextBox txtDetalle = (TextBox)row.FindControl("txt_detalle");
                        DropDownList ddarea = (DropDownList)row.FindControl("dd_area2");
                        TextBox txtAExtintor = (TextBox)row.FindControl("txt_aextintor");
                        TextBox txtMarca = (TextBox)row.FindControl("txt_marca");
                        TextBox txtCapacidad = (TextBox)row.FindControl("txt_capacidad");
                        TextBox txtcodSistema = (TextBox)row.FindControl("txt_codsistema");
                        DropDownList ddEstadoextintor = (DropDownList)row.FindControl("dd_eextintor2");
                        TextBox txtAnioPruebaH = (TextBox)row.FindControl("txt_pphidrostatica");
                        TextBox txtFechadecarga = ((TextBox)row.FindControl("txt_fdcarga"));
                        TextBox txtFechaproximadcarga = ((TextBox)row.FindControl("txt_fproximacarga"));

                        NCorpal_Extintor negocio = new NCorpal_Extintor();

                        ActualizarDatosRegistro(codigo, txtDetalle, ddarea, txtAExtintor, txtMarca, txtCapacidad, txtcodSistema, ddEstadoextintor, txtAnioPruebaH, txtFechadecarga, txtFechaproximadcarga);
                        showalert($"Registro actualizado");
                        mostarRegistros("");
                    }
                }
            }
            catch(Exception ex)
            {
                showalert($"Error inesperado. {ex.Message}");
            }
            */
        }
        /*
        private void ActualizarDatosRegistro(int codigo, TextBox txtdetalle, TextBox txtarea, TextBox txtaExtintor, TextBox txtmarca, 
            TextBox txtcapacidad, TextBox txtcodsistema, TextBox txtestadoExtintor, TextBox txtanio, TextBox txtfechadecarga, TextBox txfechaproximadcarga)
        {
            try
            {
                string detalle = txtdetalle.Text;
                string area = txtarea.Text;
                
                string aExtintor = txtaExtintor.Text;
                string marca = txtmarca.Text;
                string fechadecarga = ConvertidorFecha(txtfechadecarga.Text);
                string fechaproximadcarga = ConvertidorFecha(txfechaproximadcarga.Text);

                string capacidadTexto = txtcapacidad.Text.Replace('.', ',');
                float capacidad = 0;
                if(!float.TryParse(capacidadTexto, out capacidad))
                {
                    showalert("La capacidad no tiene el formato correcto.");
                    return;
                }

                string codSistema = txtcodsistema.Text;
                string estadoExtintor = txtestadoExtintor.Text;

                int ppanio = 0;
                if(!int.TryParse(txtanio.Text, out ppanio))
                {
                    showalert("El año de prueba no tiene el formato correcto");
                    return;
                }

                NCorpal_Extintor negocio = new NCorpal_Extintor();
                bool resultado = negocio.update_registros(codigo, detalle, area, aExtintor, marca, capacidad, codSistema, 
                    estadoExtintor, ppanio, fechadecarga, fechaproximadcarga);

                if (resultado)
                {
                    showalert("Campos correctos");
                }
                else
                {
                    // Muestra la consulta SQL que estás ejecutando
                    
                    showalert($"Error al actualizar los campos del codigo : {codigo}");
                }
            }
            catch(Exception ex)
            {
                showalert($"Ocurrio un error inesperado al actualizar los datos. {ex.Message}");
            }
        }
        */
        private bool ValidarDropDowns(GridViewRow row)
        {
            DropDownList ddArea = row.FindControl("dd_area2") as DropDownList;
            DropDownList ddEstadoExtintor = row.FindControl("dd_eextintor2") as DropDownList;

            if(ddArea != null && ddArea.SelectedIndex == 0)
            {
                showalert("Por favor, selecciona un área válida.");
                return false;
            }

            if(ddEstadoExtintor != null && ddEstadoExtintor.SelectedIndex == 0)
            {
                showalert("Por favor, selecciona un estado extintor válido.");
                return false;
            }
            return true;

        }
        private void ActualizarRegistros(GridViewRow row, int codigo)
        {
            try
            {
                TextBox txtDetalle = row.FindControl("txt_detalle") as TextBox;
                TextBox txtArea = row.FindControl("txt_area") as TextBox;
                TextBox txtAExtintor = row.FindControl("txt_aextintor") as TextBox;
                TextBox txtMarca = row.FindControl("txt_marca") as TextBox;
                TextBox txtCapacidad = row.FindControl("txt_capacidad") as TextBox;
                TextBox txtCodSistema = row.FindControl("txt_codsistema") as TextBox;
                TextBox txtestadoExtintor = row.FindControl("txt_estadoExtintor") as TextBox;
                TextBox txtAnioPruebaH = row.FindControl("txt_pphidrostatica") as TextBox;
                TextBox txtFechadecarga = row.FindControl("txt_fdcarga") as TextBox;
                TextBox txtFechaproximadcarga = row.FindControl("txt_fproximacarga") as TextBox;

                string detalle = txtDetalle?.Text ?? "";
                string area = txtArea?.Text ?? "";
                string aExtintor = txtAExtintor?.Text ?? "";
                string marca = txtMarca?.Text ?? "";
                string codSistema = txtCodSistema?.Text ?? "";
                string estadoExtintor = txtestadoExtintor?.Text ?? "";
                string fechadecarga = string.IsNullOrWhiteSpace(txtFechadecarga?.Text) || txtFechadecarga.Text.Trim() == "null" ? null : ConvertidorFecha(txtFechadecarga?.Text);
                string fechaproximadcarga = string.IsNullOrWhiteSpace(txtFechaproximadcarga?.Text) || txtFechaproximadcarga.Text.Trim() == "null" ? null : ConvertidorFecha(txtFechaproximadcarga?.Text);

                int anioPruebaH = 0;
                if(!string.IsNullOrWhiteSpace(txtAnioPruebaH?.Text) && int.TryParse(txtAnioPruebaH?.Text, out int parsedAnioPruebaH))
                {
                    anioPruebaH = parsedAnioPruebaH;
                }
                else
                {
                    anioPruebaH = 0;
                }
                
                if (!float.TryParse(txtCapacidad?.Text.Replace('.',','), out float capacidad))
                {
                    showalert("La capacidad no tiene el formato correcto");
                    return;
                }

                NCorpal_Extintor negocio = new NCorpal_Extintor();
                bool resultado = negocio.update_registros(codigo, detalle, area, aExtintor, marca, capacidad, codSistema, estadoExtintor,
                    anioPruebaH, fechadecarga, fechaproximadcarga);

                if (!resultado)
                {
                    showalert($"Error al actualizar el registro");
                }
            }
            catch(Exception ex)
            {
                showalert($"Error inesperado al actualizar el registro {codigo}: {ex.Message}");
            }
        }

        [WebMethod]
        public static List<string> getAreas(string prefixText)
        {
            List<string> areas = new List<string>
            {
                "Almacén de materia prima 1",
            "Almacén de materia prima 2",
            "Almacén de materia prima 3",
            "Almacén de producto terminado",
            "Almacén maestro",
            "Alta tensión",
            "Batido de semilla",
            "Compresores",
            "Container",
            "Envolsado de semilla",
            "Evaporadores de gas",
            "Galpón 1 - Galletas",
            "Galpón 2 - Papas",
            "Galpón 3 - Nachos",
            "Mantenimiento",
            "Oficina Administración",
            "Pasillo sazón de ají",
            "Tasques de gas",
            "Tostado de semilla"

            };
            return areas.FindAll(area => area.ToLower().Contains(prefixText.ToLower()));
        }
        [WebMethod]
        public static List<string> getEstadoExtintor(string prefixText)
        {
            List<string> estados = new List<string>
            {
                "Nuevo",
                "Mantenimiento",
                "Recarga"
            };
            return estados.FindAll(area => area.ToLower().Contains(prefixText.ToLower()));
        }
    }
}
