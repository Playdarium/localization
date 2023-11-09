using UniRx;
using UnityEngine;

namespace Playdarium.Localization.Runtime
{
	public interface ILocalizationService
	{
		SystemLanguage[] AvailableLanguages { get; }

		IReactiveProperty<SystemLanguage> CurrentLanguage { get; }

		SystemLanguage GetDefaultLanguage();

		void SetLanguage(int languageIndex);

		void NextLanguage();

		void PreviousLanguage();
	}
}