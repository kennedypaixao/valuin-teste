"use strict";

// imported old version
// please, refactory
// backlog:
//  -- text to constants
//  -- hints to constants
(function ($) {
    $.fn.CreateGridInstance = function (Options) {
        const defaultGridOptions = {
            programName: "",
            keyFields: "Id",
            commands: {

            },
            options: {}
        };
        const gridOptions = { ...defaultGridOptions, ...Options }

        const defaultCommands = {
            insert: null,
            update: null,
            delete: null,
            view: null,
            extra: []
        }
        gridOptions.commands = { ...defaultCommands, ...Options.commands }

        const defaultOptions = {
            dataSource: [],
            keyExpr: gridOptions.keyFields,
            tabindex: -1,
            paging: {
                pageSize: 10
            },
            selection: {
                mode: 'single',
                showCheckBoxesMode: 'none'
            },
            searchPanel: {
                visible: true,
                width: 300,
                placeholder: 'Procure aqui...'
            },
            filterRow: {
                visible: true,
                applyFilter: "auto"
            },
            headerFilter: {
                visible: true
            },
            pager: {
                showPageSizeSelector: true,
                allowedPageSizes: [5, 10, 20, 50, 100, 200, 500, 1000]
            },
            columnChooser: {
                enabled: true,
            },
            export: {
                enabled: true,
                fileName: Options.ProgramName,
                allowExportSelectedData: false,
                formats: ['pdf', 'xlsx'],
            },
            onEditorPreparing: function (e) {
                if (e.parentType === "filterRow" && e.dataType === "boolean") {
                    e.editorOptions.searchEnabled = false;
                }
            },
            columnAutoWidth: true,
            allowColumnResizing: true,
            showBorders: true,
            columnResizingMode: "nextColumn",
            columns: [],
            masterDetail: null,
            summary: false,
            toolbar: {
                items: []
            },
            form: null,
            groupPanel: false
        }
        gridOptions.options = { ...defaultOptions, ...gridOptions.options };
        gridOptions.options.toolbar.items.push("columnChooserButton");
        gridOptions.options.toolbar.items.push("exportButton");
        gridOptions.options.toolbar.items.push("searchPanel");

        var alterar = false;
        var excluir = false;
        var visualizar = false;

        if (typeof gridOptions.commands.insert == 'function') {
            if (!gridOptions.options.toolbar.items)
                gridOptions.options.toolbar.items = [];

            gridOptions.options.toolbar.items.push({
                location: 'before',
                widget: 'dxButton',
                options: {
                    icon: 'fa fa-plus text-white',
                    text: helperEdesoft.Constants.crud.grid.buttons.new.text,
                    hint: helperEdesoft.Constants.crud.grid.buttons.new.hint,
                    elementAttr: {
                        class: "bg-blue text-white"
                    },
                    onClick: async () => {
                        await gridOptions.commands.insert();
                    },
                },
            });
        }
        alterar = (typeof gridOptions.commands.update == 'function');
        excluir = (typeof gridOptions.commands.delete == 'function');
        visualizar = (typeof gridOptions.commands.view == 'function');

        var div = $(this);

        if (alterar || excluir || visualizar || gridOptions.commands.extra.length > 0) {
            const sizeIcon = 20;
            const sizeDelimiter = 19

            let countCommand = gridOptions.commands.extra.length;
            if (alterar) countCommand += 1;
            if (excluir) countCommand += 1;
            if (visualizar) countCommand += 1;

            gridOptions.options.columns.push({
                dataField: '#',
                caption: "Opções",
                allowFiltering: false,
                allowExporting: false,
                width: Math.max(((countCommand * sizeIcon) + ((countCommand - 1) * sizeDelimiter)), 100),
                tabindex: -1,
                fixed: true,
                alignment: 'center',
                cellTemplate: function (container, options) {
                    const keyValue = options.key;

                    let commands = []
                    const addCommand = (icon, hint, onClick) => {
                        commands.push({ icon, hint, onClick });
                    }
                    const createCommands = () => {
                        let delimiter = "";
                        commands.forEach(command => {
                            if (delimiter !== "")
                                $("<span> " + delimiter + " </span>").appendTo(container);

                            $(`<a tabindex="-1"><i class="option-grid fa-duotone ${command.icon}" rel="tooltip" title="${command.hint}"></a>`)
                                .attr('href', "javascript:;")
                                .click(async () => {
                                    await command.onClick(keyValue);
                                })
                                .appendTo(container);

                            delimiter = "|"
                        })
                    }

                    if (alterar) {
                        addCommand("fa-pen-to-square", "Alterar", async () => { gridOptions.commands.update(keyValue); })
                    }

                    if (excluir) {
                        addCommand("fa-trash", "Excluir", async () => { gridOptions.commands.delete(keyValue); })
                    }

                    if (visualizar) {
                        addCommand("fa-magnifying-glass", "Visualizar", async () => { gridOptions.commands.view(keyValue); })
                    }
                    commands = commands.concat(gridOptions.commands.extra);
                    createCommands();

                }
            });
        }
        for (var i = 0; i <= gridOptions.options.columns.length - 1; i++) {
            gridOptions.options.columns[i].tabindex = -1;
        }

        var grid = div.dxDataGrid(gridOptions.options);

        return grid.dxDataGrid("instance");

    };
}(jQuery));