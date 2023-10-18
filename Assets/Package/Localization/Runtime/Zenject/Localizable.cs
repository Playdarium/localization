using System.Text;

namespace Package.Localization.Runtime.Zenject
{
	public static class Localizable
	{
		private static readonly StringBuilder StringBuilder = new();

		public static string ToArgs(string arg0)
		{
			StringBuilder.Length = 0;
			return StringBuilder
				.Append(LocalizationValue.ARGS_BLOCK)
				.Append(arg0)
				.Append(LocalizationValue.ARGS_BLOCK)
				.ToString();
		}

		public static string ToArgs(string arg0, string arg1)
		{
			StringBuilder.Length = 0;
			return StringBuilder
				.Append(LocalizationValue.ARGS_BLOCK)
				.Append(arg0)
				.Append(LocalizationValue.ARG_SEPARATOR)
				.Append(arg1)
				.Append(LocalizationValue.ARGS_BLOCK)
				.ToString();
		}

		public static string ToArgs(string arg0, string arg1, string arg2)
		{
			StringBuilder.Length = 0;
			return StringBuilder
				.Append(LocalizationValue.ARGS_BLOCK)
				.Append(arg0)
				.Append(LocalizationValue.ARG_SEPARATOR)
				.Append(arg1)
				.Append(LocalizationValue.ARG_SEPARATOR)
				.Append(arg2)
				.Append(LocalizationValue.ARGS_BLOCK)
				.ToString();
		}

		public static string ToArgs(string arg0, string arg1, string arg2, string arg3)
		{
			StringBuilder.Length = 0;
			return StringBuilder
				.Append(LocalizationValue.ARGS_BLOCK)
				.Append(arg0)
				.Append(LocalizationValue.ARG_SEPARATOR)
				.Append(arg1)
				.Append(LocalizationValue.ARG_SEPARATOR)
				.Append(arg2)
				.Append(LocalizationValue.ARG_SEPARATOR)
				.Append(arg3)
				.Append(LocalizationValue.ARGS_BLOCK)
				.ToString();
		}

		public static string ToArgs(params string[] strings)
		{
			StringBuilder.Length = 0;
			StringBuilder.Append(LocalizationValue.ARGS_BLOCK);
			for (var i = 0; i < strings.Length; i++)
			{
				var s = strings[i];
				StringBuilder.Append(s);
				if (i + 1 < strings.Length)
					StringBuilder.Append(LocalizationValue.ARG_SEPARATOR);
			}

			return StringBuilder.Append(LocalizationValue.ARGS_BLOCK).ToString();
		}

		public static bool IsArgs(string s)
			=> !string.IsNullOrEmpty(s)
			   && s[0] == LocalizationValue.ARGS_BLOCK
			   && s[^1] == LocalizationValue.ARGS_BLOCK;
	}
}