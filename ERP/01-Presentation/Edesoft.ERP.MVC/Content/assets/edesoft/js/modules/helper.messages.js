"use strict";

const helperToast = function () {
    return {
        Base: Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
            customClass: {
                title: "toast-title"
            },
            didOpen: (toast) => {
                toast.addEventListener('mouseenter', Swal.stopTimer)
                toast.addEventListener('mouseleave', Swal.resumeTimer)
            }
        }),
        Success: (title, options) => {
            const _defaultOptions = { icon: 'success', title: title };
            const _options = { ..._defaultOptions, ...options };

            helperToast.Base.fire(_options);
        },
        Error: (title, options) => {
            const _defaultOptions = { icon: 'error', title: title };
            const _options = { ..._defaultOptions, ...options };

            helperToast.Base.fire(_options);
        },
        Warning: (title, options) => {
            const _defaultOptions = { icon: 'warning', title: title };
            const _options = { ..._defaultOptions, ...options };

            helperToast.Base.fire(_options);
        },
        Info: (title, options) => {
            var _defaultOptions = { icon: 'info', title: title };
            const _options = { ..._defaultOptions, ...options };

            helperToast.Base.fire(_options);
        },
        Question: (title, options) => {
            var _defaultOptions = { icon: 'question', title: title };
            const _options = { ..._defaultOptions, ...options };

            helperToast.Base.fire(_options);
        }

    }
}();

const helperDialog = function () {
    return {
        Base: async (Options) => {
            const defaultOptions = {
                title: "",
                text: "",
                icon: "warning",
                input: null,
                inputLabel: "",
                inputPlaceholder: "",
                inputValue: "",
                inputAttributes: {},
                showCancelButton: false,
                showConfirmButton: true,
                confirmButtonText: "Confirmar",
                cancelButtonText: "Cancelar",
                customClass: {
                    htmlContainer: "popup-htmlContainer"
                },
                validator: {
                    validateEmpty: true,
                    emptyMessage: "Campo não pode estar vazio."
                },
            }
            const option = { ...defaultOptions, ...Options }
            if (option.validator.validateEmpty)
                option.inputValidator = (value) => {
                    if (!value) {
                        return option.validator.emptyMessage;
                    }
                }


            return Swal.fire(option);//{ inputValue, confirmed };
        },
        Confirm: async function (title, text) {
            const defaultOption = {
                icon: "question",
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: "Sim",
                cancelButtonText: "Não",
            }

            const option = { ...defaultOption };
            option.title = title;
            option.text = text.replaceAll("\r\n","<br>");

            return helperDialog.Base(option);
        },
        Warning: async function (title, text) {
            const defaultOption = {
                icon: "warning",
                showCancelButton: false,
                showConfirmButton: true,
                confirmButtonText: "OK",
            }

            const option = { ...defaultOption };
            option.title = title;
            option.html = text.replaceAll("\r\n","<br>");

            return helperDialog.Base(option);
        },
        Question: async function (title, text) {
            const defaultOption = {
                icon: "question",
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: "Sim",
                cancelButtonText: "Não",
            }

            const option = { ...defaultOption };
            option.title = title;
            option.text = text.replaceAll("\r\n","<br>");

            return helperDialog.Base(option);
        },
    }
}();