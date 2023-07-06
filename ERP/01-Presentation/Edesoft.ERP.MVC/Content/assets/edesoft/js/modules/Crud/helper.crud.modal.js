"use strict";

class helperCrudModal {

    static mode = {
        insert: 0,
        update: 1,
        view: 2
    }
    $modalContext = null
    constructor(options) {
        this.normalizeOptions(options);
    }
    get wrapperContext() {
        return this._wrapperContext;
    }
    set wrapperContext(name) {
        this.options._wrapperContext = $(name);
    }
    normalizeOptions = function (options) {
        this.options = options;
        const defaultOptions = {
            wrapperContext: null,
            urlViewModal: null,
            footer: {},
            modalName: "",
            endpoint: {},
            customValidation: null,
            actions: {},
        };
        this.options = { ...defaultOptions, ...this.options };
        this.wrapperContext = this.options.wrapperContext;

        const defaultEndpointOptions = {
            url: null,
            virtualMode: false,
            actions: {}
        };
        this.options.endpoint = { ...defaultEndpointOptions, ...this.options.endpoint };
        const defaultEndpointActions = {
            post: null,
            put: null,
            delete: null
        };
        this.options.endpoint.actions = { ...defaultEndpointActions, ...this.options.endpoint.actions };

        const defaultActions = {
            getObject: () => { alert("método getObject deve ser implementado"); },
            setInputValues: async () => { alert("método setInputValues deve ser implementado"); },
            disableInputs: () => { alert("método disableInputs deve ser implementado"); },
            getInputValues: (crud) => { alert("método getInputValues deve ser implementado"); },
            prepare: async (mode) => {  }
        };
        this.options.actions = { ...defaultActions, ...this.options.actions }

    }
    el = function (element, context) {
        return $(element, $(context??this.options.modalName));
    }
    _ajaxAction = function (action) {
        return helperEdesoft.Functions.getPath(`${this.options.endpoint.url}/${action}`)
    }
    _getData = async function (options) {
        if (!this.options.endpoint.virtualMode && (!this.options.endpoint.url || !this.options.endpoint.actions.getObject))
            return;

        if (options.mode == helperCrudModal.mode.insert) {
            this.model = this.options.getDefaultModel;
            return;
        }

        if (!this.options.endpoint.virtualMode) {
            const result = await $.ajax({
                type: 'GET',
                url: this._ajaxAction(this.options.endpoint.actions.getObject),
                data: options.key
            });

            if (result.status_code != 200) {
                helperEdesoft.Messages.Dialog.Warning(helperEdesoft.Constants.messages.titleWarning, helperEdesoft.Constants.messages.ajaxError);
                return;
            }
            this.model = result.data;
            return;
        }

        if (typeof this.options.endpoint.actions.getObject === "function")
            this.model = this.options.endpoint.actions.getObject(options.key);
        

    }
    _prepareToView = function () {
        this.el(this.options.footer.btnSave).remove();
        this.options.actions.disableInputs(this);
    }
    _save = async function (options) {
        if (options.helper.validator.validate()) {
            options.helper.inputValues = options.helper.options.actions.getInputValues(options.helper);

            if (options.helper.options.customValidation) {
                const valid = await options.helper.options.customValidation(options.options.mode, options.helper);
                if (!valid.isValid) {
                    helperEdesoft.Messages.Dialog.Warning(helperEdesoft.Constants.messages.titleWarning, valid.message);
                    return;
                }
            }
            options.options.onConfirm(options.helper.inputValues);
        }
    }
    _prepare = async function (options) {
        await this.options.actions.setInputValues(this);
        if (options.mode == helperCrudModal.mode.view) {
            this._prepareToView();
            return;
        }
        await this.options.actions.prepare(options.mode);

        const $this = this;
        this.el(this.options.footer.btnSave).click(async () => {
            $this._save({ helper: $this, options });
        });

    }
    prepare = async function (options) {
        this.modalContext = $("<div></div>");
        const $modalContext = this.modalContext
        this.options._wrapperContext.append($modalContext);
        await helperEdesoft.Loader.view($modalContext, this.options.urlViewModal);

        this.el(".modal-body").attr("data-parsley-validate", "");
        this.validator = this.el(".modal-body").parsley();
        $(this.options.modalName, $modalContext).on('hidden.bs.modal', (event) => {
            $modalContext.remove();
        })

        await this._getData(options);
        await this._prepare(options)
    }
    show = async function () {
        $(this.options.modalName, this.options._wrapperContext).modal('show');
    }
    hide = function () {
        $(this.options.modalName, this.options._wrapperContext).modal('hide');
    }
};