using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour {

	GameObject mainModel;

	// Use this for initialization
	void Start () {
	}

	void Update()
	{
		// If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			if (!mainModel) {
				if (GameManager.modelInstantiated) {
					mainModel = GameObject.FindGameObjectWithTag ("CurrentModel");
				} else {
					return;
				}
			}

			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			float weightedDiff = -deltaMagnitudeDiff * 0.005F;

			mainModel.transform.localScale = new Vector3 (mainModel.transform.localScale.x + weightedDiff, 
															mainModel.transform.localScale.y + weightedDiff,
															mainModel.transform.localScale.z + weightedDiff);

		}
	}

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
