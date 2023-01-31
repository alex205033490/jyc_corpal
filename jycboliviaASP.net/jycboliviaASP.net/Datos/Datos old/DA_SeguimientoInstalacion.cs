using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DA_SeguimientoInstalacion
    {
        private conexionMySql ConecRes = new conexionMySql();
       public DA_SeguimientoInstalacion() { }

        public bool insertar( string supervidor, string email,
                                string telefono,string pf_aoi,string pf_plano,
                                string pf_otros,string pjyc_c01,string pjyc_c05,
                                string pjyc_fa1,string pjyc_fa2,string eo_adecuado,
                                string eo_observaciones,string eo_electricidad,string eo_apli_modif_seguri,
                                string eo_cumpliotrorequisito,string ic_fechaexpedicion,
                                int ic_semanaentregarequerida,                              
                                int semanasestimadasintacion,string contratoinstalacion,
                                int semanaacumuladaportecnicoasignado)
        {
           
                string consulta = "insert into tb_seguimientoinstalacion "+
                                   " (supervidor,email, "+
                                   " telefono,pf_aoi,pf_plano, "+
                                   " pf_otros,pjyc_c01,pjyc_c05, "+
                                   " pjyc_fa1,pjyc_fa2,eo_adecuado, "+
                                   " eo_observaciones,eo_electricidad,eo_apli_modif_seguri, "+
                                   " eo_cumpliotrorequisito,ic_fechaexpedicion, "+
                                   " ic_semanaentregarequerida,"+                                   
                                   " semanasestimadasintacion,contratoinstalacion, "+
                                   " semanaacumuladaportecnicoasignado) values "+
                                   " ('"+supervidor+"','"+email+"', "+
                                   " '"+telefono+"','"+pf_aoi+"','"+pf_plano+"', "+
                                   " '"+pf_otros+"','"+pjyc_c01+"','"+pjyc_c05+"', "+
                                   " '"+pjyc_fa1+"','"+pjyc_fa2+"','"+eo_adecuado+"', "+
                                   " '"+eo_observaciones+"','"+eo_electricidad+"','"+eo_apli_modif_seguri+"', "+
                                   " '"+eo_cumpliotrorequisito+"',"+ic_fechaexpedicion+","+
                                   +ic_semanaentregarequerida+","+                                   
                                   semanasestimadasintacion+" ,'"+contratoinstalacion+"', "+
                                   semanaacumuladaportecnicoasignado+" );";

                return ConecRes.ejecutarMySql(consulta);                         
        }



        public bool modificar(int codSeguimientoInst, string supervidor, string email,
string telefono, string pf_aoi, string pf_plano,
string pf_otros, string pjyc_c01, string pjyc_c05,
string pjyc_fa1, string pjyc_fa2, string eo_adecuado,
string eo_observaciones, string eo_electricidad, string eo_apli_modif_seguri,
string eo_cumpliotrorequisito, string ic_fechaexpedicion,
int ic_semanaentregarequerida, 
int semanasestimadasintacion, string contratoinstalacion,
int semanaacumuladaportecnicoasignado)
        {
            
                string consulta = "update tb_seguimientoinstalacion set "+
                                   " supervidor = '"+supervidor+"',email = '"+email+"', "+
                                   " telefono = '"+telefono+"',pf_aoi = '"+pf_aoi+"',pf_plano = '"+pf_plano+"', "+
                                   " pf_otros = '"+pf_otros+"',pjyc_c01 ='"+pjyc_c01+"',pjyc_c05 = '"+pjyc_c05+"', "+
                                   " pjyc_fa1 = '"+pjyc_fa1+"',pjyc_fa2= '"+pjyc_fa2+"',eo_adecuado = '"+eo_adecuado+"', "+
                                   " eo_observaciones = '"+eo_observaciones+"',eo_electricidad = '"+eo_electricidad+"',eo_apli_modif_seguri = '"+eo_apli_modif_seguri+"', "+
                                   " eo_cumpliotrorequisito = '"+eo_cumpliotrorequisito+"',ic_fechaexpedicion = "+ic_fechaexpedicion+", "+
                                   " ic_semanaentregarequerida = "+ic_semanaentregarequerida+","+                                                                    
                                   " semanasestimadasintacion = "+semanasestimadasintacion+",contratoinstalacion = '"+contratoinstalacion+"', "+
                                   " semanaacumuladaportecnicoasignado = "+semanaacumuladaportecnicoasignado+" where codigo = "+codSeguimientoInst;

                return ConecRes.ejecutarMySql(consulta);

             
                    }




        public bool eliminar()
        {
            return false;
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

    }
}