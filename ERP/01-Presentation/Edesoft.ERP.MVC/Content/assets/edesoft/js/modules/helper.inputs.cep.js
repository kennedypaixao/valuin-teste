"use strict";
// imported old version
// please, refactory
// backlog:
//  -- text to constants
//  -- hints to constants

const helperInputCep = function () {

    return {
        init: function () {
            this.Handlers = {
                Cep: {
                    getOptions: (Options) => {
                        const defaultOptions = {
                            Context: null,
                            Inputs: {
                                IdCep: null,
                                CEP: null,
                                Logradouro: null,
                                UF: null,
                                Cidade: null,
                                Numero: null,
                                Bairro: null,
                                CodigoIBGE: null,
                            },
                            Controls: {
                                Button: null
                            },
                            Extra: {
                                FieldToFocus: null,
                                changeInputs: true
                            }
                        }

                        var options = { ...defaultOptions };
                        options.Inputs = { ...options.Inputs, ...Options.Inputs };
                        options.Controls = { ...options.Controls, ...Options.Controls };
                        options.Extra = { ...options.Extra, ...Options.Extra };

                        return options;
                    },
                    Config: (Options) => {
                        if (!Options.Inputs.NrCep) throw new Error('BuscaCEP: edtCEP não informado!');
                        if (!Options.Inputs.IdCep) throw new Error('BuscaCEP: Id não informado!');

                        const options = this.Handlers.Cep.getOptions(Options);

                        Options.Inputs.NrCep.off("change");
                        Options.Inputs.NrCep.change(() => {
                            Options.Inputs.IdCep.val(null);
                        });

                        options.Controls.Button.off('click'); // unbind
                        options.Controls.Button.click(async () => {
                            await this.Handlers.Cep.Busca(options);
                        });
                    },
                    Busca: async (Options) => {

                        if (!Options.Inputs.NrCep) throw new Error('BuscaCEP: edtCEP não informado!');
                        if (!Options.Inputs.IdCep) throw new Error('BuscaCEP: Id não informado!');

                        const nrCep = Options.Inputs.NrCep.val().replace('-', '');
                        const idCep = Options.Inputs.IdCep.val();

                        if (Options.Extra.changeInputs) {
                            if (Options.Inputs.Logradouro) Options.Inputs.Logradouro.val('');
                            if (Options.Inputs.Cidade) Options.Inputs.Cidade.val('');
                            if (Options.Inputs.UF) Options.Inputs.UF.val('');
                            if (Options.Inputs.Bairro) Options.Inputs.Bairro.val('');
                            if (Options.Inputs.CodigoIBGE) Options.Inputs.CodigoIBGE.val('');
                        }

                        const data = await $.ajax({
                            type: 'GET',
                            async: true,
                            timeout: 50000,
                            url: helperEdesoft.Functions.getPath('CEP/GetByCEP'),
                            data: { nrCep, idCep }
                        });

                        if (data.status_code !== 200) {
                            if (Options.Extra.callback)
                                Options.Extra.callback(null);

                            if (!Options.Extra.changeInputs)
                                return;

                            return helperEdesoft.Messages.Dialog.Warning("CEP",data.message);
                        }
                        const result = data.data;
                        const endereco = result;

                        if (Options.Inputs.Cidade) {
                            await helperEdesoft.Functions.carregaCombo({
                                endpoint: {
                                    controller: 'CEP',
                                    action: 'GetModelCidadeCombo'
                                },
                                parametros: null,
                                comboElement: Options.Inputs.Cidade,
                                fieldValue: 'Id',
                                fieldText: 'Nome',
                                insereBranco: true,
                                valorBranco: null,
                                descricaoBranco: null,
                                concatenarCodigoDescricao: false
                            })
                            Options.Inputs.Cidade.val(endereco?.Cidade?.Id);
                        }

                        if (Options.Inputs.UF) {
                            await helperEdesoft.Functions.carregaCombo({
                                endpoint: {
                                    controller: 'CEP',
                                    action: 'GetModelUFCombo'
                                },
                                parametros: null,
                                comboElement: Options.Inputs.UF,
                                fieldValue: 'Id',
                                fieldText: 'Sigla',
                                insereBranco: true,
                                valorBranco: null,
                                descricaoBranco: null,
                                concatenarCodigoDescricao: false
                            })
                            Options.Inputs.UF.val(endereco?.Cidade?.UF?.Id);
                        }

                        if (Options.Extra.callback)
                            Options.Extra.callback(endereco);

                        if (!Options.Extra.changeInputs)
                            return;

                        if (result.length === 0) {
                            helperEdesoft.Messages.Dialog.Warning("CEP",'CEP não localizado!');
                            return;
                        }

                        Options.Inputs.IdCep.val(endereco.Id);
                        Options.Inputs.NrCep.cepVal(endereco.NrCep);

                        if (Options.Inputs.Logradouro) Options.Inputs.Logradouro.val(endereco.Logradouro);
                        if (Options.Inputs.Cidade) Options.Inputs.Cidade.val(endereco.Cidade.Id)
                        if (Options.Inputs.UF) Options.Inputs.UF.val(endereco.Cidade.UF.Id);
                        if (Options.Inputs.Bairro) Options.Inputs.Bairro.val(endereco.Bairro)
                        if (Options.Inputs.CodigoIBGE) Options.Inputs.CodigoIBGE.val(endereco.CodigoIBGE)

                        if (Options.Extra.FieldToFocus != null)
                            Options.Extra.FieldToFocus.focus();
                    }
                },
            }

            return this;
        },
        GetOptions: function (Options) {
            return this.Handlers.Cep.getOptions(Options);
        },
        Config: function(Options) {
            this.Handlers.Cep.Config(this.Handlers.Cep.getOptions(Options));
        },
        Busca: async function(Options) {
            this.Handlers.Cep.Busca(this.Handlers.Cep.getOptions(Options));
        }
    }
}();