using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_AgendaTelefonica
    {
        private DA_AgendaTelefonica Dagenda = new DA_AgendaTelefonica();

        public bool insertar(string nombre, string direccion, string telefono, string celular1, string celular2, string celular3, string celular4, string email1, string email2, string email3, string email4, string fax, string nota)
        { 
            return Dagenda.insertar(nombre,direccion,telefono,celular1,celular2,celular3,celular4,email1,email2,email3,email4,fax,nota);
        }

        public bool modificar(int codigo, string nombre, string direccion, string telefono, string celular1, string celular2, string celular3, string celular4, string email1, string email2, string email3, string email4, string fax, string nota)
        {
            return Dagenda.modificar(codigo,nombre,direccion,telefono,celular1,celular2,celular3,celular4,email1,email2,email3,email4,fax,nota);
        }

        public bool eliminar(int codigo)
        {
            return Dagenda.eliminar(codigo);
        }


        public DataSet mostrarDatos(string nombreBusqueda)
        {
            nombreBusqueda = nombreBusqueda.Replace(" ", "%");

            string consulta = "select " +
                               " codigo, " +
                               " nombre as 'nombre_persona', " +
                               " direccion, " +
                               " telefono, " +
                               " celular1, " +
                               " celular2, " +
                               " celular3, " +
                               " celular4, " +
                               " email1, " +
                               " email2, " +
                               " email3, " +
                               " email4, " +
                               " fax, " +
                               " nota as 'Nota_detalle_agenda'" +
                               " from " +
                               " tb_agendatelefonica agenda " +
                               " where  " +
                               " agenda.nombre like '%" + nombreBusqueda + "%' or " +
                               " agenda.telefono like '%" + nombreBusqueda + "%' or " +
                               " agenda.celular1 like '%" + nombreBusqueda + "%' or  " +
                               " agenda.celular3 like '%" + nombreBusqueda + "%' or " +
                               " agenda.celular4 like '%" + nombreBusqueda + "%' or " +
                               " agenda.fax like '%" + nombreBusqueda + "%' or  " +
                               " agenda.direccion like '%" + nombreBusqueda + "%' or " +
                               " agenda.email1 like '%" + nombreBusqueda + "%' or " +
                               " agenda.email2 like '%" + nombreBusqueda + "%' or " +
                               " agenda.email3 like '%" + nombreBusqueda + "%' or " +
                               " agenda.email4 like '%" + nombreBusqueda + "%'  ";

            return Dagenda.getDatos(consulta);
        }


        public DataSet mostrarDatos2(string nombreBusqueda)
        {
            string consulta = "select " +                              
                               " nombre " +                               
                               " from " +
                               " tb_agendatelefonica agenda " +
                               " where " +
                               " agenda.nombre like '%" + nombreBusqueda + "%'";
            return Dagenda.getDatos(consulta);
        }


    }
}