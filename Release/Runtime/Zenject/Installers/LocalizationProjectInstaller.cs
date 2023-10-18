using Package.Localization.Runtime.Impls;
using Package.Localization.Runtime.Models;
using Package.Localization.Runtime.Zenject.Impls;
using Package.Localization.Runtime.Zenject.Impls.Access;
using UnityEngine;
using Zenject;

namespace Package.Localization.Runtime.Zenject.Installers
{
	[CreateAssetMenu(menuName = "Installers/Localization/" + nameof(LocalizationProjectInstaller),
		fileName = nameof(LocalizationProjectInstaller))]
	public class LocalizationProjectInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private LocalizationData localizationData;
		[SerializeField] private LanguageGroups languageGroups;

		public override void InstallBindings()
		{
			Container.Bind<LocalizationData>().FromInstance(localizationData)
				.WhenInjectedInto(typeof(LocalizationService), typeof(LocalizationProvider));
			Container.Bind<LanguageGroups>().FromInstance(languageGroups)
				.WhenInjectedInto(typeof(LocalizationService), typeof(LocalizationProvider));

			Container.Bind<ILocalizationProvider>().To<LocalizationProvider>().AsSingle()
				.WhenInjectedInto(typeof(DynamicLocalizableObject), typeof(StaticLocalizableObject));
			Container.Bind<ILocalizationService>().To<LocalizationService>().AsSingle();

			Container.BindInterfacesTo<LocalizableObjectFactory>().AsSingle();
			BindLocalizableObjectFactory<DynamicLocalizableObject, DynamicLocalizableObject.Factory>();
			BindLocalizableObjectFactory<StaticLocalizableObject, StaticLocalizableObject.Factory>();

			Container.BindInterfacesTo<UiTextAccessStrategy>().AsCached();
		}

		private void BindLocalizableObjectFactory<TContract, TFactory>()
			where TFactory : PlaceholderFactory<LocalizationAttribute, object, ITextAccessStrategy, TContract>
			=> Container.BindFactory<LocalizationAttribute, object, ITextAccessStrategy,
				TContract, TFactory>().AsSingle();
	}
}