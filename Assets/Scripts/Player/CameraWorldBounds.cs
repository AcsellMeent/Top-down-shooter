using UnityEngine;
using Zenject;

[RequireComponent(typeof(Camera))]
public class CameraWorldBounds : MonoBehaviour
{
    private Transform _parent;

    private Camera _camera;

    private Bounds _mapBounds;

    [SerializeField]
    private Vector3 _startPosition;

    private float _viewportXDelta;
    private float _viewportYDelta;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _parent = transform.parent;
        _viewportXDelta = _parent.position.x - _camera.ViewportToWorldPoint(Vector3.zero).x;
        _viewportYDelta = _parent.position.y - _camera.ViewportToWorldPoint(Vector3.zero).y;
    }

    [Inject]
    public void Construct(MapLimiter mapLimiter)
    {
        _mapBounds = mapLimiter.GetBounds();
    }

    private void Update()
    {
        float x = Mathf.Clamp(_parent.position.x, _mapBounds.min.x + _viewportXDelta, _mapBounds.max.x - _viewportXDelta);
        float y = Mathf.Clamp(_parent.position.y, _mapBounds.min.y + _viewportYDelta, _mapBounds.max.y - _viewportYDelta);
        transform.position = new Vector3(x, y, -10);
    }
}
