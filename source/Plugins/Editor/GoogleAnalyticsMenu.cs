/*
  Copyright 2015 Google Inc. All rights reserved.

  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

      http://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class GoogleAnalyticsSetup : EditorWindow {
  [MenuItem("Google Analytics/Setup")]
  public static void GoogleAnalyticsSetupMenu() {
    EditorWindow.GetWindow(typeof(GoogleAnalyticsSetup));
  }

  void OnGUI() {
    GUILayout.BeginArea(new Rect(20, 20, position.width - 40, position.height - 40));
    GUILayout.Label("Google Analytics Android Setup", EditorStyles.boldLabel);
    GUILayout.TextArea("Click the button below to copy the Google Play Services lib from your Android directory into your current project.\n\nThis will delete the existing version of the library if you have already imported it.");

    GUILayout.Space(10);
    if (GUILayout.Button("Install google-play-services_lib")) {
      InstallPlayServicesLib();
    }
    GUILayout.EndArea();
  }

  private void InstallPlayServicesLib() {
    string src = EditorPrefs.GetString("AndroidSdkRoot") +
        "/extras/google/google_play_services/libproject/google-play-services_lib".Replace("//", "/").Replace("\\\\", "\\");
    string dest = "Assets/Plugins/Android/google-play-services_lib";

    if (!System.IO.Directory.Exists(src)) {
      Debug.LogError("Could not locate google-play-services_lib in: " + src);
      EditorUtility.DisplayDialog("Lib not found", "Could not locate google-play-services_lib\n\nPlease ensure you have specified the correct path to your Android SDK and have downloaded the Google Play Services library.", "OK");
      return;
    }

    System.IO.Directory.CreateDirectory("Assets/Plugins/Android");

    if (System.IO.Directory.Exists(dest)) {
      System.IO.Directory.Delete(dest, true);
    }

    FileUtil.CopyFileOrDirectory(src, dest);
    AssetDatabase.Refresh();
    EditorUtility.DisplayDialog("Congrats", "Google Play Services has been copied into your project.", "OK");
  }
}