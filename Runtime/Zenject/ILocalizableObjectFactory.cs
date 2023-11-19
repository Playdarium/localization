namespace Playdarium.Localization.Runtime.Zenject
{
	public interface ILocalizableObjectFactory
	{
		ILocalizableObject Create(object localizable, ILocalizationSettings settings);
	}
}