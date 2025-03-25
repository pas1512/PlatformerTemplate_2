using UnityEngine;

public class StepedMovement : MonoBehaviour
{
    [Header("Base settings")]
    [Tooltip("Set the animation time here if you want the object to take one step per animation iteration.")]
    [SerializeField] private float _stepTime = 1;
    [SerializeField] private AnimationCurve _stepCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private float _maxDistance = 1;
    [SerializeField] private float _angle;

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

        if(_stepTime < 0)
            _stepTime = 0;

        if(_maxDistance < 0)
            _maxDistance = 0;

        _angle = Mathf.Repeat(_angle, 360);
    }

    private void Start()
    {
        _startPoint = transform.position;
        _prewX = transform.position.x;

        if(_sr != null)
            _sr.flipX = _flip;

        SetDirection();
    }

    private void Update()
    {
        float time = Time.time;
        float step = Mathf.Floor(time / _stepTime);
        float stepTime = time % _stepTime;
        float stepValue = stepTime / _stepTime;
        stepValue = _stepCurve.Evaluate(stepValue);
        float pingPong = Mathf.PingPong(step + stepValue, _maxDistance);

        Vector3 newPostion = _startPoint + _direction * pingPong;
        transform.position = newPostion;

        float dir = transform.position.x - _prewX;
        _prewX = transform.position.x;

        if (_sr == null)
            return;

        if (dir != 0)
        {
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
    }
}