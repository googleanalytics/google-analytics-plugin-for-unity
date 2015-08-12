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
using System.Collections;
using System.Collections.Generic;

/*
  Base class for building hits. This class stores data which can be sent with
  any hit type but cannot be sent independent of other hits.
 */
public abstract class HitBuilder
{
	public abstract bool IsValid { get; }

	public readonly Dictionary<int,string> CustomDimensions	= new Dictionary<int,string>();
	public readonly Dictionary<int,string> CustomMetrics	= new Dictionary<int,string>();

	public Dictionary<int,string> GetCustomDimensions() { return CustomDimensions; }
	public Dictionary<int,string> GetCustomMetrics() { return CustomMetrics; }

	public string CampaignName = "";
	public string CampaignSource = "";
	public string CampaignMedium = "";
	public string CampaignKeyword = "";
	public string CampaignContent = "";
	public string CampaignID = "";
	public string gclid = "";
	public string dclid = "";

	public string GetCampaignName() { return CampaignName; }
	public string GetCampaignSource() { return CampaignSource; }
	public string GetCampaignMedium() { return CampaignMedium; }
	public string GetCampaignKeyword() { return CampaignKeyword; }
	public string GetCampaignContent() { return CampaignContent; }
	public string GetCampaignID() { return CampaignID; }
	public string GetGclid() { return gclid; }
	public string GetDclid() { return dclid; }
}