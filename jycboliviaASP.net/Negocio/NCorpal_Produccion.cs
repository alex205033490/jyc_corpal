using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;

namespace jycboliviaASP.net.Negocio
{
    
    public class NCorpal_Produccion    
    {
        DCorpal_Produccion dproduccion = new DCorpal_Produccion();
        internal bool insertarEntregaProduccion(string nroorden, string turno, int codusuario, string respEntrega, float cantcajas, float unidadsuelta, float kgrdesperdicio, float kgrparamix, string detalleentrega, int codProdNax, string productoNax, int codresprecepcion, string resp_recepcion, int cod_respgra, float kgrdesperdicio_conaceite, float kgrdesperdicio_sinaceite, float pack_ferial, string medidaentregada, string medidapackferial, decimal kgrdesperdiciobobina)
        {
            return dproduccion.insertarEntregaProduccion( nroorden,  turno,  codusuario,  respEntrega,  cantcajas,  unidadsuelta,  kgrdesperdicio,  kgrparamix,  detalleentrega,  codProdNax,  productoNax,  codresprecepcion,  resp_recepcion,  cod_respgra,  kgrdesperdicio_conaceite,  kgrdesperdicio_sinaceite, pack_ferial, medidaentregada, medidapackferial, kgrdesperdiciobobina);
        }

        internal bool modificarEntregaProduccion(int codigo, string nroorden, string turno, int codusuario, string respEntrega, float cantcajas, float unidadsuelta, float kgrdesperdicio, float kgrparamix, string detalleentrega, int codProdNax, string productoNax, int codresprecepcion, string resp_recepcion, int cod_respgra, float kgrdesperdicio_sinaceite, float kgrdesperdicio_conaceite, float pack_ferial, string medidaentregada, string medidapackferial, decimal kgrdesperdiciobobina)
        {
            return dproduccion.modificarEntregaProduccion( codigo,  nroorden,  turno,  codusuario,  respEntrega,  cantcajas,  unidadsuelta,  kgrdesperdicio,  kgrparamix,  detalleentrega,  codProdNax,  productoNax ,  codresprecepcion,  resp_recepcion,  cod_respgra,  kgrdesperdicio_sinaceite,  kgrdesperdicio_conaceite,  pack_ferial,  medidaentregada,  medidapackferial, kgrdesperdiciobobina);
        }

        internal bool eliminarEntregaProduccion(int codigo)
        {
           return dproduccion.eliminarEntregaProduccion(codigo);
        }

        internal DataSet mostrarEmpregasProduccion(string turno, string respEntrega)
        {
            return dproduccion.mostrarEmpregasProduccion( turno,  respEntrega);
        }

        internal DataSet get_entregasProduccion(string fechadesde, string fechahasta, string Responsable, string producto)
        {
           return dproduccion.get_entregasProduccion( fechadesde,  fechahasta, Responsable,  producto);
        }

