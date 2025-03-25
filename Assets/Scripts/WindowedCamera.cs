using UnityEngine;

public class WindowedCamera : MonoBehaviour
{
    [SerializeField] private Vector2 _widowSize = Vector2.one;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private float _maxSpeed = 8;
    [SerializeField] private Transform _target;

    private bool _fit;
    private bool _inWindow;


    private bool InWindow(Vector2 player, Vector2 camera, Vector2 size)
    {
        Vector2 offset = player - camera;
        bool inVertical = Mathf.Abs(offset.y) <= size.y / 2;
        bool inHorizontal = Mathf.Abs(offset.x) <= size.x / 2;
        return inVertical && inHorizontal;
    }

    private void LateUpdate()
    {
        if (_fit)
        {
            Vector2 current = new Vector2(transform.position.x, transform.position.y);
            Vector2 target = (Vector2)_target.position - _offset;
            Vector2 offset = (target - current) * Time.deltaTime;
            offset = Vector2.ClampMagnitude(offset, _maxSpeed);
            Vector3 result = transform.position + (Vector3)offset;

            if (Vector2.Distance(result, target) < 0.1f)
                _fit = false;

            transform.position = result;
        }
        else
        {
            _inWindow = InWindow(_target.position, transform.position, _widowSize);
            _fit = !_inWindow;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + Vector3.forward * 10, _widowSize);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.forward * 10 + (Vector3)_offset, 0.25f);
    }
}