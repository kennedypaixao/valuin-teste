"use strict";

const helperConstants = {
    messages: {
        titleWarning: "Ops...",
        titleConfirmation: "Precisamos de sua confirmação",
        ajaxError: "Sentimos muito, mas essa opção está sendo revisada pelo nosso time técnico. Por favor, aguarde...",
        deleteConfirmation: "Deseja realmente excluir essa informação?"
    },
    crud: {
        defaultGridElement: ".grdData",
        defaultGridUrl: helperEdesoft.Functions.getPath("EdesoftCommon/DefaultGridArea"),
        grid: {
            defaultFieldId: ["Id"],
            buttons: {
                new: {
                    text: "Novo",
                    hint: "Adiciona um novo registro"
                },
                refresh: {
                     hint: "Atualiza os dados"
                }
            }
        }
    }
}