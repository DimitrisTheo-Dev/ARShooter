using UnityEngine;

public class MainMenuState : IState
{
    private GameObject mainMenuCanvas;
    public GameObject MainMenuCanvas { get => mainMenuCanvas; set => mainMenuCanvas = value; }

    public MainMenuState(GameObject mainMenuCanvas)
    {
        this.mainMenuCanvas = mainMenuCanvas;
    }

    public void EnterState()
    {
        Debug.Log("<<<<<<<< Enter MainMenuState >>>>>>>>");
        mainMenuCanvas.SetActive(true);
    }

    public void ExecuteState()
    {
        Debug.Log("<<<<<<<< Execute MainMenuState >>>>>>>>");
    }

    public void ExitState()
    {
        Debug.Log("<<<<<<<< Exit MainMenuState >>>>>>>>");
        mainMenuCanvas.SetActive(false);
    }
}