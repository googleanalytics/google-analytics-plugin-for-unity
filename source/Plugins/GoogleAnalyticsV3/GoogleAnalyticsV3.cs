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

using UnityEngine;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/*
  GoogleAnalyticsV3 is an interface for developers to send hits to Google
  Analytics.
  The class will delegate the hits to the appropriate helper class depending on
  the platform being built for - Android, iOS or Measurement Protocol for all
  others.

  Each method has a simple form with the hit parameters, or developers can
  pass a builder to the same method name in order to add custom metrics or
  custom dimensions to the hit.
*/
public class GoogleAnalyticsV3 : MonoBehaviour
{
	private string uncaughtExceptionStackTrace = null;
	private bool initialized = false;

	public enum DebugMode
	{
		ERROR	= 1,
		WARNING	= 2,
		INFO	= 3,
		VERBOSE	= 4
	}

	static GoogleAnalyticsV3 instance	= null;
	// Instance for running Coroutines from platform specific classes
	public static GoogleAnalyticsV3 getInstance()
	{
		if(instance == null)
		{
			instance	= GameObject.FindObjectOfType<GoogleAnalyticsV3>();
			if(instance == null)
			{
				var prefab	= Resources.FindObjectsOfTypeAll<GoogleAnalyticsV3>().FirstOrDefault();
				instance	= GameObject.Instantiate(prefab).GetComponent<GoogleAnalyticsV3>();
			}
		}

		return instance;
	}

	[Tooltip("The tracking code to be used for Android. Example value: UA-XXXX-Y.")]
	public string androidTrackingCode;
	[Tooltip("The tracking code to be used for iOS. Example value: UA-XXXX-Y.")]
	public string IOSTrackingCode;
	[Tooltip("The tracking code to be used for platforms other than Android and iOS. Example value: UA-XXXX-Y.")]
	public string otherTrackingCode;

	[Tooltip("The application name. This value should be modified in the Unity Player Settings.")]
	public string productName;

	[Tooltip("The application identifier. Example value: com.company.app.")]
	public string bundleIdentifier;

	[Tooltip("The application version. Example value: 1.2")]
	public string bundleVersion;

	[RangedTooltip("The dispatch period in seconds. Only required for Android and iOS.",0,3600)]
	public int dispatchPeriod = 5;

	[RangedTooltip("The sample rate to use. Only required for Android and iOS.",0,100)]
	public int sampleFrequency = 100;

	[Tooltip("The log level. Default is WARNING.")]
	public DebugMode logLevel = DebugMode.WARNING;

	[Tooltip("If checked, the IP address of the sender will be anonymized.")]
	public bool anonymizeIP = false;

	[Tooltip("Automatically report uncaught exceptions.")]
	public bool UncaughtExceptionReporting = false;

	[Tooltip("Automatically send a launch event when the game starts up.")]
	public bool sendLaunchEvent = false;

	[Tooltip("If checked, hits will not be dispatched. Use for testing.")]
	public bool dryRun = false;

	// TODO: Create conditional textbox attribute
	[Tooltip("The amount of time in seconds your application can stay in the background before the session is ended. Default is 30 minutes (1800 seconds). A value of -1 will disable session management.")]
	public int sessionTimeout = 1800;

	public readonly static string currencySymbol = "USD";
	public readonly static string EVENT_HIT = "createEvent";
	public readonly static string APP_VIEW = "createAppView";
	public readonly static string SET = "set";
	public readonly static string SET_ALL = "setAll";
	public readonly static string SEND = "send";
	public readonly static string ITEM_HIT = "createItem";
	public readonly static string TRANSACTION_HIT = "createTransaction";
	public readonly static string SOCIAL_HIT = "createSocial";
	public readonly static string TIMING_HIT = "createTiming";
	public readonly static string EXCEPTION_HIT = "createException";

#if UNITY_ANDROID && !UNITY_EDITOR
	private GoogleAnalyticsAndroidV3 tracker	= new GoogleAnalyticsAndroidV3();
#elif UNITY_IPHONE && !UNITY_EDITOR
	private GoogleAnalyticsiOSV3 tracker	= new GoogleAnalyticsiOSV3();
#else
	private GoogleAnalyticsMPV3 tracker	= new GoogleAnalyticsMPV3();
#endif

