"use strict";

const handleExtendJQuery = function () {
    (function ($) {
        /*
         * $.import_js() helper (for JavaScript importing within JavaScript code).
         */
        var import_js_imported = [];

        $.extend(true,
            {
                import_js: async function (script) {

                    return new Promise((resolve, reject) => {
                        try {

                            var found = false;
                            for (var i = 0; i < import_js_imported.length; i++) {
                                if (import_js_imported[i] == script) {
                                    found = true;
                                    break;
                                }
                            }
                            if (found == false) {
                                const $script = document.createElement("script");
                                $script.type = "text/javascript";
                                $script.async = true;
                                $script.src = script;

                                $script.addEventListener("load", (ev) => {
                                    resolve({ status: true });
                                });

                                $script.addEventListener("error", (ev) => {
                                    reject({
                                        status: false,
                                        message: `Failed to load the script ${script}`
                                    });
                                });

                                //document.body.appendChild($script);
                                document.head.appendChild($script);
                                import_js_imported.push(script);
                            }
                            else {
                                resolve({ status: true });
                            }

                        } catch (error) {
                            reject(error);
                        }
                    });
                }
            });

        $.fn.enable = function () {
            return this.each(function () {
                $(this).removeAttr('disabled');
            });
        };
        $.fn.disable = function () {
            return this.each(function () {
                $(this).attr('disabled', true);
            });
        };

    })(jQuery);
};
const handleSetupAjax = function () {
    return {
        init: () => {
            $.ajaxSetup({
                async: true
            });

        }
    }
}();