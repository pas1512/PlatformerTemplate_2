using UnityEngine;

public class PingPongMovement : MonoBehaviour
{
    [Header("Base settings")]
    [SerializeField] private float _speed = 1; 
    [SerializeField] private float _maxDistance = 1;
    [SerializeField] private float _angle;
    [SerializeField] private float _offset;

    [Header("Sprite renderer settings")]
    [SerializeField] private bool _flip;
    [Tooltip("Do not set this field unless you want the object to face the other side when reversing")]
    [SerializeField] private SpriteRenderer _sr;

    private Vector3 _startPoint;
    private Vector3 _direction;
    private float _prewX;

    private void SetDirection()
    {
        _angle = Mathf.Repeat(_angle, 360);
        float angleRad = _angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(angleRad);
        float y = Mathf.Sin(angleRad);
        _direction = new Vector3(x, y, 0);
    }

    private void OnValidate()
    {
        SetDirection();
    }

    private void Start()
    {
        _startPoint = transform.position;
        _prewX = transform.position.x;
        SetDirection();
    }

    private void Update()
    {
        float time = Time.time * _speed;
        float value = Mathf.PingPong(time + _offset, _maxDistance);
        transform.position = _startPoint + _direction * value;

        float dir = transform.position.x - _prewX;
        _prewX = transform.position.x;

        if (_sr != null)
        {
            if (dir == 0)
                _sr.flipX = _flip;
            else
                _sr.flipX = dir > 0 ^ _flip;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 startP = transform.position;

        if (Application.isPlaying)
            startP = _startPoint;

        Vector3 endP = startP + _direction * _maxDistance;
            
        Gizmos.DrawWireSphere(startP, 0.1f);
        Gizmos.DrawWireSphere(endP, 0.1f);
        Gizmos.DrawLine(startP, endP);

        float offsetDistance = Mathf.PingPong(_offset, _maxDistance);
        Vector3 offsetP = startP + _direction * offsetDistance;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(offsetP, 0.11f);
    }
}