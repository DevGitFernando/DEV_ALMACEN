$.ajaxPrefilter(function (options, _, jqXHR) {
    if (options.onreadystatechange) {
        var xhrFactory = options.xhr;
        options.xhr = function () {
            var xhr = xhrFactory.apply(this, arguments);
            function handler() {
                options.onreadystatechange(xhr, jqXHR);
            }
            if (xhr.addEventListener) {
                xhr.addEventListener("readystatechange", handler, false);
            } else {
                setTimeout(function () {
                    var internal = xhr.onreadystatechange;
                    if (internal) {
                        xhr.onreadystatechange = function () {
                            handler();
                            internal.apply(this, arguments);
                        };
                    }
                }, 0);
            }
            return xhr;
        };
    }
});