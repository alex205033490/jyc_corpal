<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FA_MenuPorArea.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_MenuPorArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="Shortcut Icon" href="../Images/jyc_icono.ico" />

    <!-- Bootstrap Core CSS -->    
    <link href="../jquery/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme CSS -->
    <link href="../jquery/css/freelancer.css" rel="stylesheet" type="text/css" />    
    <!-- Custom Fonts -->
    <link href="../jquery/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />    
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" rel="stylesheet" type="text/css">
</head>

<body>
    <form id="form1" runat="server">
    <div>
     <!-- Header -->
    <header>
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                <a href="#portfolio">
                    <img class="img-responsive" src="../img/profile.png" alt=""> </a>
                    <div class="intro-text">
                        <span class="name">Ingenieria</span>
                        <span class="name">En Ascensores</span>
                        </br>
                        </br>
                        </br>
                        <span class="skills"></span>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <!-- Portfolio Grid Section -->
    <section id="portfolio">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <u><h2>Sistemas</h2></u>
                    </br>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4 portfolio-item">                    
                    <a href="./WelcomeJyC.aspx" class="portfolio-link" data-toggle="modal">
                        <div class="caption">
                            <div class="caption-content">
                                <i class="fa fa-check-circle fa-3x"></i>
								<h5>Sistema JyC (Cuadros X)</h5>
                            </div>
                        </div>                        
                        <img src="../img/JYC1.png" class="img-responsive" alt="">						
                    </a>
                    <h6>JyC Srl (cuadros X)</h6>
                </div>

                <div class="col-sm-4 portfolio-item">
                    <a href="./FA_encuestajyc.aspx" class="portfolio-link" data-toggle="modal">
                        <div class="caption">
                            <div class="caption-content">
                                <i class="fa fa-check-circle fa-3x"></i>
                                <h5>Encuesta</h5>
                            </div>
                        </div>
                        <img src="../img/Encuesta1.png" class="img-responsive" alt="">
                    </a>
                <h6>Encuesta Edificio</h6>
                </div>
                <div class="col-sm-4 portfolio-item">
                    <a href=./WelcomeJyC.aspx" class="portfolio-link" data-toggle="modal">
                        <div class="caption">
                            <div class="caption-content">
                                <i class="fa fa-check-circle fa-3x"></i>
                                <h5>Mantenimiento</h5>
                            </div>
                        </div>
                        <img src="../img/mantenimiento.png" class="img-responsive" alt="">
                    </a>
                    <h6>Mantenimiento</h6>
                </div>
                <div class="col-sm-4 portfolio-item">
                    <a href="./WelcomeJyC.aspx" class="portfolio-link" data-toggle="modal">
                        <div class="caption">
                            <div class="caption-content">
                                <i class="fa fa-check-circle fa-3x"></i>
                                <h5>Comercial</h5>
                            </div>
                        </div>
                        <img src="../img/Comercial.png" class="img-responsive" alt="">
                    </a>
                    <h6>Comercial</h6>
                </div>
                <div class="col-sm-4 portfolio-item">
                    <a href="./WelcomeJyC.aspx" class="portfolio-link" data-toggle="modal">
                        <div class="caption">
                            <div class="caption-content">
                                <i class="fa fa-check-circle fa-3x"></i>
                                <h5>Call Center</h5>
                            </div>
                        </div>
                        <img src="../img/CallCenter.png" class="img-responsive" alt="">
                    </a>
                    <h6>Puertas Automaticas</h6>
                </div>
                <div class="col-sm-4 portfolio-item">
                    <a href="./WelcomeJyC.aspx" class="portfolio-link" data-toggle="modal">
                        <div class="caption">
                            <div class="caption-content">
                                <i class="fa fa-check-circle fa-3x"></i>
                                <h5>En Desarrollo</h5>
                            </div>
                        </div>
                        <img src="../img/JYC1.png" class="img-responsive" alt="">
                    </a>
                    <h6>En Desarrollo</h6>
                </div>
            </div>
        </div>
    </section>
  
   

    <!-- jQuery -->    
    <script src="../jquery/jquery/jquery.min.js" type="text/javascript"></script>

    <!-- Bootstrap Core JavaScript -->    
    <script src="../jquery/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>

    <!-- Plugin JavaScript -->
    <script src="http://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.3/jquery.easing.min.js"></script>

    <!-- Contact Form JavaScript -->
    <script src="../jquery/js/jqBootstrapValidation.js" type="text/javascript"></script>
    <script src="../jquery/js/contact_me.js" type="text/javascript"></script>    

    <!-- Theme JavaScript -->    
    <script src="../jquery/js/freelancer.min.js" type="text/javascript"></script>


    
    </div>
    </form>
</body>
</html>
