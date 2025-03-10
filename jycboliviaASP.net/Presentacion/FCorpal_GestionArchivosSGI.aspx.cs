﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Drawing;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_GestionArchivosSGI : System.Web.UI.Page
    {
        private string rutaArchivo = ConfigurationManager.AppSettings["RutaSGI"];                
            
        protected void Page_Load(object sender, EventArgs e)
        {
           // LisDirectory(TreeView1, rutaArchivo);
             this.Title = Session["BaseDatos"].ToString();

             if (tienePermisoDeIngreso(34) == false)
             {
                 string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                 Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
             } 

             string baseDatos = Session["BaseDatos"].ToString();
             if (!baseDatos.Equals("Santa Cruz") && !baseDatos.Equals("La Paz") && !baseDatos.Equals("Cochabamba") && !baseDatos.Equals("Corpal") )
             {
                 baseDatos = "JyC";
             }
             rutaArchivo = rutaArchivo + baseDatos + "/";
            
        }


        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }
   

        protected void bt_buscar_Click(object sender, EventArgs e)
        {         
            buscarArchivo(rutaArchivo,tx_nombre.Text);
            ponerColoresAnuevosArchivos(gv_tabla);
        }

        private void LisDirectory(TreeView treeView, string path) {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));            
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo) {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.ChildNodes.Add(CreateDirectoryNode(directory));

            foreach (var file in directoryInfo.GetFiles())            
                directoryNode.ChildNodes.Add(new TreeNode(file.Name));

            return directoryNode;
        }

        
        protected void Button1_Click(object sender, EventArgs e)
        {            
            LisDirectory(TreeView1, rutaArchivo);
        //    ponerColorAlArbol(TreeView1);
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if(TreeView1.SelectedNode.ChildNodes.Count == 0){  //es hoja                
                string treeNodeName = TreeView1.SelectedNode.Value.ToString();

                string baseDatos = Session["BaseDatos"].ToString();
                if (!baseDatos.Equals("Santa Cruz") && !baseDatos.Equals("La Paz") && !baseDatos.Equals("Cochabamba"))
                {
                    baseDatos = "JyC";
                }

                string dato = rutaArchivo.Remove(rutaArchivo.Length-(baseDatos.Length+1));
                //string ruta = rutaArchivo.Replace(baseDatos + "/",string.Empty) + TreeView1.SelectedNode.ValuePath.ToString().Replace(treeNodeName, string.Empty);
               string ruta = dato + TreeView1.SelectedNode.ValuePath.ToString().Replace(treeNodeName, string.Empty);
               descargarArchivo(ruta,treeNodeName);
            }
        }

        private void descargarArchivo(string RutaArchivo, string nombreArchivo)
        {
            try
            {
                if (!Directory.Exists(RutaArchivo))
                    Directory.CreateDirectory(RutaArchivo);

                RutaArchivo = RutaArchivo + "/" + nombreArchivo;
             
                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                string nombreAux = nombreArchivo.Replace(" ","_");
                Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreAux);
                // Escribimos el fichero a enviar 
                Response.WriteFile(RutaArchivo);
                // volcamos el stream 
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
            }
            catch (Exception)
            {
              //  Response.Write("<script type='text/javascript'> alert('ERROR: " + ex.Message + "') </script>");
            }
        }

        public void buscarArchivo(string rutaArchivo, string Archivo)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Archivo", typeof(string)), new DataColumn("Ruta", typeof(string)), new DataColumn("Subido al SGI", typeof(string)) });

            foreach (string d in Directory.GetDirectories(rutaArchivo)) {
                foreach (string f in Directory.GetFiles(d, "*" + Archivo + "*"))
                {
                    String rutaF = f.Replace("\\","/");
                    int index = rutaF.LastIndexOf('/');
                    string archivoF = rutaF.Substring(index+1);
                    FileInfo fileInfoServidor = new FileInfo(rutaF);
                    string fechaModi = fileInfoServidor.LastWriteTime.Day + "/" + fileInfoServidor.LastWriteTime.Month + "/" + fileInfoServidor.LastWriteTime.Year;
                    dt.Rows.Add(archivoF, rutaF,fechaModi);                   
                }
                buscarArchivo(d,Archivo);
            }

            foreach (string f in Directory.GetFiles(rutaArchivo, "*" + Archivo + "*"))
            {
                String rutaF = f.Replace("\\", "/");
                int index = rutaF.LastIndexOf('/');
                string archivoF = rutaF.Substring(index + 1);
                FileInfo fileInfoServidor = new FileInfo(rutaF);
                string fechaModi = fileInfoServidor.LastWriteTime.Day + "/" + fileInfoServidor.LastWriteTime.Month + "/" + fileInfoServidor.LastWriteTime.Year;
                dt.Rows.Add(archivoF, rutaF, fechaModi);
            }

            gv_tabla.DataSource = dt;
            gv_tabla.DataBind();
            
        }

        protected void gv_tabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Name = gv_tabla.SelectedRow.Cells[1].Text;
            string ruta =gv_tabla.SelectedRow.Cells[2].Text.Replace(Name, string.Empty);
            descargarArchivo(ruta, HttpUtility.HtmlDecode(Name));
        }



    public void ponerColoresAnuevosArchivos(GridView gv_tablaDatosAux) {
        DateTime thisday = DateTime.Today;
        int mes = thisday.Date.Month;
        int anio = thisday.Date.Year;

            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++ )
            {                 
                 string DirNombreArchivo = HttpUtility.HtmlDecode(gv_tablaDatosAux.Rows[i].Cells[2].Text);
                 FileInfo fileInfoServidor = new FileInfo(DirNombreArchivo);
                 // Response.Write("<script type='text/javascript'> alert('" + DirNombreArchivo + " = " + fileInfoServidor.LastWriteTime.Month + "/" + fileInfoServidor.LastWriteTime.Year + "') </script>");
                 if (fileInfoServidor.LastWriteTime.Month == mes && fileInfoServidor.LastWriteTime.Year == anio)
                 {
                    gv_tablaDatosAux.Rows[i].BackColor = Color.Yellow;                    
                }
            }        
        
        }


    private void PrintRecursive(TreeNode treeNode)
    {
        // Print the node.
        string ruta = treeNode.ValuePath;
        Response.Write("<script type='text/javascript'> alert('"+ruta+ "/" + treeNode.Text + "') </script>");        
        // Print each node recursively.
        foreach (TreeNode tn in treeNode.ChildNodes)
        {
            PrintRecursive(tn);
        }
    }

    public void ponerColorAlArbol(TreeView arbol) {

        TreeNodeCollection nodes = arbol.Nodes;
        foreach (TreeNode n in nodes) {
            PrintRecursive(n);
        }
    
    }

    protected void gv_tabla_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[3].Visible = false;
    }



    }

}