	void Awake()
	{
		InitializeTracker();

		if(sendLaunchEvent)
		{
			LogEvent("Google Analytics","Auto Instrumentation","Game Launch",0);
		}

		if(UncaughtExceptionReporting)
		{
#if UNITY_5
			Application.logMessageReceived += HandleException;
#else
			Application.RegisterLogCallback(HandleException);
#endif
			if(GoogleAnalyticsV3.belowThreshold(logLevel,GoogleAnalyticsV3.DebugMode.VERBOSE))
				Debug.Log("Enabling uncaught exception reporting.");
		}
	}

	void Update()
	{
		if(uncaughtExceptionStackTrace != null)
		{
			LogException(uncaughtExceptionStackTrace,true);
			uncaughtExceptionStackTrace = null;
		}
	}

	private void HandleException(string condition,string stackTrace,LogType type)
	{
		if(type != LogType.Exception)
			return;

		uncaughtExceptionStackTrace = condition + "\n" + stackTrace + UnityEngine.StackTraceUtility.ExtractStackTrace();
	}

	// TODO: Error checking on initialization parameters
	private void InitializeTracker()
	{
		if(initialized)
			return;

		instance = this;
		DontDestroyOnLoad(instance);

		Debug.Log("Initializing Google Analytics 0.1.");

		tracker.SetAppName(productName);
		tracker.SetTrackingCode(otherTrackingCode);
		tracker.SetBundleIdentifier(bundleIdentifier);
		tracker.SetAppVersion(bundleVersion);
		tracker.SetLogLevelValue(logLevel);
		tracker.SetAnonymizeIP(anonymizeIP);
#if !UNITY_EDITOR
		tracker.SetDispatchPeriod(dispatchPeriod);
		tracker.SetSampleFrequency(sampleFrequency);
#endif

		tracker.SetDryRun(dryRun);
		tracker.InitializeTracker();

		initialized = true;
		SetOnTracker(Fields.DEVELOPER_ID,"GbOCSs");
	}

	public void SetAppLevelOptOut(bool optOut)
	{
		InitializeTracker();
		tracker.SetOptOut(optOut);
	}

	public void SetUserIDOverride(string userID)
	{
		SetOnTracker(Fields.USER_ID,userID);
	}

	public void ClearUserIDOverride()
	{
		InitializeTracker();
		tracker.ClearUserIDOverride();
	}

	public void DispatchHits()
	{
		InitializeTracker();
#if !UNITY_EDITOR
		tracker.DispatchHits();
#endif
	}

	public void StartSession()
	{
		InitializeTracker();
		tracker.StartSession();
	}

	public void StopSession()
	{
		InitializeTracker();
		tracker.StopSession();
	}

	// Use values from Fields for the fieldName parameter ie. Fields.SCREEN_NAME
	public void SetOnTracker(string fieldName,object value)
	{
		InitializeTracker();
		tracker.SetTrackerVal(fieldName,value);
	}

	public void LogScreen(string title)
	{
		LogScreen(new AppViewHitBuilder(title));
	}

	public void LogScreen(AppViewHitBuilder builder)
	{
		InitializeTracker();
		if(builder.Validate() == null)
			return;

		if(GoogleAnalyticsV3.belowThreshold(logLevel,GoogleAnalyticsV3.DebugMode.VERBOSE))
			Debug.Log("Logging screen.");

		tracker.LogScreen(builder);
	}

	public void LogEvent(string eventCategory,string eventAction,string eventLabel,long value)
	{
		EventHitBuilder builder = new EventHitBuilder()
			.SetEventCategory(eventCategory)
			.SetEventAction(eventAction)
			.SetEventLabel(eventLabel)
			.SetEventValue(value);

		LogEvent(builder);
	}

	public void LogEvent(EventHitBuilder builder)
	{
		InitializeTracker();
		if(builder.Validate() == null)
		{
			return;
		}
		if(GoogleAnalyticsV3.belowThreshold(logLevel,GoogleAnalyticsV3.DebugMode.VERBOSE))
		{
			Debug.Log("Logging event.");
		}

		tracker.LogEvent(builder);
	}

	public void LogTransaction(string transID,string affiliation,double revenue,double tax,double shipping)
	{
		LogTransaction(transID,affiliation,revenue,tax,shipping,"");
	}

