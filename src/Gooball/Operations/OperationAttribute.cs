using System;

namespace Gooball {

	[AttributeUsage(AttributeTargets.Method)]
	internal class OperationAttribute : Attribute {
		public readonly Type OptionType;

		public OperationAttribute(Type optionType) {
			OptionType = optionType;
		}
	}
}
