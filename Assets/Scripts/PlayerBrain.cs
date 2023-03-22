using System.Collections.Generic;

public class PlayerBrain
{
    public bool IsJump { private get; set; }
    private readonly PlayerEntity _playerEntity;
    private readonly List<IEntityInputSource> _inputSources;
    private float _direction;

    public PlayerBrain(PlayerEntity player, List<IEntityInputSource> inputSources)
    {
        _playerEntity = player;
        _inputSources = inputSources;
    }

    public void OnFixedUpdate()
    {
        _playerEntity.Move(GetDirection());
        if (IsJump)
        {
            _playerEntity.Jump();
            IsJump = false;
        }
    }

    private float GetDirection()
    {
        foreach (var input in _inputSources)
        {
            _direction = input.HorizontalDirection;

            if (_direction == 0f)
                continue;

            return _direction;
        }
        return _direction;
    }
}