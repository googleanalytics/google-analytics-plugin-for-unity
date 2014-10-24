# Google Analytics Plugin for Unity (beta)
_Copyright (c) 2014 Google Inc. All rights reserved._

The __Google Analytics__ Plugin for Unity allows game developers to easily implement __Google Analytics__ in their Unity games on all platforms, without having to write separate implementations. Note that this is a beta and as such may contains bugs or other issues. Please report them through the Github [issue tracker](https://github.com/googleanalytics/google-analytics-plugin-for-unity/issues) or submit a pull request. The plugin comes with no guarantees.

_Unity is a trademark of Unity Technologies._ This project is not in any way endorsed or supervised by Unity Technologies.

_iOS is a trademark of Apple, Inc._

##Google Analytics V3 Plugin Installation & Set up:

####Before beginning:
 - Set up a __Google Analytics__ app property or use an existing one. Instructions can
   be found [here](https://support.google.com/analytics/answer/2587086?hl=en&ref_topic=2587085).
 - If you want to log transactions or items, make sure you have Ecommerce enabled by following the instructions [here](https://support.google.com/analytics/answer/1009612?hl=en&ref_topic=1037061).

####Set up:
1. <h4>Installation</h4>
Download __googleanalyticsv3.unitypackage__ from our [github page](https://github.com/googleanalytics/google-analytics-plugin-for-unity/raw/master/googleanalyticsV3.unitypackage) and import it into your Unity project by double clicking it .
2. <h4>Platform specific configuration</h4>
&nbsp;&nbsp;&nbsp;&nbsp;__[Android Only]__
  * If your project does not already have a file called _AndroidManifest.xml_ in _&#60;YOUR PROJECT ROOT>/Assets/Plugins/Android/_,  build the project for Android and then copy the _AndroidManifest.xml_ from the _Temp/StagingArea/_ directory (it will be under the directory you chose to build in). Paste it into _&#60;YOUR PROJECT ROOT>/Assets/Plugins/Android/_
  * Add the following permissions to the _AndroidManifest.xml_ in _&#60;YOUR PROJECT ROOT>/Assets/Plugins/Android/_ above the &#60;application> tag:
	```
	<uses-permission android:name="android.permission.INTERNET"/>
	
	<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"/>
	```
  * If you want to do campaign tracking, add the following service as well:
	```
	<service android:name="com.google.analytics.tracking.android.CampaignTrackingService" />
	
	<receiver android:name="com.google.analytics.tracking.android.CampaignTrackingReceiver" android:exported="true" >
	
	  <intent-filter>
	  
	    <action android:name="com.android.vending.INSTALL_REFERRER" />
	    
	  </intent-filter>
	  
	</receiver>
```
__[iOS Only]__
 	* When building for iOS, you will need to add the following libraries to your Xcode project:
    	* AdSupport.framework
    	* CoreData.framework
    	* SystemConfiguration.framework
    	* libz.dylib
    	* libsqlite3.dylib
 	* If you would like the libraries to be automatically added when building for iOS, copy the file _PostProcessBuildPlayer_GA_ from the _iOS Extras_ folder and put it in the _&#60;YOUR PROJECT ROOT>/Assets/Editor_ folder.
 	* Next, get the mod_pbxproj.py script available [here](https://github.com/kronenthaler/mod-pbxproj) and copy it into the Editor directory as well. The libraries will now be added automatically during the build. 
4. <h4>Configure the GAv3 prefab</h4>
Click on the prefab object called __GAv3.prefab__ which you can find in _Assets/Plugins/GoogleAnalyticsV3_ in the _Project_ view. The _Inspector_ view on the right will now display a Script component attached to the prefab with several properties underneath. 
5. Populate these fields with the correct values for your project. This will populate the object so you can use it everywhere in your project. 
 	* _Note that you can use the same property ID for all platforms._ 
 	* Sample values: <br>
	<code>Android Property ID: UA-XXXXXXX-1</code>
		<br>
	<code>iOS Property ID: UA-XXXXXXX-2</code>
		<br>
	<code>Other Property ID: UA-XXXXXXX-3</code>
		<br>
	<code>App Name: MyGame</code>
		<br>
	<code>Bundle ID: com.example.games</code>
		<br>
	<code>App Version: 1.0</code>
	<br>
	<code>Dispatch Period: 5</code>
	<br>
	<code>Sample Frequency: 100</code>
	<br>
	<code>Debug Mode: VERBOSE</code>
	<br>
	<code>Anonymize IP: false</code>
	<br>
	<code>Dry Run: false</code>
	<br>
5. Drag the populated GAv3 from the _Project_ view up into the _Object Hierarchy_.

6. Identify a _GameObject_ you wish to track (for example, a Player object) and click it in the _Object Hierarchy_ view. Create a new script using the _Add Component_ button in the _Inspector_ view if you do not already have a script attached to the object that you want to track.

7. <h4>Store tracker in a public variable</h4>
Create a public variable in the class to hold the GoogleAnalyticsV3 object by adding a public variable like this:<br>
  <code>public GoogleAnalyticsV3 googleAnalytics;</code>

9. Save the script and return to your Unity window.

10. In the _Inspector_ view for that _GameObject_, you should now see the script you created and a field to populate called _Google Analytics_ (or the variable name you chose in Step 6). Drag the _GAv3_ object from the _Object Hierarchy_ view onto the field. 
	* If you do not see the field to add the _GAv3_ prefab, check to make sure there are no compile errors in your script. The _Inspector_ will not update if there are existing errors. 

11. <h4>Ready to track!</h4>
You are now ready to start tracking that _GameObject_. Repeat the process for other objects you wish to track using the same prefab. See the API reference below to learn what kind of hits you can send to __Google Analytics__.


##Check out these additional resources:
  - Google Analyics Plugin for Unity developer guide: Coming soon!
  - [Mobile App Analytics Google Group](https://groups.google.com/forum/?fromgroups#!forum/ga-mobile-app-analytics)
  - [Mobile Implementation Guide](https://developers.google.com/analytics/solutions/mobile-implementation-guide)

________________
________________

##Unity Tracking API Reference:

Each hit can be sent using the 'Basic' method or the 'Builder' method. 'Builder' methods are required if you wish to append campaign parameters or custom dimensions or metrics. Hits can be sent using either method interchangeably, they will build identical hits.

Don't forget you need to add a public variable to the top of your script like:

        public GoogleAnalyticsV3 googleAnalytics;
Then, after saving the script, drag the _GAv3_ prefab from your object hierarchy onto the new variable in the _Inspector_ view for your script.

###General:
```csharp
    public void DispatchHits();
    public void StartSession();
    public void StopSession();
```
________________
###Screen Tracking:

#####Basic:
```csharp
    public void LogScreen(string title);
``` 
#####Builder:
```csharp
    public void LogScreen(AppViewHitBuilder builder);
```
#####Sample Hits:
```csharp
    googleAnalytics.LogScreen("Main Menu");
    
    //Builder Hit with all App View parameters (all parameters required):
    googleAnalytics.LogScreen(new AppViewHitBuilder()
        .SetScreenName("Main Menu"));
```
________________
###Event Tracking:

#####Basic:
```csharp
    public void LogEvent(string eventCategory, 
    	string eventAction, 
    	string eventLabel, 
    	long value);
``` 
#####Builder:
```csharp
    public void LogEvent(EventHitBuilder builder);
```
#####Sample Hits:
```csharp
    googleAnalytics.LogEvent("Achievement", "Unlocked", "Slay 10 dragons", 5);

    //Builder Hit with all Event parameters
    googleAnalytics.LogEvent(new EventHitBuilder()
        .SetEventCategory("Achievement")
        .SetEventAction("Unlocked")
        .SetEventLabel("Slay 10 dragons")
        .SetEventValue(5));
        
    //Builder Hit with minimum required Event parameters
    googleAnalytics.LogEvent(new EventHitBuilder()
        .SetEventCategory("Achievement")
        .SetEventAction("Unlocked"));
```
________________
###Exception Tracking:

#####Basic:
```csharp
    public void LogException(string exceptionDescription, bool isFatal);
``` 
#####Builder:
```csharp
    public void LogException(ExceptionHitBuilder builder);
```

#####Sample Hits:
```csharp
    googleAnalytics.LogException("Incorrect input exception", true);
    
    //Builder Hit with all Exception parameters
    googleAnalytics.LogException(new ExceptionHitBuilder()
        .SetExceptionDescription("Incorrect input exception")
        .SetFatal(true));
        
    //Builder Hit with minimum required Exception parameters
    googleAnalytics.LogException(new ExceptionHitBuilder());
``` 
________________
###User Timings:

#####Basic:
```csharp
    public void LogTiming(string timingCategory, 
    	long timingInterval, 
    	string timingName, 
    	string timingLabel);
``` 
#####Builder:
```csharp
    public void LogTiming(TimingHitBuilder builder);
```

#####Sample Hits:
```csharp
    googleAnalytics.LogTiming("Loading", 50L, "Main Menu", "First Load");
    
    //Builder Hit with all Timing parameters
    googleAnalytics.LogTiming(new TimingHitBuilder()
        .SetTimingCategory("Loading")
        .SetTimingInterval(50L)
        .SetTimingName("Main Menu")
        .SetTimingLabel("First load"));
        
    //Builder Hit with minimum required Timing parameters
    googleAnalytics.LogTiming(new TimingHitBuilder()
        .SetTimingCategory("Loading"));
```
________________
###Social Tracking:

#####Basic:
```csharp
    public void LogSocial(string socialNetwork, 
    	string socialAction, 
    	string socialTarget);
``` 
#####Builder:
```csharp
    public void LogSocial(SocialHitBuilder builder);
```

#####Sample Hits:
```csharp
    googleAnalytics.LogSocial("twitter", "retweet", "twitter.com/googleanalytics/status/482210840234295296");
    
    //Builder Hit with all Social parameters (all parameters required)
    googleAnalytics.LogSocial(new SocialHitBuilder()
        .SetSocialNetwork("Twitter")
        .SetSocialAction("Retweet")
        .SetSocialTarget("twitter.com/googleanalytics/status/482210840234295296"));
``` 
________________
###Ecommerce Tracking:

Methods below require Ecommerce to be enabled on the Google Analytics profile. Instructions on how to do so can be found [here]( https://support.google.com/analytics/answer/1009612?hl=en&ref_topic=1037061).

####Transaction Hit:

#####Basic:
```csharp
    public void LogTransaction(string transID, 
    	string affiliation, 
    	double revenue, 
    	double tax, 
    	double shipping);

    public void LogTransaction(string transID, 
    	string affiliation, 
    	double revenue, 
    	double tax, 
    	double shipping, 
    	string currencyCode);
``` 
#####Builder:
```csharp
    public void LogTransaction(TransactionHitBuilder builder);
```

#####Sample Hits:
```csharp
    googleAnalytics.LogTransaction("TRANS001", "Coin Store", 3.0, 0.0, 0.0);
    googleAnalytics.LogTransaction("TRANS001", "Coin Store", 3.0, 0.0, 0.0, "USD");
    
    //Builder Hit with all Transaction parameters
    googleAnalytics.LogTransaction(new TransactionHitBuilder()
        .SetTransactionID("TRANS001")
        .SetAffiliation("Coin Store")
        .SetRevenue(3.0)
        .SetTax(0)
        .SetShipping(0.0)
        .SetCurrencyCode("USD"));
        
    //Builder Hit with minimum required Transaction parameters
    googleAnalytics.LogTransaction(new TransactionHitBuilder()
        .SetTransactionID("TRANS001")
        .SetAffiliation("Coin Store"));
```
####Item Hit:

#####Basic:
```csharp
    public void LogItem(string transID, 
    	string name, 
    	string SKU, 
    	string category, 
    	double price, 
    	long quantity);

    public void LogItem(string transID, 
    	string name, 
    	string SKU, 
    	string category, 
    	double price, 
    	long quantity,  
    	string currencyCode);
``` 
#####Builder:
```csharp
    public void LogItem(ItemHitBuilder builder);
```

#####Sample Hits:
```csharp
    googleAnalytics.LogItem("TRANS001", "Sword", "SWORD1223", "Weapon", 3.0, 2);
    googleAnalytics.LogItem("TRANS001", "Sword", "SWORD1223", "Weapon", 3.0, 2, "USD");
    
    //Builder Hit with all Item parameters
    googleAnalytics.LogItem(new ItemHitBuilder()
        .SetTransactionID("TRANS001")
        .SetName("Sword")
        .SetSKU("SWORD1223")
        .SetCategory("Weapon")
        .SetPrice(3.0)
        .SetQuantity(2)
        .SetCurrencyCode("USD"));
        
    //Builder Hit with minimum required Item parameters
    googleAnalytics.LogItem(new ItemHitBuilder()
        .SetTransactionID("TRANS001")
        .SetName("Sword")
        .SetSKU("SWORD1223"));
```
________________
###Custom Dimensions:

Custom Dimensions can be sent as part of any hit by using the Builder version of the method signatures. The example below is for an App View hit, but it will work for all hit types. Custom Dimensions must be defined on the Google Analytics website before hits can be received. Instructions on how to do so can be found [here]( https://support.google.com/analytics/answer/2709886).
```csharp
    public T SetCustomDimension(int dimensionNumber, string value);
```

#####Sample Hit:
```csharp
    googleAnalytics.LogScreen(new AppViewHitBuilder()
        .SetScreenName("Another screen")
        .SetCustomDimension(1, "200"));
```
________________
###Custom Metrics:

Custom Metrics can be sent as part of any hit by using the Builder version of the method signatures. The example below is for an event hit, but it will work for all hit types which take a *Builder as a parameter. Custom Metrics must be defined on the Google Analytics website before hits can be received. Instructions on how to do can be found [here](https://support.google.com/analytics/answer/2709897).
```csharp
	public T SetCustomMetric(int metricNumber, string value);
```
#####Sample Hit:
```csharp
    googleAnalytics.LogEvent(new EventHitBuilder()
        .SetEventCategory("Achievement")
        .SetEventAction("Unlocked")
        .SetEventLabel("Slay 10 dragons")
        .SetEventValue(5)
        .SetCustomMetric(3, "200"));
```
________________
###Campaign Parameters:

Campaign parameters can be sent as part of any hit by using the Builder version of the method signatures. The example below is for a timing hit.
```csharp
	public T SetCampaignName(string campaignName);
	public T SetCampaignSource(string campaignSource);
	public T SetCampaignMedium(string campaignMedium);
	public T SetCampaignKeyword(string campaignKeyword);
	public T SetCampaignContent(string campaignContent);
	public T SetCampaignID(string campaignID);
```
#####Sample Hit:
```csharp
    googleAnalytics.LogTiming(new TimingHitBuilder()
        .SetTimingCategory("Loading")
        .SetTimingInterval(50L)
        .SetTimingName("Main Menu")
        .SetTimingLabel("First load")
        .SetCampaignName("Summer Campaign")
        .SetCampaignSource("google")
        .SetCampaignMedium("cpc")
        .SetCampaignKeyword("games")
        .SetCampaignContent("Free power ups")
        .SetCampaignId("Summer1"));
        
    //Builder Hit with minimum required Campaign parameters
    googleAnalytics.LogTiming(new TimingHitBuilder()
        .SetTimingCategory("Loading")
        .SetTimingInterval(50L)
        .SetTimingName("Main Menu")
        .SetTimingLabel("First load")
        .SetCampaignSource("google");
```
________________
###Advanced functionality

These methods are recommended for power users only - if you are not familiar with Google Analytics we suggest you stick to the methods above.

####SetOnTracker
_Use variables from Assets/Plugins/Fields.cs for the fieldName parameter ie. Fields.SCREEN_NAME._

Use to set values on the tracker to be sent with other hits. Note that if you set a value like Fields.SCREEN_NAME and then call a method which sets that value like LogScreen(...), the value on the tracker will be overridden by the value from the method call.
```csharp
    public void SetOnTracker(Field fieldName, object value);
```
#####Sample Call:
```csharp
    googleAnalytics.SetOnTracker(Fields.SCREEN_NAME, "Main Menu");
```
__
####Dispose
Free up managed resources and resets the tracker. The next tracking hit will have to create a new tracker so it is recommended to only call dispose when completely finished with tracking (For example, in an onDispose() method which executes when the user quits your game).
```csharp
    public void Dispose();
```
#####Sample Call:
```csharp
    googleAnalytics.Dispose();
```

###Debugging:

Not seeing hits? Try the suggestions below before [posting an issue](https://github.com/googleanalytics/google-analytics-plugin-for-unity/issues) on Github or on our [forums](https://groups.google.com/forum/?fromgroups#!forum/ga-mobile-app-analytics).

  1. Have you __set the log level__ to 'VERBOSE'? This will give you information about missing parameters or other issues which could be preventing your hits from being send.
  1. Did you fully __configure the prefab__ (especially the __Tracking ID__ for your platform and __Product Name__)?
  2. Did you set the __dispatch period__ to a reasonable value? A value of zero will stop dispatching from occuring all together, while too high a value will stop the hits from being show in the [Real-Time reports](https://www.google.com/analytics/web/#realtime) (though they will still be viewable in the other reports). The default value for the plugin is 5 seconds. 
  3. Are you seeing a __NullReferenceException__ when you run your game? This likely means you forgot to drag the configured prefab from the _Object Hierarchy_ onto the script reference in the _Inspector_ view.
  4. Are you getting __linker errors when building for iOS__? Follow the iOS instructions in [Step 2](https://github.com/googleanalytics/google-analytics-plugin-for-unity/blob/master/README.md#platform-specific-configuration) of the _Set up_ to automatically add the required libraries when building. 
  5. Are you getting __permissions errors on Android__? Follow the Android instructions in [Step 2](https://github.com/googleanalytics/google-analytics-plugin-for-unity/blob/master/README.md#platform-specific-configuration) of the _Set up_ to learn how to update your _AndroidManifest.xml_.
  6. Have you set the __Dry Run__ flag to unchecked? This flag is intended for testing your game when you don't wish to send hits to Google Analytics and will prevent all hits from being sent. 

###Thanks: 
  - [Knoxx-](https://github.com/Knoxx-) for fixing a typo in the Campaign tracking permissions
  - [mataneine](https://github.com/mataneine) for filtering out meta files during iOS build post processing
  - [g8minhquan](https://github.com/g8minhquan) for identifying the sqlite3.dylib library needs to be added if using the -ObjC linker flag
