using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;

    private GameObject _enemyBase;
    private GameObject _homeBase;
    private StateMachine _stateMachine;

    public GameObject HomeBase
    {
        get => _homeBase;
        set
        {
            _homeBase = value;
            StartMainGame();
        }
    }

    public GameObject EnemyBase
    { 
        get => _enemyBase;
        set 
        {
            _enemyBase = value;
            StartHomeBasePlacing();
        } 
    }

    [Header("Main Game References")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject mainGameCanvas;
    [SerializeField] private GameObject gameOverCanvas;

    [Header("PlaneScanning References")]
    [SerializeField] private GameObject planeScanningCanvas;
    [SerializeField] private GameObject enemyBasePrefab;

    [Header("HomeBasePlacing References")]
    [SerializeField] private GameObject homeBasePlacing;


    private void Awake()
    {
        if(instance != null && instance!= this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        _stateMachine = new StateMachine();
        
        _stateMachine.ChangeState(new MainMenuState(mainMenuCanvas));
        _stateMachine.ExecuteStateUpdate();
    }

    public void StartPlaneScanning()
    {
        _stateMachine.ChangeState(new PlaneScanningState(planeScanningCanvas, enemyBasePrefab));
        _stateMachine.ExecuteStateUpdate();
    }

    private void StartHomeBasePlacing()
    {
        _stateMachine.ChangeState(new HomeBasePlacing(homeBasePlacing));
        _stateMachine.ExecuteStateUpdate();
    }

    private void StartMainGame()
    {
        _stateMachine.ChangeState(new MainGameState(mainGameCanvas, _enemyBase));
        _stateMachine.ExecuteStateUpdate();
    }

    public void StartGameOver()
    {
        _stateMachine.ChangeState(new GameOverState(gameOverCanvas));
        _stateMachine.ExecuteStateUpdate();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void RestartApplication()
    {
        StartMainGame();
    }
}
