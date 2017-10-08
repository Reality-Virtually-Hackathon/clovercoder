using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.HelloAR;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public void closeView () {
		GameObject[] currentModels = GameObject.FindGameObjectsWithTag ("CurrentModel");
		foreach (GameObject currentModel in currentModels) {
			if (currentModel != null) {
				GameObject.Destroy (currentModel);
			}
		}
		GameManager.modelInstantiated = false;
	}

	public void chooseModel (string modelType) {
		HelloARController arController = gameObject.GetComponent<HelloARController> ();
		if (GameManager.modelInstantiated) {
//			switch (dropdown.value) {
//				case 0:
//				arController.andyObject.GetComponent<MeshRenderer>().enabled = true;
//				arController.robotObject.GetComponent<MeshRenderer>().enabled = false;
//					break;
//				case 1: 
//				arController.robotObject.GetComponent<MeshRenderer>().enabled = true;
//				arController.andyObject.GetComponent<MeshRenderer>().enabled = false;
//					break;
//			}

			if (modelType == "robot") {
				arController.robotObject.GetComponent<MeshRenderer> ().enabled = true;
				arController.andyObject.GetComponent<MeshRenderer> ().enabled = false;
			} else {
				arController.andyObject.GetComponent<MeshRenderer> ().enabled = true;
				arController.robotObject.GetComponent<MeshRenderer> ().enabled = false;
			}
			_ShowAndroidToastMessage(arController.andyObject.GetComponent<MeshRenderer>().enabled.ToString() + arController.robotObject.GetComponent<MeshRenderer>().enabled.ToString());
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
