using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This script controls the behavior of the a production object.
/// </summary>
public class ProductionObject : MonoBehaviour
{
	//###########################################################
	// Static attributes

	/// <summary>
	/// Static attribute to store the production manager of the scene.
	/// </summary>
	static ProductionManager productionManager = null;

	//###########################################################
	// Public attributes

	/// <summary> Object that shows the production timer to the player. </summary>
	public Image objTimer;

	/// <summary> Object that shows the production life to the player. </summary>
	public Image objLife;

	/// <summary> How hard is to destroy this object </summary>
	public int totalLife = 9;

	/// <summary> Time before the object start to blink and vanish. </summary>
	public float timeToVanish = 13;
	
	/// <summary> Time while the object is blinking before it disappears. </summary>
	public float timeToDisappear = 3;

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
	/// <summary> The shine color, while blinking. </summary>
	Color blinkColor = Color.red;

	/// <summary> When this becomes zero, the player dies. </summary>
	int currentLife = 1;

	//###########################################################
	// Public Methods

	/// <summary> 
	/// Reduces the life of this object and call defeat the object if the condition is fulfilled.
	/// </summary>
	public void OnClick()
	{
		this.currentLife -= 1;
		if (this.currentLife <= 0)
		{
			Defeated();
		}
	}

	//###########################################################
	// Inicialization
	void Awake()
	{
		if (productionManager == null)
		{
			productionManager = GameObject.
				FindObjectOfType<ProductionManager>();
		}

		imgAttribute = GetComponent<Image>();
		imgColor = imgAttribute.color;

		currentLife = totalLife;
		
		nextTimeVanish = Time.time + timeToVanish;
	}

	//###########################################################
	// Update methods
	
	// Each frame verify the time of vanish and blinking.
	void Update()
	{
		VerifyVanish();
		UpdateUIObjects();
	}

	/// <summary> Update the timer and the life. </summary>
	void UpdateUIObjects()
	{
		if (objTimer != null)
		{
			if (blinking == false)
			{
				objTimer.fillAmount = 
					(nextTimeVanish - Time.time) / timeToVanish;
			}
			else
			{
				objTimer.enabled = false;
			}
		}

		if (objLife != null)
		{
			if (currentLife < 0)
			{
				objLife.fillAmount = 0;
			}
			else
			{
				objLife.fillAmount = ((float)currentLife) / ((float)totalLife);
			}
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
	/// Makes the object change it's color to blink, and also diminish the alpha with time. 
	/// </summary>
	void Blink()
	{
		// Verify if the Image component is not null
		if (imgAttribute != null)
		{
			// Makes the alpha from 1 to 0 based on the remaining time left
			// to the object and the current difference between the next time
			// and the actual time.
			float alpha = (nextTimeVanish - Time.time) / timeToDisappear;

			// Speed of the blink. blinkTime varies from 0 to 1
			blinkTime += blinkSpeed * Time.deltaTime;
			if (blinkTime > 1)
			{
				blinkTime = 0;
				blinkDirection = -blinkDirection;
			}

			// If direction is 1, changes from normal color to blink color (red),
			// otherwise from blink color to normal color.
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
		transform.parent.GetComponent<ProductionManager>().CreateProduction();
		Destroy (gameObject);
	}

	//###########################################################
	// Private methods

	/// <summary>
	/// Create new attributes and destroys this object.
	/// </summary>
	void Defeated()
	{
		CreateAttributes();
		Disappear();
	}

	/// <summary>
	/// Create attributes objects based on the player current skill levels.
	/// </summary>
	void CreateAttributes()
	{
		productionManager.CreateAttributes();
	}

	/// <summary> Makes the object disappear when the time is over. </summary>
	void EndRecording()
	{
		Destroy (gameObject);
	}

	//###########################################################
	// Static methods

	/// <summary>
	/// Nullify the productionManager attribute that when the new scene is created it can get the right one.
	/// </summary>
	static public void MakeProductionManagerNull()
	{
		productionManager = null;
	}
	


}