using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController SceneControl { get; private set; } = null;

    private enum Scenes
    {
        MAIN_MENU = 0,
        GAME_SCENE = 1,
        END_SCENE = 2
    }

    private void Awake()
    {
        if (SceneControl == null)
        {
            SceneControl = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SwitchToGameScreen()
    {
        StartCoroutine(SwitchScene(Scenes.GAME_SCENE));
    }

    public void SwitchToMainMenuScreen()
    {
        StartCoroutine(SwitchScene(Scenes.MAIN_MENU));
    }

    public void SwitchToEndScreen()
    {
        StartCoroutine(SwitchScene(Scenes.END_SCENE));
    }

    public void CloseGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private IEnumerator SwitchScene(Scenes sceneEnum)
    {
        // From Unity SceneManager Api sample code
        // https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.MoveGameObjectToScene.html
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int) sceneEnum, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
