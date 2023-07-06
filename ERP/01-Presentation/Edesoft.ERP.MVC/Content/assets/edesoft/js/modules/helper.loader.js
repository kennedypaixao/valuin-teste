"use strict";

const helperLoader = function () {
    return {
        script: async (url) => {
            await $.import_js(url)
        },
        style: (url) => {
            return new Promise((resolve, reject) => {
                try {
                    const $style = $("<link>", {
                        rel: "stylesheet",
                        type: "text/css",
                        href: `${url}?${NewGuid()}`
                    })[0];

                    $style.onload = function () {
                        resolve({ status: true });
                    }
                    document.getElementsByTagName("head")[0].appendChild($style)

                    $style.addEventListener("error", (ev) => {
                        reject({
                            status: false,
                            message: `Failed to load the script ${url}`
                        });
                    });

                } catch (error) {
                    reject(error);
                }
            });
        },
        view: async (parent, url) => {
            var view = await $.get(url);
            var $view = document.createElement("div");
            $($view).html(view);
            parent.append($view);
        }
    }
}();