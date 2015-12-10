using UnityEngine;
using System.Collections;

public class VideoData : MonoBehaviour {
		
	public int SumAttributes () {
		return PlayerData.skillInnovation.Level + PlayerData.skillQuality.Level +
			PlayerData.skillContent.Level + PlayerData.skillCommunication.Level;
	}

	public int Hype () {
 		return PlayerData
	}
}