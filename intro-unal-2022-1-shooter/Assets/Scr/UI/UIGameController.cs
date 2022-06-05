using UnityEngine;

public class UIGameController : MonoBehaviour
{

    [SerializeField] 
    private GameObject _startScreen;
    [SerializeField] 
    private GameObject _gameScreen;
    [SerializeField] 
    private GameObject _endScreen;
    [SerializeField] 
    private HUDEndScreen _hudEndScreen;
    
    void Start()
    {
        GameEvents.OnStartScreenEvent += OnStartScreen;
        GameEvents.OnStartGameEvent += OnStartGame;
        GameEvents.OnGameOverEvent += OnGameOver;
    }

    private void OnDestroy()
    {
        GameEvents.OnStartScreenEvent -= OnStartScreen;
        GameEvents.OnStartGameEvent -= OnStartGame;
        GameEvents.OnGameOverEvent -= OnGameOver;
    }

    private void OnStartScreen()
    {
        _startScreen.SetActive(true);
        _gameScreen.SetActive(false);
        _endScreen.SetActive(false);
    }

    private void OnStartGame()
    {
        _startScreen.SetActive(false);
        _gameScreen.SetActive(true);
        _endScreen.SetActive(false);
    }

    private void OnGameOver(int score, bool isMaxScore, float time, int level)
    {
        _startScreen.SetActive(false);
        _gameScreen.SetActive(false);
        _endScreen.SetActive(true);
        
        _hudEndScreen.SetResults(score, isMaxScore, time, level);
    }
    
    //Called from a Unity button
    public void ButtonStartGame()
    {
        GameManager.Instance.StartGame();
    }
    
    //Called from a Unity button
    public void ButtonBack()
    {
        GameManager.Instance.ReturnToMenu();
    }
}
