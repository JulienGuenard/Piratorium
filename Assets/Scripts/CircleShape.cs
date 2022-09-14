using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class CircleShape : MonoBehaviour
{
    #region Show in inspector

    [Min(3)]
    [SerializeField] private int _segmentCount;

    [Range(0.01f, 5)]
    [SerializeField] private float _radius;

    [Range(0, 1)]
    [SerializeField] private float _colliderMargin;

    #endregion


    #region Public properties

    public float Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            UpdateCircle();
        }
    }

    #endregion


    #region Init

    private void Reset()
    {
        _segmentCount = 32;
        _radius = 1;
        _colliderMargin = 0;
        CircleCollider.offset = Vector2.zero;

#if UNITY_EDITOR
        LineRenderer.material = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Line.mat");
#endif

        LineRenderer.startWidth = .1f;
        LineRenderer.endWidth = .1f;
        UpdateCircle();
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _circleCollider.offset = Vector2.zero;

        LineRenderer.useWorldSpace = false;
        LineRenderer.loop = true;
        LineRenderer.positionCount = Mathf.Max(_segmentCount + 1, 0);
        CreatePoints();

        CircleCollider.radius = _radius + LineRenderer.startWidth;
    }

    #endregion


    #region Circle update

    public void UpdateCircle()
    {
        LineRenderer.useWorldSpace = false;
        LineRenderer.loop = true;
        LineRenderer.positionCount = Mathf.Max(_segmentCount + 1, 0);
        LineRenderer.numCornerVertices = 2;

        CreatePoints();

        CircleCollider.radius = _radius + _colliderMargin + LineRenderer.startWidth / 2;
    }

    #endregion


    #region Create circle

    private void CreatePoints()
    {
        if (LineRenderer.positionCount <= 2)
        {
            LineRenderer.SetPositions(new Vector3[0]);
            return;
        }

        Vector3[] positions = new Vector3[LineRenderer.positionCount];

        float x;
        float y;
        float z = 0f;

        float angle = 360f / _segmentCount;

        for (int i = 0; i < LineRenderer.positionCount; i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * _radius;

            positions[i] = new Vector3(x, y, z);

            angle += 360f / _segmentCount;
        }

        LineRenderer.SetPositions(positions);
    }

    #endregion


    #region Private

    private LineRenderer _lineRenderer;

    private LineRenderer LineRenderer
    {
        get
        {
            if (_lineRenderer == null)
            {
                _lineRenderer = GetComponent<LineRenderer>();
            }
            return _lineRenderer;
        }
    }

    private CircleCollider2D _circleCollider;
    private CircleCollider2D CircleCollider
    {
        get
        {
            if (_circleCollider == null)
            {
                _circleCollider = GetComponent<CircleCollider2D>();
            }
            return _circleCollider;
        }
    }

    #endregion
}