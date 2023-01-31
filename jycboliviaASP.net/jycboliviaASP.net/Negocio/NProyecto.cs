using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;


namespace jycboliviaASP.net.Negocio
{
    public class NProyecto
    {
        DProyecto proyecto = new DProyecto();

        public NProyecto()
        { 
        }

        public bool registrar(string nombre, string nombre2, string direccion, string fecha_Proy, int codigoEncargado, int codigoResponsable, int estado, int codzona, string departamento)
        {
            return proyecto.insertarProyecto(nombre, nombre2, direccion, fecha_Proy, codigoEncargado, codigoResponsable, estado, codzona, departamento);
        }
        public bool registrarPP(string nombre, string nombre2, string direccion, string fecha_Proy, int codigoEncargado, int codigoResponsable, int estado, int codzona, string departamento, int codcobradorasignado)
        {
            return proyecto.insertarProyectoPP(nombre, nombre2, direccion, fecha_Proy, codigoEncargado, codigoResponsable, estado, codzona, departamento, codcobradorasignado);
        }

        public bool modificarPP(int codigo, string nombre, string direccion, int codigoEncargado, int codigoResponsable, int estado, int codzona, string departamento)
        {
            return proyecto.modificarProyectoPP(codigo, nombre,  direccion,  codigoEncargado, codigoResponsable,estado, codzona, departamento);
        }

        public bool modificarEncargadoProyPP(int codigo, string nombre, string direccion, int codigoEncargado, int codigoResponsable, int estado, int codzona, string departamento, string nit, string facturar, string banco, int codcobradorasignado, string variableSimec)
        {
            return proyecto.modificarProyectoEncargadoPagoPP(codigo, nombre, direccion, codigoEncargado, codigoResponsable, estado, codzona, departamento, nit, facturar, banco, codcobradorasignado, variableSimec);
        }

        public bool modificarEncargadoProyPP2(int codigo, string nombre, string direccion, int codigoEncargado, int codigoResponsable, int estado,  string departamento, string nit, string facturar, string banco)
        {
            return proyecto.modificarProyectoEncargadoPagoPP2(codigo, nombre, direccion, codigoEncargado, codigoResponsable, estado, departamento, nit, facturar, banco );
        }

        public bool eliminar(int codigo)
        {
            return proyecto.eliminarProyecto(codigo);
        }

        public DataSet buscar(string nombre) {
            DataSet lista = proyecto.buscar(nombre);
            return lista;
        }

        public DataSet buscar_Deuda_IntalacionRepueso(string nombre)
        {
            DataSet lista = proyecto.buscar_DeudaProyecto_Repuesto(nombre);
            return lista;
        }

        public DataSet listar()
        {
            DataSet lista = proyecto.listar();
            return lista;
        }

        public DataSet listarEncargadoProy1(string nombreProyecto)
        {
            DataSet lista = proyecto.listarEncargadoProy(nombreProyecto);
            return lista;
        }

        internal void registrar()
        {
            throw new NotImplementedException();
        }

        public string obtenerFechaActual() 
        {
            return System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }


        public DataSet getProyect(int codigo) {
            return proyecto.getProyect(codigo);
        }


        public DataSet buscador1(string nombre)
        {
            return proyecto.buscar1(nombre);
        }


        public DataSet buscador2(string nombre) {
            return proyecto.buscador2(nombre);
        }

        public DataSet buscar3(string nombre)
        {
            return proyecto.buscar3(nombre);
        }

        public DataSet buscadorCallCenter(string nombre)
        {
            return proyecto.buscadorCallCenter(nombre);
        }


        public int getCodigoProyect(string nombreProyecto) {
            return proyecto.getCodigoProyect(nombreProyecto);
        }

        ////-----------------------------------------------------------------
        public bool existe(string nombreProyecto)
        {
            DataSet tuplaUsuario = proyecto.existeProyecto(nombreProyecto);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }


