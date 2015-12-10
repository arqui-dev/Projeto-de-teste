using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EUTuboScreen : MonoBehaviour
{
	public Text txtVideoScoreLast;
	public Text txtVideoScoreBefore;

	void OnEnable()
	{
		txtVideoScoreLast.text = "" + PlayerData.scoreLastVideo;
		txtVideoScoreBefore.text = "" + PlayerData.scoreVideoBefore;
	}
}

