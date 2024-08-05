using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_RutaMantenimiento
    {
        DA_RutaMantenimiento nruta = new DA_RutaMantenimiento(); 

        public NA_RutaMantenimiento() { }

        public int getsiguienteRutaMatenimiento(int mes , int anio) {
            DataSet dato = nruta.getallRutas(mes, anio);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return dato.Tables[0].Rows.Count;
            }
            else
                return 0;
        }


        public DataSet getallEquiposProyecto(string edificio) {
            return nruta.getallEquiposProyecto(edificio);
        }

        public DataSet getallEquiposProyectoRutaMantenimiento(string edificio)
        {
            return nruta.getallEquiposProyectoRutaMantenimiento(edificio);
        }

        public DataSet getAllFaltantesEquiposSinRutas(string Edificio, int mes, int anio) {
            return nruta.getAllFaltantesEquiposSinRutas(Edificio, mes, anio);
        }

        public bool insertarRutaMantenimiento(int nro, string detalle, int mes, int anio) {
            return nruta.insertarRutaMantenimiento(nro, detalle, mes, anio);
        }

        public DataSet getallRutaMantenimiento(string nombre, int mes,int anio) {
            return nruta.getallRutaMantenimiento(nombre, mes, anio);      
        }

        public DataSet getallRutaMantenimiento2(string codigo, string nombre, int mes, int anio)
        {
            return nruta.getallRutaMantenimiento2(codigo, nombre,mes,anio);
        }

        public DataSet getallEquiposRutasAsignadas(int codRuta, string nombreEdificio)
        {
            return nruta.getallEquiposRutasAsignadas(codRuta,nombreEdificio);
        }

        public DataSet getallEquiposRutasAsignadas2(string exbo, string edificio, int mes, int anio) {
            return nruta.getallEquiposRutasAsignadas2(exbo, edificio, mes, anio);
        }

        public bool modificarRutaMantenimiento(int codigo, int nro , string detalle, int mes, int anio)
        {
            return nruta.modificarRutaMantenimiento(codigo, nro, detalle, mes , anio);
        }

        public bool insertarEquipoRuta(int codRuta, int codEquipo, string horaEntrada, string horasalida, string dia, int cantvisitas, int nrodia, bool semana1, bool semana2, bool semana3, bool semana4, int mes, int anio, string fechaS1, string fechaS2, string fechaS3, string fechaS4, int codResp, float pasaje)
        {
            if (existeEquipoRuta(codRuta, codEquipo) == false)
            {
                NA_CronogramaVisitaRutaMantenimiento crono = new NA_CronogramaVisitaRutaMantenimiento();
                bool bandera = crono.insertarCronogramaVisitaRutaM(codRuta, codEquipo, cantvisitas, mes, anio, semana1, semana2, semana3, semana4, fechaS1, fechaS2, fechaS3, fechaS4, codResp,horaEntrada,horasalida,dia,nrodia,pasaje);
                return bandera;                  
            }
            else
                return false;
        }


     /*   public bool modificarCronogramaEquipoRutaMantemiento(int codRuta, int codEquipo, int codCronograma)
        {            
            return nruta.modificarCronogramaEquipoRutaMantemiento(codRuta,  codEquipo,  codCronograma);
        }*/


        public bool ModificarEquipoRuta(int codRuta, int codEquipo, string horaEntrada, string horasalida, string dia, int cantvisitas, int nrodia, bool semana1, bool semana2, bool semana3, bool semana4, int codmes, int anio, string fechaS1, string fechaS2, string fechaS3, string fechaS4, int codResp, float pasaje)
        { 
          
            NA_CronogramaVisitaRutaMantenimiento crono = new NA_CronogramaVisitaRutaMantenimiento();
            int codigoCronoMesAnio = crono.getCodigoCronoVisitaRutaEquipo(codRuta, codEquipo, codmes, anio);
            bool banderaCrono = crono.UpdateCronogramaVisitaRutaM(codigoCronoMesAnio, cantvisitas, semana1, semana2, semana3, semana4, fechaS1, fechaS2, fechaS3, fechaS4, codResp, horaEntrada,horasalida,dia,nrodia,pasaje);                                          
            return banderaCrono;
        }

        public bool insertarTecnicoRuta(int codRuta, int codTecnico, string supervisor, int mes, int anio)
        {
            return nruta.insertarTecnicoRuta( codRuta,  codTecnico,  supervisor, mes,anio);
        }

        public DataSet mostrarTecnicoRuta(int codRuta)
        {
            return nruta.mostrarTecnicoRuta(codRuta);
        }

        public DataSet mostrarTecnicoRuta(int mes, int anio)
        {
            return nruta.mostrarTecnicoRuta(mes, anio);
        }

        public DataSet mostrarTecnicoRuta2(string nombre, int mes, int anio)
        {
            return nruta.mostrarTecnicoRuta2(nombre, mes, anio);
        }


        public bool eliminarRuta(int codRuta, int mes, int anio)
        {
            return nruta.eliminarRuta(codRuta, mes, anio);
        }

        public bool tieneEquiposAsignadosRuta(int codRuta) {

            DataSet dato = nruta.equiposAsignadoRuta(codRuta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool tieneTecnicosAsignadosRuta(int codRuta)
        {

            DataSet dato = nruta.tecnicoAsignadosRuta(codRuta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool eliminarEquipoRutaMantemiento(int codRuta, int codEquipo)
        {
            return nruta.eliminarEquipoRutaMantemiento(codRuta,codEquipo);
        }

        public bool eliminarTecnicoRutaMantemiento(int codRuta, int codTecnico)
         {
             return nruta.eliminarTecnicoRutaMantemiento(codRuta, codTecnico);
        }

        public bool insertarBoletaMantenimiento(int codEquipo, int codTecnico, string boleta, string detalle, bool cambiorepuesto, string fechaboleta, string horallegada, string horasalida, string recepcion, bool banderaArreglo, string tipoBoleta, bool siningresoedificio)
        {
            return nruta.insertarBoletaMantenimiento( codEquipo,  codTecnico,  boleta,  detalle,  cambiorepuesto,  fechaboleta,  horallegada,  horasalida,  recepcion,  banderaArreglo,  tipoBoleta,  siningresoedificio);
        }

        public bool eliminarBoletaMantenimiento(int codigoBoleta) {
            return nruta.eliminarBoletaMantenimiento(codigoBoleta);
        }

        public DataSet mostrarBoletasMantenimiento(int codequipo, int codtecnico) {
            return nruta.mostrarBoletasMantenimiento( codequipo,  codtecnico);
        }

        public DataSet mostrarALLPersonalAsignadoRuta(int mes, int anio)
        {
            return nruta.mostrarALLPersonalAsignadoRuta( mes,  anio);
        }

        public DataSet mostrarALLEquiposAsignadosRutas(int mes, int anio)
        {
            return nruta.mostrarALLEquiposAsignadosRutas(mes,anio);
         
        }


        public DateTime getfechaAsignadaRutaMantenimiento(int diaInicial, int mes, int anio, int diadefecha) {
           
            DateTime date = new DateTime(anio, mes, diaInicial);

            System.Globalization.CultureInfo norwCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es");
            System.Globalization.Calendar cal = norwCulture.Calendar;
            int weekNo = cal.GetWeekOfYear(date, norwCulture.DateTimeFormat.CalendarWeekRule, norwCulture.DateTimeFormat.FirstDayOfWeek);
            int nrodiaActual = (int)date.DayOfWeek;

            if (nrodiaActual == diadefecha)
            {
               return  date;
            }
            else
            {
                int auxdiaActual = nrodiaActual;
                if (nrodiaActual == 0) { auxdiaActual = 7; }

                if (auxdiaActual > diadefecha)
                {
                    int calDia = (7 - auxdiaActual) + diadefecha;
                    DateTime auxdate = date.AddDays(calDia);
                    return auxdate;
                }
                else {
                    int calDia = (diadefecha - auxdiaActual);
                    DateTime auxdate = date.AddDays(calDia);
                    return auxdate;                
                }

            }

         }

        public DataSet mostrarALLEquiposAsignadosRutas_conFechas(int mes, int anio)
        {
            return nruta.mostrarALLEquiposAsignadosRutasFechas(mes, anio);
        }

      /*  public DataTable mostrarALLEquiposAsignadosRutas_conFechas(int mes, int anio)
        {
            DataSet Rutas = nruta.mostrarALLEquiposAsignadosRutas( mes, anio);
            

            DataTable TablaRutas = Rutas.Tables[0];
            TablaRutas.Columns.Add("Semana1", typeof(string));
            TablaRutas.Columns.Add("Semana2", typeof(string));
            TablaRutas.Columns.Add("Semana3", typeof(string));
            TablaRutas.Columns.Add("Semana4", typeof(string));

            for (int i = 0; i < TablaRutas.Rows.Count; i++)
            {
                int cantVisita = Convert.ToInt32(TablaRutas.Rows[i][7].ToString());
                int nroDia = Convert.ToInt32(TablaRutas.Rows[i][8].ToString());
                DataSet cronogramaNroVisitas = nruta.getcronogramaMesAnioRuta_porNroVisita(mes, anio, cantVisita);

                if (cronogramaNroVisitas.Tables[0].Rows.Count > 0)
                {
                    bool semana1 = (bool)cronogramaNroVisitas.Tables[0].Rows[0][3];
                    bool semana2 = (bool)cronogramaNroVisitas.Tables[0].Rows[0][4];
                    bool semana3 = (bool)cronogramaNroVisitas.Tables[0].Rows[0][5];
                    bool semana4 = (bool)cronogramaNroVisitas.Tables[0].Rows[0][6];

                    DateTime dateInicial = new DateTime(anio, mes, nroDia);
                    //primera pasada------------------------------------------------------
                    DateTime dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana1 == true)
                    {                        
                        TablaRutas.Rows[i]["Semana1"] = dateAux.ToString("dd/MM/yyyy");                        
                    }
                    else
                        TablaRutas.Rows[i]["Semana1"] = "";
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------segunda pasada ----------------------------
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana2 == true) {
                        TablaRutas.Rows[i]["Semana2"] = dateAux.ToString("dd/MM/yyyy");                        
                    }else
                        TablaRutas.Rows[i]["Semana2"] = "";
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------tercera pasada ----------------------------
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana3 == true) {
                        TablaRutas.Rows[i]["Semana3"] = dateAux.ToString("dd/MM/yyyy");                        
                    }else
                        TablaRutas.Rows[i]["Semana3"] = "";
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------cuarta pasada ----------------------------
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana4 == true) {
                        TablaRutas.Rows[i]["Semana4"] = dateAux.ToString("dd/MM/yyyy");                        
                    }else
                        TablaRutas.Rows[i]["Semana4"] = ""; 
                }

            }
            return TablaRutas;
        }
        */


        public DataTable getcalculoFechasporEquipo(int mes, int anio, int nroDia, bool semana1, bool semana2 , bool semana3, bool semana4)
        {

            DataTable TablaRutas = new DataTable();
            TablaRutas.Columns.Add("Semana1", typeof(string));
            TablaRutas.Columns.Add("Semana2", typeof(string));
            TablaRutas.Columns.Add("Semana3", typeof(string));
            TablaRutas.Columns.Add("Semana4", typeof(string));

    //----------------------------------------------------------
                    //DateTime dateInicial = new DateTime(anio, mes, nroDia);
                    DateTime dateInicial = new DateTime(anio, mes, 1);
                    //primera pasada------------------------------------------------------
                    string fechaSemana1 = "";
                    DateTime dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);                    
                    if (semana1 == true)
                    {
                       fechaSemana1 = dateAux.ToString("dd/MM/yyyy");
                    }
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------segunda pasada ----------------------------
                    string fechaSemana2 = "";
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana2 == true)
                    {
                        fechaSemana2 = dateAux.ToString("dd/MM/yyyy");
                    }
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------tercera pasada ----------------------------
                    string fechaSemana3 = "";
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana3 == true)
                    {
                        fechaSemana3 = dateAux.ToString("dd/MM/yyyy");
                    }
                    // se aumenta 1 dia para seguir la correlatividad.
                    dateInicial = dateAux.AddDays(1);
                    //----------------------------cuarta pasada ----------------------------
                    string fechaSemana4 = "";
                    dateAux = getfechaAsignadaRutaMantenimiento(dateInicial.Day, dateInicial.Month, dateInicial.Year, nroDia);
                    if (semana4 == true)
                    {
                        fechaSemana4 = dateAux.ToString("dd/MM/yyyy");
                    }

                    TablaRutas.Rows.Add(fechaSemana1, fechaSemana2, fechaSemana3, fechaSemana4);
                                       
            return TablaRutas;
        }

        public DataSet getdetalleRutaEquipoCrono(int codRuta, int codEquipo)
        {
            return nruta.getdetalleRutaEquipoCrono(codRuta,codEquipo);
        }

        public DataSet getdetalleRutaEquipoTodos(int mes, int anio)
        {
            return nruta.getdetalleRutaEquipoTodos(mes, anio);
        }

       public void generarFechasCronogramaRutaEquipo(int mes, int anio, int coduser)
        {
            DataSet rutas = getdetalleRutaEquipoTodos(mes, anio);
            for (int i = 0; i < rutas.Tables[0].Rows.Count; i++)
            {
                int codRuta = Convert.ToInt32(rutas.Tables[0].Rows[i][0].ToString());
                int codEquipo = Convert.ToInt32(rutas.Tables[0].Rows[i][1].ToString());
                string horaentrada = rutas.Tables[0].Rows[i][2].ToString();
                string horasalida = rutas.Tables[0].Rows[i][3].ToString();
                string diaSemana = rutas.Tables[0].Rows[i][4].ToString();
                int nrodia = Convert.ToInt32(rutas.Tables[0].Rows[i][5].ToString());
                float pasaje = Convert.ToSingle(rutas.Tables[0].Rows[i][12].ToString());

                if(nrodia == 0){
                    nrodia = 7;
                }
                int cantvisita = Convert.ToInt32(rutas.Tables[0].Rows[i][6].ToString());
                bool banderaSemana1 = Convert.ToBoolean(rutas.Tables[0].Rows[i][7].ToString());
                bool banderaSemana2 = Convert.ToBoolean(rutas.Tables[0].Rows[i][8].ToString());
                bool banderaSemana3 = Convert.ToBoolean(rutas.Tables[0].Rows[i][9].ToString());
                bool banderaSemana4 = Convert.ToBoolean(rutas.Tables[0].Rows[i][10].ToString());

                DataTable tablafechas = getcalculoFechasporEquipo(mes, anio, nrodia, banderaSemana1, banderaSemana2, banderaSemana3, banderaSemana4);
                string FechaSemana1 = tablafechas.Rows[0][0].ToString();
                string FechaSemana2 = tablafechas.Rows[0][1].ToString();
                string FechaSemana3 = tablafechas.Rows[0][2].ToString();
                string FechaSemana4 = tablafechas.Rows[0][3].ToString();

                string semana1 = "null";
                if (!FechaSemana1.Equals(""))
                {
                    DateTime fecha1 = Convert.ToDateTime(FechaSemana1);
                    semana1 = "'" + fecha1.ToString("yyyy/MM/dd") + "'";
                }

                string semana2 = "null";
                if (!FechaSemana2.Equals(""))
                {
                    DateTime fecha2 = Convert.ToDateTime(FechaSemana2);
                    semana2 = "'" + fecha2.ToString("yyyy/MM/dd") + "'";
                }

                string semana3 = "null";
                if (!FechaSemana3.Equals(""))
                {
                    DateTime fecha3 = Convert.ToDateTime(FechaSemana3);
                    semana3 = "'" + fecha3.ToString("yyyy/MM/dd") + "'";
                }

                string semana4 = "null";
                if (!FechaSemana4.Equals(""))
                {
                    DateTime fecha4 = Convert.ToDateTime(FechaSemana4);
                    semana4 = "'" + fecha4.ToString("yyyy/MM/dd") + "'";
                }  

                ModificarEquipoRuta(codRuta, codEquipo, horaentrada, horasalida, diaSemana, cantvisita, nrodia, banderaSemana1, banderaSemana2, banderaSemana3, banderaSemana4, mes, anio, semana1, semana2, semana3, semana4, coduser, pasaje);
            }
        }

        public bool tieneRutas(int mes, int anio)
        {
            DataSet dato = nruta.getallRutaMantenimiento("", mes, anio);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

      

        public bool generarRutasMantenimiento(int mes, int anio, int coduser, int mesCopiar, int anioCopiar)
        {
            bool bandera = false;
            if (tieneRutas(mes, anio) == false)
            {
                bandera = nruta.insert_generarRutasMantenimiento(mes, anio, coduser, mesCopiar, anioCopiar);                
                generarFechasCronogramaRutaEquipo(mes, anio, coduser);
            }
            else
            {
                bandera = nruta.update_generarRutasMantenimiento(mes, anio, coduser, mesCopiar, anioCopiar);
                generarFechasCronogramaRutaEquipo(mes, anio, coduser);
            }
            return bandera;          
        }


        public DataTable mostrarALLEquiposAsignadosRutas_consulta3( int mes, int anio, string tipoBoleta)
        {
            DataSet Rutas = nruta.mostrarALLEquiposAsignadosRutasFechas_Reporte(mes, anio);
           // int cantidadColumnasFechas = nruta.getcantidadColumnasFechasTotalRutas(mes, anio);

            DataTable TablaRutas = Rutas.Tables[0];
            TablaRutas.Columns.Add("Fecha1", typeof(string));
            TablaRutas.Columns.Add("Fecha2", typeof(string));
            TablaRutas.Columns.Add("Fecha3", typeof(string));
            TablaRutas.Columns.Add("Fecha4", typeof(string));
            TablaRutas.Columns.Add("Fecha5", typeof(string));
            TablaRutas.Columns.Add("Fecha6", typeof(string));
            TablaRutas.Columns.Add("Fecha7", typeof(string));
            TablaRutas.Columns.Add("Fecha8", typeof(string));
            TablaRutas.Columns.Add("Fecha9", typeof(string));
            TablaRutas.Columns.Add("Fecha10", typeof(string));
            TablaRutas.Columns.Add("Fecha11", typeof(string));
            TablaRutas.Columns.Add("Fecha12", typeof(string));
            TablaRutas.Columns.Add("Fecha13", typeof(string));
            TablaRutas.Columns.Add("Fecha14", typeof(string));
            TablaRutas.Columns.Add("Fecha15", typeof(string));

            for (int i = 0; i < TablaRutas.Rows.Count; i++)
            {
                int codRuta = Convert.ToInt32(Rutas.Tables[0].Rows[i][0].ToString());
                string exbo = Rutas.Tables[0].Rows[i][7].ToString();
                NEquipo neq = new NEquipo();
                int codEquipo = Convert.ToInt32(neq.getEquipo2(exbo).Tables[0].Rows[0][0].ToString());
                
                DataSet fechasBoletas = nruta.getFechasBoletasRutas(codRuta, codEquipo, mes, anio, tipoBoleta);

                for (int j = 0; j < fechasBoletas.Tables[0].Rows.Count; j++)
                {
                    string fecha = fechasBoletas.Tables[0].Rows[j][0].ToString();
                    string columna = "Fecha" + (j + 1);
                    TablaRutas.Rows[i][columna] = fecha;
                    
                }
            }
            return TablaRutas;
        }


        public DataTable mostrarALLEquiposAsignadosRutas_Bastones(int mes, int anio,string codRutaAux, string exboAux, string edificio ,string tipoBoleta)
        {
            DataSet Rutas = nruta.mostrarALLEquiposAsignadosRutasFechas_consulta(mes, anio, codRutaAux,exboAux, edificio);
            

            DataTable TablaRutas = Rutas.Tables[0];
            TablaRutas.Columns.Add("Fecha1", typeof(string));
            TablaRutas.Columns.Add("Fecha2", typeof(string));
            TablaRutas.Columns.Add("Fecha3", typeof(string));
            TablaRutas.Columns.Add("Fecha4", typeof(string));
            TablaRutas.Columns.Add("Fecha5", typeof(string));
            TablaRutas.Columns.Add("Fecha6", typeof(string));
            TablaRutas.Columns.Add("Fecha7", typeof(string));
            TablaRutas.Columns.Add("Fecha8", typeof(string));
            TablaRutas.Columns.Add("Fecha9", typeof(string));
            TablaRutas.Columns.Add("Fecha10", typeof(string));
            TablaRutas.Columns.Add("Fecha11", typeof(string));
            TablaRutas.Columns.Add("Fecha12", typeof(string));
            TablaRutas.Columns.Add("Fecha13", typeof(string));
            TablaRutas.Columns.Add("Fecha14", typeof(string));
            TablaRutas.Columns.Add("Fecha15", typeof(string));

            for (int i = 0; i < TablaRutas.Rows.Count; i++)
            {
                int codRuta = Convert.ToInt32(Rutas.Tables[0].Rows[i][0].ToString());
                string exbo = Rutas.Tables[0].Rows[i][6].ToString();
                NEquipo neq = new NEquipo();
                int codEquipo = Convert.ToInt32(neq.getEquipo2(exbo).Tables[0].Rows[0][0].ToString());

                DataSet fechasBoletas = nruta.getFechasBastones(codEquipo, mes, anio, tipoBoleta);

                for (int j = 0; j < fechasBoletas.Tables[0].Rows.Count; j++)
                {
                    string fecha = fechasBoletas.Tables[0].Rows[j][0].ToString();
                    string columna = "Fecha" + (j + 1);
                    TablaRutas.Rows[i][columna] = fecha;
                }
            }
            return TablaRutas;
        }


        public DataSet mostrarConsultaEquiposAsignadosRutasFechas(string codRuta, string exbo, int mes, int anio, string nombreProyecto, string personalAsignado)
        {
            return nruta.mostrarConsultaEquiposAsignadosRutasFechas( codRuta,  exbo,  mes,  anio,  nombreProyecto,  personalAsignado);
        }

        public DataSet getAllPersonalAsignadoRuta() {
            return nruta.getAllPersonalAsignadoRuta(); 
        }

        public bool existeEquipoRuta(int codRuta, int codEquipo ) {
            return nruta.existeEquipoRuta(codRuta, codEquipo);
        }

        public bool insertarExcelBastones(string direccionRuta, string nombreArchivo)
        {
            return nruta.insertarExcelBastones(direccionRuta,nombreArchivo);
        }

        public bool ponerEnCerolasFechasNOcumplidas() {
            return nruta.ponerEnCerolasFechasNOcumplidas();
        }

        internal bool eliminartodosequiposyrutademantenimiento(int codigoRuta)
        {
           return nruta.eliminartodosequiposyrutademantenimiento(codigoRuta);
        }
    }
}