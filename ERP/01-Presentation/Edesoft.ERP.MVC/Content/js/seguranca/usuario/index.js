"use strict";

const usuarioEndPoints = {
    url: "usuarios",
    actions: {
        getObject: "get", // get for show modal
        get: "get",       // get data
        post: "post",     // new
        // or post: { action: "post", processData: false }
        put: "put",       // update
        // or put: { action: "post", processData: false }
        delete: "delete"  // delete

    }
}
const UsuariosIndexPage = function () {
    return {
        init: async function (setup) {
            UsuariosModalCrud.wrapperContext = setup.context;

            this.grid = await new helperEdesoft.Crud.Grid({
                wrapperContext: setup.context,
                modal: UsuariosModalCrud,
                gridOptions: {
                    programName: "Usuarios",
                    keyFields: ["Id"], // já é padrão, portanto está sendo setado apenas para fins didáticos
                    commands: {
                        extra: [
                            {
                                icon: "fa-gear",
                                hint: "Opção de exemplo",
                                onClick: () => { helperEdesoft.Messages.Dialog.Warning("Desculpe", "Essa opção ainda não está disponível") }
                            }
                        ]
                    },
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
                                caption: "E-Mail",
                            },
                            {
                                dataField: 'Ativo',
                                dataType: 'boolean'
                            }
                        ]
                    }
                },
                endpoint: usuarioEndPoints
            })
        }
    }
}();

const UsuariosModalCrud = new helperCrudModal({
    wrapperContext: "",
    urlViewModal: helperEdesoft.Functions.getPath("Usuarios/_modal"),
    modalName: ".md-crud-usuario",
    footer: {
        btnSave: ".btn-save"
    },
    endpoint: usuarioEndPoints,
    getDefaultModel: {
        Id: "",
        Nome: "",
        Email: "",
        Senha: "",
        Ativo: true
    },
    _configCep: () => {
        const options = {
            Inputs: {
                IdCep: UsuariosModalCrud.el(".IdCep"),
                NrCep: UsuariosModalCrud.el(".edt-nr-cep"),
                Logradouro: UsuariosModalCrud.el(".edt-logradouro"),
                UF: UsuariosModalCrud.el(".cmb-uf"),
                Cidade: UsuariosModalCrud.el(".cmd-cidade"),
                Numero: UsuariosModalCrud.el(".edt-numero"),
                Bairro: UsuariosModalCrud.el(".edt-bairro"),
            },
            Controls: {
                Button: UsuariosModalCrud.el(".btn-busca-cep"),
            },
            Extra: {
                FieldToFocus: null,
                changeInputs: true
            }
        }
        helperEdesoft.Inputs.Cep.Config(options);
    }, // CUSTOM
    customValidation: async (mode, crud) => {
        // método opcional para fazer validação customizada.
        // Está implementado aqui a titulo de exemplo, não é obrigatório
        return {
            isValid: mode == helperCrudModal.mode.update || crud.inputValues.Nome != "Admin",
            message: "Nome não pode ser Admin"
        }
    },
    actions: {
        prepare: (mode) => {
            // para fazer algum tratamento especial, não é obrigatório
            if (mode == helperCrudModal.mode.insert)
                console.log("insert");
            if (mode == helperCrudModal.mode.update)
                console.log("update");
            if (mode == helperCrudModal.mode.view)
                console.log("view");
            UsuariosModalCrud.el(".edt-nr-cep").maskCep();
            UsuariosModalCrud.options._configCep();
        },
        setInputValues: async (crud) => {
            crud.el(".edt-id-usuario").val(crud.model.Id);
            crud.el(".edt-nome-usuario").val(crud.model.Nome);
            crud.el(".edt-email-usuario").val(crud.model.Email);
            crud.el(".edt-senha-usuario").val(crud.model.Senha);
            crud.el(".chk-usuario-ativo").prop("checked", crud.model.Ativo);

            //crud.el(".IdCep").val(crud.model.IdCep); Exemplo
            //crud.el(".edt-cliente-cep").val(crud.model.Cep); // EXEMPLO
            crud.el(".edt-nr-cep").cepVal("19015550");
            await crud.el(".btn-busca-cep").click();
        },
        getInputValues: (crud) => {
            const obj = { ...crud.options.getDefaultModel };
            obj.Id = crud.el(".edt-id-usuario").val();
            obj.Nome = crud.el(".edt-nome-usuario").val();
            obj.Email = crud.el(".edt-email-usuario").val();
            obj.Senha = crud.el(".edt-senha-usuario").val();
            obj.Ativo = crud.el(".chk-usuario-ativo").prop("checked");

            return obj;
        },
        disableInputs: (crud) => {
            crud.el(".edt-id-usuario").disable();
            crud.el(".edt-nome-usuario").disable();
            crud.el(".edt-email-usuario").disable();
            crud.el(".edt-senha-usuario").disable();
            crud.el(".chk-usuario-ativo").disable();
        }
    }
})