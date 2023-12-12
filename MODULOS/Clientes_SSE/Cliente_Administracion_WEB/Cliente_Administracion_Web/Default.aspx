<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicio de sesión</title>
    <meta charset="UTF-8">
    <link rel="shortcut icon" href="images/favicon.ico" /> 
    <style type="text/css">
        body, #frmPrincipal { position:absolute; width:100%; height:100%; overflow:hidden; margin:0px; padding:0px; -webkit-user-select: none; -moz-user-select: none; -khtml-user-select: none; -ms-user-select: none; }
        #frmPrincipal { overflow: auto; border: none; }
    </style>
    <script type="text/javascript">
        if (top.location != this.location) top.location = this.location;
    </script>
</head>
<body>
    <form id="frmDefault" runat="server">
    <div>
        <iframe id="frmPrincipal" src="" frameborder="0" scrolling="auto" runat="server"></iframe>
    </div>
    </form>
</body>
</html>
