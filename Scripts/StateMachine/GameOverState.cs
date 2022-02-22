using UnityEngine;

public class GameOverState : IState
{
    private readonly GameObject _gameOverCanvas;
    private GameObject _homeBase;
    private Health _homeHealth;
    private WaveManager _waveManager;
    
    public GameOverState(GameObject gameOverCanvas)
    {
        this._gameOverCanvas = gameOverCanvas;
    }

    public void EnterState()
    {
        Debug.Log("<<<<<<<< Enter GameOverState >>>>>>>>");
        _waveManager = GameObject.FindObjectOfType<WaveManager>();
        _gameOverCanvas.SetActive(true);
        _homeBase = GameManager.instance.HomeBase;
        _homeHealth = _homeBase.GetComponent<Health>();
    }

    public void ExecuteState()
    {
        _waveManager.CleanUpEnemies();
        _homeHealth.ChangeHealth(_homeHealth.MaxHealth);
        Debug.Log("<<<<<<<< Execute GameOverState >>>>>>>>");
    }

    public void ExitState()
    {
        Debug.Log("<<<<<<<< Exit GameOverState >>>>>>>>");
        _gameOverCanvas.SetActive(false);
    }
}