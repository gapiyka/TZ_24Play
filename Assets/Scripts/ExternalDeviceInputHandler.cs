using UnityEngine;

public class ExternalDeviceInputHandler : IEntityInputSource
{
    public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
}
