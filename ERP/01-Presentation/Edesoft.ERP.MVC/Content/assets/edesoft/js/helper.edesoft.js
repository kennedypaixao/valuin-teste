"use strict";

const handleSetupGlobal = function () {
    // initialization jquery helpers
    handleExtendJQuery();

    // setup app
    app.panel.toggle.reload.tooltipText = "Atualizar conteúdo";
    app.panel.toggle.collapse.tooltipText = "Recolher / Expandir";
    app.panel.toggle.expand.tooltipText = "Expandir / Compactar";

    // setup grid
    (DevExpress ?? null)?.localization.locale(navigator.language);
    document.addEventListener("theme-change", function (event) {
        var darkTheme = false;

        if (getCookie(app.themePanel.themeDarkMode.cookieName) == "dark-mode") {
            darkTheme = true;
        }
        DevExpress.ui.themes.current(`generic.${darkTheme ? "dark" : "softblue"}`);
    });
}
const helperEdesoft = function () {
    handleSetupGlobal();
    return {
        loaded: false,
        onready: function (event) {
            this.ready = event;

            if (this.loaded)
                this.ready();
        },
        init: async function () {
            // dependencies
            await $.import_js("../../content/assets/plugins/sweetalert2/sweetalert2.all.min.js");
            await $.import_js("../../content/assets/plugins/parsley/parsley.min.js");
            await $.import_js("../../content/assets/plugins/parsley/parsley.pt-br.js");

            // core
            await $.import_js("../../content/assets/edesoft/js/modules/helper.functions.js");
            await $.import_js("../../content/assets/edesoft/js/modules/helper.inputs.utils.mask.js");
            await $.import_js("../../content/assets/edesoft/js/modules/helper.inputs.cep.js");
            helperInputCep.init();
            this.Functions = helperFunctions;
            this.Inputs = {
                Cep: helperInputCep
            }

            // types
            await $.import_js("../../content/assets/edesoft/js/modules/types/helper.inputs.js");
            await $.import_js("../../content/assets/edesoft/js/modules/helper.jquery.inputsearch.js");
            await $.import_js("../../Content/assets/edesoft/js/modules/helper.grid.js");
            await $.import_js("../../Content/assets/edesoft/js/modules/types/helper.constants.js");
            this.Constants = helperConstants;

            // classes
            await $.import_js("../../Content/assets/edesoft/js/modules/helper.loader.js");
            this.Loader = helperLoader;
            await $.import_js("../../Content/assets/edesoft/js/modules/helper.messages.js");
            this.Messages = {
                Dialog: helperDialog,
                Toast: helperToast
            }

            await $.import_js("../../Content/assets/edesoft/js/modules/helper.panel.js");
            this.Panel = helperPanel;
            await $.import_js("../../Content/assets/edesoft/js/modules/crud/helper.crud.grid.js");
            await $.import_js("../../Content/assets/edesoft/js/modules/crud/helper.crud.modal.js");
            this.Crud = {
                Grid: helperCrudGrid,
                Modal: helperCrudModal
            }

            handleSetupAjax.init();

            this.loaded = true;
            if (this.ready)
                this.ready();
        },
    }

}();
helperEdesoft.init()
