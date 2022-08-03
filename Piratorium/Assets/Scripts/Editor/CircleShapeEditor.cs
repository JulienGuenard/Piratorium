using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CircleShape))]
public class CircleShapeEditor : Editor
{
    private void OnEnable()
    {
        _script = target as CircleShape;
    }

    public override void OnInspectorGUI()
    {
        using (var changeScope = new EditorGUI.ChangeCheckScope())
        {
            DrawDefaultInspector();

            if (changeScope.changed)
            {
                _script.UpdateCircle();
            }
        }
    }

    private CircleShape _script;
}
