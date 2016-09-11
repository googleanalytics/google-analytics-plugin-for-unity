using Google.JarResolver;
using UnityEditor;

[InitializeOnLoad]
public static class GADependencies
{
    private static readonly string PluginName = "gaUnityPlugin";
    public static PlayServicesSupport svcSupport;

    static GADependencies()
    {

        svcSupport = PlayServicesSupport.CreateInstance(
                                             PluginName,
                                             EditorPrefs.GetString("AndroidSdkRoot"),
                                             "ProjectSettings");
        RegisterDependencies();
    }

    public static void RegisterDependencies()
    {
        svcSupport.DependOn("com.google.android.gms", "play-services-analytics", "9.4");
    }
}
