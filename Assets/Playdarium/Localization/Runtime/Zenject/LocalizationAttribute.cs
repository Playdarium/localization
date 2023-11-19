using System;
using Playdarium.Localization.Runtime.Models;

namespace Playdarium.Localization.Runtime.Zenject
{
	[AttributeUsage(AttributeTargets.Field)]
	public class LocalizationAttribute : Attribute, ILocalizationSettings
	{
		public string Key { get; }
		public bool Dynamic { get; }

		private readonly ECaseType _caseType;

		public LocalizationAttribute(string key, ECaseType caseType = ECaseType.None) : this(key, false, caseType)
		{
		}

		public LocalizationAttribute(string key, bool dynamic, ECaseType caseType = ECaseType.None)
		{
			Key = key;
			Dynamic = dynamic;
			_caseType = caseType;
		}


		public virtual void PostProcessText(ref string text)
		{
			switch (_caseType)
			{
				case ECaseType.None:
					break;
				case ECaseType.ToUpper:
					text = text.ToUpper();
					break;
				case ECaseType.ToLower:
					text = text.ToLower();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}