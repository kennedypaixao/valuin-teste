using Edesoft.ERP.Shared.Classes;
using System;
using System.Collections.Generic;

namespace Edesoft.ERP.DTO.Backoffice.Setor
{
    public class SubSetoresBackofficeDto : EdesoftDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid SetorId { get; set; }
        public SetoresBackofficeDto Setor { get; set; }

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
                            check.errors.Add("Nome do subSetor não pode ser em branco");

                        check.isValid = check.errors.Count == 0;
                        return check;
                    }
                },
                new EdesoftRoleValidator()
                {
                    RoleName = "SetorId",
                    Validate = () =>
                    {
                        var check = new CustomValidatorResult();

                        if (this.SetorId == null || this.SetorId == Guid.Empty)
                            check.errors.Add("Setor é obrigatório");

                        check.isValid = check.errors.Count == 0;
                        return check;
                    }
                },
            };

            return validators;
        }
    }
}
