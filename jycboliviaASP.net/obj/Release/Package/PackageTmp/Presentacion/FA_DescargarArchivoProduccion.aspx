<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FA_DescargarArchivoProduccion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_DescargarArchivoProduccion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<<<<<<< HEAD
      <title>JyC</title>      
      <meta name="viewport" charset="utf-8" content="width=device-width, initial-scale=1" />
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
      <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet" />            
=======
      <title>JyC</title>
      <meta charset="utf-8">
      <meta name="viewport" content="width=device-width, initial-scale=1">
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
      <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet">            
>>>>>>> origin/modulo3
      <link href="../Styles/payment.css" rel="stylesheet" type="text/css" />
      <link href="../Styles/Style_Autocompletar.css" rel="stylesheet" type="text/css" />
      <link rel="Shortcut Icon" href="../Images/jyc_icono.ico" />
</head>

<body>
 
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <div>   
            <div class="payment-form dark">
              <div class="container">
                <div class="block-heading">
                  <h2>Descargar Archivo</h2>                  
                </div>
                
                  <div class="products">
                    <h3 class="title">Descargar Archivo</h3>
                      
                      <div class="form-group col-sm-6">
                        <asp:Button ID="bt_descargar" runat="server" Text="Descargar Voucher"  
                              class="btn btn-primary btn-block" onclick="bt_descargarArchivo"  />
                      </div>

                       <div class="form-group col-sm-6">
                        <asp:Button ID="bt_descargarPC" runat="server" Text="Descargar PC"  
                              class="btn btn-primary btn-block" onclick="bt_descargarPC_Click"  />
                      </div>
                        
                      <div class="form-group col-sm-6">
                      
                      </div>
                                            
                      <div class="form-group col-sm-6">
                        <asp:Button ID="bt_cancelar" runat="server" Text="Volver"  
                              class="btn btn-primary btn-block" onclick="bt_cancelar_Click"  />
                      </div>          
                  </div>
                 </div>
               </div>                          
          </div>
    </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</body>

</body>
</html>
