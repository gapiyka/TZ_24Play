using UnityEngine;

public class CubeHolder : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private Vector3 _offset = new Vector3(0f, 1f, 0f);

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == TagConsts.Pick)
            Pick(collider.transform.parent);
    }

    private void Pick(Transform obj)
    {
        int childIndex = transform.childCount - 1;
        Vector3 position = transform.GetChild(childIndex).GetChild(0).position;
        position += _offset;
        Instantiate(_prefab, position, Quaternion.identity, transform);
        Destroy(obj.gameObject);
    }
}
