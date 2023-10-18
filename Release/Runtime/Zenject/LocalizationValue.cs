using System.Collections.Generic;
using UnityEngine;

namespace Package.Localization.Runtime.Zenject
{
	public class LocalizationValue
	{
		public const char ARGS_BLOCK = '#';
		public const char ARG_SEPARATOR = '|';

		private int _startIndex = -1;
		private int _lenght;
		private bool _isParent;
		private List<LocalizationValue> _values;

		public int StartIndex => _startIndex;

		public int Lenght => _lenght;

		public bool IsParent => _isParent;

		public IEnumerable<LocalizationValue> Values => _values;

		private LocalizationValue()
		{
		}

		public static LocalizationValue Build(string pattern, int startIndex, int endIndex)
		{
			var localizationValue = new LocalizationValue();
			localizationValue.ReadFromPattern(pattern, startIndex, endIndex);
			return localizationValue;
		}

		private int ReadFromPattern(string pattern, int startIndex, int endIndex)
		{
			_startIndex = startIndex;

			for (var i = startIndex; i < endIndex; i++)
			{
				var c = pattern[i];
				switch (c)
				{
					case ARGS_BLOCK when _isParent:
						_lenght = i + 1 - _startIndex;
						return _lenght;
					case ARGS_BLOCK:
					{
						if (!_isParent && i == _startIndex)
						{
							_isParent = true;
							_values = new List<LocalizationValue>();
						}

						if (!_isParent)
						{
							_lenght = i - _startIndex;
							return _lenght;
						}

						var localizationValue = new LocalizationValue();
						var readChars = localizationValue.ReadFromPattern(pattern, i + 1, endIndex - 1);
						_values.Add(localizationValue);
						i += readChars;
						break;
					}
					case ARG_SEPARATOR when !_isParent:
						_lenght = i - _startIndex;
						return _lenght;
					case ARG_SEPARATOR:
					{
						var localizationValue = new LocalizationValue();
						var readChars = localizationValue.ReadFromPattern(pattern, i + 1, endIndex - 1);
						_values.Add(localizationValue);
						i += readChars;
						break;
					}
				}
			}

			_lenght = endIndex - startIndex;
			return _lenght;
		}

		public string Localize(
			string key,
			string pattern,
			SystemLanguage language,
			ILocalizationProvider localizationProvider
		)
			=> InternalLocalize(key, pattern, language, localizationProvider, this, 0);

		private string InternalLocalize(
			string key,
			string pattern,
			SystemLanguage language,
			ILocalizationProvider localizationProvider,
			LocalizationValue value,
			int offset
		)
		{
			var values = value._values;
			var localized = new object[values.Count - offset];
			for (var i = offset; i < values.Count; i++)
			{
				var localizationValue = values[i];
				localized[i - offset] = localizationValue._isParent
					? LocalizeChild(localizationValue, pattern, language, localizationProvider)
					: LocalizeInner(localizationValue, pattern, language, localizationProvider);
			}

			var localizationText = localizationProvider.Find(language, key);
			return string.Format(localizationText, localized);
		}

		private string LocalizeChild(
			LocalizationValue value,
			string pattern,
			SystemLanguage language,
			ILocalizationProvider localizationProvider
		)
		{
			var keyValue = value._values[0];
			var keyString = keyValue._isParent
				? LocalizeChild(keyValue, pattern, language, localizationProvider)
				: pattern.Substring(keyValue._startIndex, keyValue._lenght);

			return InternalLocalize(keyString, pattern, language, localizationProvider, value, 1);
		}

		private string LocalizeInner(
			LocalizationValue value,
			string pattern,
			SystemLanguage language,
			ILocalizationProvider localizationProvider
		)
		{
			var substring = pattern.Substring(value._startIndex, value._lenght);
			return localizationProvider.Find(language, substring, out var translate) ? translate : substring;
		}
	}
}