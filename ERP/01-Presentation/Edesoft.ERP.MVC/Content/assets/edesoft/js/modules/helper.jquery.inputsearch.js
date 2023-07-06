"use strict";
(function ($, window, document, undefined) {
    var InputSearch = function (elem, options) {
        this.elem = elem;
        this.$elem = $(elem);
        this.$originalContainer = $(this.$elem[0].parentElement);
        this.options = options;
        this.currentId = null;
    };

    InputSearch.prototype = {
        defaults: {
            search: {
                shortcut: "F2",
                caption: "F2",
                icon: "fa-magnifying-glass",
                idField: "",
                displayField: "",
                model: [],
                notFoundMessage: "Não encontrado",
                endpoint: {
                    controller: "",
                    action: "",
                    params: {},
                    setParams: () => {

                    },
                    setParam: (paramName, value) => {
                        var param = params.find(item => { return item[0] == paramName });
                        if (!param) {
                            param = {};
                            params.push(param);
                        }
                        param[paramName] = value;

                    }
                },
                modal: {
                    title: "",
                    grid: {
                        columns: []
                    }
                }
            }
        },
        config: {},

        init: function () {
            if (!this.$elem.is("input")) {
                console.log("InputSearch - Elemento deve ser um INPUT");
                return;
            }
            this.config = $.extend({}, this.defaults, this.options);
            this.config.search = $.extend({}, this.defaults.search, this.config.search);

            this._fillModel();
            this._build();

            return this;
        },

        ////////////////////////////////////////////
        ////////////// Private methods//////////////
        ////////////////////////////////////////////
        _build: function () {

            const $container = this.$originalContainer

            this.$group = $("<div></div>");
            this.$group.addClass("input-group")
            this.$group.appendTo($container);

            this.$elem.appendTo(this.$group);
            const item = this.config.search;
            this.$button = $("<button>").attr("type", "button");
            this.$button.addClass("btn").addClass("btn-primary");
            this.$button.html(`<i class="fa-duotone ${item.icon}"></i>&nbsp;${item.shortcut}<b class="btn-text">`);
            this.$button.appendTo(this.$group);

            this.$elem.keydown((e) => {
                //const button = buttons.find(button => button.shortcut === e.key);

                //if (!button) return;

                return;

                //const { modal, uid } = button;

                //e.preventDefault();
                //$('#' + inputId).off('change');
                //OpenModal(modal.id, inputId, uid);
                //$('#' + inputId).change(changeHandler);
            });

            this.$elem.change(this._changeHandler);
        },

        _fillModel: async function () {
            const item = this.config.search;
            item.model = [];
            if (item.setParams) {
                item.setParam();
            }
            const data = await $.ajax({
                type: 'GET',
                timeout: 50000,
                async: true,
                data: item.params,
                url: `/${item.endpoint.controller}/${item.endpoint.action}`,
            });

            if (data.status_code == 200)
                item.model = data.data;

            this.$elem.autocomplete({
                source: data.Data?.map(obj => {
                    const value = obj[item.displayField];
                    return value
                }),
                appendTo: this.$originalContainer
            });

        },

        _changeHandler:async function (event) {
            const value = this.value;
            const input = $(this).InputSearch();
            var obj = input.config.search.model.find(item => { return item[input.config.search.displayField] == value });
            if (!obj) {
                console.log(input);
                await helperEdesoft.Messages.Dialog.Warning("Desculpe", input.config.search.notFoundMessage);
                input.$elem.focus();
                return;
            }

            input.currentId = obj[input.config.search.idField];
        },

        /////////////////////////////////
        ////////////// API //////////////
        /////////////////////////////////

        // API invocation
        api: function (args) {
            if (args.length > 0) {
                if (InputSearch.prototype['api_' + args[0]]) {
                    return this['api_' + args[0]](args.slice(1));
                } else {
                    console.log('InputSearch - unknown command!');
                }
            }
            else {
                return this;
            }
        },

        // API functions

        api_destroy: function () {
            this.$elem.removeData("plugin_InputSearch");
            this.$elem.appendTo(this.$originalContainer);
            this.$group.remove();
            this.$button.remove();
            this.$elem.autocomplete("destroy");
            return this.$elem;
        },

        api_get: function () {
            return this.currentId;
        },

        api_set: async function (args) {
            if (args.length != 1) {
                console.log('InputSearch - unknown number of arguments.');
                return;
            }

            this.currentId = args[0];
            const idField = this.config.search.idField;
            var obj = this.config.search.model.find(item => { return item[idField] == args[0] });
            if (!obj) {
                await helperEdesoft.Messages.Dialog.Warning("Desculpe", this.config.search.notFoundMessage);
                this.$elem.focus();
                return;
            }

            this.$elem.val(obj[this.config.search.displayField]);

            return this.$elem;
        },
        api_config: function () {
            return this.config;
        },
        api_refreshModel: function (args) {
            if (args.length != 1) {
                console.log('InputSearch - unknown number of arguments.');
                return;
            }
            this._fillModel();
            return this.$elem;
        }
    };

    $.fn.InputSearch = function (options) {
        var inputArguments = arguments;

        if ($(this).length == 1) {
            var instance = $(this).data("plugin_InputSearch");
            if (!instance) {
                $(this).data("plugin_InputSearch", new InputSearch(this, options).init());
                return this;
            } else {
                return instance.api(Array.prototype.slice.call(inputArguments));
            }
        } else {
            return this.each(function () {
                var instance = $(this).data("plugin_InputSearch");
                if (!instance) {
                    $(this).data("plugin_InputSearch", new InputSearch(this, options).init());
                } else {
                    instance.api(Array.prototype.slice.call(inputArguments));
                }
            });
        }
    };


})(jQuery, window, document);
