namespace Playdarium.Localization.Runtime
{
	public interface ILanguageChangeListener
	{
		void OnLanguageChanged(ILocalizationProvider localizationProvider);
	}
}