using UnityEngine;

namespace Package.Localization.Runtime.Zenject
{
	public interface ILocalizableObject
	{
		void Localize(SystemLanguage language);
	}
}