using Edesoft.ERP.Shared.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.DTO.Backoffice.Usuario
{
	public class UsuarioBackofficeDto : EdesoftDto
	{
		public Guid Id { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Senha { get; set; }
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
							check.errors.Add("Nome do usuário não pode ser em branco");

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

						if (string.IsNullOrEmpty(this.Email))
							check.errors.Add("Email do usuário deve ser informado");

						check.isValid = check.errors.Count == 0;

						return check;
					}
				},

				new EdesoftRoleValidator()
				{
					RoleName = "Senha",
					Validate = () =>
					{
						var check = new CustomValidatorResult();
						var resultCheckPassword = new List<string>();
						if (!this.Senha.ValidatePassword(out resultCheckPassword))
							check.errors.AddRange(resultCheckPassword);

						check.isValid = check.errors.Count == 0;
						return check;
					}
				}
			};

			return validators;
		}
	}
}
