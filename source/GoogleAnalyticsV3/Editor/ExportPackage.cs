using UnityEngine;
using UnityEditor;

public class ExportPackage
{
	[MenuItem("Assets/Export Google Analytics Package", false, 301)]
	public static void Export()
	{
		Debug.Log("Google Analytics Package...");
		AssetDatabase.ExportPackage(new string[] {
			//Plugins
			"Assets/Plugins",

			//GoogleAnalyticsV3 - root
			"Assets/GoogleAnalyticsV3/Field.cs",
			"Assets/GoogleAnalyticsV3/Fields.cs",
			"Assets/GoogleAnalyticsV3/GAIHandler.cs",
			"Assets/GoogleAnalyticsV3/GAv3.prefab",
			"Assets/GoogleAnalyticsV3/GoogleAnalyticsAndroidV3.cs",
			"Assets/GoogleAnalyticsV3/GoogleAnalyticsiOSV3.cs",
			"Assets/GoogleAnalyticsV3/GoogleAnalyticsMPV3.cs",
			"Assets/GoogleAnalyticsV3/GoogleAnalyticsV3.cs",

			//GoogleAnalyticsV3 - Attributes
			"Assets/GoogleAnalyticsV3/Attributes",

			//GoogleAnalyticsV3 - HitBuilders
			"Assets/GoogleAnalyticsV3/HitBuilders",

			//GoogleAnalyticsV3 - Editor
			"Assets/GoogleAnalyticsV3/Editor/PostprocessBuildPlayer",
			"Assets/GoogleAnalyticsV3/Editor/RangedTooltipDrawer.js",
			"Assets/GoogleAnalyticsV3/Editor/TooltipDrawer.cs"
		}, 
		"google-analytics-unit.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
		Debug.Log ("Google Analytics Package Exported");
	}
}