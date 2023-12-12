$(document).ready(function () {
    $("#btnLogin").button();
    $(document).bind("contextmenu", function (e) {
        return false;
    });
    setTimeout(function () { $(".mensajes").fadeOut(800).fadeIn(800).fadeOut(500).fadeIn(500).fadeOut(300); }, 10000);
    //Div Modal
    //seleccionamos todos los a tags con el name=modal
    $("#EdoLogin").submit(function () {
        var sUsuario = document.getElementById("txtUsuario").value;
        var sPassword = document.getElementById("txtPassword").value;
        if (sUsuario == "" && sPassword == "") {
            alert("Escriba usuario y contraseña");
            $('#txtUsuario').focus();
        } else if (sUsuario == "") {
            alert("Escriba su usuario");
            $('#txtUsuario').focus();
        } else if (sPassword == "") {
            alert("Escriba su contraseña");
            $('#txtPassword').focus();
        } else if (sUsuario != "" && sPassword != "") {
            var id = document.getElementById('dialog');
            //Enviamos el height y width del screen
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();

            //Enviamos el  heigth y width del div mask para cubrir la ventana
            $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

            //Efecto de transicción
            //$('#mask').fadeIn(1000); //Efecto de 
            $('#mask').fadeTo("fast", 0.3);

            //Enviar el height y width de la ventana
            var winH = $(window).height();
            var winW = $(window).width();

            //Establecemos el popup window en centro
            $(id).css('top', winH / 2 - $(id).height() / 2);
            $(id).css('left', winW / 2 - $(id).width() / 2);

            //Efecto de transicción
            $(id).fadeIn(2000);

            return true;
        }
        return false;
    });

    //if close button is clicked
    $('.window .close').click(function (e) {
        //Cancelamos el link
        e.preventDefault();

        $('#mask').hide();
        $('.window').hide();
    });

    $(window).resize(function () {

        var box = $('#boxes .window');

        //Obtenemos  el height y width del screen
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();

        //Enviar height y width al div mask para cubrir la ventana
        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

        //Obtenemos el height y width de la ventana
        var winH = $(window).height();
        var winW = $(window).width();

        //Posicionar el popup en el centro
        box.css('top', winH / 2 - box.height() / 2);
        box.css('left', winW / 2 - box.width() / 2);
    });
    //
});