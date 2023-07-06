using Edesoft.ERP.Shared.Classes;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Edesoft.ERP.DTO.Backoffice.Cliente
{
    public class ClienteBackofficeDto : EdesoftDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }

        public override List<EdesoftRoleValidator> GetValidators()
        {
            var validators = new List<EdesoftRoleValidator>
            {
                new EdesoftRoleValidator()
                {
                    RoleName = "Nome",
                    Validate = () =>
                    {
                        var check = new CustomValidatorResult();

                        if (string.IsNullOrEmpty(this.Nome))
                            check.errors.Add("Nome do cliente não pode ser em branco");

                        check.isValid = check.errors.Count == 0;
                        return check;
                    }
                },

                new EdesoftRoleValidator()
                {
                    RoleName = "Email",
                    Validate = () =>
                    {
                        var check = new CustomValidatorResult();

                        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                        Match match = regex.Match(this.Email);

                        if (string.IsNullOrEmpty(this.Email))
                            check.errors.Add("Email do cliente deve ser informado");

                        if(!match.Success)
                            check.errors.Add("Email informado inválido");

                        check.isValid = check.errors.Count == 0;

                        return check;
                    }
                }
            };

            return validators;
        }
    }
}
