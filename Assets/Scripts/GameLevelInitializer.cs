using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelInitializer : MonoBehaviour
{
    private const int _poolSize = 6;

    [Header("Player")]
    [SerializeField] private PlayerEntity _playerEntity;
    [SerializeField] private Transform _cubeHolder;
    [Header("Level")]
    [SerializeField] private GameObject[] _platformPrefabs;
    [SerializeField] private Transform _platformsContainer;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _platformLength;
    [Header("UI")]
    [SerializeField] private UIManager _uiManager;

    private ExternalDeviceInputHandler _externalDeviceInputHandler;
    private MobileInputHandler _mobileInputHandler;
    private List<IEntityInputSource> _handlers;
    private PlayerBrain _playerBrain;
    private Queue<GameObject> _platformsPool;
    private StateType _state;
    private System.Random _random;
    private int _prevCounter = 1;
    private bool _onPause => _state != StateType.PlayMode;
    private void OnNewCube()
    {
        if (_prevCounter != _cubeHolder.childCount)
        {
            if (_prevCounter < _cubeHolder.childCount)
                _playerBrain.IsJump = true;
            _prevCounter = _cubeHolder.childCount;
            for(int i = 0; i < _cubeHolder.childCount; i++)//align
            {
                Transform cube = _cubeHolder.GetChild(i);
                cube.position = new Vector3(
                cube.position.x,
                cube.position.y,
                _cubeHolder.position.z);
            }
        }
    }

    private void Awake()
    {
        _externalDeviceInputHandler = new ExternalDeviceInputHandler();
        _mobileInputHandler = new MobileInputHandler();
        _handlers = new List<IEntityInputSource> {
            _externalDeviceInputHandler,
            _mobileInputHandler
        };
        _playerBrain = new PlayerBrain(_playerEntity, _handlers);
        _platformsPool = new Queue<GameObject>();
        _random = new System.Random(DateTime.Now.Millisecond);
        PlatformsInit();
    }

    private void Update()
    {
        if (_state == StateType.PauseMode)
            CheckMoving();
        if (_onPause)
            return;
        if (_playerEntity.PositionZ > 
            _spawnPoint.position.z - _platformLength * (_poolSize / 2))
            CreatePlatform();
        OnNewCube();
        if (!_playerBrain.OnUpdate())
            ChangeState(StateType.LoseMode);
    }

    private void FixedUpdate()
    {
        if (_onPause)
            return;

        _playerBrain.OnFixedUpdate();
    }

    private void PlatformsInit()
    {
        int count = _poolSize / 2;
        for (int i = 0; i < count; i++)
            CreatePlatform();
    }

    private void CreatePlatform()
    {
        int platformIndex = _random.Next(0, _platformPrefabs.Length);
        GameObject newObj = Instantiate(_platformPrefabs[platformIndex],
             _spawnPoint.position, Quaternion.identity, _platformsContainer);
        _platformsPool.Enqueue(newObj);
        if (_platformsPool.Count > _poolSize)
            DeletePlatform();
        _spawnPoint.position += new Vector3(0f, 0f, _platformLength);
    }

    private void DeletePlatform() => Destroy(_platformsPool.Dequeue());

    private void CheckMoving()
    {
        foreach (var handler in _handlers)
            if (handler.HorizontalDirection != 0f)
            {
                _state = StateType.PlayMode;
                ChangeState(_state);
                break;
            }
    }

    private void ChangeState(StateType state)
    {
        _state = state;
        _uiManager.OnUpdateState(state);
    }
}