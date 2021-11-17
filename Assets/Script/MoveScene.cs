using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveScene : MonoBehaviour {
	public string sceneName;

	public GameObject townScene;
	public GameObject smithyScene;
	public GameObject stageScene;
	public GameObject canvas;
	public GameObject toonBack;
    public GameObject t2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadStage()
	{
		switch (sceneName)
		{
			case "Smithy":
				townScene.SetActive(false);
				smithyScene.SetActive(true);
				break;
			case "Town":
				if(SceneManager.sceneCountInBuildSettings == 3)
				{
					SceneManager.LoadScene("Main");
				}
				else
				{
					smithyScene.SetActive(false);
					townScene.SetActive(true);
					stageScene.SetActive(false);
					canvas.SetActive(true);
				}
				break;
			case "Battle":
                //Limit Amount of Stage
                if (GameController.monsterIndex == 0 && t2.activeSelf == false) {
                    t2.SetActive(true);
                }
                else if (GameController.monsterIndex < GameController.numMonster) {
                    SceneManager.LoadScene(sceneName);
                }
                else {
                }
				break;
			case "Main":
				if (GameController.isFirstGame == false) {
					SceneManager.LoadScene(sceneName);
				}
				else {	
					toonBack.SetActive(true);
					GameController.isFirstGame = false;
				}	
				break;
			case "Title":
				SceneManager.LoadScene(sceneName);
				break;
			case "Stage":
				townScene.SetActive(false);
				smithyScene.SetActive(false);
				stageScene.SetActive(true);
				canvas.SetActive(false);
				break;
		}
	}
}
