using UnityEngine;
using UnityEditor;

public class ExportPackage
{
	[MenuItem("Assets/Export Google Analytics Package", false, 301)]
	public static void Export()
	{
		Debug.Log("Exporting ScopelyPlatform package...");
		AssetDatabase.ExportPackage(new string[] {
			//scopely platform scripts
			"Assets/ScopelyPlatform/Scripts/ScopelyPlatform.cs",
			"Assets/ScopelyPlatform/Scripts/ScopelyPlatformConfig.cs",
			"Assets/ScopelyPlatform/Scripts/ScopelyPlatformConstants.cs",
			"Assets/ScopelyPlatform/Scripts/ScopelyPlatformEnums.cs",
			"Assets/ScopelyPlatform/Scripts/IScopelyPlatformNative.cs",
			"Assets/ScopelyPlatform/Scripts/ScopelyPlatformNativeAndroid.cs",
			"Assets/ScopelyPlatform/Scripts/ScopelyPlatformNativeIos.cs",
			"Assets/ScopelyPlatform/Scripts/ABTestingServiceNativeAndroid.cs",
			"Assets/ScopelyPlatform/Scripts/ABTestingServiceNativeIos.cs",
			"Assets/ScopelyPlatform/Scripts/ABTestingServiceNativeStub.cs",
			"Assets/ScopelyPlatform/Scripts/IABTestingService.cs",
			
			//scopely platform editor
			"Assets/ScopelyPlatform/Editor/mod_pbxproj.py",
			"Assets/ScopelyPlatform/Editor/mod_pbxproj.pyc",
			"Assets/ScopelyPlatform/Editor/ScopelyPlatformConfigWindow.cs",
			"Assets/ScopelyPlatform/Editor/ScopelyPlatformEditor.cs",
			"Assets/ScopelyPlatform/Editor/ScopelyPlatformPostProcess.cs",
			"Assets/ScopelyPlatform/Editor/ScopelyPlatformPostProcess_Ios",
			
			"Assets/Editor Default Resources",
			
			//titan
			"Assets/ScopelyPlatform/Scripts/Analytics/Exceptions",
			"Assets/ScopelyPlatform/Scripts/Analytics/Models",
			"Assets/ScopelyPlatform/Scripts/Analytics/Titan.cs",
			"Assets/ScopelyPlatform/Scripts/Analytics/TitanAndroidNative.cs",
			"Assets/ScopelyPlatform/Scripts/Analytics/TitanIosNative.cs",
			
			//urban airship
			"Assets/ScopelyPlatform/Scripts/UrbanAirship",
			"Assets/ScopelyPlatform/Editor/XUPorter",
			
			//mat & adjust
			"Assets/ScopelyPlatform/Scripts/Attribution",
			
			//native info
			"Assets/ScopelyPlatform/Scripts/NativeInfo",
			
			//Helpshift
			"Assets/Helpshift",
			
			//common
			"Assets/Plugins",
			"Assets/ScopelyPlatform/Prefabs",
		}, 
		"ScopelyPlatform-Unity.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
		Debug.Log ("ScopelyPlatform.unitypackage Exported");
	}
}