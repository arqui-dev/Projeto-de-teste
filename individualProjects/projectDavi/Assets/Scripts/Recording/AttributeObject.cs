using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// The object of the attributes. Created when a production is defeated, and used to build the video.
/// </summary>
public class AttributeObject : MonoBehaviour
{
	//###########################################################
	// Public attributes

	/// <summary> Type of the attribute object. </summary>
	public PlayerData.SkillType type = PlayerData.SkillType.Innovation;

	/// <summary> Time before the object start to blink and vanish. </summary>
	public float timeToVanish = 5;

	/// <summary> Time while the object is blinking before it disappears. </summary>
	public float timeToDisappear = 2;

	[HideInInspector]
	public int level = 1;

	//###########################################################
	// Private attributes

	// Vanish and blinking attributes
	/// <summary> Time before the object starts blinking. </summary>
	float nextTimeVanish = 0;
	/// <summary> The object started to vanish, blinking. </summary>
	bool blinking = false;
	/// <summary> Direction of the lerp function to go. </summary>
	int blinkDirection = 1;
	/// <summary> Time used to lerp the Image color. </summary>
	float blinkTime = 0;
	/// <summary> Speed of the blinking effect. </summary>
	float blinkSpeed = 10;

	// image component and color for blinking
	/// <summary> Holds the Image component. </summary>
	Image imgAttribute;
	/// <summary> The Image base color. </summary>
	Color imgColor;
	/// <summary> The blink color. </summary>
	Color blinkColor = Color.red;

	// Drag attributes
	bool moving = false;

	// Destroy tag
	bool destroyNextFrame = false;

	//###########################################################
	// Drag methods

	/// <summary>
	/// Makes this object follow the mouse, and make it shows above other objects.
	/// </summary>
	public void OnStartDrag()
	{
		MoveToMouse();
		moving = true;

		// Brings the object to front
		Transform parent = transform.parent;
		transform.SetParent(null);
		transform.SetParent(parent, true);
	}

	/// <summary>
	/// Makes the object stop following the mouse and rearrange it on the screen if necessary.
	/// </summary>
	public void OnEndDrag()
	{
		moving = false;
		RearrangeOnScreen();
	}

	/// <summary> Moves to mouse. </summary>
	void MoveToMouse()
	{
		transform.position = Input.mousePosition;
	}

	/// <summary>
	/// Verify if the attribute was dropped on a folder, and if not makes it be inside the screen.
	/// </summary>
	void RearrangeOnScreen()
	{
		Vector3 objPosition = this.transform.position;
		Vector3 parentPosition = this.transform.parent.position;
		Vector3 area = this.transform.parent.
			GetComponent<RectTransform>().sizeDelta / 2 - 
				this.transform.GetComponent<RectTransform>().
				sizeDelta / 2;

		// Verify if the object was dropped on a correct folder.
		if (GetComponentInParent<ProductionManager>().
			VerifyDropAttributeInFolder(this))
		{
			destroyNextFrame = true;
			return;
		}

		// Make sure the object will be inside the screen position.
		if (objPosition.x <= parentPosition.x - area.x)
		{
			this.transform.position = new Vector3(
				parentPosition.x - area.x, this.transform.position.y,
				this.transform.position.z);
		}
		if (objPosition.x >= parentPosition.x + area.x)
		{
			this.transform.position = new Vector3(
				parentPosition.x + area.x, this.transform.position.y,
				this.transform.position.z);
		}

		if (objPosition.y <= parentPosition.y - area.y)
		{
			this.transform.position = new Vector3(
				this.transform.position.x, parentPosition.y - area.y,
				this.transform.position.z);
		}
		if (objPosition.y >= parentPosition.y + area.y)
		{
			this.transform.position = new Vector3(
				this.transform.position.x, parentPosition.y + area.y,
				this.transform.position.z);
		}
	}

	//###########################################################
	// Inicialization
	void Awake()
	{
		// Get the Image and color component
		imgAttribute = GetComponent<Image>();
		imgColor = imgAttribute.color;

		// Set time to vanish
		nextTimeVanish = Time.time + timeToVanish;
	}

	//###########################################################
	// Update and private methods

	// Each frame verify the time of vanish and blinking.
	void Update()
	{
		VerifyVanish();
		VerifyMoving();

		if (destroyNextFrame)
		{
			Disappear();
		}
	}

	/// <summary>
	/// If the moving tag is set to true, makes this object follow the mouse.
	/// </summary>
	void VerifyMoving()
	{
		if (moving)
		{
			MoveToMouse();
		}
	}

	/// <summary>
	/// Verify the blinking status, call the blink method and destroys it when it's time.
	/// </summary>
	void VerifyVanish()
	{
		if (blinking)
		{
			Blink();
		}

		if (Time.time > nextTimeVanish)
		{
			if (blinking == false)
			{
				blinking = true;
				nextTimeVanish = Time.time + timeToDisappear;
			}
			else
			{
				Disappear();
			}
		}
	}

	/// <summary> 
	/// Makes the object change it's color to blink, and also diminish the alfa with time. 
	/// </summary>
	void Blink()
	{
		if (imgAttribute != null)
		{
			float alpha = (nextTimeVanish - Time.time) / timeToDisappear;

			blinkTime += blinkSpeed * Time.deltaTime;
			if (blinkTime > 1)
			{
				blinkTime = 0;
				blinkDirection = -blinkDirection;
			}

			if (blinkDirection == 1)
			{
				imgAttribute.color = Color.Lerp(imgColor, blinkColor, blinkTime);
			}
			else
			{
				imgAttribute.color = Color.Lerp(blinkColor, imgColor, blinkTime);
			}

			Color color = imgAttribute.color;
			color.a = alpha;

			imgAttribute.color = color;
		}
	}

	/// <summary> Destroys the object. </summary>
	void Disappear()
	{
		Destroy (gameObject);
	}

	/// <summary> Makes the object disappear when the time is over. </summary>
	void EndRecording()
	{
		Destroy (gameObject);
	}
}

