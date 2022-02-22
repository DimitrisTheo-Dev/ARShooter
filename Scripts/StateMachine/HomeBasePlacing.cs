using UnityEngine;
using UnityEngine.UI;

public class HomeBasePlacing : IState
{

    private readonly GameObject _homeBasePlacing;
    private ARTapToPlace _arTapToPlace;
    private Button _homeBaseButton;

    public  HomeBasePlacing(GameObject homeBasePlacing)
    {
        this._homeBasePlacing = homeBasePlacing;
    }
    public void EnterState()
    {
        Debug.Log("<<<<<<<< Enter Home Base Placing >>>>>>>>");
        _arTapToPlace = GameObject.FindObjectOfType<ARTapToPlace>();
        _arTapToPlace.OnHomeBaseInstantiated += EnableHBButton;
        _homeBasePlacing.SetActive(true);
        _homeBaseButton = _homeBasePlacing.GetComponentInChildren<Button>();
        _homeBaseButton.gameObject.SetActive(false);
    }

    private void EnableHBButton()
    {
        _homeBaseButton.gameObject.SetActive(true);
    }

    public void ExecuteState()
    {
        Debug.Log("<<<<<<<< Execute Home Base Placing >>>>>>>>");
        _arTapToPlace.enabled = true;
        _homeBasePlacing.GetComponentInChildren<TMPro.TMP_Text>().text = "Tap to place HomeBase";
    }

  

    public void ExitState()
    {
        Debug.Log("<<<<<<<< Exit Home Base Placing >>>>>>>>");
        _homeBasePlacing.SetActive(false);
        _arTapToPlace.enabled = false;
    }
}