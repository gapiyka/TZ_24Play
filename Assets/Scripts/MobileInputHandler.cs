using UnityEngine;

public class MobileInputHandler : IEntityInputSource
{
    private float _direction = 0f;

    public float HorizontalDirection => GetDirection();

    private float GetDirection()
    {
        int touches = Input.touchCount;
        if (touches > 0)
        {
            Vector3 touchPosition = Input.GetTouch(Input.touchCount-1).position;
            if (touchPosition.x > Screen.width * 0.5f)
                _direction = 1;
            else
                _direction = -1;
        }
        else _direction = 0;
        return _direction;
    }
}

