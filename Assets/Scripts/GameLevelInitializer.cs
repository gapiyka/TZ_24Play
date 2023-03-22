using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelInitializer : MonoBehaviour
{
    private const int _poolSize = 6;

    [Header("Player")]
    [SerializeField] private PlayerEntity _playerEntity;

    [Header("Level")]
    [SerializeField] private GameObject[] _platformPrefabs;
    [SerializeField] private Transform _platformsContainer;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _platformLength;

    private ExternalDeviceInputHandler _externalDeviceInputHandler;
    private MobileInputHandler _mobileInputHandler;
    private PlayerBrain _playerBrain;
    private Queue<GameObject> _platformsPool;
    private System.Random _random;
    private bool _onPause;

    private void Awake()
    {
        _externalDeviceInputHandler = new ExternalDeviceInputHandler();
        _mobileInputHandler = new MobileInputHandler();
        _playerBrain = new PlayerBrain(_playerEntity, 
            new List<IEntityInputSource> {
            _externalDeviceInputHandler, 
            _mobileInputHandler 
        });
        _platformsPool = new Queue<GameObject>();
        _random = new System.Random(DateTime.Now.Millisecond);
        PlatformsInit();
    }

    private void Update()
    {
        if (_onPause)
            return;
        if (_playerEntity.PositionZ > 
            _spawnPoint.position.z - _platformLength * (_poolSize / 2))
            CreatePlatform();
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
}