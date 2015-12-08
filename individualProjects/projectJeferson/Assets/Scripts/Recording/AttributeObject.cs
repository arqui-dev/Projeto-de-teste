using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// The object of the attributes. Created when a production is defeated, and used to build the video.
/// </summary>
public class AttributeObject : MonoBehaviour
{
	/// <summary>
	/// Type of the attribute object.
	/// </summary>
	public enum Type {
		Content, Quality, Innovation
	}

	/// <summary>
	/// Quantity of attribute types.
	/// </summary>
	public const int totalTypes = 3;


	public float timeToVanish = 5;
	public float timeToDisappear = 2;

	float nextTimeVanish = 0;
	bool vanishing = false;
	int shineDirection = 1;

	Image imgAttribute;
	Color imgColor;
	Color shineColor = Color.red;

	void Awake()
	{
		imgAttribute = GetComponent<Image>();
		imgColor = imgAttribute.color;

		nextTimeVanish = Time.time + timeToVanish;
	}

	void Update()
	{
		VerifyVanish();
	}

	void VerifyVanish()
	{
		if (vanishing)
		{
			Shine();
		}

		if (Time.time > nextTimeVanish)
		{
			if (vanishing == false)
			{
				vanishing = true;
				nextTimeVanish = Time.time + timeToDisappear;
			}
			else
			{
				Disappear();
			}
		}
	}

	void Shine()
	{
		if (imgAttribute != null)
		{
			Color color = imgAttribute.color;
			imgAttribute.color = 
		}
	}

	void Disappear()
	{
		Destroy (gameObject);
	}
}

