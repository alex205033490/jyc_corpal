<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FA_EncuestaMantenimientoPreguntas.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_EncuestaPreguntas" %>

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
                  <h2>Encuesta de satisfaccion del cliente (Mantenimiento)</h2>                  
                </div>
                
                  <div class="products">
                  
                  <h3 class="title">MANTENIMIENTO</h3>                    
                    <div class="row">
                        <div class="form-group col-sm-8">
                             <label for="">Cumplimiento de fechas planificadas para mantenimiento</label>                        
                        </div>
                     <div class="form-group col-sm-4">                      
                                <asp:DropDownList ID="dd_Mcumplientodefechasplanificadasparamantenimiento" 
                                    runat="server" class="form-control" 
                                    placeholder="Departamento" aria-label="Card Holder" 
                                    aria-describedby="basic-addon1">
                                    <asp:ListItem Value="0">Ninguno</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>                            
                                </asp:DropDownList>                    
                     </div>

                     <div class="form-group col-sm-8">
                        <label for="">Funcionamiento del (o de los) los equipos(s)</label>                        
                     </div>
                     <div class="form-group col-sm-4">                      
                        <asp:DropDownList ID="dd_Mfuncionamientodelosequipos" runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>                            
                        </asp:DropDownList>                    
                    </div>  
                  </div>  
                        
                  <!-- ---------------------------------------------------- - -->  
                 <h3 class="title">REPARACIONES</h3>
                  <div class="row">
                      <div class="form-group col-sm-8">
                        <label for="">Rapidez de las reparaciones</label>                        
                     </div>
                     <div class="form-group col-sm-4">                      
                        <asp:DropDownList ID="dd_Rrapidezdelasreparaciones" runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>                            
                        </asp:DropDownList>                    
                    </div>  
               
                      <div class="form-group col-sm-8">
                        <label for="">Resolucion efectiva de la causa de reparacion</label>                        
                     </div>
                     <div class="form-group col-sm-4">                      
                        <asp:DropDownList ID="dd_Rresolucionefectivadelacausadereparacion" 
                             runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>                            
                        </asp:DropDownList>                    
                    </div>  
                                   
                      <div class="form-group col-sm-8">
                        <label for="">Asesoramiento y rapidez en la entrega de cotizaciones y/o informes</label>                        
                     </div>
                     <div class="form-group col-sm-4">                      
                        <asp:DropDownList ID="dd_Rasesoramientoyrapidezenlaentregadecotizacionesoinformes" 
                             runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>                            
                        </asp:DropDownList>                    
                    </div> 
                  </div>   
               <!-- ---------------------------------------------------- - -->  
               <h3 class="title">EMERGENCIAS</h3>
                 <div class="row">  
                      <div class="form-group col-sm-8">
                        <label for="">Tiempo de respuesta ante una emergencia</label>                        
                     </div>
                     <div class="form-group col-sm-4">                      
                        <asp:DropDownList ID="dd_Etiempoderespuestaanteunaemergencia" runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>                            
                        </asp:DropDownList>                    
                    </div>  

                    <div class="form-group col-sm-8">
                        <label for="">Resolucion efectiva de las emergencias (tiempo de habilitacion de equipo)</label>                        
                     </div>
                     <div class="form-group col-sm-4">                      
                        <asp:DropDownList ID="dd_Eresolucionefectivadelasemergencia" runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>                            
                        </asp:DropDownList>                    
                    </div>  
                </div>  
               <!-- ---------------------------------------------------- - -->                   
                 <h3 class="title">ATENCION AL CLIENTE</h3>
                  <div class="row">
                      <div class="form-group col-sm-8">
                        <label for="">Cordialidad y atencion del personal de cobranza</label>                        
                     </div>
                     <div class="form-group col-sm-4">                      
                        <asp:DropDownList ID="dd_Acordialidadyatenciondelpersonaldecobranza" 
                             runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>                            
                        </asp:DropDownList>                    
                    </div>  

                    <div class="form-group col-sm-8">
                        <label for="">Trato y atencion del personal administrativo</label>                        
                     </div>
                     <div class="form-group col-sm-4">                      
                        <asp:DropDownList ID="dd_Atratoyatenciondelpersonaladministrativo" 
                             runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>                            
                        </asp:DropDownList>                    
                    </div>  

                    <div class="form-group col-sm-8">
                        <label for="">Cordialidad y atencion del personal tecnico</label>                        
                     </div>
                     <div class="form-group col-sm-4">                      
                        <asp:DropDownList ID="dd_Acordialidadyatenciondelpersonaltecnico" runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>                            
                        </asp:DropDownList>                    
                    </div>  

                    <div class="form-group col-sm-8">
                        <label for="">Trato y atencion del personal de ingenieria</label>                        
                     </div>
                     <div class="form-group col-sm-4">                      
                        <asp:DropDownList ID="dd_Atratoyatenciondelpersonaldeingenieria" runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>                            
                        </asp:DropDownList>                    
                    </div>  

                    <div class="form-group col-sm-8">
                        <label for="">Trato, atencion y respuesta del personal de CallCenter</label>                        
                     </div>
                     <div class="form-group col-sm-4">                      
                        <asp:DropDownList ID="dd_Atratoatencionyrespuestadelpersonaldecallcenter" 
                             runat="server" class="form-control" 
                            placeholder="Departamento" aria-label="Card Holder" 
                            aria-describedby="basic-addon1">
                            <asp:ListItem Value="0">Ninguno</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>                            
                        </asp:DropDownList>                    
                    </div>  
                  </div>  
             <!--  ----------------------------------------------- -->             
               <div class="row">
                  <div class="form-group col-sm-12">
                        <label for="card-number">Sugerencia de mejora</label>                                  
                        <asp:TextBox ID="tx_sugerenciademejora" runat="server" class="form-control" 
                               placeholder="Observaciones" aria-label="Card Holder" 
                               aria-describedby="basic-addon1" Height="80px" TextMode="MultiLine" ></asp:TextBox>                        
                  </div>
                </div>
         <!--  ----------------------------------------------- -->
               <div class="row">               
                <div class="form-group col-sm-6">
                        <asp:Button ID="bt_realizadoOk" runat="server" Text="Realizado"  
                            class="btn btn-primary btn-block" onclick="bt_realizadoOk_Click"  />
                      </div>    
            <div class="form-group col-sm-6">
                        <asp:Button ID="bt_cancelar" runat="server" Text="Cancelar"  
                            class="btn btn-primary btn-block" onclick="bt_cancelar_Click"  />
                      </div>    
                 </div>          
        <!--  ----------------------------------------------- -->

                  </div>
                 </div>
               </div>                          
          </div>
    </div>
     <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    </form>
</body>
</html>
