using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;
using System.Globalization;

namespace jycboliviaASP.net.Negocio
{
    public class NA_CronogramaTecnico
    {        
        private DA_CronogramaTecnico Dcrono = new DA_CronogramaTecnico();

        public NA_CronogramaTecnico() { }

        private DataSet gethoras5Paradas()
        {
            string consulta = "select crono.codigo,crono.nombre,crono.5paradas from tb_cronogramatecnico crono ";
            return Dcrono.getDatos(consulta);
        }

        private int getDias5Paradas()
        {
            string consulta = "select round(sum(crono.5paradas)/8) as suma from tb_cronogramatecnico crono ;";
            DataSet tablaD = Dcrono.getDatos(consulta);
            int dato = Convert.ToInt32(tablaD.Tables[0].Rows[0][0].ToString());
            return dato;
        }
        
        private DataSet gethoras10Paradas()
        {
            string consulta = "select crono.codigo,crono.nombre,crono.10paradas from tb_cronogramatecnico crono";
            return Dcrono.getDatos(consulta);
        }

        private int getDias10Paradas()
        {
            string consulta = "select round(sum(crono.10paradas)/8) as suma from tb_cronogramatecnico crono ;";
            DataSet tablaD = Dcrono.getDatos(consulta);
            int dato = Convert.ToInt32(tablaD.Tables[0].Rows[0][0].ToString());
            return dato;
        }

        private DataSet gethoras15Paradas()
        {
            string consulta = "select crono.codigo,crono.nombre,crono.15paradas from tb_cronogramatecnico crono";
            return Dcrono.getDatos(consulta);
        }

        private int getDias15Paradas()
        {
            string consulta = "select round(sum(crono.15paradas)/8) as suma from tb_cronogramatecnico crono ;";
            DataSet tablaD = Dcrono.getDatos(consulta);
            int dato = Convert.ToInt32(tablaD.Tables[0].Rows[0][0].ToString());
            return dato;
        }

        private DataSet gethoras20Paradas()
        {
            string consulta = "select crono.codigo,crono.nombre,crono.20paradas from tb_cronogramatecnico crono";
            return Dcrono.getDatos(consulta);
        }

        private int getDias20Paradas()
        {
            string consulta = "select round(sum(crono.20paradas)/8) as suma from tb_cronogramatecnico crono ;";
            DataSet tablaD = Dcrono.getDatos(consulta);
            int dato = Convert.ToInt32(tablaD.Tables[0].Rows[0][0].ToString());
            return dato;
        }


        private DataSet gethoras25Paradas()
        {
            string consulta = "select crono.codigo,crono.nombre,crono.25paradas from tb_cronogramatecnico crono";
            return Dcrono.getDatos(consulta);
        }

        private int getDias25Paradas()
        {
            string consulta = "select round(sum(crono.25paradas)/8) as suma from tb_cronogramatecnico crono ;";
            DataSet tablaD = Dcrono.getDatos(consulta);
            int dato = Convert.ToInt32(tablaD.Tables[0].Rows[0][0].ToString());
            return dato;
        }

        
        
        public DataSet getHoras_segunParadas(int paradas) {
            DataSet dato = null;
            switch(paradas){
                case 5:
                    dato = gethoras5Paradas();
                    break;
                case 10:
                    dato = gethoras10Paradas();
                    break;
                case 15:
                    dato = gethoras15Paradas();
                    break;
                case 20:
                    dato = gethoras20Paradas();
                    break;
                case 25:
                    dato = gethoras25Paradas();
                    break;
            }
            return dato;        
        }


