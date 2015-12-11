using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public NavigationManager navigationManager;

	public Text txtMoney;
	public Text txtTotalViews;
	public Text txtTotalFans;
	public Text txtTotalVideos;

	void Update()
	{
		txtMoney.text		= "" + PlayerData.totalMoney + " $";
		txtTotalViews.text 	= "" + VideoData.totalViews + " views";
		txtTotalFans.text 	= "" + VideoData.totalFans + " fans";
		txtTotalVideos.text = "" + VideoData.numVideos + " videos";
	}

	public void NextTurn()
	{
		//*
		if(PlayerData.videoRelease != null)
		{
			PlayerData.videoRelease.DoTheMath(
				MarketingValue.Value(MarketingValue.marketingType));

			//PlayerData.videoLast = PlayerData.videoRelease;
		}

		/*
		//PlayerData.scoreVideoBefore = 0;
		if (PlayerData.videoLast != null)
		{
			PlayerData.scoreVideoBefore = PlayerData.videoLast.Score();
		}
		//*/
		PlayerData.videoLast = PlayerData.videoRelease;

		PlayerData.videoRelease = null;
		//*/

		PlayerData.turn++;
		PlayerData.recordedThisTurn = false;

		MarketingValue.EndCampaign();

		navigationManager.Deactivate();
	}
}

