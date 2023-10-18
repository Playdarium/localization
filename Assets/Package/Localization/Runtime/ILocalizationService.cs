using UniRx;
using UnityEngine;

namespace Package.Localization.Runtime
{
	public interface ILocalizationService
	{
		SystemLanguage[] AvailableLanguages { get; }

		IReactiveProperty<SystemLanguage> CurrentLanguage { get; }

		SystemLanguage GetDefaultLanguage();

		void SetLanguage(int language);

		void NextLanguage();

		void PreviousLanguage();
	}
}