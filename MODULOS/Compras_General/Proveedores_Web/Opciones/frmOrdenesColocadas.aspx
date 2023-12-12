<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmOrdenesColocadas.aspx.cs" Inherits="Opciones_frmOrdenesColocadas" %>

<%@ Register assembly="SC_Controls_CSW" namespace="SC_Controls_CSW" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ordenes Descargadas</title>
    <script type="text/javascript" language="javascript" src="../scripts/jquery-1.8.2.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.datepicker.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/i18n/jquery.ui.datepicker-es.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.core.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/jquery.dataTables.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/JScript_frmOrdenesColocadas.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/Scroller.js"></script>
	<link rel="stylesheet" type="text/css" href="../css/demos.css"/>
	<link rel="stylesheet" type="text/css" href="../css/windows.css"/>
    <link rel="stylesheet" type="text/css" href="../css/themes/redmond/jquery.ui.all.css"/>
    <link rel="stylesheet" type="text/css" href="../css/menu.css" />
    <style type="text/css">
        @import "../css/demo_page.css";
        @import "../css/demo_table.css";
        #ContenedorWindow {
            margin: 0px;
			padding: 0px;
			width: 973px; 
            height: 425px;
        }
    </style>
</head>
<body>
    <form id="OrdenesColocadas" runat="server">
    <div id="menu"></div>
    <div id="boxes">
	    <div id="dialog" class="window">
            <div class="d-login"><a href="#" class="close">
                <asp:Image ID="imgCancelar" runat="server" ImageUrl="~/images/servicestopped.ico" />&nbsp;Cancelar</a></div>
            <br />
		    <div id="imageLoad">
        	    <img src="../images/loader.gif" width="128" height="128" alt="Procesando" align="middle"/>
                <h2>Procesando</h2>
		    </div>    
	    </div>
	    <!-- Mask que cubrira el screen -->
	    <div id="mask"></div>
    </div>
     <div id="ContenedorWindow">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td id="tdContenedorb">
                    <a href="#dialog" name="modal" rev="Limpiar"><asp:Image ID="ImgLimpiar" runat="server" Height="16px" 
                        ImageUrl="~/images/nuevo.ico" ToolTip="Limpiar" Width="16px" /></a>
                    <asp:Image ID="ImgSeparador1" runat="server" Height="16px" 
                        ImageUrl="~/images/separator.png" Width="16px" />
                    <a href="#dialog" name="modal" rev="Ejecutar"><asp:Image ID="ImgEjecutar" runat="server" Height="16px" 
                        ImageUrl="~/images/btn_Ejecutar_16.ico" ToolTip="Ejecutar" Width="16px"/></a>
                </td>
            </tr>
            <tr>
                <td id="tdContenedor">
                    <table width="970px" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                             <fieldset style="width:658px; height:50px;">
			                        <legend><strong>Información de estados</strong></legend>
                                    <table width="658px" border="0" cellpadding="0" cellspacing="0" align="center" style="height:25px;">
  				                        <tr>
				                            <td  class="Labels" style="width:72px;">
                                                <asp:Label ID="lblEstado" runat="server" Text="Estado :"></asp:Label>
                                            </td>
				                            <td  style="width:233px;">
                                                <cc1:scwComboBoxExt ID="cboEstados" runat="server" width="233px" >
                                                </cc1:scwComboBoxExt>
                                            </td>
			                            </tr>			                            
			                        </table>
                        		</fieldset>
    	                    </td>
                            <td>
   	                            <fieldset style="width:302px; overflow:hidden; height:50px;">
		                            <legend><strong>Rango de fechas</strong></legend>
                                    <table width="302px" border="0" cellpadding="0" cellspacing="0" style="height:25px;">
		                                <tr>
		                                    <td class="Labels" style="width:50px;" valign="middle">
                                                <asp:Label ID="lblPeriodo" runat="server" Text="Inicio :"></asp:Label>
                                            </td>
		                                    <td width="100px" valign="middle">
                                                <cc1:scwTextBoxExt ID="dtpFechaInicial" runat="server" Width="90%" onkeydown="KeyDownHandler()"></cc1:scwTextBoxExt>
                                            </td>
                                            <td class="Labels" style="width:50px;" valign="middle">
                                                <asp:Label ID="Label2" runat="server" Text="Fin :"></asp:Label>
                                            </td>
		                                    <td width="100px" valign="middle">
                                                <cc1:scwTextBoxExt ID="dtpFechaFinal" runat="server" Width="90%" onkeydown="KeyDownHandler()"></cc1:scwTextBoxExt>
                                            </td>
                                        </tr>
		                            </table>
    	                        </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
    	                        <fieldset style="width:965px; overflow:hidden; height:345px;">
			                        <legend><strong>Listado de ordenes de compra</strong></legend>
			                        <div id="Resultado" style="width:953px; height:314px; overflow:hidden; display:block; padding:5px; position:absolute;">
			                            <asp:ListView ID="lstOrdenes" runat="server">
			                                <EmptyDataTemplate>
			                                    <center>No existe informacion para mostrar bajo los criterios seleccionados.</center>
			                                </EmptyDataTemplate>
			                            </asp:ListView>
			                        </div>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" name="__EVENTTARGET" value ="" />
    <input type="hidden" name="__EVENTARGUMENT" value ="" />
    <input type="hidden" name="__IdFolio" value ="" />
    <input type="hidden" name="__IdFarmaciaRecibe" value ="" />
    <input type="hidden" name="__IdFarmacia" value ="" />
    </form>
</body>
</html>
