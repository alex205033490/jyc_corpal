<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FA_Login.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login JyC</title>
    <link rel="Shortcut Icon" href="../Images/jyc_icono.ico" />
   
   <meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
<!--===============================================================================================-->		
	<link rel="stylesheet" type="text/css" href="../vendor/bootstrap/css/bootstrap.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../fonts/font-awesome-4.7.0/css/font-awesome.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../vendor/animate/animate.css">
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="../vendor/css-hamburgers/hamburgers.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../vendor/animsition/css/animsition.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../vendor/select2/select2.min.css">
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="../vendor/daterangepicker/daterangepicker.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../css/util.css">
    <link rel="stylesheet" type="text/css" href="../css/main.css" />
<!--===============================================================================================-->
   
   
</head>
<body>



<!--===============================================================================================-->
<div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100">
				<form id="form1" runat="server" class="login100-form validate-form p-l-55 p-r-55 p-t-178">
					<span class="login100-form-title">
						Login 
                        
                    </span>                  
                  
					<div class="wrap-input100 validate-input m-b-16">
                    	<input class="input100" type="text" name="username" placeholder="Username" runat="server" ID="tx_usuario" />                         
						<span class="focus-input100"></span>
					</div>

                    

					<div class="wrap-input100 validate-input" data-validate = "Please enter password">						
                        <input type="password" class="input100" name="pass" placeholder="Password" runat="server" id="tx_password" /> 
						<span class="focus-input100"></span>
					</div>

                    </br>

                    <div class="wrap-input100 validate-input m-b-16" data-validate="Please enter username">						
                        <asp:DropDownList class="input100"  ID="dd_loginDpto" runat="server" >
                              <asp:ListItem>Corpal</asp:ListItem>
                              <asp:ListItem>Prueba</asp:ListItem>
                            </asp:DropDownList>
						<span class="focus-input100"></span>
					</div>

					<div class="text-right p-t-13 p-b-23">
						<span class="txt1">
							Forgot
						</span>

						<a href="#" class="txt2">
							Username / Password?
						</a>
					</div>

					<div class="container-login100-form-btn">						
                        <asp:Button class="login100-form-btn" ID="bt_login" runat="server" Text="Login" 
                                    onclick="bt_login_Click" />
					</div>

					<div class="flex-col-c p-t-170 p-b-40">
						<span class="txt1 p-b-9">
							Don’t have an account?
						</span>

						<a href="#" class="txt3">
							Sign up now
						</a>
					</div>
				</form>
			</div>
		</div>
	</div>

<!--===============================================================================================-->
	<script src="../vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
	<script src="../vendor/animsition/js/animsition.min.js"></script>
<!--===============================================================================================-->
	<script src="../vendor/bootstrap/js/popper.js"></script>
	<script src="../vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
	<script src="../vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
	<script src="../vendor/daterangepicker/moment.min.js"></script>
	<script src="../vendor/daterangepicker/daterangepicker.js"></script>
<!--===============================================================================================-->
	<script src="../vendor/countdowntime/countdowntime.js"></script>
<!--===============================================================================================-->
	<script src="../js/main.js"></script>
<!--===============================================================================================-->


</body>

</html>
