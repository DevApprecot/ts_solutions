using System;
using System.Globalization;
using Foundation;

namespace Ts_Solutions.iOS
{
	public static class TranslationExtension
	{
		public static string ActiveLanguage;
		const string DefaultLanguage = "el";
		const string DefaultCulture = "el-GR";
		public static CultureInfo Culture;
		public static NSBundle LanguageBundle;

		public static void SetLanguage(string lan)
		{
			Culture = new CultureInfo(DefaultCulture);
			if (lan.Equals("en"))
			{
				LanguageBundle = NSBundle.FromPath(NSBundle.MainBundle.PathForResource(lan, "lproj"));
				ActiveLanguage = lan;
			}
			else if (lan.Equals("el"))
			{
				LanguageBundle = NSBundle.FromPath(NSBundle.MainBundle.PathForResource(lan, "lproj"));
				ActiveLanguage = lan;
			}
			else
			{
				LanguageBundle = NSBundle.FromPath(NSBundle.MainBundle.PathForResource(DefaultLanguage, "lproj"));
				ActiveLanguage = DefaultLanguage;
			}
		}
	}
}