using UnityEngine;

namespace Playdarium.Localization.Runtime.Zenject
{
	public interface ILocalizableObject
	{
		void Localize(SystemLanguage language);
	}
}