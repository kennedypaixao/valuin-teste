"use strict";

const helperPanel = function () {
    return {
        startLoading: (target) => {
            if (!target[0].classList.contains(app.panel.loadingClass)) {
                var targetBody = target[0].querySelector('.' + app.panel.bodyClass);
                var spinnerHtml = document.createElement('div');
                spinnerHtml.classList.add(app.panel.loader.class);
                spinnerHtml.innerHTML = app.panel.loader.html;
                target[0].classList.add(app.panel.loadingClass);
                targetBody.appendChild(spinnerHtml);
            }
        },
        stopLoading: (target) => {
            if (target[0].classList.contains(app.panel.loadingClass)) {

                setTimeout(() => {
                    target[0].classList.remove(app.panel.loadingClass);
                    target[0].querySelector('.' + app.panel.loader.class).remove();
                    //document.querySelector('.tooltip').remove();
                }, 500);
            }
        },
    }
}();