        public int getDias_segunParadas(int paradas) {
            int dias = 0;
            switch (paradas) { 
                case 5:
                    dias = getDias5Paradas();
                    break;
                case 10 :
                    dias = getDias10Paradas();
                    break;
                case 15:
                    dias = getDias15Paradas();
                    break;
                case 20:
                    dias = getDias20Paradas();
                    break;
                case 25:
                    dias = getDias25Paradas();
                    break;
            }
            return dias;        
        }

        
        public int getParadasExacta(int paradas) {

            int dato = 0;
            if (paradas <= 5)
                dato = 5;
            else
                if (paradas <= 10)
                    dato = 10;
                else
                    if (paradas <= 15)
                        dato = 15;
                    else
                        if (paradas <= 20)
                            dato = 20;
                        else
                            if (paradas <= 25)
                                dato = 25;
                            else
                                dato = 25;

            return dato;
        }

        public int calcularSabadosenFecha(DateTime fechaIni,int cantDias)
        {
            DateTime dia = fechaIni;
            int i = 0;
            int sabado = 0;            
            while (i < cantDias)
            {
                if (dia.DayOfWeek == DayOfWeek.Sunday)
                {                   
                    i--;
                }
                if (dia.DayOfWeek == DayOfWeek.Saturday)
                {
                    sabado++;
                }

                dia = dia.AddDays(1);
                i++;
            }
            return sabado;
        }


        public DateTime calcularFechaFinalizacionCronograma(DateTime fechaIni, int cantDias)
        {
            DateTime dia = fechaIni;            
            int i = 0;
            int domingo = 0;
            
            while (i < cantDias)
            {
                if (dia.DayOfWeek == DayOfWeek.Sunday)
                {
                    domingo++;
                }                
                
                dia = dia.AddDays(1);                   
                i++;
            }

            
           domingo--;

           dia = dia.AddDays(domingo);
            return dia;
        }


        public DataSet calcularCronogramaTecnico(DateTime fechaIni,int paradas)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            dt.Columns.Add("Horas_Dia");
            dt.Rows.Add("8:30 - 9:30");
            dt.Rows.Add("9:30 - 10:30");
            dt.Rows.Add("10:30 - 11:30");
            dt.Rows.Add("11:30 - 12:30");
            dt.Rows.Add("14:30 - 15:30");
            dt.Rows.Add("15:30 - 16:30");
            dt.Rows.Add("16:30 - 17:30");
            dt.Rows.Add("17:30 - 18:30");

            DateTime dia = fechaIni;

            int NroParadas = getParadasExacta(paradas);
            int cantDias = getDias_segunParadas(NroParadas);
            //--------------variables
            int i = 1;
            int sabado = 0;
            int domingo = 0;

            int filahoras = 0;
            DataSet tablaHorasParadas = getHoras_segunParadas(NroParadas);
            int nrofilasHoras = tablaHorasParadas.Tables[0].Rows.Count;
            

            string nombreProceso = tablaHorasParadas.Tables[0].Rows[filahoras][1].ToString();
            int horaProceso = Convert.ToInt32(tablaHorasParadas.Tables[0].Rows[filahoras][2].ToString());
            //--------------crearcolumnas
            int cantidadSabados = calcularSabadosenFecha(fechaIni, cantDias);
         //   int division = (cantidadSabados / 2);
         //   double residuo = (cantidadSabados % 2);
            int sabadoAdicional = cantidadSabados;
         //   if(residuo > 0){
         //       sabadoAdicional = division + 1;
         //   }

            if (sabadoAdicional > 1) {
                sabadoAdicional--;
            }

