using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NRKernal;

public class SceneChanger : MonoBehaviour
{
	string _sceneName;
	public Image loadingProgressBar;

    public void ChangeScene(string sceneName)
	{
		_sceneName = sceneName;
		//StartCoroutine(LoadAsyncScene());
		SceneManager.LoadScene(_sceneName);

		Debug.Log("scene changed: " + _sceneName);

		if(_sceneName == "AvatarStop")
        {
			bool switchToHandTracking = NRInput.SetInputSource(InputSourceEnum.Hands);
			if (switchToHandTracking)
				Debug.Log("hand tracking enable");
			else
				Debug.Log("error while changing input");
		}
	}

	// @TODO
	// - add async mode load (problem with the inactive mode, check here: https://forum.unity.com/threads/coroutine-couldnt-be-started-because-the-the-game-object-is-inactive.275311/)
	//IEnumerator LoadAsyncScene()
	//   {
	//	AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);
	//	//asyncLoad.allowSceneActivation = false;

	//	// Wait until the asynchronous scene fully loads
	//	while (!asyncLoad.isDone)
	//	{
	//		//progressText.text = async.progress + "";
	//		Debug.Log("loading scene " + _sceneName);
	//		yield return null;
	//	}
	//}

	public void Exit()
	{
		Debug.Log("Application closed");
		PlayerPrefs.SetString("lastAccess", System.DateTime.Now.ToShortDateString());
		Application.Quit();
	}
}