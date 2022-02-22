using UnityEngine;

public class MainGameState : IState
{
    private GameObject hudCanvas;
    private FPSLogic _fpsLogic;
    private PlayerShoot _playerShoot;
    private TMPro.TMP_Text _warningText;
    private readonly WaveManager _waveManager;

    public MainGameState(GameObject hudCanvas, GameObject enemyBase)
    {
        this.hudCanvas = hudCanvas;
        _waveManager = enemyBase.GetComponent<WaveManager>();
    }

    public void EnterState()
    {
        Debug.Log("<<<<<<<< Enter MainGameState >>>>>>>>");
        _fpsLogic = GameObject.FindObjectOfType<FPSLogic>();
        _playerShoot = GameObject.FindObjectOfType<PlayerShoot>();
        _warningText = hudCanvas.GetComponentInChildren<TMPro.TMP_Text>();
        _waveManager.OnWaveChanged += UpdateWarning;
    }

    public void ExecuteState()
    {
        Debug.Log("<<<<<<<< Execute MainGameState >>>>>>>>");
        _fpsLogic.enabled = true;
        _playerShoot.enabled = true;
        hudCanvas.SetActive(true);
        GameManager.instance.EnemyBase.GetComponent<WaveManager>().SpawnWave(0);
    }

    public void ExitState()
    {
        Debug.Log("<<<<<<<< Exit MainGameState >>>>>>>>");
        _fpsLogic.enabled = false;
        _playerShoot.enabled = false;
        hudCanvas.SetActive(false);
    }

    private void UpdateWarning(EnemyWave wave)
    {
        _warningText.text = wave.waveName;
    }
}