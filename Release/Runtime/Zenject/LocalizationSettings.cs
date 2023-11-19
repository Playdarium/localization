namespace Playdarium.Localization.Runtime.Zenject
{
	public interface ILocalizationSettings
	{
		string Key { get; }
		bool Dynamic { get; }

		void PostProcessText(ref string text);
	}
}