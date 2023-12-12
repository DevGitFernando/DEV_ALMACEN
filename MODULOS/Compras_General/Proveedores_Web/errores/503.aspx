<%@ Page Language="C#" AutoEventWireup="true" Inherits="errores_503" Codebehind="503.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    < <title>Temporización de puerta</title>
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <link href="../css/MsjGeneral.css" rel="stylesheet" type="text/css"/>
</head>
<body style="background-color:#fff">
    <form id="Denegado" runat="server">
    <div id="Contenedor">
        <table border="0" cellspacing="0" cellpadding="0" id="msj">
            <tr id="top">
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr id="txt">
                <td id="txtl">&nbsp;</td>
                <td><h1>Tiempo Agotado</h1></td>
                <td id="txtr">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
    </table>
    </div>
    <div class="error mensajes">El servidor tomó demasiado tiempo para responder y se desconectó.</div>
    </form>
</body>
</html>