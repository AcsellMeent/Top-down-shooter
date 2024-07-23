using UnityEngine;

public class MapLimiter : MonoBehaviour
{
    [SerializeField]
    private float _width;

    [SerializeField]
    private float _height;

    [SerializeField]
    private BoxCollider2D _left;

    [SerializeField]
    private BoxCollider2D _top;

    [SerializeField]
    private BoxCollider2D _right;

    [SerializeField]
    private BoxCollider2D _bottom;

    private void OnValidate()
    {
        if (!_left || !_top || !_right || !_bottom) return;

        _left.size = new Vector2(1, _height);
        _left.offset = new Vector2(-_width / 2 - 0.5f, 0);

        _right.size = new Vector2(1, _height);
        _right.offset = new Vector2(_width / 2 + 0.5f, 0);


        _top.size = new Vector2(_width, 1);
        _top.offset = new Vector2(0, _height / 2 + 0.5f);

        _bottom.size = new Vector2(_width, 1);
        _bottom.offset = new Vector2(0, -_height / 2 - 0.5f);
    }

    private void Awake()
    {
    }

    public Vector2 GetSize()
    {
        return new Vector2(_width, _height);
    }

    public Bounds GetBounds()
    {
        Bounds bounds = new Bounds(transform.position, GetSize());
        return bounds;
    }
}
