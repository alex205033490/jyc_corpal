using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace jycboliviaASP.net.Reportes
{
    public class NA_QRCodeNet
    {
        public NA_QRCodeNet() { }

        public void CrearImagenQR(string nombreArchivo,string contenido, int tamanioImagen){
               QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
               QrCode qrCode = qrEncoder.Encode(contenido);
               int moduleSizeInPixels = tamanioImagen;
               GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(moduleSizeInPixels, QuietZoneModules.Two), Brushes.Black, Brushes.White);
               using (FileStream stream = new FileStream(nombreArchivo+".jpg", FileMode.Create))
               {
                   renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
               }
        }
        
        }
}