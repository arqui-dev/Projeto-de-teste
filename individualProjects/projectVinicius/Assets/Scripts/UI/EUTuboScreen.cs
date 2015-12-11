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

	public Text txtViews;
	public Text txtTotalViews;

	public Button btnMarketing;

	public Toggle [] tglMarketing;

	public GameObject txtCampaignStarted;

	void OnEnable()
	{
		int lastScore = 0;
		if (PlayerData.videoLast != null)
		{
			lastScore = PlayerData.videoLast.Score();
		}

		int releaseScore = 0;
		if (PlayerData.videoRelease != null)
		{
			releaseScore = PlayerData.videoRelease.Score();
		}

		txtVideoScoreLast.text = "" + releaseScore;
		txtVideoScoreBefore.text = "" + lastScore;

		int views = 0;

		if (PlayerData.videoLast != null)
		{
			views = PlayerData.videoLast.ViewsVideo();
		}

		txtTotalViews.text = "" + VideoData.totalViews;
		txtViews.text = "" + views;

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

		VerifyButton();
	}

	public void StartMarketingCampaign()
	{
		if (MarketingValue.CampaignStarted() == false &&
		    PlayerData.videoRelease != null)
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

		VerifyButton();
	}

	void VerifyButton()
	{
		if (PlayerData.videoRelease == null ||
		    !PlayerData.HasMoney(MarketingValue.Cost(
			MarketingValue.marketingType)))
		{
			btnMarketing.interactable = false;
		}
	}

}

