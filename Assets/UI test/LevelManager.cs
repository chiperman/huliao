using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public string sceneToLoad = "Game";


	public void LoadGame ()
	{
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneToLoad);
	}
}
