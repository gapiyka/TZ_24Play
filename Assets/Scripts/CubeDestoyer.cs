using System.Collections;
using UnityEngine;

public class CubeDestoyer : MonoBehaviour
{
    private const float _minDif = 3f;
    private const float _delay = 1f;
    private bool _isExecuting;
    private float _collisionPosZ;
    private bool IsSamePos => transform.position.z - _collisionPosZ <_minDif;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == TagConsts.Wall && !_isExecuting)
            StartCoroutine(DestroyCube());
    }

    private IEnumerator DestroyCube()
    {
        _isExecuting = true;
        _collisionPosZ = transform.position.z;
        yield return new WaitForSeconds(_delay);
        if (IsSamePos)
            Destroy(transform.gameObject);
        else
        {
            yield return new WaitForSeconds(_delay);
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.parent.position.z);
        }
        _isExecuting = false;
    }
}
