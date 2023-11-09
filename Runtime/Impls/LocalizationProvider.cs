using Playdarium.Localization.Runtime.Models;
using UnityEngine;

namespace Playdarium.Localization.Runtime.Impls
{
	public class LocalizationProvider : ILocalizationProvider
	{
		private readonly LocalizationData _data;

		public LocalizationProvider(LocalizationData data)
		{
			_data = data;
		}

		public string Find(SystemLanguage language, string domainKey)
		{
			var key = _data.Find(domainKey);
			if (key != null)
				return key.Get(language);

			Debug.Log($"[{nameof(LocalizationProvider)}]  Can't get translate \"{domainKey}\"");
			return domainKey;
		}

		public bool Find(SystemLanguage language, string domainKey, out string value)
		{
			var key = _data.Find(domainKey);
			if (key == null)
			{
				value = string.Empty;
				return false;
			}

			value = key.Get(language);
			return true;
		}
	}
}