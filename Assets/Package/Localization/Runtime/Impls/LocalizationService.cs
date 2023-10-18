﻿using System;
using Package.Localization.Runtime.Models;
using UniRx;
using UnityEngine;

namespace Package.Localization.Runtime.Impls
{
	public class LocalizationService : ILocalizationService
	{
		private readonly LanguageGroups _groups;

		public SystemLanguage[] AvailableLanguages { get; }

		private readonly ReactiveProperty<SystemLanguage> _currentLanguageProperty;
		public IReactiveProperty<SystemLanguage> CurrentLanguage => _currentLanguageProperty;

		public LocalizationService(
			LocalizationData data,
			LanguageGroups groups
		)
		{
			_groups = groups;
			AvailableLanguages = data.Languages;
			_currentLanguageProperty = new ReactiveProperty<SystemLanguage>(GetDefaultLanguage());
		}

		public SystemLanguage GetDefaultLanguage()
			=> _groups.FindSubstitution(AvailableLanguages, Application.systemLanguage);

		public void PreviousLanguage()
		{
			var index = Array.IndexOf(AvailableLanguages, CurrentLanguage) - 1;
			if (index < 0)
				index += AvailableLanguages.Length;

			_currentLanguageProperty.Value = AvailableLanguages[index];
		}

		public void NextLanguage()
		{
			var index = Array.IndexOf(AvailableLanguages, CurrentLanguage) + 1;
			_currentLanguageProperty.Value = AvailableLanguages[index % AvailableLanguages.Length];
		}

		public void SetLanguage(int language)
		{
			_currentLanguageProperty.Value = AvailableLanguages[language];
		}

		public void SetLanguage(SystemLanguage language)
		{
			var index = Array.IndexOf(AvailableLanguages, language);
			if (index < 0)
				index += AvailableLanguages.Length;

			_currentLanguageProperty.Value = AvailableLanguages[index];
		}
	}
}