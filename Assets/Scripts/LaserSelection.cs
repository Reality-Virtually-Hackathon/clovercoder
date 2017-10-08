using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSelection : MonoBehaviour {
	public Camera cam;

	void Update () {
		if (Input.touchCount > 0) {
			Vector3 pos = new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f);
			Ray toTouch = cam.ScreenPointToRay (pos);
			RaycastHit rhInfo;
			if (Physics.Raycast (toTouch, out rhInfo, Mathf.Infinity)) {
				_ShowAndroidToastMessage (rhInfo.collider.gameObject.tag);
			} else {
				_ShowAndroidToastMessage ("clicked on empty space");
			}
		}
	}



	/// <summary>
	/// Show an Android toast message.
	/// </summary>
	/// <param name="message">Message string to show in the toast.</param>
	/// <param name="length">Toast message time length.</param>
	private static void _ShowAndroidToastMessage(string message)
	{
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		if (unityActivity != null)
		{
			AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
			unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
				{
					AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
						message, 0);
					toastObject.Call("show");
				}));
		}
	}
}
