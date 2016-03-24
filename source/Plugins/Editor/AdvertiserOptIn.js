/*
  Copyright 2014 Google Inc. All rights reserved.

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

  @CustomPropertyDrawer(AdvertiserOptInAttribute)
  class AdvertiserOptInDrawer extends PropertyDrawer {

  function OnGUI(position : Rect, property : SerializedProperty, label : GUIContent) {

    // TODO: I don't like this, but guilayout was not cooperating, revisit later.
    var indent = 25;
    var row1 = position.y + 15;
    var row2 = row1 + 25;
    var row3 = row2 + 85;

    EditorGUI.LabelField(Rect(position.x, row1, 155, 25), "Advertiser Id Support");

    var style = new GUIStyle(EditorStyles.textArea);
    style.wordWrap = true;

    EditorGUI.TextArea(Rect(position.x+indent, row2, position.width - 40, 75), "If you enable this collection, ensure that you review and adhere to the Google Analytics policies for SDKs and advertising features. Click the button below to view them in your browser.", style);
    EditorGUI.LabelField(Rect(position.x+indent, row3, 155, 25), "Send IDFA/AdID");
    EditorGUI.PropertyField(Rect(position.x+125, row3, 15, 25), property, GUIContent.none);

    if(GUI.Button(Rect(position.x+150, row3 - 5, 115, 25), "View GA Policies")) {
     Application.OpenURL("https://support.google.com/analytics/answer/2700409");
    }
  }

  function GetPropertyHeight (property : SerializedProperty, label : GUIContent) {
    var height = EditorGUI.GetPropertyHeight(property, label);
    return height + 140;
  }
}
