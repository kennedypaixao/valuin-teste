using Edesoft.ERP.DTO.Backoffice.Setor;
using Edesoft.ERP.Shared.Classes;
using System;
using System.Collections.Generic;

namespace Edesoft.ERP.DTO.Backoffice.Ativos
{
    public class AtivosBackofficeDto : EdesoftDto
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public Guid SetorId { get; set; }
        public Guid SubSetorId { get; set; }
        public string Logotipo { get; set; }
        public string NomeSetor { get; set; }
        public string NomeSubSetor { get; set; }
        public SetoresBackofficeDto Setor { get; set; }
        public SubSetoresBackofficeDto SubSetor { get; set; }

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
                            check.errors.Add("Nome do ativo não pode ser em branco");

                        check.isValid = check.errors.Count == 0;
                        return check;
                    }
                },
                new EdesoftRoleValidator()
                {
                    RoleName = "Codigo",
                    Validate = () =>
                    {
                        var check = new CustomValidatorResult();

                        if (string.IsNullOrEmpty(this.Codigo))
                            check.errors.Add("Código do ativo não pode ser em branco");

                        check.isValid = check.errors.Count == 0;
                        return check;
                    }
                },
                new EdesoftRoleValidator()
                {
                    RoleName = "Logotipo",
                    Validate = () =>
                    {
                        var check = new CustomValidatorResult();

                        if (string.IsNullOrEmpty(this.Codigo))
                            check.errors.Add("Logotipo do ativo não pode ser em branco");

                        check.isValid = check.errors.Count == 0;
                        return check;
                    }
                }
            };

            return validators;
        }
    }
}
