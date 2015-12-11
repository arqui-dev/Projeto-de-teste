using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FolderObject : MonoBehaviour
{
	public PlayerData.SkillType type = PlayerData.SkillType.Innovation;

	public Text txtLevel;

	int level = 0;

	//static public int totalVideoScore = 0;

	void OnEnable()
	{
		level = 0;
		txtLevel.text = "" + level;
	}

	public bool VerifyInsideFolder(AttributeObject attribute)
	{
		if (attribute.type != type)
		{
			//Debug.Log ("Not the same type");
			return false;
		}

		Vector3 folderPosition = this.transform.position;
		Vector3 attributePosition = attribute.transform.position;
		Vector3 area = this.transform.GetComponent<RectTransform>().
			sizeDelta;

		if (attributePosition.x >= folderPosition.x - area.x &&
		    attributePosition.x <= folderPosition.x + area.x &&
		    attributePosition.y >= folderPosition.y - area.y &&
		    attributePosition.y <= folderPosition.y + area.y)
		{
			level += attribute.level;

			//totalVideoScore += attribute.level;

			if (txtLevel != null)
			{
				txtLevel.text = "" + level;
			}
			else
			{
				Debug.LogError("txtLevel not assigned");
			}
			return true;
		}

		return false;
	}

	public int Level()
	{
		return level;
	}
}
