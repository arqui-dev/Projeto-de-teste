using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EUTuboScreen : MonoBehaviour
{
	public Text txtVideoScoreLast;
	public Text txtVideoScoreBefore;

	public Text txtMarketingCost;
	public Text txtMarketingBonus;
	public Text txtMarketingValue;

	public Button btnMarketing;

	public Toggle [] tglMarketing;

	public GameObject txtCampaignStarted;

	void OnEnable()
	{
		txtVideoScoreLast.text = "" + PlayerData.scoreLastVideo;
		txtVideoScoreBefore.text = "" + PlayerData.scoreVideoBefore;

		ChangeMarketingType(MarketingValue.marketingType);

		ToggleToggle();
	}

	public void ChangeMarketingType(int type)
	{
		MarketingValue.marketingType = type;

		txtMarketingCost.text = "" + MarketingValue.Cost(type);
		txtMarketingBonus.text = "" + MarketingValue.Bonus(type);
		txtMarketingValue.text = "" + MarketingValue.Value(type);

		btnMarketing.interactable = PlayerData.HasMoney(
			MarketingValue.Cost(type));
	}

	public void StartMarketingCampaign()
	{
		if (MarketingValue.CampaignStarted() == false)
		{
			int cost = MarketingValue.Cost(
				MarketingValue.marketingType);

			if (PlayerData.RemoveMoney(cost))
			{
				MarketingValue.StartCampaign();
				ToggleToggle();
			}
			else
			{
				Debug.Log ("Not enough money");
			}
		}
	}

	void ToggleToggle()
	{
		foreach(Toggle togg in tglMarketing)
		{
			togg.interactable = !MarketingValue.CampaignStarted();
		}

		btnMarketing.interactable = !MarketingValue.CampaignStarted();

		txtCampaignStarted.SetActive(MarketingValue.CampaignStarted());
	} 
}

