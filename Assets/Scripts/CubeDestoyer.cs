using System.Collections;
using UnityEngine;

public class CubeDestoyer : MonoBehaviour
{
    private const string _wallTag = "Obstacle";
    private const float _minDif = 1f;
    private const float _delay = 1f;
    private bool _isExecuting;
    private float _collisionPosZ;
    private bool IsSamePos => transform.position.z - _collisionPosZ <_minDif;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == _wallTag && !_isExecuting)
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
            transform.position = new Vector3(
                transform.position.x, 
                transform.position.y, 
                transform.parent.position.z);
        _isExecuting = false;
    }
}