            int cantDiasAux = cantDias + sabadoAdicional;
            while (i <= cantDiasAux)
            {
                if (dia.DayOfWeek == DayOfWeek.Sunday)
                {
                    domingo++;
                    i--;
                }
                else
                {
                    dt.Columns.Add(dia.ToString("d", CultureInfo.CreateSpecificCulture("en-NZ")));
                                        
                    if (dia.DayOfWeek == DayOfWeek.Saturday)
                    {
                        sabado++;
                        
                        int horas = 1;
                        while (horas <= 4)
                        {
                            if (horaProceso == 0 && filahoras < nrofilasHoras)
                            {                                
                                nombreProceso = tablaHorasParadas.Tables[0].Rows[filahoras][1].ToString();
                                horaProceso = Convert.ToInt32(tablaHorasParadas.Tables[0].Rows[filahoras][2].ToString());
                            }

                            while (horaProceso > 0 && horas <= 4)
                            {
                                dt.Rows[horas - 1][i] = nombreProceso;
                                horaProceso--;
                                horas++;
                            }
                            filahoras++;

                            if (horaProceso > 0 && horas > 4)
                                filahoras--;

                            if (filahoras >= nrofilasHoras)
                                break;
                        }

                    }
                    else
                    {
                       int horas = 1;
                        while (horas <= 8)
                        {
                            if (horaProceso == 0 && filahoras < nrofilasHoras) {
                                nombreProceso = tablaHorasParadas.Tables[0].Rows[filahoras][1].ToString();
                                horaProceso = Convert.ToInt32(tablaHorasParadas.Tables[0].Rows[filahoras][2].ToString());
                            }

                            while (horaProceso > 0 && horas <= 8)
                            {                               
                                dt.Rows[horas-1][i]= nombreProceso;
                                horaProceso--;
                                horas++;
                            }
                            filahoras++;
                            if (horaProceso > 0 && horas > 8)
                                filahoras--;
                            if (filahoras >= nrofilasHoras)
                                break;
                        }
                    }                    
                }
                dia = dia.AddDays(1);
                i++;
             
            }
            dia = dia.AddDays(-1);


            //---------------------
        
            return ds;
        }

        //-------------------------------- separar por fases ------------------------
        private DataSet gethoras5Paradas2(int desde , int hasta)
        {
            string consulta = "select crono.codigo,crono.nombre,crono.5paradas from tb_cronogramatecnico crono "+
                                " where crono.codigo>="+desde+" and crono.codigo<="+hasta;
            return Dcrono.getDatos(consulta);
        }

        private int getDias5Paradas2(int desde, int hasta)
        {
            string consulta = "select round(sum(crono.5paradas)/8) as suma from tb_cronogramatecnico crono "+
                " where crono.codigo>=" + desde + " and crono.codigo<=" + hasta; 
            DataSet tablaD = Dcrono.getDatos(consulta);
            int dato = Convert.ToInt32(tablaD.Tables[0].Rows[0][0].ToString());
            return dato;
        }

        private DataSet gethoras10Paradas2(int desde, int hasta)
        {
            string consulta = "select crono.codigo,crono.nombre,crono.10paradas from tb_cronogramatecnico crono "+
                " where crono.codigo>=" + desde + " and crono.codigo<=" + hasta; 
            return Dcrono.getDatos(consulta);
        }

        private int getDias10Paradas2(int desde, int hasta)
        {
            string consulta = "select round(sum(crono.10paradas)/8) as suma from tb_cronogramatecnico crono "+
                " where crono.codigo>=" + desde + " and crono.codigo<=" + hasta; 
            DataSet tablaD = Dcrono.getDatos(consulta);
            int dato = Convert.ToInt32(tablaD.Tables[0].Rows[0][0].ToString());
            return dato;
        }

        private DataSet gethoras15Paradas2(int desde, int hasta)
        {
            string consulta = "select crono.codigo,crono.nombre,crono.15paradas from tb_cronogramatecnico crono "+
                " where crono.codigo>=" + desde + " and crono.codigo<=" + hasta; 
            return Dcrono.getDatos(consulta);
        }

        private int getDias15Paradas2(int desde, int hasta)
        {
            string consulta = "select round(sum(crono.15paradas)/8) as suma from tb_cronogramatecnico crono "+
                " where crono.codigo>=" + desde + " and crono.codigo<=" + hasta; 
            DataSet tablaD = Dcrono.getDatos(consulta);
            int dato = Convert.ToInt32(tablaD.Tables[0].Rows[0][0].ToString());
            return dato;
        }

