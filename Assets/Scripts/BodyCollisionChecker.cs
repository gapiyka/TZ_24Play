using UnityEngine;

public class BodyCollisionChecker : MonoBehaviour
{
    public bool IsTouchedWall { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == TagConsts.Wall)
            IsTouchedWall = true;
    }
}
