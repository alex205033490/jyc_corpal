using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_Cliente
    {
        private conexionMySql conexion = new conexionMySql();
        public DCorpal_Cliente() { 
        }


        public bool eliminar_cliente(int codigo)
        {
            try
            {
                // CAMBIO IMPORTANTE: Ya no usamos DELETE.
                // Usamos UPDATE para cambiar el estado a 0 (Inactivo/Eliminado).
                string consulta = "UPDATE tbcorpal_cliente SET estado = 0 WHERE codigo = " + codigo;

                conexion.consultaMySql(consulta);
                return true;
            }
            catch (Exception ex)
            {
                // Nota: Con el borrado lógico, ya NO te darán errores de llaves foráneas 
                // (por ventas o historial), así que es seguro retornar false si falla la conexión.
                return false;
            }
        }

        internal DataSet get_clienteNombreExcluyendoID(string nombreCliente, int idExcluir)
        {
            // La clave es: AND codigo != idExcluir
            // Esto significa: "Busca alguien con este nombre, que NO sea yo mismo"
            string consulta = "SELECT * FROM tbcorpal_cliente " +
                              "WHERE tiendaname = '" + nombreCliente + "' " +
                              "AND codigo != " + idExcluir;

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }
        public bool modificar_cliente(
    int codigo, // ID para el WHERE
    string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona,
    string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo,
    string facturar_a, string facturar_nit, string facturar_correo, string observacion,
    int codrespgra,  string direccion_lat, string direccion_lng,
    int id_tipocliente, int id_listaprecio)
        {
            try
            {
                // Tratamiento del NULL para SQL
                

                string consulta = "UPDATE tbcorpal_cliente SET " +
                                  "tiendaname = '" + tiendaname + "', " +
                                  "tiendadir = '" + tiendadir + "', " +
                                  "tiendatelefono = '" + tiendatelefono + "', " +
                                  "tiendadepartamento = '" + tiendadepartamento + "', " +
                                  "tiendazona = '" + tiendazona + "', " +

                                  "propietarioname = '" + propietarioname + "', " +
                                  "propietarioci = '" + propietarioci + "', " +
                                  "propietariodir = '" + propietariodir + "', " +
                                  "propietariocelular = '" + propietariocelular + "', " +
                                  "propietarionit = '" + propietarionit + "', " +
                                  "propietariocorreo = '" + propietariocorreo + "', " +

                                  "facturar_a = '" + facturar_a + "', " +
                                  "facturar_nit = '" + facturar_nit + "', " +
                                  "facturar_correo = '" + facturar_correo + "', " +
                                  "observacion = '" + observacion + "', " +

                                  // Actualizamos también quién hizo el cambio (codrespgra)
                                  "codrespgra = " + codrespgra + ", " +

                                  "direccion_lat = '" + direccion_lat + "', " +
                                  "direccion_lng = '" + direccion_lng + "', " +

                                  "id_tipocliente = " + id_tipocliente + ", " +
                                  "id_listaprecio = " + id_listaprecio + " " +

                                  // CLAUSULA WHERE CRITICA
                                  "WHERE codigo = " + codigo;

                conexion.consultaMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool eliminarListaPrecio(int idListaProducto) {
            try
            {
                // Usamos interpolación ($@) para que el código quede limpio y fácil de leer.
                // Cambiamos el estado a 0 (inactivo/eliminado) filtrando por el código de la lista.
                string consulta = $@"
                    UPDATE tbcorpal_listaprecio 
                    SET estado = 0 
                    WHERE codigo = {idListaProducto}";

                // Ejecutamos la consulta en tu clase de conexión
                conexion.consultaMySql(consulta);

                return true; // Si todo sale bien, devolvemos true
            }
            catch (Exception)
            {
                return false; // Si hay algún error con la base de datos, devolvemos false
            }


        }

        public bool insertarListaPrecio(string nombre, string descripcion, decimal descuentogral)
        {
            try
            {
                // Esto asegura que el decimal se convierta a texto usando un punto (ej. 15.50) y no una coma
                string descFormateado = descuentogral.ToString(System.Globalization.CultureInfo.InvariantCulture);

                string consulta = "INSERT INTO tbcorpal_listaprecio " +
                                  "(" +
                                      "nombre, descripcion, " +
                                      "estado, descuentogral_porcentaje" +
                                  ") " +
                                  "VALUES " +
                                  "(" +
                                      "'" + nombre + "', " +
                                      "'" + descripcion + "', " +
                                      "1, " +               // Estado activo por defecto (1)
                                      descFormateado +      // El número decimal formateado (sin comillas simples)
                                  ")";

                conexion.consultaMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                // Considera hacer un throw; o registrar el error en un log para saber por qué falló
                return false;
            }
        }

        public bool insertar_cliente(
    // Datos Tienda
    string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona,
    // Datos Propietario
    string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo,
    // Datos Facturación
    string facturar_a, string facturar_nit, string facturar_correo, string observacion,
    // --- NUEVOS CAMPOS ---
    int codrespgra, string direccion_lat, string direccion_lng,
    // Llaves Foráneas
    int id_tipocliente, int id_listaprecio)
        {
            try
            {
                // NOTA: Recuerda que concatenar strings es riesgoso (SQL Injection). 
                // Pero siguiendo tu estructura solicitada:

                string consulta = "INSERT INTO tbcorpal_cliente " +
                                  "(" +
                                      "fechagra, horagra, " +
                                      "tiendaname, tiendadir, tiendatelefono, tiendadepartamento, tiendazona, " +
                                      "propietarioname, propietarioci, propietariodir, propietariocelular, propietarionit, propietariocorreo, " +
                                      "facturar_a, facturar_nit, facturar_correo, observacion, " +
                                      "codrespgra,  direccion_lat, direccion_lng, " +
                                      "id_tipocliente, id_listaprecio, estado" +
                                  ") " +
                                  "VALUES " +
                                  "(" +
                                      "CURDATE(), CURTIME(), " +
                                      "'" + tiendaname + "', " +
                                      "'" + tiendadir + "', " +
                                      "'" + tiendatelefono + "', " +
                                      "'" + tiendadepartamento + "', " +
                                      "'" + tiendazona + "', " +
                                      "'" + propietarioname + "', " +
                                      "'" + propietarioci + "', " +
                                      "'" + propietariodir + "', " +
                                      "'" + propietariocelular + "', " +
                                      "'" + propietarionit + "', " +
                                      "'" + propietariocorreo + "', " +
                                      "'" + facturar_a + "', " +
                                      "'" + facturar_nit + "', " +
                                      "'" + facturar_correo + "', " +
                                      "'" + observacion + "', " +

                                      // AQUI ESTAN LOS NUEVOS DATOS
                                      codrespgra + ", " +              // int sin comillas
                                     
                                      "'" + direccion_lat + "', " +    // varchar con comillas
                                      "'" + direccion_lng + "', " +    // varchar con comillas

                                      id_tipocliente + ", " +
                                      id_listaprecio + ", " +
                                      "1" +
                                  ")";

                conexion.consultaMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        internal DataSet mostrarListaPrecio()
        {
            string consulta = "SELECT " +
    "    lp.codigo, " +
    "    lp.nombre " +
    "FROM tbcorpal_listaprecio lp " +
    "WHERE lp.estado = 1;";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }
        

        internal DataSet mostrarTipoCliente()
        {
            string consulta = "SELECT " +
    "    tc.codigo, " +
    "    tc.nombre " +
    "FROM tbcorpal_tipocliente tc " +
    "WHERE tc.estado = 1;";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_clienteNombre(string nombreCliente)
        {

            string consulta = "SELECT * FROM tbcorpal_cliente WHERE tiendaname = '" + nombreCliente + "'";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_cliente(int codigo) {
            string consulta = "SELECT " +
    "    tiendaname, " +
    "    tiendadir, " +
    "    tiendatelefono, " +
    "    tiendadepartamento, " +
    "    tiendazona, " +
    "    propietarioname, " +
    "    propietarioci, " +
    "    propietarionit, " +
    "    propietariocelular, " +
    "    propietariodir, " +
    "    propietariocorreo, " +
    "    facturar_a, " +
    "    facturar_nit, " +
    "    facturar_correo, " +
    "    id_tipocliente, " +
    "    id_listaprecio, " +
    "    observacion " +
    "FROM tbcorpal_cliente " +
    "WHERE codigo = " + codigo + " ";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }
        
        internal DataSet listarTiendas2(string nombreTiendas)
        {
            string consulta = "SELECT " +
                    "    c.codigo, " +
                    "    c.tiendaname, " +
                    "    c.tiendadir, " +
                    "    c.tiendatelefono, " +
                    "    c.propietarioname, " +
                    "    c.propietariocelular, " +
                    "    tc.nombre AS NombreTipoCliente, " +
                    "    lp.nombre AS NombreListaPrecio " +
                    "FROM tbcorpal_cliente c " +
                    "INNER JOIN tbcorpal_tipocliente tc ON c.id_tipocliente = tc.codigo " +
                    "INNER JOIN tbcorpal_listaprecio lp ON c.id_listaprecio = lp.codigo " +

                    // --- AQUÍ EL CAMBIO ---
                    // Filtramos que el estado sea 1 Y que coincida el nombre
                    "WHERE c.estado = 1 AND c.tiendaname LIKE '%" + nombreTiendas + "%' " +

                    // Opcional: Ordenar para ver los nuevos primero
                    "ORDER BY c.codigo DESC";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet listarListaProducto(string nombreLista)
        {
            string consulta = "SELECT " +
                "    lp.codigo, " +
                "    lp.nombre,  lp.descripcion, " +
                "    lp.descuentogral_porcentaje " +
                "FROM tbcorpal_listaprecio lp " +
                "WHERE lp.estado = 1 " +
                "AND lp.nombre LIKE '%" + nombreLista + "%' " +
        
                    "ORDER BY lp.codigo DESC";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet listarDetalleListaProducto(int idLista)
        {
            string consulta = "SELECT " +
            "    dlp.codigo, " +
            "    pro.producto, " +
            "    pro.medida, " +
            "    dlp.precio, " +
            "    dlp.precio_especial, " +
            "    dlp.porcentaje_descuento " +
            "FROM tbcorpal_detallelistaprecio dlp " +
            "INNER JOIN tbcorpal_producto pro ON dlp.id_producto = pro.codigo " +
            "WHERE dlp.estado = 1 " +
            "AND dlp.id_listaprecio = " + idLista; 
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }



        internal DataSet listarTiendas(string nombreTiendas)
        {
            string consulta = "Select "+
                             " codigo, "+                                      
                             " tiendaname,  tiendadir,  tiendatelefono, "+
                             " tiendadepartamento,  tiendazona,  propietarioname, "+
                             " propietarioci,  propietariodir,  propietariocelular, "+                       
                             " propietarionit,  propietariocorreo, facturar_a, "+                    
                             " facturar_nit,  facturar_correo, observacion "+     
                             " from tbcorpal_cliente tt "+
                            "  where tt.tiendaname like '%"+nombreTiendas+"%'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal bool eliminarTienda(int codigo)
        {
            string consulta = "delete from tbcorpal_cliente where tbcorpal_cliente.codigo = " + codigo;            
            return conexion.ejecutarMySql(consulta);
        }

        internal bool guardarDatosTienda(string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona, string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo, string facturar_a, string facturar_nit, string facturar_correo, string observacion, int codUserGra)
        {
            string consulta = "insert into tbcorpal_cliente(  fechagra,horagra, "+
                               " tiendaname,  tiendadir,  tiendatelefono, "+
                               " tiendadepartamento,  tiendazona,  propietarioname, "+
                               " propietarioci,  propietariodir,  propietariocelular, "+                       
                               " propietarionit,  propietariocorreo, facturar_a, "+                 
                               " facturar_nit,  facturar_correo, observacion, codrespgra) "+  
                               " values( "+
                               " current_date(), current_time(), "+
                               " '"+tiendaname+"',  '"+tiendadir+"',  '"+tiendatelefono+"', "+
                               " '"+tiendadepartamento+"',  '"+tiendazona+"',  '"+propietarioname+"', "+
                               " '"+propietarioci+"',  '"+propietariodir+"',  '"+propietariocelular+"', "+                       
                               " '"+propietarionit+"',  '"+propietariocorreo+"', '"+facturar_a+"', "+
                               " '" + facturar_nit + "',  '" + facturar_correo + "', '" + observacion + "', " + codUserGra + ")";
            return conexion.ejecutarMySql(consulta);
        }

        internal bool updateDatosTienda(int codigo, string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona, string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo, string facturar_a, string facturar_nit, string facturar_correo, string observacion, int codUserGra)
        {
            string consulta = "update tbcorpal_cliente set "+                     
                                 " tbcorpal_cliente.tiendaname = '"+tiendaname+"', "+ 
                                 " tbcorpal_cliente.tiendadir = '"+tiendadir+"', "+ 
                                 " tbcorpal_cliente.tiendatelefono = '"+tiendatelefono+"', "+
                                 " tbcorpal_cliente.tiendadepartamento = '"+tiendadepartamento+"', "+ 
                                 " tbcorpal_cliente.tiendazona = '"+tiendazona+"', "+ 
                                 " tbcorpal_cliente.propietarioname = '"+propietarioname+"', "+
                                 " tbcorpal_cliente.propietarioci = '"+propietarioci+"', "+ 
                                 " tbcorpal_cliente.propietariodir = '"+propietariodir+"', "+ 
                                 " tbcorpal_cliente.propietariocelular = '"+propietariocelular+"', "+                       
                                 " tbcorpal_cliente.propietarionit = '"+propietarionit+"', "+ 
                                 " tbcorpal_cliente.propietariocorreo = '"+propietariocorreo+"', "+ 
                                 " tbcorpal_cliente.facturar_a = '"+facturar_a+"', "+                 
                                 " tbcorpal_cliente.facturar_nit = '"+facturar_nit+"', "+  
                                 " tbcorpal_cliente.facturar_correo = '"+facturar_correo+"', "+
                                 " tbcorpal_cliente.observacion = '" + observacion + "', " +
                                 " tbcorpal_cliente.codrespgra = "+ codUserGra+
                                 " where "+
                                 " tbcorpal_cliente.codigo = "+codigo;
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_tienda(int codigo)
        {
            string consulta = "Select " +
                            " codigo, " +
                            " tiendaname,  tiendadir,  tiendatelefono, " +
                            " tiendadepartamento,  tiendazona,  propietarioname, " +
                            " propietarioci,  propietariodir,  propietariocelular, " +
                            " propietarionit,  propietariocorreo, facturar_a, " +
                            " facturar_nit,  facturar_correo, observacion " +
                            " from tbcorpal_cliente tt " +
                           "  where tt.codigo = '" + codigo + "'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet buscarPropietario(string nombre)
        {
            string consulta = "Select "+
                             " codigo, "+                      
                             " propietarioname, "+
                             " propietarioci, "+
                             " propietariodir, "+
                             " propietariocelular, "+                        
                             " propietarionit, "+
                             " propietariocorreo "+ 
                             " from "+
                             " tbcorpal_cliente cc "+
                             " where cc.propietarioname like '%"+nombre+"%' ";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_ClienteNombre(string tiendanombre)
        {
            string consulta = "Select " +
                            " codigo, " +
                            " tiendaname,  tiendadir,  tiendatelefono, " +
                            " tiendadepartamento,  tiendazona,  propietarioname, " +
                            " propietarioci,  propietariodir,  propietariocelular, " +
                            " propietarionit,  propietariocorreo, facturar_a, " +
                            " facturar_nit,  facturar_correo, observacion " +
                            " from tbcorpal_cliente tt " +
                           "  where tt.tiendaname like '%" + tiendanombre + "%'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_ClienteNombreEspecifico(string tiendanombre)
        {
            string consulta = "Select " +
                            " codigo, " +
                            " tiendaname,  tiendadir,  tiendatelefono, " +
                            " tiendadepartamento,  tiendazona,  propietarioname, " +
                            " propietarioci,  propietariodir,  propietariocelular, " +
                            " propietarionit,  propietariocorreo, facturar_a, " +
                            " facturar_nit,  facturar_correo, observacion " +
                            " from tbcorpal_cliente tt " +
                           "  where tt.tiendaname = '" + tiendanombre + "'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }
        internal DataSet get_ClienteCodCliente(int codigo)
        {
            string consulta = "Select " +
                            " codigo, " +
                            " tiendaname,  tiendadir,  tiendatelefono, " +
                            " tiendadepartamento,  tiendazona,  propietarioname, " +
                            " propietarioci,  propietariodir,  propietariocelular, " +
                            " propietarionit,  propietariocorreo, facturar_a, " +
                            " facturar_nit, observacion, direccion_lat, direccion_lng " +
                            " from tbcorpal_cliente tt " +
                           "  where tt.codigo = " + codigo + "";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_clienteUltimoIngresado(string cliente, string propietario, string razonSocial, string nit)
        {
            string consulta = "select max(cc.codigo) from tbcorpal_cliente cc  " +
                " where  cc.tiendaname = '"+cliente+"' and cc.propietarioname = '"+propietario+"' " +
                " and cc.facturar_a = '"+razonSocial+"' and cc.facturar_nit = '"+nit+"'";
            return conexion.consultaMySql(consulta);
        }

        internal bool updateDatosTiendaSolicitud(int codigCliente, string cliente, string propietario, string razonsocial,string nit, int codpersolicitante)
        {
            string consulta = "update tbcorpal_cliente set  " +
                 " tbcorpal_cliente.tiendaname = '" + cliente + "' , " +
                 " tbcorpal_cliente.propietarioname = '" + propietario + "' , " +
                 " tbcorpal_cliente.facturar_a = '" + razonsocial + "', " +
                 " tbcorpal_cliente.facturar_nit = '" + nit + "' " +
                 " where tbcorpal_cliente.codigo = " + codigCliente;
            return conexion.ejecutarMySql(consulta);    
        }


    }
}