        public bool existe(int codProyecto)
        {
            DataSet tuplaUsuario = proyecto.existeProyecto(codProyecto);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool existeProyectoParecido(string nombreProyecto)
        {
            DataSet tuplaUsuario = proyecto.existeProyectoParecido(nombreProyecto);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool tieneDeudasPendientesProyecto(int codigoProyecto) {
            DataSet tuplaUsuario = proyecto.tieneDeudasPendientesProyecto(codigoProyecto);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        

        public int CuantosAscensorestieneParadoCliente(int CodigoEdificio)
        {
            DataSet tuplaUsuario = proyecto.CuantosAscensorestieneParadoCliente(CodigoEdificio);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return tuplaUsuario.Tables[0].Rows.Count;
            }
            else
                return 0;
        }

        public int CuantosAscensorestieneParadoNosotros(int CodigoEdificio)
        {
            DataSet tuplaUsuario = proyecto.CuantosAscensorestieneParadoNosotros(CodigoEdificio);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return tuplaUsuario.Tables[0].Rows.Count;
            }
            else
                return 0;
        }

        public bool tienePlanPago(int CodigoEdificio)
        {
            DataSet tuplaUsuario = proyecto.tienePlanPago(CodigoEdificio);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

          public bool tieneServicioSuspendido(int CodigoEdificio)
         {
             DataSet tuplaUsuario = proyecto.tieneServicioSuspendido(CodigoEdificio);
             if (tuplaUsuario.Tables[0].Rows.Count > 0)
             {
                 return true;
             }
             else
                 return false;
         }

          public string horariodeAtencion(int CodigoEdificio)
          {
              if(CodigoEdificio > 0){
                  return proyecto.horariodeAtencion(CodigoEdificio).Tables[0].Rows[0][0].ToString() ;
              }else
                  return "Horario No Definido";
          }

        public bool tieneDeudasPendientesProyectoPolizadeSeguro(int codigoProyecto)
        {
            DataSet tuplaUsuario = proyecto.tieneDeudasPendientesProyectoPolizadeSeguro(codigoProyecto);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

   
        public bool tieneDeudasPendientesProyectoBoletaBancaria(int codigoProyecto)
        {
            DataSet tuplaUsuario = proyecto.tieneDeudasPendientesProyectoBoletaBancaria(codigoProyecto);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool tieneDeudasPendientesProyectoLetradeCambio(int codigoProyecto)
        {
            DataSet tuplaUsuario = proyecto.tieneDeudasPendientesProyectoLetradeCambio(codigoProyecto);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool tieneDeudasRepuestoPendientesProyecto(int codigoProyecto)
        {
            DataSet tuplaUsuario = proyecto.tieneDeudasRepuestoPendientesProyecto(codigoProyecto);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }



        public bool existeProyectoEspecifico(string nombreProyecto) {
            DataSet tuplaUsuario = proyecto.existeProyectoEspecificoConEquipos_CallCenter(nombreProyecto);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }


        public bool registrar1(string nombre, string nombre2, string fecha_Proy, int codigoEncargado, int estado, string ciudad, int codPropietario, string empresacontratoproyecto)
        {
            return proyecto.insertarProyecto1(nombre, nombre2, fecha_Proy, codigoEncargado, estado, ciudad, codPropietario, empresacontratoproyecto);
        }

        public bool modificarProyecto1(int codProyecto, string nombre, string fecha_Proy, int estado, string ciudad, string empresacontratoproyecto)
        {
            return proyecto.modificarProyecto1(codProyecto, nombre, fecha_Proy, estado, ciudad, empresacontratoproyecto);
        }

        public bool modificarProyecto3(int codProyecto, string empresacontratoproyecto)
        { 
            return proyecto.modificarProyecto3(codProyecto, empresacontratoproyecto);
        }

        public int obtenerUltimoCodigo()
        {
            return proyecto.getUltimoCodigo();
        }


        public bool cambiarEncargadoPago1(int codEncargado, int codigoProyecto) {
            return proyecto.cambiarEncargadoPago1(codEncargado,codigoProyecto);
        }

        public bool cambiarDepartamento(int codigoProyecto, string departamento) {
            return proyecto.cambiarDepartamento(codigoProyecto,departamento);        
        }


        ////---------------------otro 

        public DataSet BuscarMonitoreoGestionProyecto(string nombreProyecto) {
            return proyecto.BuscarMonitoreoGestionProyecto(nombreProyecto);
        }

        public bool actualicarControl(int codigoProyecto, string apertura, string salidafab, string entregacontrato, string entregaacordada, string cobroadm,
                                       string r189, string r190, string r182, string r197, string r198, string r199, string r200,
                                       string r201, string r202, string r188, string r183, string r184, string r194, string r186) {
            
            return proyecto.actualicarControl(codigoProyecto, apertura, salidafab, entregacontrato, entregaacordada, cobroadm,
                                       r189, r190, r182, r197,  r198,  r199,  r200,
                                       r201, r202, r188, r183,  r184,  r194,  r186);
        }


        public bool tieneEquiposAsignados(int codProyecto)
        {
            DataSet tuplaUsuario = proyecto.getEquiposdeProyect(codProyecto);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public DataSet getProyect2(string nombreProyecto)
        {
            return proyecto.getProyect2(nombreProyecto);
        }


        public bool modificarPlanPago(int CodigoEdificio, bool bandera)
        {
            return proyecto.modificarPlanPago(CodigoEdificio, bandera);
        }

        public bool modificarServicioSuspendido(int CodigoEdificio, bool bandera)
        {
            return proyecto.modificarServicioSuspendido(CodigoEdificio, bandera);
        }

        public bool modificarHorarioOficina(int CodigoEdificio, bool bandera)
        {
            return proyecto.modificarHorarioOficina(CodigoEdificio, bandera);
        }
        

        public bool modificarDeudaProyectoPolizadeSeguro(int CodigoEdificio, bool bandera)
        {
            return proyecto.modificarDeudaProyectoPolizadeSeguro(CodigoEdificio, bandera);
        }

        public bool modificarDeudaProyectoBoletaBancaria(int CodigoEdificio, bool bandera)
        {
            return proyecto.modificarDeudaProyectoBoletaBancaria(CodigoEdificio, bandera);
        }

        public bool modificarDeudaProyectoLetradeCambio(int CodigoEdificio, bool bandera)
        {
            return proyecto.modificarDeudaProyectoLetradeCambio(CodigoEdificio, bandera);
        }

        public bool modificarDeudaProyecto(int CodigoEdificio, bool bandera)
        {
            return proyecto.modificarDeudaProyecto(CodigoEdificio,bandera);
        }

        public bool modificarDeudaRepuestoProyeto(int CodigoEdificio, bool bandera)
        {
            return proyecto.modificarDeudaRepuestoProyeto(CodigoEdificio, bandera);
        }


        public bool modificarDireccion(int codigo, string direccion)
        {
            return proyecto.modificarDireccion(codigo, direccion);
        }


        public int obtenerCodigoEncargadoPago(int codigoProyecto)
        {
            return proyecto.obtenerCodigoEncargadoPago(codigoProyecto);
        }

        public DataSet getProyectoTodos_like(string nombreProyecto) {
            return proyecto.getProyectoTodos_like(nombreProyecto);
        }



        public string tienePrevisionActual(int codigoEdificio)
        {
            DataSet dato = proyecto.tienePrevisionActual(codigoEdificio);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return "Tiene Mantenimiento";
            }
            else
                return "No Esta en Mantenimiento";
        }

        public string VerificarEstadosEquipos(int codigoEdificio)
        {
            DataSet datos = proyecto.VerificarEstadosEquipos(codigoEdificio);
            if (datos.Tables[0].Rows.Count > 0)
            {
                string resultado = "";
                for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
                {
                    resultado = resultado + datos.Tables[0].Rows[i][0].ToString() + " " + datos.Tables[0].Rows[i][2].ToString() + "; ";
                }
                return resultado;
            }
            else
                return "Tiene Atencion";
        }

        internal string getfechaVencimientoBoletaGarantia(int codigoEdificio)
        {
            DataSet datos = proyecto.getfechaVencimientoBoletaGarantia(codigoEdificio);
            if (datos.Tables[0].Rows.Count > 0)
            {
                string resultado = datos.Tables[0].Rows[0][0].ToString(); 
                return resultado;
            }
            else
                return "";
        }

        internal bool modificarfechaVencimientoBoletaGarantia(int codigoEdificio, string fecha)
        {
            return proyecto.modificarfechaVencimientoBoletaGarantia(codigoEdificio, fecha);
        }
    }
}