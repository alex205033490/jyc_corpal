using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_RutaMantenimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             this.Title = Session["BaseDatos"].ToString();

             if (tienePermisoDeIngreso(61) == false)
             {
                 string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                 Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
             } 

             if (!IsPostBack)
             {
                DateTime fechaNow = DateTime.Now;
                int mes = fechaNow.Month;
                int anio = fechaNow.Year;
                mostrarRutasMantenimiento("",mes, anio);
                chbx_verfaltantesderuta.Checked = true;
                desactivarBotonCopiarRutasAnteriores();
             }
        }

        private void desactivarBotonCopiarRutasAnteriores()
        {
            if (tienePermisoDeIngreso(79) == true)
            {
                bt_generar.Enabled = true;
            }else
                bt_generar.Enabled = false;
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

       

        private void mostrarRutasMantenimiento(string nombre,int mes, int anio)
        {
            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
            DataSet dato = nruta.getallRutaMantenimiento(nombre, mes, anio);
            gv_rutas.DataSource = dato;
            gv_rutas.DataBind();
            lb_ruta.Text = gv_rutas.Rows.Count.ToString();

        }



        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProyectos2(string prefixText, int count)
        {
            string nombreProyecto = prefixText;

            NProyecto proyectoN = new NProyecto();
            DataSet tuplas = proyectoN.buscador2(nombreProyecto);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaResponsable2(string prefixText, int count)
        {
            string nombreResponsable = prefixText;

            NA_Responsables Nrespon = new NA_Responsables();
            DataSet tuplas = Nrespon.mostrarTodos_AutoComplit(nombreResponsable);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }


        protected void bt_buscarEdificios_Click(object sender, EventArgs e)
        {            
            buscarEdificios();
        }

        private void buscarEdificios()
        {
            DateTime fechaNow = DateTime.Now;
            int mes = fechaNow.Month;
            int anio = fechaNow.Year;

            gv_equipo.SelectedIndex = -1;
            string edificio = tx_edificioBuscar.Text;
            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
            DataSet dato = null;
            if(chbx_verfaltantesderuta.Checked == true){
                 dato = nruta.getAllFaltantesEquiposSinRutas(edificio,mes, anio);
            }else
                 dato = nruta.getallEquiposProyectoRutaMantenimiento(edificio);

            gv_equipo.DataSource = dato;
            gv_equipo.DataBind();
            lb_equipos.Text = gv_equipo.Rows.Count.ToString();
        }

       

        private void asignarEquiposRuta() {
            if (gv_rutas.SelectedIndex > -1)
            {
                if (gv_equipo.SelectedIndex > -1)
                {

                    int codigoEquipo = Convert.ToInt32(gv_equipo.SelectedRow.Cells[1].Text);
                    int codigoRuta = Convert.ToInt32(gv_rutas.SelectedRow.Cells[1].Text);

                    string horaingreso = tx_popuhoraIngreso.Text;
                    string horasalida = tx_popuhorasalida.Text;
                    int cantVisita = Convert.ToInt32(tx_popucantidadVistas.Text);
                    string dia = dd_diapopu.SelectedItem.Text;
                    int nrodia = dd_diapopu.SelectedIndex;
                    float pasaje = Convert.ToSingle(tx_popuPasaje.Text.Replace('.',','));
                    string ascensor = tx_popuAscensor.Text;

                    bool bandera = verifiartablaCronogramaCorrecto(cantVisita);
                    if(bandera){
                        string semana1 = "null";
                        if (!tx_popuFechaSemana1.Text.Equals(""))
                        {
                            DateTime fecha1 = Convert.ToDateTime(tx_popuFechaSemana1.Text);
                            semana1 = "'"+fecha1.ToString("yyyy/MM/dd")+"'";
                        }

                        string semana2 = "null";
                        if (!tx_popuFechaSemana2.Text.Equals(""))
                        {
                            DateTime fecha2 = Convert.ToDateTime(tx_popuFechaSemana2.Text);
                            semana2 = "'"+fecha2.ToString("yyyy/MM/dd")+"'";
                        }

                        string semana3 = "null";
                        if (!tx_popuFechaSemana3.Text.Equals(""))
                        {
                            DateTime fecha3 = Convert.ToDateTime(tx_popuFechaSemana3.Text);
                            semana3 = "'"+fecha3.ToString("yyyy/MM/dd")+"'";
                        }

                        string semana4 = "null";
                        if (!tx_popuFechaSemana4.Text.Equals(""))
                        {
                            DateTime fecha4 = Convert.ToDateTime(tx_popuFechaSemana4.Text);
                            semana4 = "'"+fecha4.ToString("yyyy/MM/dd")+"'";
                        }

                         DateTime fechaNow = DateTime.Now;
                        int mes = fechaNow.Month;
                        int anio = fechaNow.Year;

                        bool banderaSemana1 = chx_s1.Checked;
                        bool banderaSemana2 = chx_s2.Checked;
                        bool banderaSemana3 = chx_s3.Checked;
                        bool banderaSemana4 = chx_s4.Checked;
                        
                        //--------------------------------------
                        NA_Responsables Nresp = new NA_Responsables();
                        string usuarioAux = Session["NameUser"].ToString();
                        string passwordAux = Session["passworuser"].ToString();
                        int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                        //--------------------------------------------------------
                        NA_Equipo neq = new NA_Equipo();
                        neq.actualizarLetraID_equipo(codigoEquipo, ascensor);

                        NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
                        bool banderaRuta = nruta.insertarEquipoRuta(codigoRuta, codigoEquipo, horaingreso, horasalida, dia, cantVisita, nrodia,banderaSemana1,banderaSemana2,banderaSemana3,banderaSemana4,mes,anio,semana1,semana2,semana3,semana4,codUser, pasaje);

                        //-----------------------historial
                        // NA_Historial nhistorial = new NA_Historial();
                        // nhistorial.insertar(codUser, "Ha insertado el Equipo del Proyecto " + nombreProyecto + " con el codigo " + codigoProyecto + " exboEquipo " + exbo);
                        //----------------- historial----
                        if(banderaRuta == true){
                            Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
                        }else
                            Response.Write("<script type='text/javascript'> alert('Error: OK') </script>");
                        
                        verEquiposAsignadosRutas(codigoRuta,"");
                        buscarEdificios();
                        limpiarPopuAgregar();
                        
                    }
                   
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: No Selecciono Equipo') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: No Selecciono Ruta') </script>");
        }

        private void limpiarPopuAgregar()
        {   
                    tx_popuhoraIngreso.Text= "";
                    tx_popuhorasalida.Text = "";
                    tx_popucantidadVistas.Text = "0";                   
                    dd_diapopu.SelectedIndex = 0;
                    tx_popuFechaSemana1.Text = "";
                    tx_popuFechaSemana2.Text = "";
                    tx_popuFechaSemana3.Text = "";
                    tx_popuFechaSemana4.Text = "";                    
                    chx_s1.Checked = false;
                    chx_s2.Checked = false;
                    chx_s3.Checked = false;
                    chx_s4.Checked = false;          
        }


        private void limpiarPopuModificar()
        {
            tx_popuHoraIngresoModificar.Text = "";
            tx_popuHoraSalidaModificar.Text = "";
            tx_popuCantVisitaMoficar.Text = "0";
            dd_popuDiamodificar.SelectedIndex = 0;
            tx_popuFechaSemana1modificar.Text = "";
            tx_popuFechaSemana2Modificar.Text = "";
            tx_popuFechaSemana3Modificar.Text = "";
            tx_popuFechaSemana4modificar.Text = "";
            chx_popuSemana1modificar.Checked = false;
            chx_popuSemana2modificar.Checked = false;
            chx_popuSemana3modificar.Checked = false;
            chx_popuSemana4modificar.Checked = false;
        }

        private bool existetuplaAsignada(int codigoEquipo)
        {
            DataTable datolistaAsignado = Session["RM_listaAdisionados"] as DataTable;
            DataRow[] result = datolistaAsignado.Select("Codigo = " + codigoEquipo);

            if (result.Length > 0)
            {
                return true;
            }
            else
                return false;
           
        }



        protected void bt_insertarRuta_Click(object sender, EventArgs e)
        {
            DateTime fechaNow = DateTime.Now;
            int mes = fechaNow.Month;
            int anio = fechaNow.Year;

            insertarRutaMantenimiento(mes, anio);
            mostrarRutasMantenimiento("",mes , anio);
        }

        private void insertarRutaMantenimiento(int mes, int anio)
        {
            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
            int nro = Convert.ToInt32(tx_numeroRuta.Text);
            string detalle = tx_detalleRuta.Text;
            bool bandera = nruta.insertarRutaMantenimiento(nro, detalle, mes, anio);

        }

        protected void gv_rutas_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarPopuModificar();
            selecciondedatos();
            int codigoRuta = Convert.ToInt32(gv_rutas.SelectedRow.Cells[1].Text);
            verEquiposAsignadosRutas(codigoRuta,"");
        }

        private void selecciondedatos()
        {
            tx_numeroRuta.Text = gv_rutas.SelectedRow.Cells[2].Text;
            if (!gv_rutas.SelectedRow.Cells[3].Text.Equals("&nbsp;"))
            {
                tx_detalleRuta.Text = gv_rutas.SelectedRow.Cells[3].Text;
            }
            else
                tx_detalleRuta.Text = "";
            
        }

        private void verEquiposAsignadosRutas(int codigoRuta,string edificio)
        {
            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();            
            DataSet dato = nruta.getallEquiposRutasAsignadas(codigoRuta, edificio);
            gv_equiposAsignados.DataSource = dato;
            gv_equiposAsignados.DataBind();
            lb_cantequipoAsignados.Text = gv_equiposAsignados.Rows.Count.ToString();
            
            
            DataSet dato1 = nruta.mostrarTecnicoRuta(codigoRuta);
            gv_tecnicosAsignados.DataSource = dato1;
            gv_tecnicosAsignados.DataBind();
            lb_canttecnicoAsignado.Text = gv_tecnicosAsignados.Rows.Count.ToString();
            
        }

        protected void bt_modificarRuta_Click(object sender, EventArgs e)
        {
            DateTime fechaNow = DateTime.Now;
            int mes = fechaNow.Month;
            int anio = fechaNow.Year;
            modificarRuta(mes, anio);
            mostrarRutasMantenimiento("", mes, anio);
        }

        private void modificarRuta(int mes, int anio)
        {
            if(gv_rutas.SelectedIndex > -1){
            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
            int codigo = Convert.ToInt32(gv_rutas.SelectedRow.Cells[1].Text);
            int nro = Convert.ToInt32(tx_numeroRuta.Text);
            string detalle = tx_detalleRuta.Text;
            bool bandera = nruta.modificarRutaMantenimiento(codigo,nro, detalle, mes, anio);
            }else
                Response.Write("<script type='text/javascript'> alert('Error: No Selecciono Ruta') </script>");
        }

        protected void bt_popupok_Click(object sender, EventArgs e)
        {
            asignarEquiposRuta();           
        }

        protected void bt_asignarTecnico_Click(object sender, EventArgs e)
        {
            asignarTecnicoRuta();
        }

        private void asignarTecnicoRuta()
        {
            DateTime fechaNow = DateTime.Now;
            int mes = fechaNow.Month;
            int anio = fechaNow.Year;

            if(gv_rutas.SelectedIndex > -1){
                int codRuta = Convert.ToInt32(gv_rutas.SelectedRow.Cells[1].Text);
                string nombreTecnico = tx_tecnico.Text;
                NA_Responsables nresponsable = new NA_Responsables();
                DataSet Tecnico = nresponsable.getResponsable_SinExepcion(nombreTecnico);
                int codTecnico = Convert.ToInt32(Tecnico.Tables[0].Rows[0][0].ToString());
                               
                string supervisor = dd_supervisorRuta.SelectedItem.Text;

                NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
                nruta.insertarTecnicoRuta(codRuta, codTecnico, supervisor,mes,anio);
                DataSet dato = nruta.mostrarTecnicoRuta(codRuta);
                gv_tecnicosAsignados.DataSource = dato;
                gv_tecnicosAsignados.DataBind();
                lb_canttecnicoAsignado.Text = gv_tecnicosAsignados.Rows.Count.ToString();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: No Selecciono Ruta') </script>");

        }


        public string aFecha(string fecha)
        {
            if (fecha == "" || fecha == "&nbsp;")
            {
                return fecha = "null";
            }

            else
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'"+_fecha+"'";
            }

        }

      

        private void eliminarRuta(int mes, int anio)
        {            
            int codigoRuta = Convert.ToInt32(gv_rutas.SelectedRow.Cells[1].Text);
            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
            if(!nruta.tieneEquiposAsignadosRuta(codigoRuta)){
            if(!nruta.tieneTecnicosAsignadosRuta(codigoRuta)){
                nruta.eliminarRuta(codigoRuta, mes, anio);
                mostrarRutasMantenimiento("",mes, anio);
                gv_rutas.SelectedIndex = -1;
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Tiene Tecnicos Asignados') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Tiene Equipos Asignados') </script>");

        }

        protected void bt_eliminarRuta_Click(object sender, EventArgs e)
        {
            DateTime fechaNow = DateTime.Now;
            int mes = fechaNow.Month;
            int anio = fechaNow.Year;
            eliminarRuta(mes, anio);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            limpiarTodoGridView();
            DateTime fechaNow = DateTime.Now;
            int mes = fechaNow.Month;
            int anio = fechaNow.Year;
            mostrarRutasMantenimiento(tx_detalleRuta.Text,mes, anio);
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarEquipoRutaMantenimiento();
        }

        private void eliminarEquipoRutaMantenimiento()
        {
            if(gv_rutas.SelectedIndex > -1){
            if(gv_equiposAsignados.SelectedIndex > -1){
                int codruta = Convert.ToInt32(gv_rutas.SelectedRow.Cells[1].Text);
                int codEquipo = Convert.ToInt32(gv_equiposAsignados.SelectedRow.Cells[1].Text);

                NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
                bool bandera = nruta.eliminarEquipoRutaMantemiento(codruta, codEquipo);
                if(bandera){
                    int codigoRuta = Convert.ToInt32(gv_rutas.SelectedRow.Cells[1].Text);
                    verEquiposAsignadosRutas(codigoRuta, "");
                    gv_equiposAsignados.SelectedIndex = -1;
                    Response.Write("<script type='text/javascript'> alert('Eliminado: OK') </script>");
                }else
                    Response.Write("<script type='text/javascript'> alert('Error') </script>");
                
            }else
                Response.Write("<script type='text/javascript'> alert('Error: NO Tiene Equipos Seleccionado') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: NO Tiene Ruta Seleccionada') </script>");
            
        }



        private void eliminarTecnicoRuta(GridViewDeleteEventArgs e)
        {
         if(gv_rutas.SelectedIndex > -1){          
              int codTecnico = Convert.ToInt32(gv_tecnicosAsignados.Rows[e.RowIndex].Cells[1].Text);
              int codRuta = Convert.ToInt32(gv_rutas.SelectedRow.Cells[1].Text);
              NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
              nruta.eliminarTecnicoRutaMantemiento(codRuta, codTecnico);
              DataSet dato = nruta.mostrarTecnicoRuta(codRuta);
              gv_tecnicosAsignados.DataSource = dato;
              gv_tecnicosAsignados.DataBind();
          
         }else
             Response.Write("<script type='text/javascript'> alert('Error: NO Tiene Ruta Seleccionada') </script>");

        }

        protected void gv_tecnicosAsignados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            eliminarTecnicoRuta(e);
        }

             
        protected void bt_popuCalcular_Click(object sender, EventArgs e)
        {
            calcularCronograma();
        }



        private void calcularCronograma()
        {
            DateTime fechaNow = DateTime.Now;
            int mes = fechaNow.Month;
            int anio = fechaNow.Year;

            int cantVisita = Convert.ToInt32(tx_popucantidadVistas.Text);
            int diaSemana = dd_diapopu.SelectedIndex;
            if (diaSemana == 0)
                diaSemana = 7;
                        
            bool verificacion = verifiartablaCronogramaCorrecto(cantVisita);
            if (verificacion == true)
            {
                bool semana1 = chx_s1.Checked;
                bool semana2 = chx_s2.Checked;
                bool semana3 = chx_s3.Checked;
                bool semana4 = chx_s4.Checked;
                NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
                DataTable tablafechas = nruta.getcalculoFechasporEquipo(mes, anio, diaSemana, semana1, semana2, semana3, semana4);
                tx_popuFechaSemana1.Text = tablafechas.Rows[0][0].ToString();
                tx_popuFechaSemana2.Text = tablafechas.Rows[0][1].ToString();
                tx_popuFechaSemana3.Text = tablafechas.Rows[0][2].ToString();
                tx_popuFechaSemana4.Text = tablafechas.Rows[0][3].ToString();
                bt_asignar_ModalPopupExtender.Show();
            }
            else
            {
                Response.Write("<script type='text/javascript'> alert('Error: Datos Cronograma No son Correctos') </script>");
                bt_asignar_ModalPopupExtender.Show();
            }
        }
        
        private bool verifiartablaCronogramaCorrecto(int cantVisita)
        {
            int cant = 0;

            if (chx_s1.Checked)
                cant++;

            if (chx_s2.Checked)
                cant++;

            if (chx_s3.Checked)
                cant++;

            if (chx_s4.Checked)
                cant++;

            if (cantVisita == cant)
            {
                return true;
            }
            else
                return false;

        }

        private bool verifiartablaCronogramaCorrectoModificar(int cantVisita)
        {
            int cant = 0;

            if (chx_popuSemana1modificar.Checked)
                cant++;

            if (chx_popuSemana2modificar.Checked)
                cant++;

            if (chx_popuSemana3modificar.Checked)
                cant++;

            if (chx_popuSemana4modificar.Checked)
                cant++;

            if (cantVisita == cant)
            {
                return true;
            }
            else
                return false;
        }

        private void calcularCronogramaModifiar()
        {
            DateTime fechaNow = DateTime.Now;
            int mes = fechaNow.Month;
            int anio = fechaNow.Year;
            int cantVisita = Convert.ToInt32(tx_popuCantVisitaMoficar.Text);
            int diaSemana = dd_popuDiamodificar.SelectedIndex;
            if (diaSemana == 0)
                diaSemana = 7;

            bool verificacion = verifiartablaCronogramaCorrectoModificar(cantVisita);
            if (verificacion == true)
            {
                bool semana1 = chx_popuSemana1modificar.Checked;
                bool semana2 = chx_popuSemana2modificar.Checked;
                bool semana3 = chx_popuSemana3modificar.Checked;
                bool semana4 = chx_popuSemana4modificar.Checked;
                NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
                DataTable tablafechas = nruta.getcalculoFechasporEquipo(mes, anio, diaSemana, semana1, semana2, semana3, semana4);
                tx_popuFechaSemana1modificar.Text = tablafechas.Rows[0][0].ToString();
                tx_popuFechaSemana2Modificar.Text = tablafechas.Rows[0][1].ToString();
                tx_popuFechaSemana3Modificar.Text = tablafechas.Rows[0][2].ToString();
                tx_popuFechaSemana4modificar.Text = tablafechas.Rows[0][3].ToString();
                bt_modificarPopu_ModalPopupExtender.Show();
            }
            else
            {
                Response.Write("<script type='text/javascript'> alert('Error: Datos Cronograma No son Correctos') </script>");
                bt_modificarPopu_ModalPopupExtender.Show();
            }
        }

        protected void bt_popuCalcularMoficar_Click(object sender, EventArgs e)
        {
            calcularCronogramaModifiar();
        }

        protected void bt_popuok_modificar_Click1(object sender, EventArgs e)
        {
            BotorModificarPopu();
        }

        private void BotorModificarPopu()
        {           

            if (gv_rutas.SelectedIndex > -1)
            {
                if (gv_equiposAsignados.SelectedIndex > -1)
                {

                    int codigoEquipo = Convert.ToInt32(gv_equiposAsignados.SelectedRow.Cells[1].Text);
                    int codigoRuta = Convert.ToInt32(gv_rutas.SelectedRow.Cells[1].Text);

                    string horaingreso = tx_popuHoraIngresoModificar.Text;
                    string horasalida = tx_popuHoraSalidaModificar.Text;
                    int cantVisita = Convert.ToInt32(tx_popuCantVisitaMoficar.Text);
                    string dia = dd_popuDiamodificar.SelectedItem.Text;
                    int nrodia = dd_popuDiamodificar.SelectedIndex;
                    float pasaje = Convert.ToSingle(tx_popuModificarPasaje.Text.Replace('.',','));
                    string ascensor = tx_popuModificarAscensor.Text;

                    bool bandera = verifiartablaCronogramaCorrectoModificar(cantVisita);
                    if (bandera)
                    {
                        string semana1 = "null";
                        if (!tx_popuFechaSemana1modificar.Text.Equals(""))
                        {
                            DateTime fecha1 = Convert.ToDateTime(tx_popuFechaSemana1modificar.Text);
                            semana1 = "'" + fecha1.ToString("yyyy/MM/dd") + "'";
                        }

                        string semana2 = "null";
                        if (!tx_popuFechaSemana2Modificar.Text.Equals(""))
                        {
                            DateTime fecha2 = Convert.ToDateTime(tx_popuFechaSemana2Modificar.Text);
                            semana2 = "'" + fecha2.ToString("yyyy/MM/dd") + "'";
                        }

                        string semana3 = "null";
                        if (!tx_popuFechaSemana3Modificar.Text.Equals(""))
                        {
                            DateTime fecha3 = Convert.ToDateTime(tx_popuFechaSemana3Modificar.Text);
                            semana3 = "'" + fecha3.ToString("yyyy/MM/dd") + "'";
                        }

                        string semana4 = "null";
                        if (!tx_popuFechaSemana4modificar.Text.Equals(""))
                        {
                            DateTime fecha4 = Convert.ToDateTime(tx_popuFechaSemana4modificar.Text);
                            semana4 = "'" + fecha4.ToString("yyyy/MM/dd") + "'";
                        }

                        DateTime fechaNow = DateTime.Now;
                        int mes = fechaNow.Month;
                        int anio = fechaNow.Year;

                        bool banderaSemana1 = chx_popuSemana1modificar.Checked;
                        bool banderaSemana2 = chx_popuSemana2modificar.Checked;
                        bool banderaSemana3 = chx_popuSemana3modificar.Checked;
                        bool banderaSemana4 = chx_popuSemana4modificar.Checked;

                        //--------------------------------------
                        NA_Responsables Nresp = new NA_Responsables();
                        string usuarioAux = Session["NameUser"].ToString();
                        string passwordAux = Session["passworuser"].ToString();
                        int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                        //--------------------------------------------------------
                        NA_Equipo neq = new NA_Equipo();
                        neq.actualizarLetraID_equipo(codigoEquipo, ascensor);

                        NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
                        nruta.ModificarEquipoRuta(codigoRuta, codigoEquipo, horaingreso, horasalida, dia, cantVisita, nrodia, banderaSemana1, banderaSemana2, banderaSemana3, banderaSemana4, mes, anio, semana1, semana2, semana3, semana4,codUser,pasaje);
                        nruta.ponerEnCerolasFechasNOcumplidas();

                        //-----------------------historial
                        // NA_Historial nhistorial = new NA_Historial();
                        // nhistorial.insertar(codUser, "Ha insertado el Equipo del Proyecto " + nombreProyecto + " con el codigo " + codigoProyecto + " exboEquipo " + exbo);
                        //----------------- historial----
                        verEquiposAsignadosRutas(codigoRuta,"");
                        buscarEdificios();
                        limpiarPopuModificar();
                        gv_equiposAsignados.SelectedIndex = -1;
                        Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
                    }

                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: No Selecciono Equipo') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: No Selecciono Ruta') </script>");
        }

      /*  protected void gv_equiposAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatosParaModificar();
        }

      private void cargarDatosParaModificar()
        {
            if(gv_rutas.SelectedIndex > 0){
             if(gv_equiposAsignados.SelectedIndex > 0){
                NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
                int codigoRuta = Convert.ToInt32(gv_rutas.SelectedRow.Cells[0].ToString());
                int codigoEqAsig = Convert.ToInt32(gv_equiposAsignados.SelectedRow.Cells[0].ToString());
                DataSet dato = nruta.getdetalleRutaEquipoCrono(codigoRuta,codigoEqAsig);
                string horaIngresoM = dato.Tables[0].Rows[0][2].ToString();
                string horaSalidaM = dato.Tables[0].Rows[0][3].ToString();
                int nroDiaM = Convert.ToInt32(dato.Tables[0].Rows[0][4].ToString());
                int cantVisitaM = Convert.ToInt32(dato.Tables[0].Rows[0][5].ToString());
                bool semana1M = Convert.ToBoolean(dato.Tables[0].Rows[0][6].ToString());
                bool semana2M = Convert.ToBoolean(dato.Tables[0].Rows[0][7].ToString());
                bool semana3M = Convert.ToBoolean(dato.Tables[0].Rows[0][8].ToString());
                bool semana4M = Convert.ToBoolean(dato.Tables[0].Rows[0][9].ToString());
                string Fechasemana1M = dato.Tables[0].Rows[0][10].ToString();
                string Fechasemana2M = dato.Tables[0].Rows[0][11].ToString();
                string Fechasemana3M = dato.Tables[0].Rows[0][12].ToString();
                string Fechasemana4M = dato.Tables[0].Rows[0][13].ToString();
                 ///------------------------cargar datos de modificar
                 
                tx_popuHoraIngresoModificar.Text = horaIngresoM;
                tx_popuHoraSalidaModificar.Text = horaSalidaM;
                tx_popuCantVisitaMoficar.Text = cantVisitaM.ToString();
                dd_popuDiamodificar.SelectedIndex = nroDiaM;
                tx_popuFechaSemana1modificar.Text = Fechasemana1M;
                tx_popuFechaSemana2Modificar.Text = Fechasemana2M;
                tx_popuFechaSemana3Modificar.Text = Fechasemana3M;
                tx_popuFechaSemana4modificar.Text = Fechasemana4M;
                chx_popuSemana1modificar.Checked = semana1M;
                chx_popuSemana2modificar.Checked = semana2M;
                chx_popuSemana3modificar.Checked = semana3M;
                chx_popuSemana4modificar.Checked = semana4M;
            }
           }
        }
        */
        public void limpiarTodoGridView() {
            gv_rutas.DataSource = null;
            gv_rutas.DataBind();

            gv_equipo.DataSource = null;
            gv_equipo.DataBind();

            gv_equiposAsignados.DataSource = null;
            gv_equiposAsignados.DataBind();

            gv_tecnicosAsignados.DataSource = null;
            gv_tecnicosAsignados.DataBind();
        
        }

        protected void bt_generar_Click(object sender, EventArgs e)
        {
            limpiarTodoGridView();

            DateTime fechaNow = DateTime.Now;
            int mes = fechaNow.Month;
            int anio = fechaNow.Year;

            //--------------------------------------
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            //--------------------------------------------------------

            DateTime fechaCopiar = fechaNow.AddMonths(-1);
            int mesCopiar = fechaCopiar.Month;
            int anioCopiar = fechaCopiar.Year;

            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
            bool bandera = nruta.generarRutasMantenimiento(mes, anio, codUser, mesCopiar, anioCopiar);
            nruta.ponerEnCerolasFechasNOcumplidas();
            if(bandera){
                Response.Write("<script type='text/javascript'> alert('Generado de Fechas: OK') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Fallo Generacion Ruta') </script>");

            mostrarRutasMantenimiento("",mes,anio);
        }

        protected void gv_equiposAsignados_SelectedIndexChanged1(object sender, EventArgs e)
        {
            limpiarPopuModificar();
            llenarParaModificar();
        }

        private void llenarParaModificar()
        {
            string horaIngreso = gv_equiposAsignados.SelectedRow.Cells[7].Text.Substring(0,5);
            string horaSalida = gv_equiposAsignados.SelectedRow.Cells[8].Text.Substring(0,5);
            int nroDia = Convert.ToInt32(gv_equiposAsignados.SelectedRow.Cells[10].Text);
            int cantDia = Convert.ToInt32(gv_equiposAsignados.SelectedRow.Cells[12].Text);
            CheckBox boolSemana1 = (CheckBox)gv_equiposAsignados.SelectedRow.Cells[14].Controls[0];
            CheckBox boolSemana2 = (CheckBox)gv_equiposAsignados.SelectedRow.Cells[15].Controls[0];
            CheckBox boolSemana3 = (CheckBox)gv_equiposAsignados.SelectedRow.Cells[16].Controls[0];
            CheckBox boolSemana4 = (CheckBox)gv_equiposAsignados.SelectedRow.Cells[17].Controls[0];
            bool semana1 = boolSemana1.Checked;
            bool semana2 = boolSemana2.Checked;
            bool semana3 = boolSemana3.Checked;
            bool semana4 = boolSemana4.Checked;
            string pasaje = gv_equiposAsignados.SelectedRow.Cells[13].Text;
            string ascensor = gv_equiposAsignados.SelectedRow.Cells[4].Text;
            if (ascensor.Equals("&nbsp;"))
            {
                ascensor = "";
            }

            tx_popuHoraIngresoModificar.Text = horaIngreso;
            tx_popuHoraSalidaModificar.Text = horaSalida;
            tx_popuCantVisitaMoficar.Text = cantDia.ToString();
            dd_popuDiamodificar.SelectedIndex = nroDia;
            tx_popuModificarPasaje.Text = pasaje;
            tx_popuModificarAscensor.Text = ascensor;
            chx_popuSemana1modificar.Checked = semana1;
            chx_popuSemana2modificar.Checked = semana2;
            chx_popuSemana3modificar.Checked = semana3;
            chx_popuSemana4modificar.Checked = semana4;

        }

        protected void bt_buscarEquiposAsignados_Click(object sender, EventArgs e)
        {
            int codigoRuta = Convert.ToInt32(gv_rutas.SelectedRow.Cells[1].Text);
            string edificio = tx_buscarEquiposAsignados.Text;
            verEquiposAsignadosRutas(codigoRuta, edificio);
        }

        protected void bt_eliminarTodaRuta1_Click(object sender, EventArgs e)
        {
            eliminarTodoslosEquiposylaRuta();
        }

        private void eliminarTodoslosEquiposylaRuta()
        {
            if(gv_rutas.SelectedIndex > -1){
                int codigoRuta;
                int.TryParse(gv_rutas.SelectedRow.Cells[1].Text, out codigoRuta);
                NA_RutaMantenimiento nr = new NA_RutaMantenimiento();
                bool bandera = nr.eliminartodosequiposyrutademantenimiento(codigoRuta);
                if(bandera == true){
                    DateTime fechaNow = DateTime.Now;
                    int mes = fechaNow.Month;
                    int anio = fechaNow.Year;
                    mostrarRutasMantenimiento("", mes, anio);
                    Response.Write("<script type='text/javascript'> alert('Borrado: OK') </script>");
                }else
                    Response.Write("<script type='text/javascript'> alert('Error: Fallo') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione una Ruta') </script>");
        }

            
                      
    }
}