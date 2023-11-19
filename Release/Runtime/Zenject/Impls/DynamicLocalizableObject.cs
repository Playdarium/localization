using UnityEngine;
using Zenject;

namespace Playdarium.Localization.Runtime.Zenject.Impls
{
	public class DynamicLocalizableObject : ILocalizableObject
	{
		private readonly object _localizable;
		private readonly ILocalizationSettings _settings;
		private readonly ITextAccessStrategy _textAccessStrategy;
		private readonly ILocalizationProvider _localizationProvider;

		private SystemLanguage _language;
		private string _previousLocalizationPattern;


		public DynamicLocalizableObject(
			object localizable,
			ILocalizationSettings settings,
			ITextAccessStrategy textAccessStrategy,
			ILocalizationProvider localizationProvider
		)
		{
			_settings = settings;
			_localizable = localizable;
			_textAccessStrategy = textAccessStrategy;
			_localizationProvider = localizationProvider;

			Initialize();
		}

		private void Initialize()
		{
			_textAccessStrategy.SubscribeOnChange(_localizable, () => Localize(_language));
		}

		public void Localize(SystemLanguage language)
		{
			_language = language;
			var text = _textAccessStrategy.GetText(_localizable);
			if (!Localizable.IsArgs(text))
			{
				if (string.IsNullOrEmpty(_previousLocalizationPattern))
					return;

				text = _previousLocalizationPattern;
			}

			_previousLocalizationPattern = text;
			var localizationValue = LocalizationValue.Build(text, 0, text.Length);
			text = localizationValue.Localize(_settings.Key, text, language,
				_localizationProvider);
			_settings.PostProcessText(ref text);
			_textAccessStrategy.SetText(text, _localizable);
		}

		public class Factory : PlaceholderFactory<object, ILocalizationSettings, ITextAccessStrategy,
			DynamicLocalizableObject>
		{
		}
	}
}