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

		public void Localize(Component localizableView, List<ILocalizableObject> localizableObjects)
			=> RegisterLocalizableFields(localizableView, localizableObjects);

		private void RegisterLocalizableFields(
			Component localizableView,
			List<ILocalizableObject> localizableObjects
		)
		{
			var type = localizableView.GetType();
			do
			{
				var fields = type.GetFields(FIELD_BINDING);
				foreach (var fieldInfo in fields)
				{
					var attr = fieldInfo.GetCustomAttribute<LocalizationAttribute>();
					if (attr == null)
						continue;

					var localizableObject = Localize(fieldInfo.GetValue(localizableView), attr);
					if (localizableObject == null)
						continue;

					localizableObjects.Add(localizableObject);
				}

				type = type.BaseType;
			} while (type != null);
		}

		public ILocalizableObject Localize(object localizable, ILocalizationSettings settings)
			=> _localizableObjectFactory.Create(localizable, settings);
	}
}