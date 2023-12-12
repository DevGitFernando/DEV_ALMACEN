<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmGruposDeUsuario.aspx.cs" Inherits="UsuariosyPermisos_frmGruposDeUsuario" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">--%>
<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <title>Grupos de usuario</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" /> 
	<link rel="shortcut icon" href="../images/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="../css/m-styles.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/GeneralForm.css" />
    <link rel="stylesheet" type="text/css" href="../css/default.css" />
    <%  if (DtGeneral.IdEmpresa == "001")
        {%>
    
            <link rel="stylesheet" href="../css/south-street/jquery-ui-1.10.1.custom.css" />
    <%
        }
        else
        {
    %>
            <link rel="stylesheet" href="../css/redmond/jquery-ui-1.10.3.custom.css" />
    <%
        }
    %>
    <% { Response.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/GeneralForm_" + DtGeneral.IdEmpresa + ".css\" />"); } %>
    <% { Response.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/" + DtGeneral.IdEmpresa + ".css\" />"); } %>
    <link rel="stylesheet" type="text/css" href="../css/components.css" />
    <link rel="stylesheet" type="text/css" href="../css/toastr.css"/>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.datepicker-es.min.js"></script>
    <script type="text/javascript" src="../js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/toastr.js"></script>
    <script type="text/javascript" src="../js/Scroller.js"></script>
    <script type="text/javascript" src="../js/general.js"></script>
    <style type="text/css">
        
        body
        {
            background-image:none;
            background-color:#e8e6fd;
        }
        
        .container {
            width: 600px;
            height: auto;
        }
        #Info
        {
            height: 471px;
            margin-bottom: 0px;
            width: 100%;
        }
        #Groups,
        #Users
        {
            position: relative;
            float: left;
            width: 294px;
            height: 398px;
            padding: 0px;
            margin-bottom: 0px;
        }
        #Users
        {
            margin-right: 0px;
        }
        .first
        {
            margin-top: 8px;
        }
        #Permisos
        {
            width: 582px;
            margin-bottom: 8px;
            padding-top: 16px;
            padding-bottom: 0px;
        }
        #cboPerfiles
        {
            width: 490px;
        }
        #ulUsuarios
        {
            margin-top: 16px;
            height: 382px;
            overflow-y: auto;
        }
        
        #Groups
        {
            overflow-y: auto;
        }
        
        #Groups div 
        {
            width: 276px;
            margin: 8px;
            margin-bottom: 1px;
            background: white;
            border: 1px #D5D5D5 solid;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            -ms-border-radius: 5px;
            -o-border-radius: 5px;
            border-radius: 5px;
            overflow: hidden;
        }
        
        #Groups div span
        {
            padding: 0px 10px 8px 10px;
            border-bottom: 1px solid #E8E8E8;
            cursor: pointer;
            line-height: 35px;
        }
        .first
        {
            margin-top: 16px;
        }
        #Groups div:nth-child(0) 
        { 
            margin-top: 16px !important;
        }
        #Groups ul
        {
            margin-top: 1px;
        }
    </style>
    <script type="text/javascript">
        if (top.location == this.location) top.location = '../Default.aspx';
        $(function () {
            function initPermisos() {
                var cboPerfiles = $('#cboPerfiles');

                initOptions();
                initDelete();

                cboPerfiles.off();
                cboPerfiles.on('change', function (e) {
                    General.clearToastMsj();
                    General.showToastMsj('Procesando informaci&oacute;n, espere un momento', true, 'info', 10000, 'bottom-full-width');
                    designGroups($(this).val());
                }).removeAttr('disabled', 'disabled');
            }

            function designGroups(val) {
                var parametros = {
                    sOption: val
                };

                $.ajax({
                    url: "../DllClienteRegionalWeb/wsGeneral.aspx/designGroups",
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(parametros),
                    success: function Ready(res) {
                        General.clearToastMsj();
                        if (res.d != '') {
                            $('#Groups').html(res.d);
                            initOptions();
                        }
                        else {
                            General.showToastMsj('No se cargaron los perfiles, int&eacute;ntelo nuevamente.', false, 'warning', 10000, 'bottom-right');
                        }
                    },
                    error: function errorcall(res) {
                        General.showToastMsj('No se cargaron los perfiles, int&eacute;ntelo nuevamente.', false, 'warning', 10000, 'bottom-right');
                    }
                }).done(function () {
                    initDelete();
                });
            }

            function initOptions() {
                $("#ulUsuarios li").draggable({
                    appendTo: "body",
                    helper: "clone",
                    start: function (e, ui) {
                        $(ui.helper).addClass("ui-draggable-helper");
                        $(ui.helper).css({ 'background-color': $(this).css('background-color'), 'color': $(this).css('color'), 'width': $(this).width(), 'height': $(this).height(), 'list-style': 'none', 'padding': $(this).css('padding') });
                    }
                });

                $("#Groups div").droppable({
                    activeClass: "a",
                    hoverClass: "h",
                    accept: ":not(.ui-sortable-helper)",
                    drop: function (event, ui) {
                        var $divLista = $(this);
                        var $ulLista = $divLista.find('ul');
                        var IdUser = ui.draggable.attr("value");
                        var IdGpo = $divLista.attr('dir');

                        if (!$ulLista.length) {
                            $("<ul></ul>").appendTo($divLista);
                            $ulLista = $divLista.find('ul');
                        }

                        if ($ulLista.text().indexOf(ui.draggable.text()) == -1) {
                            AddRelaciones(IdGpo, 'Miembros', 'Add', ui, $ulLista);
                        } else {
                            General.showToastMsj('Ya existe el usuario ' + ui.draggable.text() + ' en el perfil ' + $divLista.attr('id'), false, 'warning', 10000, 'bottom-right');
                        }
                    }
                });
            }

            function initDelete() {
                $(".btn_del_relgpo").off();
                $(".btn_del_relgpo").on('click', function (e) {
                    var $liPadre = $($(this).parents().get(0));
                    var $ulLista = $($liPadre.parents().get(0));
                    DelRelaciones($(this).attr('rel'), "Miembros", "Del", $liPadre, $ulLista);
                });
            }

            function AddRelaciones(Grupo, Opcion, Tipo, ui, $ulLista) {

                var parametros = { iIdGrupo: Grupo, sOpcion: Opcion, sTipo: Tipo, sIdUsuario: ui.draggable.val(), sLoginUser: ui.draggable.text() };

                sWebService = '../DllClienteRegionalWeb/wsGeneral.aspx/ManageRel';

                $.ajax({
                    url: sWebService,
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(parametros),
                    success: function fnSuccess(res) {
                        if (res.d == '') {
                            if (Tipo == 'Add') {

                                $("<li></li>").html(ui.draggable.text() + ' <a class="btn_del_relgpo" rel="' + Grupo + '" rev="' + ui.draggable.text() + '"><i class="icon-trash"></i></a>').appendTo($ulLista);

                                initDelete();
                            }
                        } else {
                            General.showToastMsj('No se agrego el usuario al perfil, int&eacute;ntelo nuevamente.', false, 'warning', 10000, 'bottom-right');
                        }
                    },
                    error: function errorcall(res) {
                        General.showToastMsj('No se agrego el usuario al perfil, int&eacute;ntelo nuevamente.', false, 'warning', 10000, 'bottom-right');
                    }
                }).done(function () {
                    oAjax = '';
                });
            }

            function DelRelaciones(Grupo, Opcion, Tipo, ui, $ulLista) {

                var IdUsuario = ui.find('a').attr('rev');
                var parametros = { iIdGrupo: Grupo, sOpcion: Opcion, sTipo: Tipo, sIdUsuario: IdUsuario, sLoginUser: '' };
                sWebService = '../DllClienteRegionalWeb/wsGeneral.aspx/ManageRel';
                $.ajax({
                    url: sWebService,
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(parametros),
                    success: function fnSuccess(res) {
                        if (res.d == '') {

                            var $liPadre = ui;

                            $liPadre.remove();

                            if ($ulLista.find('li').length == 0) { $ulLista.remove(); }

                        } else {
                            General.showToastMsj(res.d, false, 'warning', 10000, 'bottom-right');
                        }
                    },
                    error: function errorcall(res) {
                        General.showToastMsj('No se eliminó usuario del perfil, int&eacute;ntelo nuevamente.', false, 'warning', 10000, 'bottom-right');
                    }
                }).done(function () {});
            }

            initPermisos();
        });
    </script>
</head>
<body>
    <form id="frmGruposDeUsuario" runat="server">
    <div id="containerPerfiles" class="container elementCenter">
        <div id="Info" class="Controls cont">
            <div id="Permisos" class="Groups">
                <span class="labelGpoleft">Permisos</span>
                <div id="Combos" class="Combo m-input-prepend">
                    <label class="m-wrap">
                        <span class="add-on">Perfiles :</span>
                        <select id="cboPerfiles" class="m-wrap">
                            <option value="A" selected="selected">Administrador Estatal</option>
                            <option value="G">Consultor global</option>
                            <option value="J">Consultor de jurisdicción</option>
                            <option value="U">Consultor por tipo de unidad</option>
                            <%--<option value="U">Consultor de CAISES/CESSAS</option>--%>
                        </select>
                    </label>
                    <div class="clear"></div>
                </div>
            </div>
            <div id="Groups" class="Groups" runat="server">
                <span class="tille">Perfil</span>
            </div>
            <div id="Users" class="Groups" runat="server">
                <span class="labelGpoleft">Usuarios</span>
            </div>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>
