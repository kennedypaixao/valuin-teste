const helperInputsUtilsMask = function () {
    (function ($) {
        $.fn.maskCep = function () {
            $(this).unmask();
            $(this).mask('00000-000');
        };
        $.fn.cepVal = function (value) {
            const getNrCep = (value) => {
                return String((value.match(/\d*\d+/g)??[]).join(""))
            }
            $(this).unmask();
            $(this).val(getNrCep(value??""));
            $(this).maskCep();

        };
        $.fn.maskCPFCNPJ = function () {
            const CpfCnpjMaskBehavior = function (val) {
                return val.replace(/\D/g, '').length <= 11 ? '000.000.000-009' : '00.000.000/0000-00';
            };
            const cpfCnpjpOptions = {
                onKeyPress: function (val, e, field, options) {
                    field.mask(CpfCnpjMaskBehavior.apply({}, arguments), options);
                }
            };

            $(this).unmask();
            $(this).mask(CpfCnpjMaskBehavior, cpfCnpjpOptions);
        };
        $.fn.CPFCNPJVal = function (value) {
            const getNrCPFCNPJ = (value) => {
                return String((value.match(/\d*\d+/g)??[]).join(""))
            }
            $(this).unmask();
            $(this).val(getNrCPFCNPJ(value??""));
            $(this).maskCPFCNPJ();

        };
        $.fn.maskTelefone = function () {
             const telefoneMaskBehavior = function (val) {
                return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
            };
            const telefoneOptions = {
                onKeyPress: function (val, e, field, options) {
                    field.mask(telefoneMaskBehavior.apply({}, arguments), options);
                }
            };

            $(this).unmask();
            $(this).mask(telefoneMaskBehavior, telefoneOptions);
        };
        $.fn.TelefoneVal = function (value) {
            const getNrTelefone = (value) => {
                return String((value.match(/\d*\d+/g)??[]).join(""))
            }
            $(this).unmask();
            $(this).val(getNrTelefone(value??""));
            $(this).maskTelefone();

        };

    })(jQuery);
}();