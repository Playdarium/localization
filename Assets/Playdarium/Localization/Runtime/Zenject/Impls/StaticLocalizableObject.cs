using UnityEngine;
using Zenject;

namespace Playdarium.Localization.Runtime.Zenject.Impls
{
	public class StaticLocalizableObject : ILocalizableObject
	{
		private readonly LocalizationAttribute _attribute;
		private readonly object _localizable;
		private readonly ITextAccessStrategy _textAccessStrategy;
		private readonly ILocalizationProvider _localizationProvider;

		public StaticLocalizableObject(
			LocalizationAttribute attribute,
			object localizable,
			ITextAccessStrategy textAccessStrategy,
			ILocalizationProvider localizationProvider
		)
		{
			_attribute = attribute;
			_localizable = localizable;
			_textAccessStrategy = textAccessStrategy;
			_localizationProvider = localizationProvider;
		}

		public void Localize(SystemLanguage language)
		{
			var text = _localizationProvider.Find(language, _attribute.Key);
			_attribute.PostProcessText(ref text);
			_textAccessStrategy.SetText(text, _localizable);
		}

		public class Factory : PlaceholderFactory<LocalizationAttribute, object, ITextAccessStrategy,
			StaticLocalizableObject>
		{
		}
	}
}