using System.Collections.Generic;
using UnityEngine;

public class GameLevelInitializer : MonoBehaviour
{
    [SerializeField] private PlayerEntity _playerEntity;

    private ExternalDeviceInputHandler _externalDeviceInputHandler;
    private MobileInputHandler _mobileInputHandler;
    private PlayerBrain _playerBrain;

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
    }

    private void FixedUpdate()
    {
        if (_onPause)
            return;

        _playerBrain.OnFixedUpdate();
    }
}