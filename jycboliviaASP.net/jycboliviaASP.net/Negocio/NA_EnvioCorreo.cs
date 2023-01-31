using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace jycboliviaASP.net.Negocio
{
    public class NA_EnvioCorreo
    {
        public NA_EnvioCorreo() { }




        public bool  Enviar_Correo_BoletaConObservacion(string asunto, string cuerpoMensaje, string BaseDeDatos)
        {
            MailMessage correo = new MailMessage();
            //correo.From = new MailAddress("automail@jycbolivia.com");
            SmtpClient smtp = new SmtpClient();
            //------------------------------------------------------
            string baseDatos = BaseDeDatos;

            if (baseDatos.Equals("La Paz"))
            {
                correo.From = new MailAddress("boletas@elevamerica.com");
                smtp.Host = "mail.elevamerica.com";
                smtp.Port = 26;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("boletas@elevamerica.com", "kqjW;MdD3FatF2v&$t");
                smtp.EnableSsl = false;
                //-------------------------------------
                correo.To.Add("rcc1@jycbolivia.com");
                correo.To.Add("rin1@jycbolivia.com");                
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    correo.From = new MailAddress("boletas@melevar.com");
                    smtp.Host = "mail.melevar.com";
                    smtp.Port = 26;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("boletas@melevar.com", "HC{;-@N~D$=L)S+TA;");
                    smtp.EnableSsl = false;
                    //--------------------------------------
                    correo.To.Add("rin2@jycbolivia.com");
                    correo.To.Add("rin21@jycbolivia.com");
                    correo.To.Add("aai2@jycbolivia.com");
                    correo.To.Add("rcc2@jycbolivia.com");
                }
                else
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        correo.From = new MailAddress("boletas@interlogisrl.com");
                        smtp.Host = "mail.interlogisrl.com";
                        smtp.Port = 26;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("boletas@interlogisrl.com", "LL@Hjf[1RyJGW*DG[g");
                        smtp.EnableSsl = false;
                        //-------------------------------------
                        correo.To.Add("rin3@jycbolivia.com");
                        correo.To.Add("ati3@jycbolivia.com");
                    }
                    else
                    {
                        /*correo.From = new MailAddress("automail@jycbolivia.com");
                        smtp.Host = "mail.jycbolivia.com";
                        smtp.Port = 26;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("automail@jycbolivia.com", "alex79016002");
                        smtp.EnableSsl = false;*/
                        correo.From = new MailAddress("jyc.servidor@gmail.com");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("jyc.servidor@gmail.com", "alex79016002");
                        smtp.EnableSsl = true;
                        correo.To.Add("sistema@jycbolivia.com");
                    }

            //------------------------------------------            
            correo.To.Add("automail@jycbolivia.com");
            correo.Subject = asunto;
            correo.Body = cuerpoMensaje;
            
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            //
            
            
            try
            {
                smtp.Send(correo);
                correo.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public bool EnvioCorreoAdicionarCotiRepuesto( int codiCotizacion,string nombreEdificio,string cuerpoMensaje, string BaseDeDatos)
        {

            MailMessage correo = new MailMessage();
           // correo.From = new MailAddress("repocotirepuesto@jycbolivia.com");
            //------------------------------------------------------
            //string baseDatos = Session["BaseDatos"].ToString();
            string baseDatos = BaseDeDatos;
            /*
                if (baseDatos.Equals("La Paz"))
                {
                    correo.To.Add("rac1@jycbolivia.com");
                    correo.To.Add("rcb1@jycbolivia.com");
                    correo.To.Add("rcc1@jycbolivia.com");
                    correo.To.Add("rin1@jycbolivia.com");
                    correo.To.Add("efernandez@jycbolivia.com");
                }
                else
                    if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Oruro"))
                    {
                        correo.To.Add("acc2@jycbolivia.com");
                        correo.To.Add("rin2@jycbolivia.com");
                        correo.To.Add("rin21@jycbolivia.com");
                        correo.To.Add("efernandez@jycbolivia.com");
                    }
                    else
                        if (baseDatos.Equals("Santa Cruz"))
                        {
                            correo.To.Add("rcc3@jycbolivia.com");
                            correo.To.Add("rcc31@jycbolivia.com");                        
                            correo.To.Add("rin3@jycbolivia.com");
                            correo.To.Add("rar3@jycbolivia.com");
                            correo.To.Add("apr3@jycbolivia.com");
                            correo.To.Add("ati3@jycbolivia.com");
                            correo.To.Add("efernandez@jycbolivia.com");
                        }
                        else   */
            if (baseDatos.Equals("Prueba"))
                        {
                            correo.To.Add("sistema@jycbolivia.com");
                        }
            
            //------------------------------------------

            correo.To.Add("repocotirepuesto@jycbolivia.com");
            correo.To.Add("cotirepuesto@jycbolivia.com");

            string rutaGuardarR144 = ConfigurationManager.AppSettings["guardar_r144"];
           // int codigoCoti = Convert.ToInt32(Session["codcotiRepuesto"].ToString());
            int codigoCoti = codiCotizacion;
            //string Edificio = Session["EdificioRepuesto"].ToString();
            string Edificio = nombreEdificio;
            string nombreArchivo = "R-144_coti" + codigoCoti + "_" + Edificio;
            string direccionGuardarR144 = rutaGuardarR144 + nombreArchivo;

            string asunto = nombreArchivo;
            correo.Subject = asunto + " -- (" + baseDatos+")";
            correo.Body = cuerpoMensaje;
            //----------------adjunto--------------

            Attachment data = new Attachment(@direccionGuardarR144 + ".pdf");
            data.Name = nombreArchivo.Replace('Ñ', 'N').Replace('ñ', 'n') + ".pdf";
            correo.Attachments.Add(data);
            //--------------------------------
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            //
            SmtpClient smtp = new SmtpClient();
            //
            //---------------------------------------------
            // Estos datos debes rellanarlos correctamente
            //---------------------------------------------
            /*smtp.Host = "mail.jycbolivia.com";
            smtp.Port = 26;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("repocotirepuesto@jycbolivia.com", "alex79016002");
            smtp.EnableSsl = false;*/

            correo.From = new MailAddress("jyc.servidor@gmail.com");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("jyc.servidor@gmail.com", "alex79016002");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(correo);
                correo.Dispose();
                //Response.Write("<script type='text/javascript'> alert('Envio ok') </script>");
                return true;
            }
            catch (Exception ex)
            {
                //Response.Write("<script type='text/javascript'> alert('Error: " + ex.Message + "') </script>");
                return false;
            }
        }



        public bool Enviar_Correo_HabilitacionEquipo_Equipo(string asunto, string cuerpoMensaje, string baseDeDatos)
        {
            MailMessage correo = new MailMessage();
           // correo.From = new MailAddress("automail@jycbolivia.com");
            //------------------------------------------------------
          //  string baseDatos = Session["BaseDatos"].ToString();
            string baseDatos = baseDeDatos;

            if (baseDatos.Equals("La Paz"))
            {
                correo.To.Add("rcc1@jycbolivia.com");
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    correo.To.Add("rcc2@jycbolivia.com");
                }
                else
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        correo.To.Add("rcc3@jycbolivia.com");
                    }


            correo.To.Add("fdd3@jycbolivia.com");
            correo.To.Add("rcn@jycbolivia.com");
            //------------------------------------------

            correo.To.Add("automail@jycbolivia.com");
            correo.Subject = asunto;
            correo.Body = cuerpoMensaje;
            //----------------adjunto--------------

            //--------------------------------
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            //
            SmtpClient smtp = new SmtpClient();
            //
            //---------------------------------------------
            // Estos datos debes rellanarlos correctamente
            //---------------------------------------------
            /*
            smtp.Host = "mail.jycbolivia.com";
            smtp.Port = 26;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("automail@jycbolivia.com", "alex79016002");
            smtp.EnableSsl = false;*/
            correo.From = new MailAddress("jyc.servidor@gmail.com");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("jyc.servidor@gmail.com", "alex79016002");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(correo);
                correo.Dispose();
                //  Response.Write("<script type='text/javascript'> alert('Envio ok') </script>");
                return true;
            }
            catch (Exception ex)
            {
                //Response.Write("<script type='text/javascript'> alert('Error: " + ex.Message + "') </script>");
                return false;
            }
        }



        public bool Enviar_Correo_Equipo(string asunto, string cuerpoMensaje, string BaseDeDatos)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("jyc.servidor@gmail.com");
            //------------------------------------------------------
            //string baseDatos = Session["BaseDatos"].ToString();
            string baseDatos = BaseDeDatos;

            /*   if (baseDatos.Equals("La Paz")||baseDatos.Equals("Potosi")||baseDatos.Equals("Sucre"))
               {
                   correo.To.Add("rcn3@jycbolivia.com");
               }
               else
                   if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Oruro"))
                   {
                       correo.To.Add("rcn2@jycbolivia.com");
                   }
                   else
                       if (baseDatos.Equals("Santa Cruz") || baseDatos.Equals("Tarija") || baseDatos.Equals("Pando") || baseDatos.Equals("Beni"))
                       {
                           correo.To.Add("ran2@jycbolivia.com");
                       }  */
            
            correo.To.Add("rcn2@jycbolivia.com");
            correo.To.Add("rcn3@jycbolivia.com");
            correo.To.Add("jan@jycbolivia.com");
            correo.To.Add("fdd3@jycbolivia.com");

            //------------------------------------------

            correo.To.Add("automail@jycbolivia.com");
            correo.Subject = asunto;
            correo.Body = cuerpoMensaje;
            //----------------adjunto--------------

            //--------------------------------
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            //
            SmtpClient smtp = new SmtpClient();
            //
            //---------------------------------------------
            // Estos datos debes rellanarlos correctamente
            //---------------------------------------------
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("jyc.servidor@gmail.com", "alex79016002");

            smtp.EnableSsl = true;
            try
            {
                smtp.Send(correo);
                correo.Dispose();
                //  Response.Write("<script type='text/javascript'> alert('Envio ok') </script>");
                return true;
            }
            catch (Exception ex)
            {
                //Response.Write("<script type='text/javascript'> alert('Error: " + ex.Message + "') </script>");
                return false;
            }
        }


        public bool envioCorreoJanSfi(string asunto, string cuerpoMensaje, string BaseDeDatos)
        {
            MailMessage correo = new MailMessage();
            //correo.From = new MailAddress("automail@jycbolivia.com");
            SmtpClient smtp = new SmtpClient();
            //------------------------------------------------------
            string baseDatos = BaseDeDatos;

            if (baseDatos.Equals("La Paz"))
            {
                correo.From = new MailAddress("boletas@elevamerica.com");
                smtp.Host = "mail.elevamerica.com";
                smtp.Port = 26;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("boletas@elevamerica.com", "kqjW;MdD3FatF2v&$t");
                smtp.EnableSsl = false;
                //-------------------------------------                
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    correo.From = new MailAddress("boletas@melevar.com");
                    smtp.Host = "mail.melevar.com";
                    smtp.Port = 26;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("boletas@melevar.com", "HC{;-@N~D$=L)S+TA;");
                    smtp.EnableSsl = false;
                    //--------------------------------------                    
                }
                else
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        correo.From = new MailAddress("boletas@interlogisrl.com");
                        smtp.Host = "mail.interlogisrl.com";
                        smtp.Port = 26;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("boletas@interlogisrl.com", "LL@Hjf[1RyJGW*DG[g");
                        smtp.EnableSsl = false;
                        //-------------------------------------                        
                    }
                    else
                    {
                     /*   correo.From = new MailAddress("automail@jycbolivia.com");
                        smtp.Host = "mail.jycbolivia.com";
                        smtp.Port = 26;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("automail@jycbolivia.com", "alex79016002");
                        smtp.EnableSsl = false;
                        correo.To.Add("sistema@jycbolivia.com");*/
                        correo.From = new MailAddress("jyc.servidor@gmail.com");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("jyc.servidor@gmail.com", "alex79016002");
                        smtp.EnableSsl = true;
                    }

            //------------------------------------------
            correo.To.Add("sfi@jycbolivia.com");
            correo.To.Add("jan@jycbolivia.com");
            correo.To.Add("automail@jycbolivia.com");
            correo.Subject = asunto+" de la UNE "+BaseDeDatos;
            correo.Body = cuerpoMensaje;

            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            //


            try
            {
                smtp.Send(correo);
                correo.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Enviar_CorreoAContable(string asunto, string cuerpoMensaje, string BaseDeDatos)
        {
            MailMessage correo = new MailMessage();
            //correo.From = new MailAddress("automail@jycbolivia.com");
            SmtpClient smtp = new SmtpClient();
            //------------------------------------------------------
            string baseDatos = BaseDeDatos;

            if (baseDatos.Equals("La Paz"))
            {
                correo.From = new MailAddress("automail@elevamerica.com");
                smtp.Host = "mail.elevamerica.com";
                smtp.Port = 26;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("automail@elevamerica.com", "k&nWa1AIEIe8");
                smtp.EnableSsl = false;
                //-------------------------------------                
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    correo.From = new MailAddress("automail@melevar.com");
                    smtp.Host = "mail.melevar.com";
                    smtp.Port = 26;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("automail@melevar.com", "2Uc}Js1uq9i{");
                    smtp.EnableSsl = false;
                    //--------------------------------------                    
                }
                else
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        correo.From = new MailAddress("automail@interlogisrl.com");
                        smtp.Host = "mail.interlogisrl.com";
                        smtp.Port = 26;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("automail@interlogisrl.com", "gg.INiwC6[5%");
                        smtp.EnableSsl = false;
                        //-------------------------------------                        
                    }
                    else
                    {
                       /* correo.From = new MailAddress("automail@jycbolivia.com");
                        smtp.Host = "mail.jycbolivia.com";
                        smtp.Port = 26;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("automail@jycbolivia.com", "alex79016002");
                        smtp.EnableSsl = false;
                        correo.To.Add("sistema@jycbolivia.com");*/
                        correo.From = new MailAddress("jyc.servidor@gmail.com");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("jyc.servidor@gmail.com", "alex79016002");
                        smtp.EnableSsl = true;
                    }

            //------------------------------------------            
            correo.To.Add("rcn@jycbolivia.com");
            correo.To.Add("automail@jycbolivia.com");
            correo.Subject = asunto + " de la UNE " + BaseDeDatos;
            correo.Body = cuerpoMensaje;

            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            //


            try
            {
                smtp.Send(correo);
                correo.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        public bool enviar_Correo_gestionarEstadoInstalacion(string asunto, string cuerpoMensaje, string BaseDeDatos)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("jyc.servidor@gmail.com");
            SmtpClient smtp = new SmtpClient();
            //------------------------------------------------------
            string baseDatos = BaseDeDatos;

            /*if (baseDatos.Equals("La Paz"))
            {
                correo.From = new MailAddress("automail@elevamerica.com");
                smtp.Host = "mail.elevamerica.com";
                smtp.Port = 26;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("automail@elevamerica.com", "k&nWa1AIEIe8");
                smtp.EnableSsl = false;
                //-------------------------------------                
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    correo.From = new MailAddress("automail@melevar.com");
                    smtp.Host = "mail.melevar.com";
                    smtp.Port = 26;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("automail@melevar.com", "2Uc}Js1uq9i{");
                    smtp.EnableSsl = false;
                    //--------------------------------------                    
                }
                else
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        correo.From = new MailAddress("automail@interlogisrl.com");
                        smtp.Host = "mail.interlogisrl.com";
                        smtp.Port = 26;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("automail@interlogisrl.com", "gg.INiwC6[5%");
                        smtp.EnableSsl = false;
                        //-------------------------------------                        
                    }
                    else
                    {*/
                        correo.From = new MailAddress("jyc.servidor@gmail.com");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("jyc.servidor@gmail.com", "alex79016002");
                        smtp.EnableSsl = true;
                                    
                   // }
            

            if (baseDatos.Equals("La Paz"))
            {
                correo.To.Add("jad1@jycbolivia.com");
                correo.To.Add("rac1@jycbolivia.com");
                correo.To.Add("rcn2@jycbolivia.com");
                correo.To.Add("rcn3@jycbolivia.com");
                correo.To.Add("jan@jycbolivia.com");
                correo.To.Add("fpr11@jycbolivia.com");
                correo.To.Add("fpr12@jycbolivia.com");
                correo.To.Add("fpr13@jycbolivia.com");
                correo.To.Add("serviciosscz@jycbolivia.com");
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    correo.To.Add("jad2@jycbolivia.com");
                    correo.To.Add("rac2@jycbolivia.com");
                    correo.To.Add("rcn2@jycbolivia.com");
                    correo.To.Add("rcn3@jycbolivia.com");
                    correo.To.Add("jan@jycbolivia.com");
                    correo.To.Add("fpr21@jycbolivia.com");
                    correo.To.Add("fpr22@jycbolivia.com");
                    correo.To.Add("fpr23@jycbolivia.com");
                    correo.To.Add("serviciosscz@jycbolivia.com");
                }
                else
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        correo.To.Add("jad3@jycbolivia.com");
                        correo.To.Add("rac3@jycbolivia.com");
                        correo.To.Add("rcn2@jycbolivia.com");
                        correo.To.Add("rcn3@jycbolivia.com");
                        correo.To.Add("jan@jycbolivia.com");
                        correo.To.Add("fpr31@jycbolivia.com");
                        correo.To.Add("fpr32@jycbolivia.com");
                        correo.To.Add("fpr33@jycbolivia.com");
                        correo.To.Add("fpr34@jycbolivia.com");
                        correo.To.Add("serviciosscz@jycbolivia.com");
                    }
                    else
                        correo.To.Add("sistema@jycbolivia.com");



            //------------------------------------------            
            correo.To.Add("automail@jycbolivia.com");
            correo.To.Add("sistema@jycbolivia.com");
            correo.To.Add("fdd3@jycbolivia.com");
            correo.Subject = asunto;
            correo.Body = cuerpoMensaje;
            //----------------adjunto--------------
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;            
            try
            {
                smtp.Send(correo);
                correo.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        public bool enviar_Correo_CotizacionesRepuesto(string asunto, string cuerpoMensaje, string BaseDeDatos)
        {
            MailMessage correo = new MailMessage();
            //correo.From = new MailAddress("automail@jycbolivia.com");
            SmtpClient smtp = new SmtpClient();
            //------------------------------------------------------
            string baseDatos = BaseDeDatos;

            if (baseDatos.Equals("La Paz"))
            {
                correo.From = new MailAddress("automail@elevamerica.com");
                smtp.Host = "mail.elevamerica.com";
                smtp.Port = 26;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("automail@elevamerica.com", "k&nWa1AIEIe8");
                smtp.EnableSsl = false;
                //-------------------------------------                
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    correo.From = new MailAddress("automail@melevar.com");
                    smtp.Host = "mail.melevar.com";
                    smtp.Port = 26;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("automail@melevar.com", "2Uc}Js1uq9i{");
                    smtp.EnableSsl = false;
                    //--------------------------------------                    
                }
                else
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        correo.From = new MailAddress("automail@interlogisrl.com");
                        smtp.Host = "mail.interlogisrl.com";
                        smtp.Port = 26;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("automail@interlogisrl.com", "gg.INiwC6[5%");
                        smtp.EnableSsl = false;
                        //-------------------------------------                        
                    }
                    else
                    {
                       /* correo.From = new MailAddress("automail@jycbolivia.com");
                        smtp.Host = "mail.jycbolivia.com";
                        smtp.Port = 26;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("automail@jycbolivia.com", "alex79016002");
                        smtp.EnableSsl = false;*/
                        correo.From = new MailAddress("jyc.servidor@gmail.com");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("jyc.servidor@gmail.com", "alex79016002");
                        smtp.EnableSsl = true;
                    }


            if (baseDatos.Equals("La Paz"))
            {
                correo.To.Add("rin1@jycbolivia.com");
                correo.To.Add("rin12@jycbolivia.com");
                correo.To.Add("rcc1@jycbolivia.com");
                correo.To.Add("rcc12@jycbolivia.com");
                correo.To.Add("era1@jycbolivia.com");
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    correo.To.Add("rin2@jycbolivia.com");
                    correo.To.Add("rin21@jycbolivia.com");
                    correo.To.Add("rin22@jycbolivia.com");
                    correo.To.Add("rcc2@jycbolivia.com");
                    correo.To.Add("rcc21@jycbolivia.com");                    
                    correo.To.Add("era2@jycbolivia.com");
                }
                else
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        correo.To.Add("rin3@jycbolivia.com");                        
                        correo.To.Add("rcc3@jycbolivia.com");
                        correo.To.Add("rcc31@jycbolivia.com");
                        correo.To.Add("era3@jycbolivia.com");
                    }
                    else
                        correo.To.Add("sistema@jycbolivia.com");
            
            //------------------------------------------
            correo.To.Add("cotirepuesto@jycbolivia.com");
            correo.To.Add("automail@jycbolivia.com");
            correo.Subject = asunto;
            correo.Body = cuerpoMensaje;
            //----------------adjunto--------------
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            try
            {
                smtp.Send(correo);
                correo.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        internal void Enviar_Correo_CronogramaTecnicoFaseIFaseII(string asunto, string cuerpoMensaje, string baseDeDatos)
        {
            MailMessage correo = new MailMessage();          
            correo.To.Add("fdd3@jycbolivia.com");
            //correo.To.Add("aad@jycbolivia.com");
            correo.To.Add("rcn4@jycbolivia.com");
            correo.To.Add("jyc.servidor@gmail.com");
            correo.To.Add("sistema@jycbolivia.com");
            //------------------------------------------
            correo.To.Add("automail@jycbolivia.com");
            correo.Subject = "(" + baseDeDatos + ") " + asunto + " Unidad de Negocio " + baseDeDatos;
            correo.Body = cuerpoMensaje;
            //----------------adjunto--------------

            //--------------------------------
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            //
            SmtpClient smtp = new SmtpClient();
            //
            //---------------------------------------------
            // Estos datos debes rellanarlos correctamente
            //---------------------------------------------
            correo.From = new MailAddress("jyc.servidor@gmail.com");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("jyc.servidor@gmail.com", "alex79016002");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(correo);
                correo.Dispose();
                //  Response.Write("<script type='text/javascript'> alert('Envio ok') </script>");
            }
            catch (Exception ex)
            {
                //  Response.Write("<script type='text/javascript'> alert('Error: " + ex.Message + "') </script>");
            }
        }
    }
}