        internal int get_ultimoInsertadoEntregaProduccion(string respEntrega)
        {
            DataSet ultimo = dproduccion.get_ultimoInsertadoEntregaProduccion( respEntrega);
            if (ultimo.Tables[0].Rows.Count > 0)
            {
                int codigo;
                int.TryParse(ultimo.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }
            else
                return -1;
        }

        internal DataSet get_DatosEntregaProduccion(int codigoEntregaProduccion)
        {
            DataSet tuplas = dproduccion.get_DatosEntregaProduccion(codigoEntregaProduccion);
            return tuplas;
        }

        internal bool set_objetivoProduccion(string fechalimite, int codprod, string producto, float cantidadprod,
                                           string medida, string detalle, int codusergra, string respgra)
        {
            return dproduccion.set_objetivoProduccion( fechalimite,  codprod,  producto,  cantidadprod, medida,  detalle,  codusergra,  respgra);
        }

        internal DataSet get_objetivosDeProduccion(string fechalimite, string producto)
        {
            return dproduccion.get_objetivosDeProduccion( fechalimite,  producto);
        }

        internal bool update_objetivoProduccion(int codigo, string fechalimite, int codprod, string producto, float cantidadprod, string medida, string detalle, int codusergra, string respgra)
        {
            return dproduccion.update_objetivoProduccion(codigo, fechalimite, codprod, producto, cantidadprod, medida, detalle, codusergra, respgra);
        }

        internal bool delete_objetivoProduccion(int codigo, int codUser)
        {
            return dproduccion.delete_objetivoProduccion( codigo,  codUser);
        }

        internal DataSet get_devolucionProductos(string vendedor, string producto)
        {
            return dproduccion.get_devolucionProductos( vendedor,  producto);
        }

        internal bool insertarDevolucionProduccion(string fechadevolucion, string vendedor, int codvendedor, string producto, int codproducto, float cantidad, string almacenerorecibe, string motivodevolucion, string seenviaa, string observacionesdevolucion, string medida)
        {
            return dproduccion.insertarDevolucionProduccion( fechadevolucion, vendedor, codvendedor, producto, codproducto, cantidad, almacenerorecibe, motivodevolucion, seenviaa, observacionesdevolucion, medida);
        }

        internal bool modificarDevolucionProduccion(int codigoD, string fechadevolucion, string vendedor, int codvendedor, string producto, int codproducto, float cantidad, string almacenerorecibe, string motivodevolucion, string seenviaa, string observacionesdevolucion, string medida)
        {
            return dproduccion.modificarDevolucionProduccion(codigoD, fechadevolucion, vendedor, codvendedor, producto, codproducto, cantidad, almacenerorecibe, motivodevolucion, seenviaa, observacionesdevolucion, medida);
        }

        internal bool eliminarDevolucionProduccion(int codigoD)
        {
            return dproduccion.eliminarDevolucionProduccion(codigoD);
        }

        internal DataSet get_devolucionProductosNoAprovados(string fechaDevolucion, string Producto)
        {
           return dproduccion.get_devolucionProductosNoAprovados( fechaDevolucion,  Producto);
        }

        internal bool set_AutorizadoDevolucionProducto(int codigoDevolucion, bool marcado, int codUserAutorizado, string nombreAutorizacion)
        {
            return dproduccion.set_objetivoProduccion( codigoDevolucion,  marcado,  codUserAutorizado,  nombreAutorizacion);
        }

        internal DataSet get_objetivosDeProduccionMensual(int Mes, int anio, string producto)
        {
           return dproduccion.get_objetivosDeProduccionMensual( Mes,  anio,  producto);
        }

        internal bool update_objetivoProduccionMensual(int codigo, int codMes, string mesTexto, int anio, int codprod, string producto, float cantidadprod, string medida, string detalle, int codusergra, string respgra)
        { 
            return dproduccion.update_objetivoProduccionMensual( codigo,  codMes,  mesTexto,  anio,  codprod,  producto,  cantidadprod,  medida,  detalle,  codusergra,  respgra);
        }

        internal bool set_objetivoProduccionMensual(int codMes, string mesTexto, int anio, int codprod, string producto, float cantidadprod, string medida, string detalle, int codusergra, string respgra)
        {
           return dproduccion.set_objetivoProduccionMensual( codMes,  mesTexto,  anio,  codprod,  producto,  cantidadprod,  medida,  detalle,  codusergra,  respgra);
        }

        internal bool delete_objetivoProduccionMensual(int codigo, int codUser)
        {
            return dproduccion.delete_objetivoProduccionMensual( codigo,  codUser);
        }

        internal DataSet get_datosOrdenProduccion(string Producto)
        {
            return dproduccion.get_datosOrdenProduccion(Producto);
        }

        internal DataSet get_datosOrdenProduccion(int codigoOrden)
        {
            return dproduccion.get_datosOrdenProduccion(codigoOrden);
        }

        internal bool insertarOrdenProduccion(string fechaproduccion, int codProductonax, string productoNax, decimal cantcajasproduccion,
                                               string medida, string detalleproduccion,  int cod_respgra, string responsable, decimal cantturnodia, decimal cantturnotarde, decimal cantturnonoche)
        {
            return dproduccion.insertarOrdenProduccion( fechaproduccion, codProductonax, productoNax, cantcajasproduccion,
                                                medida, detalleproduccion,  cod_respgra, responsable,  cantturnodia,  cantturnotarde,  cantturnonoche);
        }

        internal bool eliminar_ordenProduccion(int codigoOrden)
        {
            return dproduccion.eliminar_ordenProduccion( codigoOrden);
        }

        internal bool modificarOrdenProduccion(int codigoOrden, string fechaProduccion, int codProducto, string producto, decimal cantcajas, string medidaProduccion, string detalleProduccion, int codUser, string responsable, decimal cantturnodia, decimal cantturnotarde, decimal cantturnonoche)
        {
            return dproduccion.modificarOrdenProduccion( codigoOrden,  fechaProduccion,  codProducto,  producto,  cantcajas, medidaProduccion, detalleProduccion, codUser, responsable,  cantturnodia,  cantturnotarde,  cantturnonoche);
        }

        internal int get_codigoCorrelativoOrdenProduccion()
        {
            DataSet datoPP = dproduccion.get_codigoCorrelativoOrdenProduccion();
            if (datoPP.Tables[0].Rows.Count > 0)
            {
                int codigo;
                int.TryParse(datoPP.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo+1;
            }
            else
                return 0;

        }

        internal DataSet get_datosEntregaProduccionFechaTurno(string fechadesde, string fechahasta, string producto)
        {
            return dproduccion.get_datosEntregaProduccionFechaTurno( fechadesde,  fechahasta,  producto);
        }

        internal DataSet get_insumosporProductoNormal(int codNax, string nameProd, decimal cantidadNecesitada)
        {
            return dproduccion.get_insumosporProductoNormal(codNax, nameProd, cantidadNecesitada);
        }

        internal DataSet get_insumosCreadosporProducto(int codNax,  decimal cantidadNecesitada)
        {
            DataSet listInsumosCreados = dproduccion.get_insumosCreadosporProducto(codNax, cantidadNecesitada);
            string consultaArmada = "";
            if (listInsumosCreados.Tables[0].Rows.Count > 0)
            {
                int cantFilas = listInsumosCreados.Tables[0].Rows.Count;
                for (int i = 0; i < cantFilas; i++)
                {
                    int codigoInsumoCreado;
                    int.TryParse(listInsumosCreados.Tables[0].Rows[i][0].ToString(), out codigoInsumoCreado);
                    string insumoCreado = listInsumosCreados.Tables[0].Rows[i][1].ToString();
                    decimal cantidad;
                    decimal.TryParse(listInsumosCreados.Tables[0].Rows[i][2].ToString().Replace('.', ','), out cantidad);
                    consultaArmada = consultaArmada +
                        "select " +
                        " ins.codigo," +
                        " ii.nombre as 'insumoCreado'," +
                        " ins.nombre as 'insumo'," +
                        " (dins.cantidad * '" + cantidad.ToString().Replace(',', '.') + "') as 'cantidad'," +
                        " ins.Medida, " +                        
                        " '"+cantidad+"' as 'cantidaInsumoCreado'"+
                        " from" +
                        " tbcorpal_insumoscreados ii," +
                        " tbcorpal_detinsumocreado dins," +
                        " tbcorpal_insumo ins" +
                        " where" +
                        " ii.codigo = dins.codinsumocreado and" +
                        " dins.codinsumo = ins.codigo and " +
                        " ii.codigo = " + codigoInsumoCreado;
                    if (i < (cantFilas-1))
                    {
                        consultaArmada = consultaArmada + " UNION ";
                    }
                }
                DataSet resultDatos = dproduccion.get_consultaMySql(consultaArmada);
                return resultDatos;
            }
            else {
                string consultaVacia = "select  ins.codigo, ii.nombre as 'insumoCreado', ins.nombre as 'insumo', (dins.cantidad * 0) as 'cantidad', ins.Medida  from tbcorpal_insumoscreados ii, tbcorpal_detinsumocreado dins, tbcorpal_insumo ins where ii.codigo = dins.codinsumocreado and dins.codinsumo = ins.codigo and ii.codigo = 0";
                return dproduccion.get_consultaMySql(consultaVacia); ;
            }
            
        }

        internal int get_codigoOrdenProduccionUltimoInsertado(int codProducto, int codUser)
        {
            DataSet datoPP = dproduccion.get_codigoOrdenProduccionUltimoInsertado(codProducto, codUser);
            if (datoPP.Tables[0].Rows.Count > 0)
            {
                int codigo;
                int.TryParse(datoPP.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }
            else
                return 0;
        }

        internal bool get_tieneRecetaelProducto(int codProducto)
        {
            DataSet receta = dproduccion.get_tieneRecetaProducto(codProducto);
            if (receta.Tables[0].Rows.Count > 0) {
                return true;
            }else
                return false;
        }

        internal DataSet get_insumosNormal(string nombreInsumo)
        {
            return dproduccion.get_insumosNormal( nombreInsumo);
        }

        internal DataSet get_insumosCompuesto(string nombreInsumo)
        {
            return dproduccion.get_insumosCompuesto(nombreInsumo);
        }

        internal bool existeInsumo(string insumos)
        {
            DataSet tuplas = dproduccion.get_insumosNormalExacto(insumos);
            if (tuplas.Tables[0].Rows.Count>0) {
                return true;
            }else
                return false;
        }

        internal int get_codigoInsumo(string insumos)
        {
            DataSet tuplas = dproduccion.get_insumosNormalExacto(insumos);
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                int codigo;
                int.TryParse(tuplas.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }
            else
                return 0;
        }

        internal DataSet buscarRecetas(string receta)
        {
            return dproduccion.buscarRecetas( receta);
        }

        internal bool existeInsumoCompuesto(string insumosCompuesto)
        {
            DataSet tuplas = dproduccion.get_insumosCompuestoExacto(insumosCompuesto);
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        internal int get_codigoInsumoCompuesto(string insumosCompuesto)
        {
            DataSet tuplas = dproduccion.get_insumosCompuestoExacto(insumosCompuesto);
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                int codigoInsumo;
                int.TryParse(tuplas.Tables[0].Rows[0][0].ToString(), out codigoInsumo);
                return codigoInsumo;
            }
            else
                return 0;
        }

        internal DataSet get_productoAsignar(int codigoProductoAsignado)
        {
            return dproduccion.get_productoAsignar(codigoProductoAsignado);
        }

        internal DataSet get_insumosdeReceta(int codigoReceta)
        {
            return dproduccion.get_insumosdeReceta( codigoReceta);
        }

        internal DataSet get_insumosCompuestodeReceta(int codigoReceta)
        {
           return dproduccion.get_insumosCompuestodeReceta(codigoReceta);
        }

       

        internal bool existeProductoAsignadoaReceta(int codigoProducto)
        {
            DataSet tupla = dproduccion.existeProductoAsignadoaReceta( codigoProducto);
            if (tupla.Tables[0].Rows.Count > 0 ) { 
                return true;
            }else
                return false;
        }

        internal bool insertarReceta(string receta, int codigoProducto, int coduser, decimal cantpordia, bool condicionante)
        {
            return dproduccion.insertarReceta( receta,  codigoProducto,  coduser,  cantpordia,  condicionante);
        }

        internal int get_codigoRecetaUltima(string receta, int codigoProducto)
        {
            DataSet tupla = dproduccion.get_codigoRecetaUltima( receta,  codigoProducto);
            if (tupla.Tables[0].Rows.Count > 0) {
                int codigo;
                int.TryParse(tupla.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }else
                return 0;
        }

        internal bool insertarInsumoNormalReceta(int codigoReceta, int codigoInsumo, decimal cantidad, int coduser)
        {
            return dproduccion.insertarInsumoNormalReceta( codigoReceta, codigoInsumo, cantidad, coduser);
        }

        internal bool insertarInsumoCompuestoReceta(int codigoReceta, int codigoInsumoCompuesto, decimal cantidad , int coduser)
        {
            return dproduccion.insertarInsumoCompuestoReceta( codigoReceta, codigoInsumoCompuesto, cantidad, coduser);
        }

        internal bool eliminarInsumosNormalesAgregados(int codigoReceta)
        {
            return dproduccion.eliminarInsumosNormalesAgregados(codigoReceta);            
        }

        internal bool eliminarInsumosCompuestosAgregados(int codigoReceta)
        {
            return dproduccion.eliminarInsumosCompuestosAgregados(codigoReceta);
        }

        internal bool eliminar_Receta(int codigoReceta, int codUser)
        {
            return dproduccion.eliminar_Receta(codigoReceta, codUser);
        }

        internal bool eliminar_insumoReceta(int codigoReceta, int codigoInsumo)
        {
            return dproduccion.eliminar_insumoReceta( codigoReceta,  codigoInsumo);
        }

        internal bool eliminar_insumoCompuestoReceta(int codigoReceta, int codigoInsumoCompuesto)
        {
            return dproduccion.eliminar_insumoCompuestoReceta(codigoReceta, codigoInsumoCompuesto);
        }

        internal bool update_recetaProducto(int codigoReceta, string receta, decimal cantpordia, bool condicionante)
        {
            return dproduccion.update_recetaProducto( codigoReceta,  receta,  cantpordia,  condicionante);
        }

        internal DataSet get_Receta(int codigoReceta)
        {
            return dproduccion.get_Receta(codigoReceta);
        }

        internal bool tieneCondidion(int codigoOrdenProduccion)
        {
            DataSet tuplas = dproduccion.tieneCondidion(codigoOrdenProduccion);
            try
            {
                if (tuplas.Tables[0].Rows.Count > 0) {
                    bool tieneCondicion = Convert.ToBoolean(tuplas.Tables[0].Rows[0][2].ToString());
                    return tieneCondicion;
                }else
                    return false;
            }
            catch (Exception )
            {
                return false;
            }

        }

        internal bool tieneCondidion_porProducto(int codigoOrdenProduccion)
        {
            DataSet tuplas = dproduccion.tieneCondidion_porProducto(codigoOrdenProduccion);
            try
            {
                if (tuplas.Tables[0].Rows.Count > 0)
                {
                    bool tieneCondicion = Convert.ToBoolean(tuplas.Tables[0].Rows[0][2].ToString());
                    return tieneCondicion;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        internal decimal get_cantidadporDia(int codigoOrdenProduccion)
        {
            DataSet tuplas = dproduccion.tieneCondidion(codigoOrdenProduccion);
            try
            {
                if (tuplas.Tables[0].Rows.Count > 0)
                {
                    decimal cantidadDias;
                    decimal.TryParse(tuplas.Tables[0].Rows[0][3].ToString().Replace(".",","), out cantidadDias);
                    return cantidadDias;
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        internal decimal get_cantidadporDia_porProducto(int codProducto)
        {

            DataSet tuplas = dproduccion.tieneCondidion_porProducto(codProducto);
            try
            {
                if (tuplas.Tables[0].Rows.Count > 0)
                {
                    decimal cantidadDias;
                    decimal.TryParse(tuplas.Tables[0].Rows[0][3].ToString().Replace(".", ","), out cantidadDias);
                    return cantidadDias;
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        internal bool insertarInsumoSolo(string insumos, string codigoGrupo, string codigoUpon, string medida, string detalle, int codUser)
        {
            return dproduccion.insertarInsumoSolo( insumos,  codigoGrupo,  codigoUpon,  medida,  detalle, codUser);
        }

        internal bool modificarInsumoSolo(int codigo, string insumos, string codigoGrupo, string codigoUpon, string medida, string detalle, int codUser)
        {
            return dproduccion.modificarInsumoSolo( codigo,  insumos,  codigoGrupo,  codigoUpon,  medida,  detalle,  codUser);
        }

        internal bool eliminarInsumoSolo(int codigo, int codUser)
        {
            return dproduccion.eliminarInsumoSolo( codigo,  codUser);
        }

        internal DataSet get_insumosCodigo(int codigo)
        {
            return dproduccion.get_insumosCodigo( codigo);
        }

        internal DataSet get_allReceta()
        {
            return dproduccion.get_allReceta();
        }

        internal DataSet get_allRecetaInsumoCreado()
        {
            return dproduccion.get_allRecetaInsumoCreado();
        }

        internal DataSet get_allRecetaInsumoCreado(string codigo)
        {
            return dproduccion.get_allRecetaInsumoCreado(codigo);
        }

        internal DataSet get_objetivoproduccion_vs_entregaproduccion_consalidaalmacen(string fechahasta)
        {
            return dproduccion.get_objetivoproduccion_vs_entregaproduccion_consalidaalmacen( fechahasta);
        }

        internal DataSet get_calidadNachosProceso_RemojadoyLavado(string fechadesde, string fechahasta)
        {
            return dproduccion.get_calidadNachosProceso_RemojadoyLavado( fechadesde,  fechahasta);
        }

        internal DataSet get_calidadNachosProceso_Molinos(string fechadesde, string fechahasta)
        {
            return dproduccion.get_calidadNachosProceso_Molinos(fechadesde, fechahasta);
        }

        internal DataSet get_calidadNachosProceso_Formadora(string fechadesde, string fechahasta)
        {
            return dproduccion.get_calidadNachosProceso_Formadora(fechadesde, fechahasta);
        }

        internal DataSet get_calidadNachosProceso_Fritadora(string fechadesde, string fechahasta)
        {
            return dproduccion.get_calidadNachosProceso_Fritadora(fechadesde, fechahasta);
        }

        internal DataSet get_calidadNachosProceso_SazonadoControlSensorial(string fechadesde, string fechahasta)
        {
            return dproduccion.get_calidadNachosProceso_SazonadoControlSensorial(fechadesde, fechahasta);
        }

        internal DataSet get_calidadNachosProceso_Envasadora(string fechadesde, string fechahasta)
        {
            return dproduccion.get_calidadNachosProceso_Envasadora(fechadesde, fechahasta);
        }
    }
}