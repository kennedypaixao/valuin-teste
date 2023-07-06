"use strict";

class helperCrudGrid {
    constructor(options) {
        return (async () => {
            this.normalizeOptions(options);

            this.$context = $(this.options.wrapperContext);

            await this.initControls();

            this.refreshModel(this.createGrid);

            return this;

        })();
    }
    initControls = async function () {

        if (!this.options.endpoint.virtualMode) {
            await helperEdesoft.Loader.view(this.$context, helperEdesoft.Constants.crud.defaultGridUrl);
            App.initComponent();
        }

        $(helperEdesoft.Constants.crud.btnInserir, this.$context).click(async () => {
            await this.insert();
        });

    }
    _save = async function (action, object) {
        if (!this.options.endpoint.virtualMode) {
            const urlAction = typeof action === "string" ? action : action.action;

            const ajaxParams = {
                type: 'POST',
                url: this._ajaxAction(urlAction),
                data: object
            };

            if (typeof action === "object" && !(action.processData ?? true)) {
                ajaxParams.contentType = false;
                ajaxParams.processData = false;
            }

            const result = await $.ajax(ajaxParams);

            if (result.status_code != 200) {
                helperEdesoft.Messages.Dialog.Warning(helperEdesoft.Constants.messages.titleWarning, result.message);
                return false;
            }
        }
        else {
            action(object);
        }

        this.refreshModel(this.refreshGrid);
        return true;
    }
    _delete = async function (key) {
        if (!this.options.endpoint.virtualMode) {
            const result = await $.ajax({
                type: 'POST',
                url: this._ajaxAction(this.options.endpoint.actions.delete),
                data: key
            });

            if (result.status_code != 200) {
                helperEdesoft.Messages.Dialog.Warning(helperEdesoft.Constants.messages.titleWarning, result.message);
                return false;
            }
        }
        else {
            this.options.endpoint.actions.delete(key)
        }
        this.refreshModel(this.refreshGrid);
        return true;
    }
    insert = async () => {
        await this.options.modal.prepare({
            mode: helperCrudModal.mode.insert,
            onConfirm: async (object) => {

                const saved = await this._save(this.options.endpoint.actions.post, object);
                if (!saved) return;
                this.options.modal.hide();
            }
        });
        this.options.modal.show();
    }
    update = async (key) => {
        await this.options.modal.prepare({
            mode: helperCrudModal.mode.update,
            onConfirm: async (object) => {
                const saved = await this._save(this.options.endpoint.actions.put, object);
                if (!saved) return;
                this.options.modal.hide();
            },
            key
        });
        this.options.modal.show();
    }
    delete = async (key) => {
        const option = await helperEdesoft.Messages.Dialog.Question(
            helperEdesoft.Constants.messages.titleCofirmation,
            helperEdesoft.Constants.messages.deleteConfirmation
        );
        if (option.isConfirmed)
            this._delete(key);
    }
    view = async (key) => {
        await this.options.modal.prepare({
            mode: helperCrudModal.mode.view,
            key
        });
        this.options.modal.show();
    }
    _ajaxAction = function (action) {
        return helperEdesoft.Functions.getPath(`${this.options.endpoint.url}/${action}`)
    }
    normalizeOptions = function (setup) {
        const defaultOptions = {
            wrapperContext: null,
            modal: null,
            endpoint: {},
            gridOptions: null,
        }
        this.options = { ...defaultOptions, ...setup };

        const defaultModalOptions = {
            instance: null,
        }
        this.options.modal = { ...defaultModalOptions, ...this.options.modal }

        const $this = this;
        const defaultGridOptions = {
            gridElement: helperEdesoft.Constants.crud.defaultGridElement,
            programName: this.options.wrapperContext,
            keyFields: helperEdesoft.Constants.crud.grid.defaultFieldId,
            commands: {},
            options: {}
        }
        this.options.gridOptions = { ...defaultGridOptions, ...this.options.gridOptions };

        const defaultCommandOptions = {
            update: null,
            delete: null,
            view: null,
            extra: []
        }
        this.options.gridOptions.commands = { ...defaultCommandOptions, ...this.options.gridOptions.commands }

        // endpoints/actions
        const defaultEndpointOptions = {
            virtualMode: false,
            url: null,
            actions: {}
        }
        this.options.endpoint = { ...defaultEndpointOptions, ...this.options.endpoint }
        const defaultActions = {
            get: null,
            post: null,
            put: null,
            delete: null
        }
        this.options.endpoint.actions = { ...defaultActions, ...this.options.endpoint.actions }

        if (this.options.endpoint.actions.put) {
            this.options.gridOptions.commands.update = this.update;
            this.options.gridOptions.commands.view = this.view;
        }
        if (this.options.endpoint.actions.delete) {
            this.options.gridOptions.commands.delete = this.delete;
        }

        // Grid
        const defaultOptGrid = {
            dataSource: [],
            columns: [],
            toolbar: {}
        }
        this.options.gridOptions.options = { ...defaultOptGrid, ...this.options.gridOptions.options }
        this.options.gridOptions.options.toolbar.items = [];
        if (this.options.endpoint.actions.post) {
            this.options.gridOptions.commands.view = this.view;
            this.options.gridOptions.commands.insert = this.insert;
        }
        if (this.options.endpoint.url && this.options.endpoint.actions.get) {
            this.options.gridOptions.options.toolbar.items.push(
                {
                    location: 'after',
                    widget: 'dxButton',
                    options: {
                        icon: 'refresh',
                        hint: helperEdesoft.Constants.crud.grid.buttons.refresh.hint,
                        onClick: async () => {
                            await $this.refreshModel($this.refreshGrid);
                        },
                    },
                }
            )
        }

    }
    getModel = async function () {
        if (!this.options.endpoint.virtualMode && (!this.options.endpoint.url || !this.options.endpoint.actions.get))
            return;

        if (typeof this.options.endpoint.actions.get === "string") {
            const result = await $.ajax({
                type: 'GET',
                url: this._ajaxAction(this.options.endpoint.actions.get),
            });

            if (result.status_code != 200) {
                helperEdesoft.Messages.Dialog.Warning(helperEdesoft.Constants.messages.titleWarning, helperEdesoft.Constants.messages.ajaxError);
                return;
            }

            return result.data;
        }
        if (typeof this.options.endpoint.actions.get === "function") {
            return this.options.endpoint.actions.get();
        }

    }
    refreshModel = async function (afterGetModel) {
        var target = $('.' + app.panel.class, this.$context);
        if (target[0])
            helperEdesoft.Panel.startLoading(target);

        this.model = await this.getModel();
        this.options.gridOptions.options.dataSource = this.model;

        afterGetModel();
        if (target[0])
            helperEdesoft.Panel.stopLoading(target);
    }
    createGrid = async () => {
        this.$grid = $(this.options.gridOptions.gridElement, this.$context).CreateGridInstance(this.options.gridOptions);
    }
    refreshGrid = async () => {
        this.$grid.option("dataSource", this.model);
    }
    addInput = async function (setup) {
        const defaultOptions = {
            inputName: "",
            dataType: inputType.text,
            defaultValue: "",
            defaultValidation: -1,
            customValidation: null
        }

        const options = { ...defaultOptions, ...setup };

        this.inputList.push(options);
    }
};