using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MarketManager : MonoBehaviour
{
	public Button [] buyButtons;

	public Image [] imgBought;

	int [] costs = {
		900, 1800, 3600, 7200, 24300, 115200
	};

	public void BuyObject(int obj)
	{
		if (PlayerData.RemoveMoney(costs[obj]))
		{
			PlayerData.marketItens[obj].Buy();
			VerifyButtons();
		}
	}

	void OnEnable()
	{
		VerifyButtons();
	}

	void VerifyButtons()
	{
		for (int i = 0; i < buyButtons.Length; i++)
		{
			buyButtons[i].interactable = PlayerData.HasMoney(costs[i]);
		}
		
		for (int i = 0; i < imgBought.Length; i++)
		{
			imgBought[i].enabled = PlayerData.marketItens[i].Bought();
		}
	}
}

