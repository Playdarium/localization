using System;
using System.Collections.Generic;
using System.Reflection;
using UniRx;
using UnityEngine;
using Zenject;

namespace Package.Localization.Runtime.Zenject.Impls
{
	public class LocalizationInjector : ILocalizationInjector, IInitializable, IDisposable
	{
		private const BindingFlags FIELD_BINDING = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		private readonly List<ILocalizableObject> _localizableObjects = new();
		private readonly List<ILocalizable> _localizables;

		private readonly ILocalizableObjectFactory _localizableObjectFactory;
		private readonly ILocalizationService _localizationService;
		private readonly CompositeDisposable _disposables = new();

		public LocalizationInjector(
			[InjectLocal] List<ILocalizable> localizables,
			ILocalizableObjectFactory localizableObjectFactory,
			ILocalizationService localizationService
		)
		{
			_localizableObjectFactory = localizableObjectFactory;
			_localizationService = localizationService;
			_localizables = localizables;
		}

		public void Initialize()
		{
			foreach (var localizable in _localizables)
				RegisterLocalizable(localizable, false);

			_localizationService.CurrentLanguage.Subscribe(OnLanguageChanged).AddTo(_disposables);
		}

		public void Dispose() => _disposables.Dispose();

		public void Subscribe(ILocalizable localizable) => RegisterLocalizable(localizable, true);

		private void RegisterLocalizable(ILocalizable localizable, bool notify)
		{
			RegisterLocalizableFields(localizable, notify);

			if (localizable is not Component component)
				return;

			using var _ = UnityEngine.Pool.ListPool<ILocalizable>.Get(out var buffer);
			component.GetComponentsInChildren(true, buffer);
			foreach (var localizableChild in buffer)
				RegisterLocalizableFields(localizableChild, notify);
		}

		private void RegisterLocalizableFields(ILocalizable localizable, bool notify)
		{
			var localizableFields = GetLocalizableFields(localizable);
			foreach (var localizableField in localizableFields)
			{
				var localizableObject = _localizableObjectFactory.Create(localizableField, localizable);
				if (localizableObject == null)
					continue;

				if (notify)
					localizableObject.Localize(_localizationService.CurrentLanguage.Value);

				_localizableObjects.Add(localizableObject);
			}
		}

		private void OnLanguageChanged(SystemLanguage language)
		{
			foreach (var localizableObject in _localizableObjects)
				localizableObject.Localize(language);
		}

		private static List<LocalizableField> GetLocalizableFields(ILocalizable localizable)
		{
			var list = new List<LocalizableField>();
			var type = localizable.GetType();
			do
			{
				var fields = type.GetFields(FIELD_BINDING);
				foreach (var fieldInfo in fields)
				{
					var attr = fieldInfo.GetCustomAttribute<LocalizationAttribute>();
					if (attr == null)
						continue;

					list.Add(new LocalizableField(attr, fieldInfo));
				}

				type = type.BaseType;
			} while (type != null);

			return list;
		}
	}
}