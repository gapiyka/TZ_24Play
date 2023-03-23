using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _startTip;
    [SerializeField] private Button _restartButton;

    private Dictionary<StateType, Action> _actions;

    private void Awake()
    {
        _restartButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        _actions = new Dictionary<StateType, Action>
        {
            {StateType.PauseMode, OnPause},
            {StateType.PlayMode, OnPlay},
            {StateType.LoseMode, OnLose},
        };
    }

    public void OnUpdateState(StateType state) => _actions[state]();

    private  void OnPause() => _startTip.SetActive(true);
    
    private  void OnPlay() => _startTip.SetActive(false);

    private  void OnLose() =>_losePanel.SetActive(true);
}
