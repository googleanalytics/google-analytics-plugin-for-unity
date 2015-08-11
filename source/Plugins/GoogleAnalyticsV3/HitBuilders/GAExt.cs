using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class GAExt
{
	public static T Validate<T>(this T hitBuilder) where T : HitBuilder
	{
		if(hitBuilder != null && hitBuilder.IsValid)
			return hitBuilder;

		return null;
	}


	public static T SetCustomDimension<T>(this T hitBuilder,int dimensionNumber,string value) where T : HitBuilder
	{
		hitBuilder.CustomDimensions.Add(dimensionNumber,value);
		return hitBuilder;
	}

	public static T SetCustomMetric<T>(this T hitBuilder,int metricNumber,string value) where T : HitBuilder
	{
		hitBuilder.CustomMetrics.Add(metricNumber,value);
		return hitBuilder;
	}

	public static T SetCampaignName<T>(this T hitBuilder,string campaignName) where T : HitBuilder
	{
		if(campaignName != null)
			hitBuilder.CampaignName = campaignName;

		return hitBuilder;
	}

	public static T SetCampaignSource<T>(this T hitBuilder,string campaignSource) where T : HitBuilder
	{
		if(campaignSource != null)
			hitBuilder.CampaignSource = campaignSource;
		else Debug.Log("Campaign source cannot be null or empty");

		return hitBuilder;
	}

	public static T SetCampaignMedium<T>(this T hitBuilder,string campaignMedium) where T : HitBuilder
	{
		if(campaignMedium != null)
			hitBuilder.CampaignMedium = campaignMedium;

		return hitBuilder;
	}

	public static T SetCampaignKeyword<T>(this T hitBuilder,string campaignKeyword) where T : HitBuilder
	{
		if(campaignKeyword != null)
			hitBuilder.CampaignKeyword = campaignKeyword;

		return hitBuilder;
	}

	public static T SetCampaignContent<T>(this T hitBuilder,string campaignContent) where T : HitBuilder
	{
		if(campaignContent != null)
			hitBuilder.CampaignContent = campaignContent;

		return hitBuilder;
	}

	public static T SetCampaignID<T>(this T hitBuilder,string campaignID) where T : HitBuilder
	{
		if(campaignID != null)
			hitBuilder.CampaignID = campaignID;

		return hitBuilder;
	}

	public static T SetGclid<T>(this T hitBuilder,string gclid) where T : HitBuilder
	{
		if(gclid != null)
			hitBuilder.gclid = gclid;

		return hitBuilder;
	}

	public static T SetDclid<T>(this T hitBuilder,string dclid) where T : HitBuilder
	{
		if(dclid != null)
			hitBuilder.dclid = dclid;
	
		return hitBuilder;
	}
}
