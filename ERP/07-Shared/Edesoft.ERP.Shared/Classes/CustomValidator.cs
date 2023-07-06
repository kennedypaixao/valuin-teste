using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Shared.Classes
{

	public interface IEdesoftDto
	{
		List<EdesoftRoleValidator> GetValidators();
	}

	public class CustomValidatorResult
	{
		public bool isValid { get; set; } = true;
		public List<string> errors { get; set; } = new List<string>();
	}


	public class EdesoftRoleValidator
	{
		public string RoleName { get; set; }
		public Func<CustomValidatorResult> Validate { get; set; }
	}

	public abstract class EdesoftDto : IEdesoftDto
	{
		private CustomValidatorResult Validate()
		{
			var validators = GetValidators();

			var result = new CustomValidatorResult();

			foreach(var validator in validators)
			{
				var resultField = validator.Validate();
				if (!resultField.isValid)
				{
					result.isValid = false;
					result.errors.AddRange(resultField.errors);
				}
			}

			return result;
		}
		public CustomValidatorResult ValidateRole(string roleName)
		{
			var role = GetValidators().FirstOrDefault(p => p.RoleName == roleName);
			if (role == null) throw new Exception($"Role {roleName} not exists");

			return role.Validate();
		}

		public CustomValidatorResult ValidateRole(EdesoftRoleValidator role)
		{
			return role.Validate();
		}

		public void ValidateModel()
		{
			var valid = Validate();
			if (!valid.isValid)
				throw new Exception(String.Join(Environment.NewLine, valid.errors.ToArray()));
		}

		public abstract List<EdesoftRoleValidator> GetValidators();
	}
}
