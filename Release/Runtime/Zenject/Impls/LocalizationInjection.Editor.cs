using System.Linq;

#if UNITY_EDITOR

namespace Playdarium.Localization.Runtime.Zenject.Impls
{
	public partial class LocalizationInjection
	{
		public bool EditorHasNullReferences() => staticObjects.Any(staticObject => staticObject.Component == null);

		public int EditorClearNullReferences()
		{
			var localizedObjects = staticObjects.Where(staticObject => staticObject.Component != null).ToArray();
			var removed = staticObjects.Length - localizedObjects.Length;
			staticObjects = localizedObjects;
			return removed;
		}
	}
}
#endif