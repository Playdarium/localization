using System;
using Playdarium.Localization.Runtime.Models;
using UniRx;
using UnityEngine;

namespace Playdarium.Localization.Runtime.Impls
{
	public class LocalizationService : ILocalizationService
	{
		private readonly LanguageGroups _groups;
		private readonly ILocalizationProvider _localizationProvider;

		public SystemLanguage[] AvailableLanguages { get; }

		private readonly ReactiveProperty<SystemLanguage> _currentLanguageProperty;
		public IReactiveProperty<SystemLanguage> CurrentLanguage => _currentLanguageProperty;

		public LocalizationService(
			LocalizationData data,
			LanguageGroups groups,
			ILocalizationProvider localizationProvider
		)
		{
			_groups = groups;
			_localizationProvider = localizationProvider;
			AvailableLanguages = data.Languages;
			_currentLanguageProperty = new ReactiveProperty<SystemLanguage>(GetDefaultLanguage());
		}

		public SystemLanguage GetDefaultLanguage()
			=> _groups.FindSubstitution(AvailableLanguages, Application.systemLanguage);

		public void PreviousLanguage()
		{
			var index = Array.IndexOf(AvailableLanguages, CurrentLanguage.Value) - 1;
			if (index < 0)
				index += AvailableLanguages.Length;

			_currentLanguageProperty.Value = AvailableLanguages[index];
		}

		public void NextLanguage()
		{
			var index = Array.IndexOf(AvailableLanguages, CurrentLanguage.Value) + 1;
			_currentLanguageProperty.Value = AvailableLanguages[index % AvailableLanguages.Length];
		}

		public void SetLanguage(int languageIndex)
		{
			_currentLanguageProperty.Value = AvailableLanguages[languageIndex];
		}

		public void SetLanguage(SystemLanguage language)
		{
			var index = Array.IndexOf(AvailableLanguages, language);
			if (index < 0)
				index += AvailableLanguages.Length;

			_currentLanguageProperty.Value = AvailableLanguages[index];
		}

		public string Find(string key) => _localizationProvider.Find(_currentLanguageProperty.Value, key);
	}
}