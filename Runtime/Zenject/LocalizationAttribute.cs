using System;

namespace Playdarium.Localization.Runtime.Zenject
{
	[AttributeUsage(AttributeTargets.Field)]
	public class LocalizationAttribute : Attribute
	{
		public readonly bool Dynamic;
		public readonly string Key;

		public LocalizationAttribute(string key, bool dynamic = false)
		{
			Key = key;
			Dynamic = dynamic;
		}
	}
}