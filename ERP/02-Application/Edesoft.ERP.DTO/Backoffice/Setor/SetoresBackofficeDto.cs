using Edesoft.ERP.Shared.Classes;
using System;
using System.Collections.Generic;

namespace Edesoft.ERP.DTO.Backoffice.Setor
{
    public class SetoresBackofficeDto : EdesoftDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
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
                            check.errors.Add("Nome do setor não pode ser em branco");

                        check.isValid = check.errors.Count == 0;
                        return check;
                    }
                }
            };

            return validators;
        }
    }
}
