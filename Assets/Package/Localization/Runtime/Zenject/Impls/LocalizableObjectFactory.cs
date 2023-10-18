using System.Collections.Generic;

namespace Package.Localization.Runtime.Zenject.Impls
{
	public class LocalizableObjectFactory : ILocalizableObjectFactory
	{
		private readonly List<ITextAccessStrategy> _textAccessStrategies;
		private readonly StaticLocalizableObject.Factory _staticLocalizableObjectFactory;
		private readonly DynamicLocalizableObject.Factory _dynamicLocalizableObjectFactory;

		public LocalizableObjectFactory(
			List<ITextAccessStrategy> textAccessStrategies,
			StaticLocalizableObject.Factory staticLocalizableObjectFactory,
			DynamicLocalizableObject.Factory dynamicLocalizableObjectFactory
		)
		{
			_textAccessStrategies = textAccessStrategies;
			_staticLocalizableObjectFactory = staticLocalizableObjectFactory;
			_dynamicLocalizableObjectFactory = dynamicLocalizableObjectFactory;
		}

		public ILocalizableObject Create(LocalizableField localizableField, ILocalizable localizable)
		{
			var fieldValue = localizableField.FieldInfo.GetValue(localizable);
			var accessStrategy = GetAccessStrategy(fieldValue);
			if (accessStrategy == null)
				return null;

			return localizableField.Attribute.Dynamic
				? _dynamicLocalizableObjectFactory.Create(localizableField.Attribute, fieldValue,
					accessStrategy)
				: _staticLocalizableObjectFactory.Create(localizableField.Attribute, fieldValue, accessStrategy);
		}

		private ITextAccessStrategy GetAccessStrategy(object obj)
		{
			foreach (var strategy in _textAccessStrategies)
			{
				if (strategy.CanAccess(obj))
					return strategy;
			}

			return null;
		}
	}
}