        private DataSet gethoras20Paradas2(int desde, int hasta)
        {
            string consulta = "select crono.codigo,crono.nombre,crono.20paradas from tb_cronogramatecnico crono "+
                " where crono.codigo>=" + desde + " and crono.codigo<=" + hasta; 
            return Dcrono.getDatos(consulta);
        }

        private int getDias20Paradas2(int desde, int hasta)
        {
            string consulta = "select round(sum(crono.20paradas)/8) as suma from tb_cronogramatecnico crono "+
                " where crono.codigo>=" + desde + " and crono.codigo<=" + hasta; 
            DataSet tablaD = Dcrono.getDatos(consulta);
            int dato = Convert.ToInt32(tablaD.Tables[0].Rows[0][0].ToString());
            return dato;
        }


        private DataSet gethoras25Paradas2(int desde, int hasta)
        {
            string consulta = "select crono.codigo,crono.nombre,crono.25paradas from tb_cronogramatecnico crono "+
                " where crono.codigo>=" + desde + " and crono.codigo<=" + hasta; 
            return Dcrono.getDatos(consulta);
        }

        private int getDias25Paradas2(int desde, int hasta)
        {
            string consulta = "select round(sum(crono.25paradas)/8) as suma from tb_cronogramatecnico crono "+
                " where crono.codigo>=" + desde + " and crono.codigo<=" + hasta; 
            DataSet tablaD = Dcrono.getDatos(consulta);
            int dato = Convert.ToInt32(tablaD.Tables[0].Rows[0][0].ToString());
            return dato;
        }


        //--------------- FASE I -----------
        public DataSet getHoras_segunParadas_FaseI(int paradas)
        {
            DataSet dato = null;
            switch (paradas)
            {
                case 5:
                    dato = gethoras5Paradas2(1, 13);
                    break;
                case 10:
                    dato = gethoras10Paradas2(1,13);
                    break;
                case 15:
                    dato = gethoras15Paradas2(1,13);
                    break;
                case 20:
                    dato = gethoras20Paradas2(1,13);
                    break;
                case 25:
                    dato = gethoras25Paradas2(1,13);
                    break;
            }
            return dato;
        }


        public int getDias_segunParadas_FaseI(int paradas)
        {
            int dias = 0;
            switch (paradas)
            {
                case 5:
                    dias = getDias5Paradas2(1,13);
                    break;
                case 10:
                    dias = getDias10Paradas2(1,13);
                    break;
                case 15:
                    dias = getDias15Paradas2(1,13);
                    break;
                case 20:
                    dias = getDias20Paradas2(1,13);
                    break;
                case 25:
                    dias = getDias25Paradas2(1,13);
                    break;
            }
            return dias;
        }



        public DataSet calcularCronogramaTecnico_faceI(DateTime fechaIni, int paradas)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            dt.Columns.Add("Horas_Dia");
            dt.Rows.Add("8:30 - 9:30");
            dt.Rows.Add("9:30 - 10:30");
            dt.Rows.Add("10:30 - 11:30");
            dt.Rows.Add("11:30 - 12:30");
            dt.Rows.Add("14:30 - 15:30");
            dt.Rows.Add("15:30 - 16:30");
            dt.Rows.Add("16:30 - 17:30");
            dt.Rows.Add("17:30 - 18:30");

            DateTime dia = fechaIni;

            int NroParadas = getParadasExacta(paradas);
            int cantDias = getDias_segunParadas_FaseI(NroParadas);
            //--------------variables
            int i = 1;
            int sabado = 0;
            int domingo = 0;

            int filahoras = 0;
            DataSet tablaHorasParadas = getHoras_segunParadas_FaseI(NroParadas);
            int nrofilasHoras = tablaHorasParadas.Tables[0].Rows.Count;


