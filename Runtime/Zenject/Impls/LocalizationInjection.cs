using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Playdarium.Localization.Runtime.Zenject.Impls
{
	public class LocalizationInjection : MonoBehaviour
	{
		private readonly List<ILocalizableObject> _localizableObjects = new();

		[SerializeField] private Component[] components = Array.Empty<Component>();
		[SerializeField] private StaticLocalizedObject[] staticObjects;

		private ILocalizationInjector _localizationInjector;
		private ILocalizationService _localizationService;

		private bool _isInitialized;
		private IDisposable _languageChangedDisposable;

		[Inject]
		public void Construct(
			ILocalizationInjector localizationInjector,
			ILocalizationService localizationService
		)
		{
			_localizationInjector = localizationInjector;
			_localizationService = localizationService;
			Initialize();
		}

		private void Initialize()
		{
			_isInitialized = true;

			foreach (var component in components)
				_localizationInjector.Localize(component, _localizableObjects);

			foreach (var staticObject in staticObjects)
			{
				var localizableObject = _localizationInjector.Localize(staticObject.Component, staticObject);
				if (localizableObject == null)
					continue;

				_localizableObjects.Add(localizableObject);
			}

			if (_languageChangedDisposable == null)
				OnEnable();
		}

		private void OnEnable()
		{
			if (!_isInitialized)
				return;

			_languageChangedDisposable = _localizationService.CurrentLanguage.Subscribe(OnLanguageChanged);
		}

		private void OnLanguageChanged(SystemLanguage language)
		{
			foreach (var localizableObject in _localizableObjects)
				localizableObject.Localize(language);
		}

		private void OnDisable()
		{
			_languageChangedDisposable?.Dispose();
			_languageChangedDisposable = null;
		}

		private void OnDestroy()
		{
			_languageChangedDisposable?.Dispose();
		}

		[Serializable]
		private class StaticLocalizedObject : ILocalizationSettings
		{
			[SerializeField] private Component component;
			[SerializeField] private string key;

			public Component Component => component;
			public string Key => key;
			public bool Dynamic => false;

			public void PostProcessText(ref string text)
			{
			}
		}
	}
}