$(document).ready(function () {

    $(document).on("contextmenu", function (e) {
        return false;
    });

    //if (top.location != this.location) top.location = this.location;


    //Tiempo de visualización mensajes
    //setTimeout(function () { $("#Mensaje").fadeOut(800).fadeIn(800).fadeOut(500).fadeIn(500).fadeOut(300); }, 10000);

    //keypress
    $(".username").on('keypress', function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $('#txtPassword').focus();
        }
    });

    $(".password").on('keypress', function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $('#btnLogin').click();
        }
    });

    //Animacion iconos input
    $(".username").focus(function () {
        $(".user-icon").css("left", "-48px");
    });
    $(".username").blur(function () {
        $(".user-icon").css("left", "0px");
    });

    $(".password").focus(function () {
        $(".pass-icon").css("left", "-48px");
    });
    $(".password").blur(function () {
        $(".pass-icon").css("left", "0px");
    });

    $('#btnLogin').on('click', function (e) {
        $('#Mensaje').fadeOut(500);
        if (Validar()) {
            AjaxLoad();
            var parametros = { sUser: $('#txtUsuario').val(), sPass: $('#txtPassword').val() };
            $.ajax({
                url: "EdoLogin.aspx/Autenticar",
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(parametros),
                success: Ready,
                error: Error
            });
            $('#txtUsuario').focus();
            $('#txtPassword').val('');
        }
    });

    function Validar() {
        var bRerturn = false;
        var sUsuario = $('#txtUsuario').val();
        var sPassword = $('#txtPassword').val();
        if (sUsuario == "") {
            Mensaje('No ha capturado un usuario para el sistema.');
            $('#txtUsuario').focus();
        } else if (sPassword == "") {
            Mensaje('No ha especificado el password de usuario.');
            $('#txtPassword').focus();
        } else {
            bRerturn = true;
        }
        return bRerturn;
    }

    function AjaxLoad() {

        $.fancybox('<div id="ajaxLoad"> ' +
                	    '<img id="imgUser" src="../images/user_' + Id + '.gif" width="64" height="64" alt="Procesando" align="middle" runat="server"/> ' +
                        '<h2>Iniciando sesión,<br />espere un momento.</h2> ' +
        		    '</div>',
        		    {
        		        'modal': true
        		    }
        	    );
    }

    function Ready(res) {
        if (res.d == '') {
            $(location).attr('href', '../Default.aspx');
        }
        else {
            $.fancybox.close();
            Mensaje(res.d);
        }
    }

    function Error(res) {
        $.fancybox.close();
        //var error = JSON.parse(res.responseText).Message;
        //alert('Solicitud no enviada debido a ' + error);
        //alert('No se pudo autenticar, intentelo de nuevo');

    }

    function Mensaje(text) {
        $('#Mensaje').html('<h2>' + text + '</h2>');
        $('#Mensaje').fadeIn(500);
    }

    $('#txtUsuario').focus();

});