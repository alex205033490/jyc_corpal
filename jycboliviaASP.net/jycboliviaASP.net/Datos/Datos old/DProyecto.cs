using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DProyecto
    {

        private conexionMySql conexion = new conexionMySql();

        public DProyecto() { }

        public bool insertarProyecto(string nombre, string nombre2, string direccion, string fecha_Proy, int codigoEncargado, int codigoResponsable, int estado, int codzona, string departamento)
        {
            string zonaAux = "null";
            string codencargadoAux = "null";
            if (codigoEncargado > -1)
            {
                codencargadoAux = Convert.ToString(codigoEncargado);
            }

            if (codzona > -1)
            {
                zonaAux = Convert.ToString(codzona);
            }

            string consulta = "insert into tb_proyecto(nombre, nombre2, direccion, fecha_Proy, codEncargado, codigores, estado,codzona, departamento) values('" + nombre + "','" + nombre2 + "', '" + direccion + "', '" + fecha_Proy + "', " + codencargadoAux + ", " + codigoResponsable + ", " + estado + "," + zonaAux + ", '" + departamento + "',);";
            return conexion.ejecutarMySql(consulta);
        }

        public bool insertarProyectoPP(string nombre, string nombre2, string direccion, string fecha_Proy, int codigoEncargado, int codigoResponsable, int estado, int codzona, string departamento, int codcobradorasignado)
        {
                string zonaAux = "null";
                string codencargadoAux = "null";
                string codcobradorAux = "null";

                if(codigoEncargado > -1){
                    codencargadoAux = Convert.ToString(codigoEncargado);
                }

                if(codzona > -1){
                    zonaAux = Convert.ToString(codzona);
                }

                if (codcobradorasignado > -1)
                {
                    codcobradorAux = Convert.ToString(codcobradorasignado);
                }

                string consulta = "insert into tb_proyecto(nombre, nombre2, direccion, fecha_Proy, codEncargado, codigores, estado,codzona, departamento,codcobradorasignado) values('" + nombre + "','" + nombre2 + "', '" + direccion + "', '" + fecha_Proy + "', " + codencargadoAux + ", " + codigoResponsable + ", " + estado + "," + zonaAux + ", '" + departamento + "'," + codcobradorAux + ");";
                return conexion.ejecutarMySql(consulta);
        }

        public bool modificarProyectoEncargadoPagoPP(int codigo, string nombre, string direccion, int codigoEncargado, int codigoResponsable, int estado, int codzona, string departamento, string nit, string facturar, string banco, int codcobradorasignado, string variableSimec) 
        {
            
                string zonaAux = "null";
                string codencargadoAux = "null";
                string codcobradorAux = "null";
                if (codigoEncargado > -1)
                {
                    codencargadoAux = Convert.ToString(codigoEncargado);
                }

                if (codzona > -1)
                {
                    zonaAux = Convert.ToString(codzona);
                }

            if(codcobradorasignado > -1){
                codcobradorAux = Convert.ToString(codcobradorasignado);
            }

                string consulta = "update tb_proyecto set tb_proyecto.nombre= '" + nombre + "', tb_proyecto.direccion= '" + direccion + "',  tb_proyecto.codEncargado=" + codencargadoAux + ", tb_proyecto.codigores =" + codigoResponsable + ", tb_proyecto.codzona = " + zonaAux + " , tb_proyecto.estado =" + 1 + ", tb_proyecto.departamento = '" + departamento + "',"+
                                   " tb_proyecto.nit_proy = '" + nit + "',tb_proyecto.facturar_proy = '" + facturar + "', tb_proyecto.banco_proy = '"+banco+"' "+
                                   " , tb_proyecto.codcobradorasignado = " + codcobradorAux +
                                  " , tb_proyecto.codvariablesimec = '"+variableSimec+"'"+
                    " where tb_proyecto.codigo= " + codigo + ";";
                return conexion.ejecutarMySql(consulta);
        }

        public bool modificarProyectoEncargadoPagoPP2(int codigo, string nombre, string direccion, int codigoEncargado, int codigoResponsable, int estado, string departamento, string nit, string facturar, string banco)
        {
            
            string codencargadoAux = "null";
            if (codigoEncargado > -1)
            {
                codencargadoAux = Convert.ToString(codigoEncargado);
            }

         
            string consulta = "update tb_proyecto set tb_proyecto.nombre= '" + nombre + "', tb_proyecto.direccion= '" + direccion + "',  tb_proyecto.codEncargado=" + codencargadoAux + ", tb_proyecto.codigores =" + codigoResponsable + ", tb_proyecto.estado =" + 1 + ", tb_proyecto.departamento = '" + departamento + "'," +
                               " tb_proyecto.nit_proy = '" + nit + "',tb_proyecto.facturar_proy = '" + facturar + "', tb_proyecto.banco_proy = '" + banco + "' " +                              
                " where tb_proyecto.codigo= " + codigo + ";";
            return conexion.ejecutarMySql(consulta);
        }

        public bool modificarProyectoPP(int codigo, string nombre, string direccion, int codigoEncargado, int codigoResponsable, int estado, int codzona, string departamento)
        {

            string zonaAux = "null";
            string codencargadoAux = "null";
            if (codigoEncargado > -1)
            {
                codencargadoAux = Convert.ToString(codigoEncargado);
            }

            if (codzona > -1)
            {
                zonaAux = Convert.ToString(codzona);
            }

            string consulta = "update tb_proyecto set tb_proyecto.nombre= '" + nombre + "', tb_proyecto.direccion= '" + direccion + "',  tb_proyecto.codEncargado=" + codencargadoAux + ", tb_proyecto.codigores =" + codigoResponsable + ", tb_proyecto.codzona = " + zonaAux + " , tb_proyecto.estado =" + 1 + ", tb_proyecto.departamento = '" + departamento + "' " +                               
                " where tb_proyecto.codigo= " + codigo + ";";
            return conexion.ejecutarMySql(consulta);
        }

        public bool eliminarProyecto(int codigo) 
        {
           
                string consulta = "update tb_proyecto set tb_proyecto.estado =" + 0 + " where tb_proyecto.codigo= " + codigo + ";";
                return conexion.ejecutarMySql(consulta);
            
           
        }

        public DataSet listarEncargadoProy(string nombreProyecto)
        {
            string consulta = "select  p.codigo, p.nombre, p.direccion,DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, "+
                               " p.departamento,z.nombre as 'Zona',  "+
                               " epago.nombre as 'EncargadoPago', epago.ci, epago.telefono, epago.celular, "+
                               " epago.direccion as 'DireccionEncargado', epago.email, epago.facturar_A, "+
                               " epago.Nit, epago.banco, epago.observacion, "+ 
                               " res.nombre as 'Cobrador', "+
                               " p.codvariablesimec "+
                               " from tb_proyecto p "+
                               " left join tb_encargado_pago epago on (p.codEncargado = epago.codigo) "+
                               " left join tb_zona z on (p.codzona = z.codigo) "+
                               " left join tb_responsable res on p.codcobradorasignado = res.codigo "+
                               " where p.estado = 1 and "+
                               " p.codigo in "+
                               " (select eq.cod_proyecto from tb_equipo eq group by eq.cod_proyecto ) ";
            if(!nombreProyecto.Equals("")){
                consulta = consulta + " and p.nombre like '%" + nombreProyecto + "%' ";
            }

            consulta = consulta + " order by p.nombre asc" ;

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listar()
        {
            string consulta = "select  p.codigo, p.nombre, p.direccion, " +
                              " DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, " +
                              " epago.nombre as 'EncargadoPago', " +                              
                              " z.nombre as 'Zona', p.departamento, " +
                              " epago.nit, epago.observacion " +
                              " from tb_proyecto p " +
                              " left join tb_encargado_pago epago on (p.codEncargado = epago.codigo) " +                              
                              " left join tb_zona z on (p.codzona = z.codigo) " +
                              " where p.estado = 1  order by p.nombre asc";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscar(string nombre) 
        {
            string consulta = "select  p.codigo, p.nombre, p.direccion, " +
                             " DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, " +
                             " epago.nombre as 'EncargadoPago', " +                             
                             " z.nombre as 'Zona' , p.departamento" +
                             " from tb_proyecto p " +
                             " left join tb_encargado_pago epago on (p.codEncargado = epago.codigo) " +                             
                             " left join tb_zona z on (p.codzona = z.codigo) " +
                             " where p.estado = 1 and p.nombre like '%"+nombre+"%' order by p.nombre asc";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscar_DeudaProyecto_Repuesto(string nombre)
        {
            string consulta = "select  p.codigo, p.nombre, p.direccion, " +
                             " DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, " +
                             " epago.nombre as 'EncargadoPago', " +
                             " z.nombre as 'Zona' , p.departamento" +
                             " , p.deudaproy as 'deudaInstalacion', p.deudarepuesto "+
                             " from tb_proyecto p " +
                             " left join tb_encargado_pago epago on (p.codEncargado = epago.codigo) " +
                             " left join tb_zona z on (p.codzona = z.codigo) " +
                             " where p.estado = 1 and p.nombre like '%" + nombre + "%' order by p.nombre asc";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscar1(string nombre)
        {
            string consulta = "select  p.codigo, p.nombre, p.direccion, " +
                             " DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, " +
                             " epago.nombre as 'EncargadoPago', " +                             
                             " z.nombre as 'Zona' , p.departamento" +
                             " from tb_proyecto p " +
                             " left join tb_encargado_pago epago on (p.codEncargado = epago.codigo) " +                             
                             " left join tb_zona z on (p.codzona = z.codigo) " +
                             " where p.estado = 1 and p.nombre like '%" + nombre + "%' order by p.nombre asc";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        

        public DataSet buscar3(string nombre) 
        {
            string consulta = "select  p.deudaproy as 'DeudaProyecto', p.codigo, p.nombre, p.direccion, " +
                             " DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, " +
                             " epago.nombre as 'EncargadoPago', " +                             
                             " z.nombre as 'Zona' , p.departamento" +
                             " from tb_proyecto p " +
                             " left join tb_encargado_pago epago on (p.codEncargado = epago.codigo) " +                             
                             " left join tb_zona z on (p.codzona = z.codigo) " +
                             " where p.estado = 1 and p.nombre like '%"+nombre+"%' order by p.nombre asc";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscador2(string nombre)
        {
            string consulta = "select  p.nombre "+
                             " from tb_proyecto p " +
                             " left join tb_encargado_pago epago on (p.codEncargado = epago.codigo) " +                             
                             " left join tb_zona z on (p.codzona = z.codigo) " +
                             " where p.estado = 1 and p.nombre like '%" + nombre + "%' order by p.nombre asc";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscadorCallCenter(string nombre)
        {

            ///modificar para los equipos que existen pero no estan en el lado de mantenimiento
            string consulta = "select  p.nombre  "+
                               " from tb_proyecto p "+
                               " where p.estado = 1 and  "+
                               " p.nombre like '%"+nombre+"%' and "+
                               " p.codigo in ( "+
                               " select eq.cod_proyecto "+
                               " from tb_equipo eq "+
                               " where eq.estado = 1 "+
                               " group by eq.cod_proyecto  "+
                               " )  "+
                               " order by p.nombre asc";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public int getCodigoProyect(string nombreProyecto) {
            string consulta = "select proy.codigo from tb_proyecto proy where proy.nombre = '"+nombreProyecto+"'";
            DataSet datoResul = conexion.consultaMySql(consulta);
            if (datoResul.Tables[0].Rows.Count > 0)
            {
                int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codUltimo;
            }
            else
                return -1;
        }

        public DataSet getProyect2(string nombreProyecto)
        {
            string consulta = "select  p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy "+
            " from tb_proyecto p where p.nombre ='" + nombreProyecto + "'";
            DataSet datoResul = conexion.consultaMySql(consulta);
            return datoResul;
        }


          public DataSet getProyect(int codigo)
        {
            string consulta = "select  p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy  from tb_proyecto p where p.estado =1 and p.codigo = "+ codigo ;
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }


        ///-----------------------------------------
         public DataSet existeProyecto(string nombreProyecto)
        {
            string consulta = "SELECT p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy "+ 
            " FROM tb_proyecto p WHERE p.nombre='" + nombreProyecto + "'";
            DataSet resultado = conexion.consultaMySql(consulta);
            return resultado;
        }

         public DataSet existeProyectoParecido(string nombreProyecto)
         {
             string consulta = "SELECT p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy " + 
            " FROM tb_proyecto p WHERE p.nombre like '%" + nombreProyecto + "%'";
             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;
         }



         public DataSet CuantosAscensorestieneParadoCliente(int CodigoEdificio)
         {
            /* string consulta = "select "+
                               " eq.exbo,ee.nombre as 'EstadoE' ,fe.fecha,fe.hora, res.nombre "+
                               " from tb_equipo eq, tb_fechaestadoequipo fe, tb_responsable res, tb_estado_equipo ee, "+
                               " tb_proyecto proy "+
                               " where "+
                               " eq.codfechaestadoequipo = fe.codigo and "+
                               " proy.codigo = eq.cod_proyecto and "+
                               " fe.codusercambio = res.codigo and "+
                               " fe.codEstadoEquipo = ee.codigo and "+
                               " fe.codEstadoEquipo  = 8 and "+
                               " proy.codigo = "+CodigoEdificio;*/
             string consulta = "select " +
                               " eq.exbo , " +
                               " esm.nombre as 'EstadoE', " +
                               " DATE_FORMAT(fe.fecha,'%d/%m/%Y') as 'FechaEstado',  " +
                               " fe.hora, " +
                               " IFNULL(res.nombre,'Sistema') as 'EstadoCambio' " +
                               " from  " +
                               " tb_proyecto proy , " +
                               " tb_equipo eq, " +
                               " tb_seguimiento seg, " +
                               " tb_estado_mantenimiento esm, " +
                               " tb_fechaestadomantenimiento fe " +
                               " left join tb_responsable res on (fe.codusercambio = res.codigo) " +
                               " where  " +
                               " eq.cod_proyecto = proy.codigo and " +
                               " seg.cod_equipo = eq.codigo and " +
                               " seg.years = year(now()) and " +
                               " seg.codfechaestadoman = fe.codigo and " +
                               " fe.codEstadoMan = esm.codigo and " +
                               " esm.codigo = 8 and "+
                               " proy.codigo = " + CodigoEdificio;

             DataSet resultado = conexion.consultaMySql(consulta); 
             return resultado;
         }

         public DataSet CuantosAscensorestieneParadoNosotros(int CodigoEdificio)
         {
             string consulta = "select " +
                               " eq.exbo,ee.nombre as 'EstadoE' ,fe.fecha,fe.hora, res.nombre " +
                               " from tb_equipo eq, tb_fechaestadoequipo fe, tb_responsable res, tb_estado_equipo ee, " +
                               " tb_proyecto proy " +
                               " where " +
                               " eq.codfechaestadoequipo = fe.codigo and " +
                               " proy.codigo = eq.cod_proyecto and " +
                               " fe.codusercambio = res.codigo and " +
                               " fe.codEstadoEquipo = ee.codigo and " +
                               " fe.codEstadoEquipo  = 9 and " +
                               " proy.codigo = " + CodigoEdificio;
             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;
         }


         public DataSet tienePlanPago(int CodigoEdificio)
         {
             string consulta = "SELECT  p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy " +
                               " FROM tb_proyecto p WHERE p.codigo = " + CodigoEdificio + " and p.planpago = true";
             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;
         }

         public DataSet tieneServicioSuspendido(int CodigoEdificio)
         {
             string consulta = "SELECT  p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy " +
                               " FROM tb_proyecto p WHERE p.codigo = " + CodigoEdificio + " and p.serviciosuspendido = true";
             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;
         }


         public DataSet horariodeAtencion(int CodigoEdificio)
         {
             string consulta = "SELECT  IFNULL(p.horarioatencion,'No Definido') AS 'HORARIO' from tb_proyecto p where " +
                               " p.codigo = " + CodigoEdificio + " and " +
                               " p.estado = 1";
             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;
         }

        public DataSet tieneDeudasPendientesProyectoPolizadeSeguro(int CodigoEdificio)
         {
             string consulta = "SELECT  p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy "+
                               " FROM tb_proyecto p WHERE p.codigo = "+CodigoEdificio+" and p.polizaseguro = true";
             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;
         }

        public DataSet tieneDeudasPendientesProyectoBoletaBancaria(int CodigoEdificio)
        {
            string consulta = "SELECT  p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy " +
                              " FROM tb_proyecto p WHERE p.codigo = " + CodigoEdificio + " and p.boletabancaria = true";
            DataSet resultado = conexion.consultaMySql(consulta);
            return resultado;
        }


        public DataSet tieneDeudasPendientesProyectoLetradeCambio(int CodigoEdificio)
        {
            string consulta = "SELECT  p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy " +
                              " FROM tb_proyecto p WHERE p.codigo = " + CodigoEdificio + " and p.letracambio = true";
            DataSet resultado = conexion.consultaMySql(consulta);
            return resultado;
        }

         public DataSet tieneDeudasPendientesProyecto(int CodigoEdificio)
         {
             string consulta = "SELECT  p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy " +
            " FROM tb_proyecto p WHERE p.codigo = "+CodigoEdificio+" and p.deudaproy = true";
             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;
         }

         public DataSet tieneDeudasRepuestoPendientesProyecto(int CodigoEdificio)
         {
             string consulta = "SELECT p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy " +
            " FROM tb_proyecto p WHERE p.codigo = " + CodigoEdificio + " and p.deudarepuesto = true";
             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;
         }


         

        /* public DataSet existeProyectoEspecifico(string nombreProyecto)
         {
             string consulta = "SELECT  p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy " +
             " FROM tb_proyecto p WHERE p.nombre = '" + nombreProyecto + "'";
             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;
         }*/

         public DataSet existeProyectoEspecificoConEquipos_CallCenter(string nombreProyecto)
         {             
             string consulta = "select  p.nombre  "+
                               " from tb_proyecto p "+
                               " where p.estado = 1 and  "+
                               " p.nombre = '"+nombreProyecto+"' and "+
                               " p.codigo in ( "+
                               " select eq.cod_proyecto "+
                               " from tb_equipo eq "+
                               " where eq.estado = 1 "+
                               " group by eq.cod_proyecto  "+
                               " )  "+
                               " order by p.nombre asc";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;             
         }

         
        public DataSet existeProyecto(int codProyecto)         
         {
             string consulta = "SELECT p.codigo, p.nombre, p.nombre2, p.direccion, DATE_FORMAT(p.fecha_Proy,'%d/%m/%Y') as Fecha, p.codEncargado, p.codigores , p.estado, p.codzona, p.departamento, p.nit_proy, p.facturar_proy, p.banco_proy " +
             " FROM tb_proyecto p WHERE p.codigo = "+ codProyecto;
             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;
         }


        public bool insertarProyecto1(string nombre, string nombre2, string fecha_Proy, int codigoEncargado, int estado, string ciudad, int codpropietario, string empresacontratoproyecto)
         {
             
                 string codencargadoAux = "null";
                 if (codigoEncargado > -1)
                 {
                     codencargadoAux = Convert.ToString(codigoEncargado);
                 }

                 string codPropietarioAux = "null";
                 if (codpropietario > -1)
                 {
                     codPropietarioAux = Convert.ToString(codpropietario);
                 }

                 string consulta = "insert into tb_proyecto(nombre, nombre2, fecha_Proy, codEncargado, estado, departamento,codpropietario, empresacontratoproyecto) " +
                                   " values('" + nombre + "','" + nombre2 + "', '" + fecha_Proy + "', " + codencargadoAux + ", " + estado + ", '" + ciudad + "'," + codPropietarioAux + ", '" + empresacontratoproyecto + "'); ";
                 return conexion.ejecutarMySql(consulta);
                  
            
         }


        public bool modificarProyecto1(int codProyecto, string nombre, string fecha_Proy, int estado, string ciudad, string empresacontratoproyecto)
         {
             string consulta = "update tb_proyecto " +
                               " set " +
                               " tb_proyecto.nombre = '" + nombre + "', " +                               
                               " tb_proyecto.fecha_Proy = '" + fecha_Proy + "', " +                               
                               " tb_proyecto.estado = " + estado + ", " +
                               " tb_proyecto.departamento = '" + ciudad + "', " +
                               " tb_proyecto.empresacontratoproyecto = '"+empresacontratoproyecto+"' "+
                               " where tb_proyecto.codigo = " + codProyecto;
             return conexion.ejecutarMySql(consulta);
         }

        public bool modificarProyecto3(int codProyecto,string empresacontratoproyecto)
        {
            string consulta = "update tb_proyecto " +
                              " set " +                              
                              " tb_proyecto.empresacontratoproyecto = '" + empresacontratoproyecto + "' " +
                              " where tb_proyecto.codigo = " + codProyecto;
            return conexion.ejecutarMySql(consulta);
        }

         public int getUltimoCodigo()
         {
             try
             {
                 string consulta = "SELECT MAX(codigo) FROM tb_proyecto";
                 DataSet datoResul = conexion.consultaMySql(consulta);
                 int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                 return codUltimo;
             }
             catch (Exception)
             {
                 return -1;
             }
         }

         public bool cambiarEncargadoPago1(int codEncargado, int codigoProyecto) {
             string codencargadoAux = "null";
             if (codEncargado > -1)
             {
                 codencargadoAux = Convert.ToString(codEncargado);
             }

             string consulta = "UPDATE tb_proyecto set tb_proyecto.codpropietario = " + codencargadoAux + "  where tb_proyecto.codigo = " + codigoProyecto;
                return conexion.ejecutarMySql(consulta);
                
             
         
         }

         public bool cambiarDepartamento(int codigoProyecto, string departamento)
         {
          
                 string consulta = "UPDATE tb_proyecto set tb_proyecto.departamento = '"+departamento+"'  where tb_proyecto.codigo = " + codigoProyecto;
                 return conexion.ejecutarMySql(consulta);
                  
          

         }



        ///------------------------- otro
        ///
         public DataSet BuscarMonitoreoGestionProyecto(string nombreProyecto) {
             string consulta = "select "+
                                " p.nombre as 'Nombre Proyecto', "+
                                " DATE_FORMAT(p.apertura,'%d/%m/%Y') as 'Apertura', "+
                                " DATE_FORMAT(p.salidafab,'%d/%m/%Y') as 'Salida FAB', "+
                                " DATE_FORMAT(p.entregacontrato,'%d/%m/%Y') as 'Entrega Contrato', "+
                                " DATE_FORMAT(p.entregaacordada,'%d/%m/%Y') as 'Entrega Acordada', "+
                                " DATE_FORMAT(p.cobroadm,'%d/%m/%Y') as 'Cobro Administrativo', "+
                                " DATE_FORMAT(p.r189,'%d/%m/%Y') as 'R-189', "+
                                " DATE_FORMAT(p.r190,'%d/%m/%Y') as 'R-190', "+
                                " DATE_FORMAT(p.r182,'%d/%m/%Y') as 'R-182', "+
                                " DATE_FORMAT(p.r197,'%d/%m/%Y') as 'R-197', "+
                                " DATE_FORMAT(p.r198,'%d/%m/%Y') as 'R-198', "+
                                " DATE_FORMAT(p.r199,'%d/%m/%Y') as 'R-199', "+
                                " DATE_FORMAT(p.r200,'%d/%m/%Y') as 'R-200', "+
                                " DATE_FORMAT(p.r201,'%d/%m/%Y') as 'R-201', "+
                                " DATE_FORMAT(p.r202,'%d/%m/%Y') as 'R-202', "+
                                " DATE_FORMAT(p.r188,'%d/%m/%Y') as 'R-188', "+
                                " DATE_FORMAT(p.r183,'%d/%m/%Y') as 'R-183', "+
                                " DATE_FORMAT(p.r184,'%d/%m/%Y') as 'R-184', "+
                                " DATE_FORMAT(p.r194,'%d/%m/%Y') as 'R-194', "+
                                " DATE_FORMAT(p.r186,'%d/%m/%Y') as 'R-186' "+
                                " from tb_proyecto p "+ 
                                " where p.nombre like '%"+nombreProyecto+"%' "+
                                " group by p.nombre asc";

             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;
         
         }

         public DataSet getEquiposdeProyect(int codProyecto)
         {
             string consulta = "select * from tb_equipo eq where eq.cod_proyecto = "+codProyecto;

             DataSet resultado = conexion.consultaMySql(consulta);
             return resultado;

         }


         public bool actualicarControl(int codigoProyecto,string apertura ,string salidafab,string entregacontrato,string entregaacordada,string cobroadm ,
                                        string r189 ,string r190 ,string r182 ,string r197 ,string r198 ,string r199 ,string r200 ,
                                        string r201 ,string r202 ,string r188 ,string r183 ,string r184 ,string r194 ,string r186 ) {
            
                 string consulta = "update tb_proyecto set  "+
                                    " apertura = "+apertura+", "+
                                    " salidafab = "+salidafab+", "+
                                    " entregacontrato = "+entregacontrato+", "+
                                    " entregaacordada = "+entregaacordada+", "+
                                    " cobroadm = "+cobroadm+", "+
                                    " r189 = "+r189+", "+
                                    " r190 = "+r190+", "+
                                    " r182 = "+r182+", "+
                                    " r197 = "+r197+", "+
                                    " r198 = "+r198+", "+
                                    " r199 = "+r199+", "+
                                    " r200 = "+r200+", "+ 
                                    " r201 = "+r201+", "+
                                    " r202 = "+r202+", "+
                                    " r188 = "+r188+", "+
                                    " r183 = "+r183+", "+
                                    " r184 = "+r184+", "+
                                    " r194 = "+r194+", "+
                                    " r186 = "+r186+" "+
                                    " where tb_proyecto.codigo = "+codigoProyecto;
                 return conexion.ejecutarMySql(consulta);
                
         }

         public bool modificarPlanPago(int CodigoEdificio, bool bandera)
         {
             string consulta = "update tb_proyecto set tb_proyecto.planpago = " + bandera + " where  tb_proyecto.codigo = " + CodigoEdificio;
             return conexion.ejecutarMySql(consulta);
         }

         public bool modificarServicioSuspendido(int CodigoEdificio, bool bandera)
         {
             string consulta = "update tb_proyecto set tb_proyecto.serviciosuspendido = " + bandera + " where  tb_proyecto.codigo = " + CodigoEdificio;
             return conexion.ejecutarMySql(consulta);
         }

         public bool modificarHorarioOficina(int CodigoEdificio, bool bandera)
         {
             string consulta = "";
             if(bandera == true){
                 consulta = "update tb_proyecto set tb_proyecto.horarioatencion = 'solo Horario Oficina' where tb_proyecto.codigo =" + CodigoEdificio;
             }else
                 consulta = "update tb_proyecto set tb_proyecto.horarioatencion = '24horas' where tb_proyecto.codigo =" + CodigoEdificio;
             
             return conexion.ejecutarMySql(consulta);
         }

         public bool modificarDeudaProyectoPolizadeSeguro(int CodigoEdificio, bool bandera)
         {
             string consulta = "update tb_proyecto set tb_proyecto.polizaseguro = " + bandera + " where  tb_proyecto.codigo = " + CodigoEdificio;
             return conexion.ejecutarMySql(consulta);
         }

         public bool modificarDeudaProyectoBoletaBancaria(int CodigoEdificio, bool bandera)
         {
             string consulta = "update tb_proyecto set tb_proyecto.boletabancaria = " + bandera + " where  tb_proyecto.codigo = " + CodigoEdificio;
             return conexion.ejecutarMySql(consulta);
         }


         public bool modificarDeudaProyectoLetradeCambio(int CodigoEdificio, bool bandera)
         {
             string consulta = "update tb_proyecto set tb_proyecto.letracambio = " + bandera + " where  tb_proyecto.codigo = " + CodigoEdificio;
             return conexion.ejecutarMySql(consulta);
         }


         public bool modificarDeudaProyecto(int CodigoEdificio,bool bandera)
         {
             string consulta = "update tb_proyecto set tb_proyecto.deudaproy = " + bandera + " where  tb_proyecto.codigo = " + CodigoEdificio;
             return conexion.ejecutarMySql(consulta);
         }

         public bool modificarDeudaRepuestoProyeto(int CodigoEdificio, bool bandera)
         {
             string consulta = "update tb_proyecto set tb_proyecto.deudarepuesto = " + bandera + " where  tb_proyecto.codigo = " + CodigoEdificio;
             return conexion.ejecutarMySql(consulta);
         }


         public bool modificarDireccion(int codigo, string direccion)
         {
             string consulta = "update tb_proyecto set  tb_proyecto.direccion= '" + direccion + "'" +
                 " where tb_proyecto.codigo= " + codigo + ";";
             return conexion.ejecutarMySql(consulta);
         }


         public int obtenerCodigoEncargadoPago(int codigoProyecto)
         {
            string consulta = "select proy.codEncargado from tb_proyecto proy where proy.codigo = "+codigoProyecto;
            DataSet datoResul = conexion.consultaMySql(consulta);
            if(datoResul.Tables[0].Rows.Count > 0){
                int result;
                bool isnumero = int.TryParse(datoResul.Tables[0].Rows[0][0].ToString(), out result);            
                if (isnumero){                    
                    return result;
                }
                else
                    return -1;
            }else
                return -1;                  
         }

         public DataSet getProyectoTodos_like(string nombreProyecto) {
             string consulsa = "select  " +
                               " proy.codigo, " +
                               " DATE_FORMAT(proy.fecha_Proy,'%d/%m/%Y') as 'fecha', " +
                               " proy.nombre, " +
                               " proy.direccion, " +
                               " proy.departamento " +
                               " from tb_proyecto proy " +
                               " where " +
                               " proy.estado = 1 and " +
                               " proy.nombre like '%" + nombreProyecto + "%'";
             return conexion.consultaMySql(consulsa);
         }


         public DataSet tienePrevisionActual(int codigoEdificio)
         {
             string consulta = "select "+
                               " eq.exbo , "+
                               " seg.years as 'Prevision', "+
                               " esm.nombre as 'EstadoE', "+
                               " DATE_FORMAT(fe.fecha,'%d/%m/%Y') as 'FechaEstado',  "+
                               " fe.hora, "+
                               " IFNULL(res.nombre,'Sistema') as 'EstadoCambio' "+
                               " from  "+
                               " tb_proyecto proy , "+
                               " tb_equipo eq, "+
                               " tb_seguimiento seg, "+
                               " tb_estado_mantenimiento esm, "+
                               " tb_fechaestadomantenimiento fe "+
                               " left join tb_responsable res on (fe.codusercambio = res.codigo) "+
                               " where  "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " seg.cod_equipo = eq.codigo and "+
                               " seg.years = year(now()) and "+
                               " seg.codfechaestadoman = fe.codigo and "+
                               " fe.codEstadoMan = esm.codigo and "+
                               " proy.codigo = "+codigoEdificio+
                               " group by eq.codigo";
             return conexion.consultaMySql(consulta);
         }

         public DataSet VerificarEstadosEquipos(int codigoEdificio)
         {
             string consulta = "select " +
                                " count(*) as 'CantAsc' , " +
                                " esm.nombre as 'EstadoE', " +
                                " esm.acronimo " +
                                " from  " +
                                " tb_proyecto proy , " +
                                " tb_equipo eq, " +
                                " tb_seguimiento seg, " +
                                " tb_estado_mantenimiento esm, " +
                                " tb_fechaestadomantenimiento fe " +
                                " where  " +
                                " eq.cod_proyecto = proy.codigo and " +
                                " seg.cod_equipo = eq.codigo and " +
                                " seg.years = year(now()) and " +
                                " seg.codfechaestadoman = fe.codigo and " +
                                " fe.codEstadoMan = esm.codigo and " +
                                " esm.codigo in (2,3,8,5,7)  and " +
                                " proy.codigo = " + codigoEdificio +
                                " group by esm.nombre ";
             return conexion.consultaMySql(consulta);
         }

         internal DataSet getfechaVencimientoBoletaGarantia(int codigoEdificio)
         {
             string consulta = "select  "+
                                 " DATE_FORMAT(pp.vencimientoboletagarantia,'%d/%m/%Y') as 'FechaVencimientoBoletaGarantia' " +
                                 " from tb_proyecto pp "+
                                 " where pp.codigo = "+codigoEdificio ;
             return conexion.consultaMySql(consulta);
         }

         internal bool modificarfechaVencimientoBoletaGarantia(int codigoEdificio, string fecha)
         {
             string consulta = "update tb_proyecto set tb_proyecto.vencimientoboletagarantia = "+fecha+
                               " where tb_proyecto.codigo =  "+codigoEdificio;
             return conexion.ejecutarMySql(consulta);
         }
    }
}