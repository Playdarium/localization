using UnityEngine;
using Zenject;

namespace Playdarium.Localization.Runtime.Zenject.Impls
{
	public class StaticLocalizableObject : ILocalizableObject
	{
		private readonly object _localizable;
		private readonly ILocalizationSettings _settings;
		private readonly ITextAccessStrategy _textAccessStrategy;
		private readonly ILocalizationProvider _localizationProvider;

		public StaticLocalizableObject(
			object localizable,
			ILocalizationSettings settings,
			ITextAccessStrategy textAccessStrategy,
			ILocalizationProvider localizationProvider
		)
		{
			_localizable = localizable;
			_settings = settings;
			_textAccessStrategy = textAccessStrategy;
			_localizationProvider = localizationProvider;
		}

		public void Localize(SystemLanguage language)
		{
			var text = _localizationProvider.Find(language, _settings.Key);
			_settings.PostProcessText(ref text);
			_textAccessStrategy.SetText(text, _localizable);
		}

		public class Factory : PlaceholderFactory<object, ILocalizationSettings, ITextAccessStrategy,
			StaticLocalizableObject>
		{
		}
	}
}