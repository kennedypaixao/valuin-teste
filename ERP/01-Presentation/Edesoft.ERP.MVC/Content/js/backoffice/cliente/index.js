"use strict";

const clienteBackofficeEndPoints = {
    url: "ClienteBackoffice",
    actions: {
        getObject: "get",
        get: "get",
        post: "post",
        put: "put",
        delete: "delete"
    }
}
const ClienteBackofficeIndexPage = function () {
    return {
        init: async function (setup) {
            ClienteBackofficeModalCrud.wrapperContext = setup.context;

            this.grid = await new helperEdesoft.Crud.Grid({
                wrapperContext: setup.context,
                modal: ClienteBackofficeModalCrud,
                gridOptions: {
                    programName: "ClientesBackoffice",
                    options: {
                        columns: [
                            {
                                dataField: 'Nome',
                                caption: "Nome",
                                sortOrder: 'asc',
                                sortIndex: 0
                            },
                            {
                                dataField: 'Email',
                                caption: "E-Mail"
                            },
                            {
                                dataField: 'Ativo',
                                dataType: 'boolean'
                            }
                        ]
                    }
                },
                endpoint: clienteBackofficeEndPoints
            })
        }
    }
}();

const ClienteBackofficeModalCrud = new helperCrudModal({
    wrapperContext: "",
    urlViewModal: helperEdesoft.Functions.getPath("ClienteBackoffice/_modal"),
    modalName: ".md-crud-cliente-backoffice",
    footer: {
        btnSave: ".btn-save"
    },
    endpoint: clienteBackofficeEndPoints,
    getDefaultModel: {
        Id: "",
        Nome: "",
        Email: "",
        Ativo: true
    },
    actions: {
        setInputValues: (crud) => {
            crud.el(".edt-id-cliente").val(crud.model.Id);
            crud.el(".edt-nome-cliente").val(crud.model.Nome);
            crud.el(".edt-email-cliente").val(crud.model.Email);
            crud.el(".chk-ativo-cliente").prop("checked", crud.model.Ativo);
        },
        getInputValues: (crud) => {
            const obj = { ...crud.options.getDefaultModel };
            obj.Id = crud.el(".edt-id-cliente").val();
            obj.Nome = crud.el(".edt-nome-cliente").val();
            obj.Email = crud.el(".edt-email-cliente").val();
            obj.Ativo = crud.el(".chk-ativo-cliente").prop("checked");

            return obj;
        },
        disableInputs: (crud) => {
            crud.el(".edt-id-cliente").disable();
            crud.el(".edt-nome-cliente").disable();
            crud.el(".edt-email-cliente").disable();
            crud.el(".chk-ativo-cliente").disable();
        }
    }
})