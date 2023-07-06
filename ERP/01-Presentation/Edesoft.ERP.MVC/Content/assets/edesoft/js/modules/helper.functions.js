"use strict";

const helperFunctions = function () {
    return {
        getPath: (url) => {
            return `${window.location.origin}/${url}`;
        },
        carregaCombo: async (Options) => {
            // imported old version
            // please, refactory

            const defaultOptions = {
                endpoint: {
                    controller: "",
                    action: ""
                },
                parametros: null,
                comboElement: null,
                fieldValue: null,
                fieldText: null,
                insereBranco: null,
                valorBranco: null,
                descricaoBranco: null,
                concatenarCodigoDescricao: false
            }

            const options = { ...defaultOptions, ...Options };

            var { endpoint, parametros, comboElement, fieldValue, fieldText, insereBranco, valorBranco, descricaoBranco, concatenarCodigoDescricao } = options;

            comboElement.empty();
            const data = await $.ajax({
                async: true,
                type: 'GET',
                url: helperEdesoft.Functions.getPath(`${endpoint.controller}/${endpoint.action}`),
                data: parametros
            });

            if (insereBranco) {
                comboElement.append($('<option>', { value: valorBranco, text: descricaoBranco }));
            }
            if (data.status_code == 200)
                data.data.forEach(item => {
                    if (concatenarCodigoDescricao) {
                        comboElement.append($('<option>', { value: item[fieldValue], text: item[fieldValue] + " - " + item[fieldText] }));
                    }
                    else {
                        comboElement.append($('<option>', { value: item[fieldValue], text: item[fieldText] }));
                    }

                });

            return data.data;
        },
        objects: {
            defaults: (destination, origin) => {
                return _.defaults(destination, origin);
            },
            assign: (destination, origin) => {
                _.assign(destination, origin);
            },
        }
    }
}();