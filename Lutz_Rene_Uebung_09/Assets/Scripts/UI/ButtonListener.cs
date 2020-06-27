using UnityEngine;

public class ButtonListener : MonoBehaviour
{ 
    public void ToMainMenu()
    {
        SceneController.SceneControl.SwitchToMainMenuScreen();
    }

    public void ToGame()
    {
        SceneController.SceneControl.SwitchToGameScreen();
    }

    public void ExitGame()
    {
        SceneController.SceneControl.CloseGame();
    }
}
