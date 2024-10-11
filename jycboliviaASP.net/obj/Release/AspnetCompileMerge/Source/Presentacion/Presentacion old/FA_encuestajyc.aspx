<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FA_encuestajyc.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_encuestajyc" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>JyC</title>
      <meta charset="utf-8">
      <meta name="viewport" content="width=device-width, initial-scale=1">
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
      <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet">            
      <link href="../Styles/payment.css" rel="stylesheet" type="text/css" />
      <link href="../Styles/Style_Autocompletar.css" rel="stylesheet" type="text/css" />
      <link rel="Shortcut Icon" href="../Images/jyc_icono.ico" />
</head>

<body>
 
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <div>   
            <div class="payment-form dark">
              <div class="container">
                <div class="block-heading">
                  <h2>Datos del Proyecto / Cliente</h2>                  
                </div>
                
                  <div class="products">
                    <h3 class="title">Datos del Proyecto</h3>
                    
                    <div class="item">                      
                      <p class="item-name">EDIFICIO:</p>
                       <asp:TextBox ID="tx_edificio" runat="server" class="form-control" placeholder="Nombre Edificio" aria-label="Card Holder" aria-describedby="basic-addon1" ></asp:TextBox>                                      
                       <asp:AutoCompleteExtender ID="tx_edificio_AutoCompleteExtender" runat="server"  
                                    TargetControlID="tx_edificio"
                                    Enabled="True" BehaviorID="AutoCompleteEx" CompletionInterval="200"
                                    ServicePath="" servicemethod="GetlistaProyectos"
                                    minimumprefixlength="2"  DelimiterCharacters="" enablecaching="true"                     
                                    completionsetcount="30" 
                                    ShowOnlyCurrentWordInCompletionListItem="True" 
                                    CompletionListCssClass="CompletionList" 
                                    CompletionListItemCssClass="CompletionlistItem" 
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" >
                </asp:AutoCompleteExtender>
                
                    </div>
                    
                    <div class="item">                      
                      <p class="item-name">DIRECCION:</p>
                      <asp:TextBox ID="tx_direccionEdificio" runat="server" class="form-control" placeholder="Direccion"  aria-label="Card Holder" aria-describedby="basic-addon1" ></asp:TextBox>                      
                    </div>                    
                    
                    <div class="item">                      
                      <p class="item-name">DEPARTAMENTO:</p>
                        <asp:DropDownList ID="dd_departamento" runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem>Santa Cruz</asp:ListItem>
                            <asp:ListItem>Cochabamba</asp:ListItem>
                            <asp:ListItem>La Paz</asp:ListItem>
                            <asp:ListItem>Beni</asp:ListItem>
                            <asp:ListItem>Pando</asp:ListItem>
                            <asp:ListItem>Tarija</asp:ListItem>
                            <asp:ListItem>Oruro</asp:ListItem>
                            <asp:ListItem>Potosi</asp:ListItem>
                            <asp:ListItem>Chuquisaca</asp:ListItem>
                        </asp:DropDownList>
                    
                    </div>  
                  
                  <h3 class="title">Datos Encargado Pago</h3>
                    
                    <div class="row">
                      <div class="form-group col-sm-7">
                        <label for="card-holder">Encargado Pago:</label>
                        <asp:TextBox ID="tx_encargadoPago" runat="server" class="form-control" placeholder="Nombre Encargado" aria-label="Card Holder" aria-describedby="basic-addon1" ></asp:TextBox>                        
                      </div>
                      
                      <div class="form-group col-sm-5">
                        <label for="">Celular / Telefono</label>
                        <div class="input-group expiration-date">
                          <asp:TextBox ID="tx_celularEncargado" runat="server" class="form-control" placeholder="Celular" aria-label="Card Holder" aria-describedby="basic-addon1" ></asp:TextBox>                                                  
                          <span class="date-separator">/</span>
                          <asp:TextBox ID="tx_telefonoEncargado" runat="server" class="form-control" placeholder="Telefono" aria-label="Card Holder" aria-describedby="basic-addon1" ></asp:TextBox>                                                  
                        </div>                      
                     </div>

                      <div class="form-group col-sm-7">
                        <label for="card-number">Direccion Encargado:</label>
                        <asp:TextBox ID="tx_direccionEncargado" runat="server" class="form-control" placeholder="Direccion" aria-label="Card Holder" aria-describedby="basic-addon1" ></asp:TextBox>                                                                          
                      </div>
                      
                      <div class="form-group col-sm-5">
                        <label for="cvc">Correo:</label>
                        <asp:TextBox ID="tx_correoEncargado" runat="server" class="form-control" placeholder="Correo" aria-label="Card Holder" aria-describedby="basic-addon1" ></asp:TextBox>                                                                                                  
                      </div>
                      
                       <div class="form-group col-sm-4">
                        <label for="card-number">Facturar A:</label>
                        <asp:TextBox ID="tx_facturarAEncargado" runat="server" class="form-control" placeholder="Facturar" aria-label="Card Holder" aria-describedby="basic-addon1" ></asp:TextBox>                                                                                                                          
                      </div>

                      <div class="form-group col-sm-4">
                        <label for="card-number">Banco:</label>
                        <asp:TextBox ID="tx_bancoEncargado" runat="server" class="form-control" placeholder="Banco" aria-label="Card Holder" aria-describedby="basic-addon1" ></asp:TextBox>                                                                                                                          
                      </div>
                      
                       <div class="form-group col-sm-4">
                        <label for="card-number">Nit:</label>
                        <asp:TextBox ID="tx_nitEncargado" runat="server" class="form-control" placeholder="Nit" aria-label="Card Holder" aria-describedby="basic-addon1" ></asp:TextBox>                        
                      </div>
                      
                       <div class="form-group col-sm-12">
                        <label for="card-number">Observaciones:</label>                                  
                        <asp:TextBox ID="tx_observacionesEncargado" runat="server" class="form-control" 
                               placeholder="Observaciones" aria-label="Card Holder" 
                               aria-describedby="basic-addon1" Height="80px" TextMode="MultiLine" ></asp:TextBox>                        
                      </div>
                      
                      <div class="form-group col-sm-6">
                        <asp:Button ID="bt_verificar" runat="server" Text="Verificar"  
                              class="btn btn-primary btn-block" onclick="bt_verificar_Click1"  />
                      </div>
                      
                      <div class="form-group col-sm-6">
                        <asp:Button ID="bt_Actualizar" runat="server" Text="Actualizar"  
                              class="btn btn-primary btn-block" onclick="bt_Actualizar_Click"  />
                      </div>
                      
                      <div class="form-group col-sm-6">
                        <asp:Button ID="bt_encuesta" runat="server" Text="Encuesta"  
                              class="btn btn-primary btn-block" onclick="bt_encuesta_Click"  />
                      </div>
                      
                      <div class="form-group col-sm-6">
                        <asp:Button ID="bt_cancelar" runat="server" Text="Cancelar"  
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
</html>
