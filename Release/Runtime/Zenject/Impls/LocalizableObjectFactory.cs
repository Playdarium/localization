using System.Collections.Generic;

namespace Playdarium.Localization.Runtime.Zenject.Impls
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

		public ILocalizableObject Create(object localizable, ILocalizationSettings settings)
		{
			var accessStrategy = GetAccessStrategy(localizable);
			if (accessStrategy == null)
				return null;

			return settings.Dynamic
				? _dynamicLocalizableObjectFactory.Create(localizable, settings, accessStrategy)
				: _staticLocalizableObjectFactory.Create(localizable, settings, accessStrategy);
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