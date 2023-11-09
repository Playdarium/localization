using UnityEngine;

namespace Playdarium.Localization.Runtime
{
	public interface ILocalizationProvider
	{
		string Find(SystemLanguage language, string domainKey);

		bool Find(SystemLanguage language, string domainKey, out string value);
	}
}