	public void LogTransaction(string transID,string affiliation,double revenue,double tax,double shipping,string currencyCode)
	{
		TransactionHitBuilder builder = new TransactionHitBuilder()
			.SetTransactionID(transID)
			.SetAffiliation(affiliation)
			.SetRevenue(revenue)
			.SetTax(tax)
			.SetShipping(shipping)
			.SetCurrencyCode(currencyCode);

		LogTransaction(builder);
	}

	public void LogTransaction(TransactionHitBuilder builder)
	{
		InitializeTracker();
		if(builder.Validate() == null)
		{
			return;
		}
		if(GoogleAnalyticsV3.belowThreshold(logLevel,GoogleAnalyticsV3.DebugMode.VERBOSE))
		{
			Debug.Log("Logging transaction.");
		}

		tracker.LogTransaction(builder);
	}

	public void LogItem(string transID,string name,string sku,string category,double price,long quantity)
	{
		LogItem(transID,name,sku,category,price,quantity,null);
	}

	public void LogItem(string transID,string name,string sku,string category,double price,long quantity,string currencyCode)
	{
		ItemHitBuilder builder = new ItemHitBuilder()
			.SetTransactionID(transID)
			.SetName(name)
			.SetSKU(sku)
			.SetCategory(category)
			.SetPrice(price)
			.SetQuantity(quantity)
			.SetCurrencyCode(currencyCode);

		LogItem(builder);
	}

	public void LogItem(ItemHitBuilder builder)
	{
		InitializeTracker();
		if(builder.Validate() == null)
		{
			return;
		}
		if(GoogleAnalyticsV3.belowThreshold(logLevel,GoogleAnalyticsV3.DebugMode.VERBOSE))
		{
			Debug.Log("Logging item.");
		}

		tracker.LogItem(builder);
	}

	public void LogException(string exceptionDescription,bool isFatal)
	{
		ExceptionHitBuilder builder = new ExceptionHitBuilder()
			.SetExceptionDescription(exceptionDescription)
			.SetFatal(isFatal);

		LogException(builder);
	}

	public void LogException(ExceptionHitBuilder builder)
	{
		InitializeTracker();
		if(!builder.IsValid)
			return;

		if(GoogleAnalyticsV3.belowThreshold(logLevel,GoogleAnalyticsV3.DebugMode.VERBOSE))
		{
			Debug.Log("Logging exception.");
		}

		tracker.LogException(builder);
	}

	public void LogSocial(string socialNetwork,string socialAction,
		string socialTarget)
	{
		SocialHitBuilder builder = new SocialHitBuilder()
			.SetSocialNetwork(socialNetwork)
			.SetSocialAction(socialAction)
			.SetSocialTarget(socialTarget);

		LogSocial(builder);
	}

	public void LogSocial(SocialHitBuilder builder)
	{
		InitializeTracker();
		if(builder.Validate() == null)
		{
			return;
		}
		if(GoogleAnalyticsV3.belowThreshold(logLevel,GoogleAnalyticsV3.DebugMode.VERBOSE))
		{
			Debug.Log("Logging social.");
		}

		tracker.LogSocial(builder);
	}

	public void LogTiming(string timingCategory,long timingInterval,
		string timingName,string timingLabel)
	{
		TimingHitBuilder builder = new TimingHitBuilder()
			.SetTimingCategory(timingCategory)
			.SetTimingInterval(timingInterval)
			.SetTimingName(timingName)
			.SetTimingLabel(timingLabel);

		LogTiming(builder);
	}

	public void LogTiming(TimingHitBuilder builder)
	{
		InitializeTracker();
		if(builder.Validate() == null)
			return;

		if(GoogleAnalyticsV3.belowThreshold(logLevel,GoogleAnalyticsV3.DebugMode.VERBOSE))
			Debug.Log("Logging timing.");

		tracker.LogTiming(builder);
	}

	public void Dispose()
	{
		initialized = false;
#if UNITY_ANDROID && !UNITY_EDITOR
		tracker.Dispose();
#endif
	}

	public static bool belowThreshold(DebugMode userLogLevel,DebugMode comparelogLevel)
	{
		return comparelogLevel >= userLogLevel;
	}
}
