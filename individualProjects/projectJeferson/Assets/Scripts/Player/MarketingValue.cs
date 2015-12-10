using UnityEngine;
using System.Collections;

public class MarketingValue
{
	static int [] costs = {
		0, 10, 100, 1000
	};

	static int [] bonus = {
		1,2,4,8
	};

	static public int marketingType = 0;

	static bool campaignStarted = false;

	static public bool CampaignStarted()
	{
		return campaignStarted;
	}

	static public int Cost(int type)
	{
		marketingType = type;
		return costs[marketingType];
	}

	static public int Bonus(int type)
	{
		marketingType = type;
		return bonus[marketingType];
	}

	static public int Value(int type)
	{
		marketingType = type;
		return PlayerData.CalculateMarketingValue(
			Bonus(marketingType), Cost(marketingType));
	}

	static public void StartCampaign()
	{
		campaignStarted = true;
	}
}

