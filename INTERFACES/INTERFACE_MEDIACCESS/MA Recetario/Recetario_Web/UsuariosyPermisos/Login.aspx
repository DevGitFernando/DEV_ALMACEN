<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="UsuariosyPermisos_Login" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">--%>
<!doctype html>
<html lang="es">
<head runat="server">
	<meta charset="utf-8">
    <link rel="stylesheet" href="../css/login.css">
    <link rel="stylesheet" href="../css/componentes.css">
    
    <title>Login</title>
    <script type="text/javascript">
        if (top != self) top.location.href = location.href;
    </script>
</head>

<body>
    <div id="loader" class="mask">
        <div class="elementCenter loader">
            <span class="msjLoader">Iniciando sesión, espere por favor.</span>
        </div>
    </div>
      <header>
     	<span id="logoime"></span>
      	<span id="titulo" class="titulo" runat="server">Receta electrónica</span>
        <span id="logomedia"></span>
     </header>
     
	 <section id="content_general">
        <div id="wrapper" runat="server">
           
            <div class="title_login">Acceso Médicos</div>
            
            	 <!--<select name="Empresa" id="select" class="icoempresa">
                 	<option>Empresa</option>
                    <option>Intercontinental de Medicamentos</option>
                    <option>Phenix Farmacéutica</option>
                    <option>Dbmex</option>
                </select>-->
                
                <%--<select name="Estado" id="cboEstado" class="icoestado">
                    <option>Estado</option>
                    <option>Puebla</option>
                    <option>Guanajuato</option>
                    <option>México</option>
                </select>
                <select name="Farmacia" id="cboFarmacia" class="icofarmacia">
                    <option>Farmacia</option>
                    <option>2403 CENTRO DE SALUD URBANO COMPLEJO MÉDICO GONZALO RÍO ARRONTE</option>
                    <option>2404 HOSPITAL GENERAL GONZALO RÍO ARRONTE</option>
                    <option>2505 CENTRO DE SALUD URBANO COMPLEJO MÉDICO</option>
                </select>--%>
               
                <input id="txtUser" type="text"  class="icouser input_txt" placeholder="Clave Médico" maxlength="500"/>
                <input id="txtPassword" type="password"  class="icopassword input_txt" name="Contrasena" value="" placeholder="Contraseña" maxlength="500"/>
                
            	<div id="btn_sign">INGRESAR</div>
	            <!--<span class="mensaje_pass">Olvidaste tu contraseña <a href="#"><strong>click aquí</strong></a></span>-->
   			
           
        </div>
    </section>
	<footer></footer>
    <script type="text/javascript" src="../js/jquery-3.0.0.min.js"></script>
    <script type="text/javascript" src="../js/login.js"></script>
    <script type="text/javascript">
        $(function () {
            login.init();
        });
	</script>
    </body>
</html>
