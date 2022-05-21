using UnityEngine;

public class UIGameController : MonoBehaviour
{

    [SerializeField] 
    private GameObject _startScreen;
    [SerializeField] 
    private GameObject _gameScreen;
    [SerializeField] 
    private GameObject _endScreen;
    
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

    private void OnGameOver()
    {
        _startScreen.SetActive(false);
        _gameScreen.SetActive(false);
        _endScreen.SetActive(true);
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
