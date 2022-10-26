using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NRKernal;

//@TODO
// - add dontdestroy to scene changer 
public class SceneChanger : MonoBehaviour
{
	string _sceneName;
	List<string> controller_scene = new List<string>()
		{
			"Start",
			"Settings",
			"Helper"
		};
	//public GameObject loadingInterface;
	//public Image loadingProgressBar;
	//public Animator transition;
	AsyncOperation sceneToLoad;

	//private void Awake()
	//{
	//	DontDestroyOnLoad(this.gameObject);
	//}
    //  void Start()
    //  {
    ////loadingInterface.SetActive(false);
    //  }

    public void ChangeScene(string sceneName)
	{
		//ShowLoadingBar();
		_sceneName = sceneName;
		//StartCoroutine(LoadAsyncScene());
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

	//  https://www.youtube.com/watch?v=zObWVOv1GlE

	IEnumerator LoadAsyncScene()
    {
		float totalProgress = 0;
		//transition.SetTrigger("Start");
		// asyncLoad = SceneManager.LoadSceneAsync(_sceneName);
		//asyncLoad.allowSceneActivation = false;
		//Debug.Log("loading scene");
		// Wait until the asynchronous scene fully loads
		while (!sceneToLoad.isDone)
        {
			totalProgress += sceneToLoad.progress;
			//loadingProgressBar.fillAmount = totalProgress;
            //progressText.text = async.progress + "";
            Debug.Log("loading scene " + _sceneName + " | Progress " + totalProgress);
            yield return null;
        }
	}

	//public void ShowLoadingBar()
 //   {
	//	loadingInterface.SetActive(true);
	//}

    public void Exit()
	{
		Debug.Log("Application closed");
		PlayerPrefs.SetString("lastAccess", System.DateTime.Now.ToShortDateString());
		PlayerPrefs.SetInt("firstAccess", 0);
		Application.Quit();
	}
}