using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformMenu : MonoBehaviour
{
    public event Action OnOpened;
    public event Action OnClosed;
    
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _pauseText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    
    [SerializeField] private Button _toLevelMap;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _continue;
    
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;

    [SerializeField] private float _speedAnim;
    
    private GameStateMachine _gameStateMachine;

    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    public void SetHighScore(int score)
    {
        if (score > 0)
            _highScoreText.text = "HIGH SCORE - " + score;
        else
            _highScoreText.text = score.ToString();
    }

    private void Awake()
    {
        _continue.onClick.AddListener(() => ClosePauseMenu());
        _restart.onClick.AddListener(() => _gameStateMachine.Enter<LoadLevelState>(SceneNavigator.GetCurrentLvlName()));
        _toLevelMap.onClick.AddListener( () => _gameStateMachine.Enter<LoadMenuState>()); 
    }    
    
    public void OpenPauseMenu()
    {
        _pauseText.SetActive(true);
        _buttons.SetActive(true);
        StartCoroutine(OpenMenu());
    }
    
    public void ClosePauseMenu(float delay = 0)
    {
        StartCoroutine(CloseMenu(delay));
    }
    
    private IEnumerator OpenMenu()
    {
        while ((_start.position.y - transform.position.y) >= 0.01f)
        {
            transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, _start.position.y, _speedAnim));
            yield return null;
        }
        
        OnOpened?.Invoke();
    }

    private IEnumerator CloseMenu(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        
        while (Math.Abs(_end.position.y - transform.position.y) >= 0.01f)
        {
            transform.position = new Vector3(transform.position.x,
                Mathf.MoveTowards(transform.position.y, _end.position.y, _speedAnim));
            yield return null;
        }
        
        OnClosed?.Invoke();
    }
}