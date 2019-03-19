using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

/// <summary>
/// This Script ensures:
/// 
/// 1.MaterialManager Gameobject is not scaled and rotated when the Target Object is. Menu Canvas is always rotated towards the HMD.
/// 2.MaterialManager Gameobject is not scaled and rotated when parenting/unparenting from the Target Game object.
/// 
/// It is executed only in editor mode and does not affect performance in playmode.
/// </summary>
/// 
[ExecuteInEditMode]
public class MenuSafeTransform : MonoBehaviour {
	/// <summary>
	/// In Editmode Menu Canvas is always oriented forward-up. In play mode it always follows VR Head Mounted Camera
	/// </summary>
	Quaternion CanvasWorldRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
	Transform TargetObject;//this is the object which we intend to apply different materials
	GameObject Menu;//the Canvas child of the MaterialManager object

	private void Start()
	{
		hideFlags = HideFlags.HideInInspector;
	}

	void Update()
	{
		
		Vector3 savedWorldPosition;

		if (Application.isPlaying)
			return;
		Menu =GetComponentInChildren<Canvas>().gameObject;
		if (Menu == null)
			return;
		savedWorldPosition = transform.position;
		TargetObject = transform.parent;

		//if Material Manager has no parent or has been detached from a parent 
		if (transform.parent == null)
		{
			transform.localScale = Vector3.one; ;
			transform.localRotation = Quaternion.identity;
			Menu.transform.rotation = CanvasWorldRotation;

			return;
		}

		//If the MaterialManager parent(Target Game object) is being modified. 
		transform.SetParent(null);
		
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetObject.rotation,360);
		transform.SetParent(TargetObject);
		transform.position = savedWorldPosition;
		Menu.transform.rotation = CanvasWorldRotation;

	}

}

#endif
