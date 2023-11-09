using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Playdarium.Localization.Runtime.Zenject.Impls
{
	public class LocalizationInjector : ILocalizationInjector
	{
		private const BindingFlags FIELD_BINDING = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		private readonly ILocalizableObjectFactory _localizableObjectFactory;

		public LocalizationInjector(
			ILocalizableObjectFactory localizableObjectFactory
		)
		{
			_localizableObjectFactory = localizableObjectFactory;
		}

		public void Localize(Component localizable, List<ILocalizableObject> localizableObjects)
			=> RegisterLocalizableFields(localizable, localizableObjects);

		private void RegisterLocalizableFields(
			Component localizable,
			List<ILocalizableObject> localizableObjects
		)
		{
			var type = localizable.GetType();
			do
			{
				var fields = type.GetFields(FIELD_BINDING);
				foreach (var fieldInfo in fields)
				{
					var attr = fieldInfo.GetCustomAttribute<LocalizationAttribute>();
					if (attr == null)
						continue;

					var localizableField = new LocalizableField(attr, fieldInfo.GetValue(localizable));
					var localizableObject = _localizableObjectFactory.Create(localizableField, localizable);
					if (localizableObject == null)
						continue;

					localizableObjects.Add(localizableObject);
				}

				type = type.BaseType;
			} while (type != null);
		}
	}
}