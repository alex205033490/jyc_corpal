using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Responsables
    {
        DA_Responsable resp = new DA_Responsable();
        public NA_Responsables() { 
                
        }

        public bool insertarResponsable(string nombre, string direccion, string telefono, string celular, string email, string dpto, string ciudad, int sueldo, string loggin, string PassWord, int cargo, int estado) {
            
            try
            {
                resp.insertarResponsable(nombre,direccion,telefono,celular,email,dpto,ciudad,sueldo,loggin,PassWord,cargo,1);
                return true;
            }
            catch (Exception )
            {
                return false;            
            }

        }

        public bool modificarResponsable(int codigo, string nombre, string direccion, string telefono, string celular, string email, string dpto, string ciudad, int sueldo, string loggin, string PassWord, int cargo, int estado)
        {
            return resp.modificarResponsable(codigo, nombre, direccion, telefono, celular, email, dpto, ciudad, sueldo, loggin, PassWord, cargo, estado);
        }

        public bool eliminarResponsable(int codigo)
        {
            return resp.eliminarResponsable(codigo,0);
        }

        public DataSet mostrarTodosDatos() {
            string consulta = "select res.codigo,res.nombre, res.direccion, res.telefono, "+
                                "res.celular,res.email, res.departamento,res.ciudad, "+
                                "res.sueldo, res.cargoc from tb_responsable res where res.estado = 1";
            DataSet datosR = resp.getDatos(consulta);
            return datosR;        
        }

        public DataSet mostrarTodosDatos2(string nombreResponsable)
        {
            string consulta = "select res.codigo,res.nombre, res.direccion, res.telefono, " +
                                "res.celular,res.email, res.departamento,res.ciudad, " +
                                "res.sueldo, res.cargoc from tb_responsable res where res.estado = 1 "+
                                " and res.nombre like '%"+nombreResponsable+"%' ";
            DataSet datosR = resp.getDatos(consulta);
            return datosR;
        }

        public DataSet buscar_responsable(string nombre, string direccion, string telefono, string celular, string email, string dpto, string ciudad, int sueldo, string loggin, string PassWord, int cargo)
        {
            string consulta = "select resp.codigo,resp.nombre, resp.direccion, resp.telefono, " +
                              "resp.celular,resp.email, resp.departamento,resp.ciudad, " +
                              "resp.sueldo, resp.cargoc " +
                              " from tb_responsable resp where resp.nombre like '%" + nombre + "%' and resp.direccion like '%" + direccion + "%' and resp.telefono like '%" + telefono + "%' and resp.celular like '%" + celular + "%' and resp.email like '%" + email + "%' and resp.departamento like '%" + dpto + "%' and resp.ciudad like '%" + ciudad + "%' and resp.sueldo=" + sueldo + " and resp.Loggin like '%" + loggin + "%' and resp.Passw like '%" + PassWord + "%' and resp.cargoc=" + cargo;
        DataSet datosR = resp.getDatos(consulta);
        return datosR;        
        }

        public DataSet get_responsable(int codigoresponsable)
        {
            string consulta = "select  "+
                               " res.codigo, "+
                               " res.nombre, "+
                               " res.direccion, "+
                               " res.telefono, "+
                               " res.celular, "+
                               " res.email, "+
                               " res.departamento, "+
                               " res.ciudad, "+
                               " res.sueldo, "+
                               " res.Loggin, "+
                               " res.Passw, "+
                               " res.cargoc, "+
                               " res.estado"+
                                " from tb_responsable res where  res.codigo = " + codigoresponsable;
            DataSet datosR = resp.getDatos(consulta);
            return datosR;
        }

        public DataSet get_AllFiscalesProy()
        {
            string consulta = "select resp.codigo, resp.nombre from tb_responsable resp where resp.cargoc =  12;";
            DataSet datosR = resp.getDatos(consulta);
            return datosR;
        }

        public bool autenticarUsuario(string usuario, string password) {
            DataSet tuplaUsuario = getUsuario(usuario, password);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool ExisteUsuario(string usuario)
        {
            DataSet tuplaUsuario = getUsuario2(usuario);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }



        public DataSet getUsuario2(string usuario)
        {
            string consulta = "select * from tb_responsable res where res.estado = 1 and res.Loggin = '" + usuario + "'";
            DataSet resultado = resp.getDatos(consulta);
            return resultado;
        }

        public DataSet getUsuario(string usuario, string password) {
            string consulta = "select * from tb_responsable res where res.estado = 1 and res.Loggin = '"+usuario+"' and res.Passw = '"+password+"'";
            DataSet resultado = resp.getDatos(consulta);
            return resultado;
        }

        public int getCodUsuario(string usuario, string password)
        { 
            try{
                string consulta = "select res.codigo from tb_responsable res where res.estado = 1 and res.Loggin = '" + usuario + "' and res.Passw = '" + password + "'";
                DataSet resultado = resp.getDatos(consulta);
                int coduser = Convert.ToInt32(resultado.Tables[0].Rows[0][0].ToString());
                return coduser;
            }catch(Exception){
                return -1;
            }
        }

        public List<int> getPermisoUsuario(int codigoUser) {
            List<int> listaPermisos = new List<int>();
            string consulta = "select permiso.COD_PERMISO from tb_responsable res, tb_formulario f, tb_detalle_permisos permiso where res.codigo = permiso.COD_RESPONSABLE and f.CODIGO = permiso.COD_PERMISO and res.codigo = "+codigoUser;
            DataSet resultado = resp.getDatos(consulta);
            for (int i = 0; i < resultado.Tables[0].Rows.Count; i++ )
            {
                int codigoFor = Convert.ToInt32(resultado.Tables[0].Rows[i][0].ToString());
                listaPermisos.Add(codigoFor);
            }
            return listaPermisos;
        }

        public bool tienePermiso(int codigoUser, int codPermiso) {
            string consulta = "select * from tb_detalle_permisos per where per.COD_RESPONSABLE = "+codigoUser+" and per.COD_PERMISO = "+codPermiso;
            DataSet resultado = resp.getDatos(consulta);
            if (resultado.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }


        public DataSet mostrarTodos_AutoComplit(string nombreResponsable)
        {
            string consulta = "select res.nombre from tb_responsable res where res.estado = 1 " +
                                " and res.nombre like '%" + nombreResponsable + "%' ";
            DataSet datosR = resp.getDatos(consulta);
            return datosR;
        }


        public DataSet getResponsable_SinExepcion(string nombreResponsable)
        {
            string consulta = "select res.codigo,res.nombre, res.direccion, res.telefono, " +
                                "res.celular,res.email, res.departamento,res.ciudad, " +
                                "res.sueldo, res.cargoc from tb_responsable res where " +
                                " res.nombre = '" + nombreResponsable + "' ";
            DataSet datosR = resp.getDatos(consulta);
            return datosR;
        }

        public DataSet getResponsableCobrador(int codigo, string nombreResponsable)
        {
            string consulta = "select  "+
                               " res.codigo, "+
                               " res.nombre "+
                               " from tb_responsable res  "+
                               " where  "+                               
                               " res.cargoc = 8 ";
            if(codigo > 0){
                consulta = consulta + " and res.codigo = " + codigo;
            }
                               
            if(!nombreResponsable.Equals("")){
                consulta = consulta + " and res.nombre = '"+nombreResponsable+"';";
            }
                               
            DataSet datosR = resp.getDatos(consulta);
            return datosR;
        }

        public string getCodUsuarioVendedor(int codigoUsuario)
        {
                string consulta = "select "+
                                   " res.codvendedor "+
                                   " from tb_responsable res where "+
                                   " res.codigo = "+codigoUsuario;
                DataSet resultado = resp.getDatos(consulta);
                if (resultado.Tables[0].Rows.Count > 0)
                {
                    string coduser = resultado.Tables[0].Rows[0][0].ToString();
                    return coduser;
                }
                else
                    return "";           
        }

        public int getCodigo_NombreResponsable(string NombreResponsable)
        {
            string consulta = "select  "+
                               " res.codigo, "+
                               " res.nombre  "+
                               " from "+
                               " tb_responsable res "+
                               " where  "+
                               " res.estado = 1 and "+
                               " res.nombre = '" + NombreResponsable + "'";
            DataSet resultado = resp.getDatos(consulta);
            if (resultado.Tables[0].Rows.Count > 0)
            {
                string coduser = resultado.Tables[0].Rows[0][0].ToString();
                int codigo;
                int.TryParse(coduser, out codigo);
                return codigo;
            }
            else
                return -1;
        }
    }
}

