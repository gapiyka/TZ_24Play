using UnityEngine;

public class MobileInputHandler : IEntityInputSource
{
    private float _direction = 0f;

    public float HorizontalDirection => GetDirection();

    private float GetDirection()
    {
        if (Input.touchCount > 0)
        {
            Vector3 touchPosition = Input.GetTouch(0).position;
            if (touchPosition.x > Screen.width * 0.5f)
                _direction = 1;
            else
                _direction = -1;
        }
        else _direction = 0;
        return _direction;
    }
}

