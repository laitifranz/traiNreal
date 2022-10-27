// reference: https://www.youtube.com/watch?v=zObWVOv1GlE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NRKernal;

public class SceneChanger : MonoBehaviour
{
	string _sceneName;
	List<string> controller_scene = new List<string>()
		{
			"Start",
			"Settings",
			"Helper"
		};

	AsyncOperation sceneToLoad;


    public void ChangeScene(string sceneName)
	{
		_sceneName = sceneName;
		sceneToLoad = SceneManager.LoadSceneAsync(_sceneName);

		if(!controller_scene.Contains(_sceneName))
        {
			bool switchToHandTracking = NRInput.SetInputSource(InputSourceEnum.Hands);
			if (switchToHandTracking)
				Debug.Log("hand tracking enable");
			else
				Debug.Log("error while changing input");
		}

		if (sceneName == "EndScene") PlayerPrefs.SetInt("tutorialEnabled", 0);

		Debug.Log("scene changed: " + _sceneName);

		StartCoroutine(LoadAsyncScene());
	}



	IEnumerator LoadAsyncScene()
    {
		float totalProgress = 0;
		while (!sceneToLoad.isDone)
        {
			totalProgress += sceneToLoad.progress;
            Debug.Log("loading scene " + _sceneName + " | Progress " + totalProgress);
            yield return null;
        }
	}

    public void Exit()
	{
		Debug.Log("Application closed");
		PlayerPrefs.SetString("lastAccess", System.DateTime.Now.ToShortDateString());
		PlayerPrefs.SetInt("firstAccess", 0);
		Application.Quit();
	}
}