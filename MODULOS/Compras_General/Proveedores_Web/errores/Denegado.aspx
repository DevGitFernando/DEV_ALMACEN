﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Denegado.aspx.cs" Inherits="errores_Denegado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acceso denagado</title>
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <link href="../css/MsjGeneral.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" language="javascript">
        if (top.location != this.location) 
        {
            top.location = this.location;
        }
    </script>
</head>
<body>
    <form id="Denegado" runat="server">
    <div id="Contenedor">
        <table border="0" cellspacing="0" cellpadding="0" id="msj">
            <tr id="top">
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr id="txt">
                <td id="txtl">&nbsp;</td>
                <td><h1>Acceso Denegado</h1></td>
                <td id="txtr">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
    </table>
    </div>
    <div class="error mensajes">Es posible que necesite registrarse en el sitio antes de estar autorizado a acceder a él.</div>
    </form>
</body>
</html>