            string nombreProceso = tablaHorasParadas.Tables[0].Rows[filahoras][1].ToString();
            int horaProceso = Convert.ToInt32(tablaHorasParadas.Tables[0].Rows[filahoras][2].ToString());
            //--------------crearcolumnas
            int cantidadSabados = calcularSabadosenFecha(fechaIni, cantDias);
            //   int division = (cantidadSabados / 2);
            //   double residuo = (cantidadSabados % 2);
           // int sabadoAdicional = cantidadSabados;
            //   if(residuo > 0){
            //       sabadoAdicional = division + 1;
            //   }

          //  if (sabadoAdicional > 1)
          //  {
           //     sabadoAdicional--;
          //  }

          //  int cantDiasAux = cantDias + sabadoAdicional;
            int cantDiasAux = cantDias + cantidadSabados;
            while (i <= cantDiasAux)
            {
                if (dia.DayOfWeek == DayOfWeek.Sunday)
                {
                    domingo++;
                    i--;
                }
                else
                {
                    dt.Columns.Add(dia.ToString("d", CultureInfo.CreateSpecificCulture("en-NZ")));

                    if (dia.DayOfWeek == DayOfWeek.Saturday)
                    {
                        sabado++;

                        int horas = 1;
                        while (horas <= 4)
                        {
                            if (horaProceso == 0 && filahoras < nrofilasHoras)
                            {
                                nombreProceso = tablaHorasParadas.Tables[0].Rows[filahoras][1].ToString();
                                horaProceso = Convert.ToInt32(tablaHorasParadas.Tables[0].Rows[filahoras][2].ToString());
                            }

                            while (horaProceso > 0 && horas <= 4)
                            {
                                dt.Rows[horas - 1][i] = nombreProceso;
                                horaProceso--;
                                horas++;
                            }
                            filahoras++;

                            if (horaProceso > 0 && horas > 4)
                                filahoras--;

                            if (filahoras >= nrofilasHoras)
                                break;
                        }

                    }
                    else
                    {
                        int horas = 1;
                        while (horas <= 8)
                        {
                            if (horaProceso == 0 && filahoras < nrofilasHoras)
                            {
                                nombreProceso = tablaHorasParadas.Tables[0].Rows[filahoras][1].ToString();
                                horaProceso = Convert.ToInt32(tablaHorasParadas.Tables[0].Rows[filahoras][2].ToString());
                            }

                            while (horaProceso > 0 && horas <= 8)
                            {
                                dt.Rows[horas - 1][i] = nombreProceso;
                                horaProceso--;
                                horas++;
                            }
                            filahoras++;
                            if (horaProceso > 0 && horas > 8)
                                filahoras--;
                            if (filahoras >= nrofilasHoras)
                                break;
                        }
                    }
                }
                dia = dia.AddDays(1);
                i++;
            }
            dia = dia.AddDays(-1);
            //---------------------
            return ds;
        }


        //------------------  FASE II
        public DataSet getHoras_segunParadas_FaseII(int paradas)
        {
            DataSet dato = null;
            switch (paradas)
            {
                case 5:
                    dato = gethoras5Paradas2(14, 20);
                    break;
                case 10:
                    dato = gethoras10Paradas2(14, 20);
                    break;
                case 15:
                    dato = gethoras15Paradas2(14, 20);
                    break;
                case 20:
                    dato = gethoras20Paradas2(14, 20);
                    break;
                case 25:
                    dato = gethoras25Paradas2(14, 20);
                    break;
            }
            return dato;
        }


        public int getDias_segunParadas_FaseII(int paradas)
        {
            int dias = 0;
            switch (paradas)
            {
                case 5:
                    dias = getDias5Paradas2(14, 20);
                    break;
                case 10:
                    dias = getDias10Paradas2(14, 20);
                    break;
                case 15:
                    dias = getDias15Paradas2(14, 20);
                    break;
                case 20:
                    dias = getDias20Paradas2(14, 20);
                    break;
                case 25:
                    dias = getDias25Paradas2(14, 20);
                    break;
            }
            return dias;
        }



        public DataSet calcularCronogramaTecnico_faceII(DateTime fechaIni, int paradas)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            dt.Columns.Add("Horas_Dia");
            dt.Rows.Add("8:30 - 9:30");
            dt.Rows.Add("9:30 - 10:30");
            dt.Rows.Add("10:30 - 11:30");
            dt.Rows.Add("11:30 - 12:30");
            dt.Rows.Add("14:30 - 15:30");
            dt.Rows.Add("15:30 - 16:30");
            dt.Rows.Add("16:30 - 17:30");
            dt.Rows.Add("17:30 - 18:30");

            DateTime dia = fechaIni;

            int NroParadas = getParadasExacta(paradas);
            int cantDias = getDias_segunParadas_FaseII(NroParadas);
            //--------------variables
            int i = 1;
            int sabado = 0;
            int domingo = 0;

            int filahoras = 0;
            DataSet tablaHorasParadas = getHoras_segunParadas_FaseII(NroParadas);
            int nrofilasHoras = tablaHorasParadas.Tables[0].Rows.Count;


            string nombreProceso = tablaHorasParadas.Tables[0].Rows[filahoras][1].ToString();
            int horaProceso = Convert.ToInt32(tablaHorasParadas.Tables[0].Rows[filahoras][2].ToString());
            //--------------crearcolumnas
            int cantidadSabados = calcularSabadosenFecha(fechaIni, cantDias);
            //   int division = (cantidadSabados / 2);
            //   double residuo = (cantidadSabados % 2);
            int sabadoAdicional = cantidadSabados;
            //   if(residuo > 0){
            //       sabadoAdicional = division + 1;
            //   }

            if (sabadoAdicional > 1)
            {
                sabadoAdicional--;
            }

            int cantDiasAux = cantDias + sabadoAdicional;
            while (i <= cantDiasAux)
            {
                if (dia.DayOfWeek == DayOfWeek.Sunday)
                {
                    domingo++;
                    i--;
                }
                else
                {
                    dt.Columns.Add(dia.ToString("d", CultureInfo.CreateSpecificCulture("en-NZ")));

                    if (dia.DayOfWeek == DayOfWeek.Saturday)
                    {
                        sabado++;

                        int horas = 1;
                        while (horas <= 4)
                        {
                            if (horaProceso == 0 && filahoras < nrofilasHoras)
                            {
                                nombreProceso = tablaHorasParadas.Tables[0].Rows[filahoras][1].ToString();
                                horaProceso = Convert.ToInt32(tablaHorasParadas.Tables[0].Rows[filahoras][2].ToString());
                            }

                            while (horaProceso > 0 && horas <= 4)
                            {
                                dt.Rows[horas - 1][i] = nombreProceso;
                                horaProceso--;
                                horas++;
                            }
                            filahoras++;

                            if (horaProceso > 0 && horas > 4)
                                filahoras--;

                            if (filahoras >= nrofilasHoras)
                                break;
                        }

                    }
                    else
                    {
                        int horas = 1;
                        while (horas <= 8)
                        {
                            if (horaProceso == 0 && filahoras < nrofilasHoras)
                            {
                                nombreProceso = tablaHorasParadas.Tables[0].Rows[filahoras][1].ToString();
                                horaProceso = Convert.ToInt32(tablaHorasParadas.Tables[0].Rows[filahoras][2].ToString());
                            }

                            while (horaProceso > 0 && horas <= 8)
                            {
                                dt.Rows[horas - 1][i] = nombreProceso;
                                horaProceso--;
                                horas++;
                            }
                            filahoras++;
                            if (horaProceso > 0 && horas > 8)
                                filahoras--;
                            if (filahoras >= nrofilasHoras)
                                break;
                        }
                    }
                }
                dia = dia.AddDays(1);
                i++;
            }
            dia = dia.AddDays(-1);
            //---------------------
            return ds;
        }

//---------------------- fin fases



    }
}