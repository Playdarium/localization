using UnityEngine;
using Zenject;

namespace Package.Localization.Runtime.Zenject.Impls
{
	public class LocalizationDynamicInjection : MonoBehaviour
	{
		private LazyInject<ILocalizationInjector> _localizationInjector;

		[Inject]
		public void Construct([InjectLocal] LazyInject<ILocalizationInjector> localizationInjector)
		{
			_localizationInjector = localizationInjector;
		}

		private void Start()
		{
			if (_localizationInjector == null)
			{
				UnityEngine.Debug.LogError($"[{nameof(LocalizationDynamicInjection)}] No ILocalizationInjector");
				return;
			}

			var localizable = GetComponent<ILocalizable>();
			if (localizable == null)
			{
				UnityEngine.Debug.LogError($"[{nameof(LocalizationDynamicInjection)}] No IUiView in components");
				return;
			}

			_localizationInjector.Value.Subscribe(localizable);
		}
	}
}