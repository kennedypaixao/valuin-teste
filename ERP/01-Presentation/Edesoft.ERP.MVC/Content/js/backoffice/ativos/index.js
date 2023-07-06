"use strict";

const ativoBackofficeEndPoints = {
    url: "AtivosBackoffice",
    actions: {
        getObject: "get",
        get: "get",
        post: "post",
        put: "put",
        delete: "delete"
    }
}
const AtivoBackofficeIndexPage = function () {
    return {
        init: async function (setup) {
            AtivoBackofficeModalCrud.wrapperContext = setup.context;

            this.grid = await new helperEdesoft.Crud.Grid({
                wrapperContext: setup.context,
                modal: AtivoBackofficeModalCrud,
                gridOptions: {
                    programName: "AtivosBackoffice",
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
                endpoint: ativoBackofficeEndPoints
            })
        }
    }
}();

const AtivoBackofficeModalCrud = new helperCrudModal({
    wrapperContext: "",
    urlViewModal: helperEdesoft.Functions.getPath("AtivosBackoffice/_modal"),
    modalName: ".md-crud-ativo-backoffice",
    footer: {
        btnSave: ".btn-save"
    },
    endpoint: ativoBackofficeEndPoints,
    getDefaultModel: {
        Id: "",
        Nome: "",
        Email: "",
        Ativo: true
    },
    actions: {
        setInputValues: (crud) => {
            crud.el(".edt-id-ativo").val(crud.model.Id);
            crud.el(".edt-codigo-ativo").val(crud.model.Codigo);
            crud.el(".edt-nome-ativo").val(crud.model.Nome);
            crud.el(".chk-ativo-ativo").prop("checked", crud.model.Ativo);

            crud.el(".edt-logotipo-ativo").change(() => {
                const fileUpload = crud.el(".edt-logotipo-ativo").get(0);
                const files = fileUpload.files;

                const fileData = new FormData();

                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                $.ajax({
                    url: helperEdesoft.Functions.getPath("File/Upload"),
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fileData,
                    success: function (result) {
                        const imagemDir = result.data;
                        crud.el(".edt-logotipo-id-ativo").val(imagemDir);
                    }
                });
            });

            crud.el(".edt-setor-ativo").InputSearch({
                search: {
                    shortcut: "F2",
                    caption: "F2",
                    idField: "Id",
                    displayField: "Nome",
                    endpoint: {
                        controller: "SetoresBackoffice",
                        action: "Get"
                    },
                    modal: {
                        title: "Pesquisa de Setores",
                        grid: {
                            columns: [{ dataField: 'Nome', caption: 'Nome dos setores' }]
                        }
                    }
                }
            });

            crud.el(".edt-subsetor-ativo").InputSearch({
                search: {
                    shortcut: "F2",
                    caption: "F2",
                    idField: "Id",
                    displayField: "Nome",
                    params: { Id: crud.el(".edt-setor-ativo").InputSearch('get') },
                    endpoint: {
                        controller: "SubSetoresBackoffice",
                        action: `GetBySetor`
                    },
                    modal: {
                        title: "Pesquisa de SubSetores",
                        grid: {
                            columns: [{ dataField: 'Nome', caption: 'Nome dos setores' }]
                        }
                    }
                }
            });

            if (crud.model.SetorId != null && crud.model.SetorId != undefined)
                crud.el(".edt-setor-ativo").InputSearch('set', crud.model.SetorId.toLowerCase());

            //if (crud.model.SubSetorId != null && crud.model.SubSetorId != undefined)
            //    $(".edt-subsetor-ativo").InputSearch('set', crud.model.SubSetorId.toLowerCase());
        },
        getInputValues: (crud) => {
            const obj = { ...crud.options.getDefaultModel };
            obj.Id = crud.el(".edt-id-ativo").val();
            obj.codigo = crud.el(".edt-codigo-ativo").val();
            obj.Nome = crud.el(".edt-nome-ativo").val();
            obj.Logotipo = crud.el(".edt-logotipo-id-ativo").val();
            obj.SetorId = crud.el(".edt-setor-ativo").InputSearch('get');
            if (obj.SetorId == null || obj.SetorId == undefined) obj.NomeSetor = crud.el(".edt-setor-ativo").val();
            obj.SubSetorId = crud.el(".edt-subsetor-ativo").InputSearch('get');
            if (obj.SubSetorId == null || obj.SubSetorId == undefined) obj.NomeSubSetor = crud.el(".edt-subsetor-ativo").val();
            obj.Ativo = crud.el(".chk-ativo-ativo").prop("checked");

            return obj;
        },
        disableInputs: (crud) => {
            crud.el(".edt-id-ativo").disable();
            crud.el(".edt-nome-ativo").disable();
            crud.el(".edt-email-ativo").disable();
            crud.el(".chk-ativo-ativo").disable();
        }
    }
})