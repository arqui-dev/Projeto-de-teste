using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FolderObject : MonoBehaviour
{
	public AttributeObject.Type type = AttributeObject.Type.Innovation;

	public Text txtEarnedXP;

	int earnedXP = 0;

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
			earnedXP += attribute.level;
			if (txtEarnedXP != null)
			{
				txtEarnedXP.text = "" + earnedXP;
			}
			else
			{
				Debug.LogError("txtEarnedXP not assigned");
			}
			return true;
		}

		return false;
	}
}
