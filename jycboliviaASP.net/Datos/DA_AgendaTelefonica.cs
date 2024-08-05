using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{    
    public class DA_AgendaTelefonica
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_AgendaTelefonica() { 
        
        }

        public bool insertar(string nombre,string direccion,string telefono,string celular1,string celular2,string celular3,string celular4,string email1,string email2,string email3,string email4,string fax,string nota)
        {

            string consulta = "insert into tb_agendatelefonica( "+
                                   " tb_agendatelefonica.nombre, "+
                                   " tb_agendatelefonica.direccion, "+
                                   " tb_agendatelefonica.telefono, "+
                                   " tb_agendatelefonica.celular1, "+
                                   " tb_agendatelefonica.celular2, "+
                                   " tb_agendatelefonica.celular3, "+
                                   " tb_agendatelefonica.celular4, "+
                                   " tb_agendatelefonica.email1, "+
                                   " tb_agendatelefonica.email2, "+
                                   " tb_agendatelefonica.email3, "+
                                   " tb_agendatelefonica.email4, "+
                                   " tb_agendatelefonica.fax, "+
                                   " tb_agendatelefonica.nota, "+
                                   " tb_agendatelefonica.fecha) "+
                                   " values "+
                                   " ('"+nombre+"', "+
                                   " '"+direccion+"', "+
                                   " '"+telefono+"', "+
                                   " '"+celular1+"', "+
                                   " '"+celular2+"', "+
                                   " '"+celular3+"', "+
                                   " '"+celular4+"', "+
                                   " '"+email1+"', "+
                                   " '"+email2+"', "+
                                   " '"+email3+"', "+
                                   " '"+email4+"', "+
                                   " '"+fax+"', "+
                                   " '"+nota+"', " +
                                   " curdate());";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool modificar(int codigo,string nombre, string direccion, string telefono, string celular1, string celular2, string celular3, string celular4, string email1, string email2, string email3, string email4, string fax, string nota)
        {
            string consulta = "update tb_agendatelefonica set "+
                               " tb_agendatelefonica.nombre = '"+nombre+"', "+
                               " tb_agendatelefonica.direccion = '"+direccion+"', "+
                               " tb_agendatelefonica.telefono = '"+telefono+"', "+
                               " tb_agendatelefonica.celular1 = '"+celular1+"', "+
                               " tb_agendatelefonica.celular2 = '"+celular2+"', "+
                               " tb_agendatelefonica.celular3 = '"+celular3+"', "+
                               " tb_agendatelefonica.celular4 = '"+celular4+"', "+
                               " tb_agendatelefonica.email1 = '"+email1+"', "+
                               " tb_agendatelefonica.email2 = '"+email2+"', "+
                               " tb_agendatelefonica.email3 = '"+email3+"', "+
                               " tb_agendatelefonica.email4 = '"+email4+"', "+
                               " tb_agendatelefonica.fax = '"+fax+"', "+
                               " tb_agendatelefonica.nota = '"+nota+"' "+
                               " where " +
                               " tb_agendatelefonica.codigo = "+codigo+" ;";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool eliminar(int codigo)
        {
            string consulta = "delete from tb_agendatelefonica where tb_agendatelefonica.codigo = "+codigo+";";
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

    }
}