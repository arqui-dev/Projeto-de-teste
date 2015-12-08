using UnityEngine;
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
	static public int totalTypes = 3;
}

