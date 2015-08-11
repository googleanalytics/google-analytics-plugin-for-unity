/*
  Copyright 2014 Google Inc. All rights reserved.

  Licensed under the Apache License, Version 2.0 (the "License";
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
using System.Collections;

/*
  Available strings to use with SetOnTracker(string stringName, object value)
*/
public class Fields
{
	//General
	public const string ANONYMIZE_IP = "&aip";
	public const string HIT_TYPE = "&t";
	public const string SESSION_CONTROL = "&sc";

	public const string SCREEN_NAME = "&cd";
	public const string LOCATION = "&dl";
	public const string REFERRER = "&dr";
	public const string PAGE = "&dp";
	public const string HOSTNAME = "&dh";
	public const string TITLE = "&dt";
	public const string LANGUAGE = "&ul";
	public const string ENCODING = "&de";

	// System
	public const string SCREEN_COLORS = "&sd";
	public const string SCREEN_RESOLUTION = "&sr";
	public const string VIEWPORT_SIZE = "&vp";

	// Application
	public const string APP_NAME = "&an";
	public const string APP_ID = "&aid";
	public const string APP_INSTALLER_ID = "&aiid";
	public const string APP_VERSION = "&av";

	// Visitor
	public const string CLIENT_ID = "&cid";
	public const string USER_ID = "&uid";

	// Campaign related strings; used in all hits.
	public const string CAMPAIGN_NAME = "&cn";
	public const string CAMPAIGN_SOURCE = "&cs";
	public const string CAMPAIGN_MEDIUM = "&cm";
	public const string CAMPAIGN_KEYWORD = "&ck";
	public const string CAMPAIGN_CONTENT = "&cc";
	public const string CAMPAIGN_ID = "&ci";
	// Autopopulated campaign strings
	public const string GCLID = "&gclid";
	public const string DCLID = "&dclid";


	// Event Hit (&t=event)
	public const string EVENT_CATEGORY = "&ec";
	public const string EVENT_ACTION = "&ea";
	public const string EVENT_LABEL = "&el";
	public const string EVENT_VALUE = "&ev";

	// Social Hit (&t=social)
	public const string SOCIAL_NETWORK = "&sn";
	public const string SOCIAL_ACTION = "&sa";
	public const string SOCIAL_TARGET = "&st";

	// Timing Hit (&t=timing)
	public const string TIMING_VAR = "&utv";
	public const string TIMING_VALUE = "&utt";
	public const string TIMING_CATEGORY = "&utc";
	public const string TIMING_LABEL = "&utl";


	// Exception Hit (&t=exception)
	public const string EX_DESCRIPTION = "&exd";
	public const string EX_FATAL = "&exf";

	// Ecommerce (&t=transaction / &t=item)
	public const string CURRENCY_CODE = "&cu";
	public const string TRANSACTION_ID = "&ti";
	public const string TRANSACTION_AFFILIATION = "&ta";
	public const string TRANSACTION_SHIPPING = "&ts";
	public const string TRANSACTION_TAX = "&tt";
	public const string TRANSACTION_REVENUE = "&tr";
	public const string ITEM_SKU = "&ic";
	public const string ITEM_NAME = "&in";
	public const string ITEM_CATEGORY = "&iv";
	public const string ITEM_PRICE = "&ip";
	public const string ITEM_QUANTITY = "&iq";

	// General Configuration
	public const string TRACKING_ID = "&tid";
	public const string SAMPLE_RATE = "&sf";
	public const string DEVELOPER_ID = "&did";

	public const string CUSTOM_METRIC = "&cm";
	public const string CUSTOM_DIMENSION = "&cd";
}