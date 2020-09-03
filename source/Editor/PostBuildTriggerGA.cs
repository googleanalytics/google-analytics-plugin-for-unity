using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class PostBuildTriggerGA {

	[PostProcessBuildAttribute(1)]
	public static void OnPostProcessBuild(BuildTarget target, string path)
	{
		Debug.Log( "GAv4 Unity: Post build script starts");
		if (target == BuildTarget.iOS)
		{
			
			// Get target for Xcode project
			string projPath = PBXProject.GetPBXProjectPath(path);
			Debug.Log( "GAv4 Unity: Project path is " + projPath);

			PBXProject proj = new PBXProject();
			proj.ReadFromString(File.ReadAllText(projPath));

			string targetName = PBXProject.GetUnityTargetName();
			string projectTarget = proj.TargetGuidByName(targetName);

			// Add dependencies
			Debug.Log( "GAv4 Unity: Adding frameworks");

			proj.AddFrameworkToProject(projectTarget, "AdSupport.framework", false);
			proj.AddFrameworkToProject(projectTarget, "CoreData.framework", false);
			proj.AddFrameworkToProject(projectTarget, "SystemConfiguration.framework", false);
			proj.AddFrameworkToProject(projectTarget, "libz.dylib", false);
			proj.AddFrameworkToProject(projectTarget, "libsqlite3.tbd", false);

			File.WriteAllText(projPath, proj.WriteToString());

		}
	}
}
