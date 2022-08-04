using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorManager : MonoBehaviour
{
    public Texture2D cursorMove;
    public Texture2D cursorResize;

    public Vector2 cursorSpot;

    GameObject effectorHovered = null;
    GameObject effectorToMoveSelected = null;
    CircleShape effectorToResizeSelected = null;

    public float boundX;
    public float boundY;

    bool toMove = false;
    bool toResize = false;

    float initialRadius = 1f;
    float lastMouseMagnitude = 0f;
    float zIndex;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hitInformation = Physics2D.GetRayIntersection(ray, 100);

        if (Input.GetMouseButtonDown(0))
        {
            if (effectorHovered == null) return;

            if (effectorToMoveSelected == null)
            {
                if (toMove)
                {
                    effectorToMoveSelected = effectorHovered;
                }
                if (toResize)
                {
                    effectorToResizeSelected = effectorHovered.GetComponent<CircleShape>();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (effectorToMoveSelected != null)
            {
                effectorToMoveSelected = null;
            }
            if (effectorToResizeSelected != null)
            {
                effectorToResizeSelected = null;
            }
        }


        if (effectorToMoveSelected != null)
        {
            zIndex = effectorToMoveSelected.transform.position.z;
            if (Mathf.Abs(hitInformation.point.x) > boundX) return;
            if (Mathf.Abs(hitInformation.point.y) > boundY) return;
            effectorToMoveSelected.transform.position = (Vector3)hitInformation.point + new Vector3(0,0,zIndex);
        }

        if (effectorToResizeSelected != null)
        {
            if (hitInformation.point.magnitude != 0) lastMouseMagnitude = ((Vector2)effectorToResizeSelected.transform.position - hitInformation.point).magnitude;

            Debug.Log(((Vector2)effectorToResizeSelected.transform.position - hitInformation.point).magnitude);
            float multiplier = lastMouseMagnitude;
            effectorToResizeSelected.Radius = initialRadius * multiplier;
        }

        if (hitInformation.collider == null)
        {
            Cursor.SetCursor(null, cursorSpot, CursorMode.Auto);
            effectorHovered = null;
            return;
        }

            if (hitInformation.collider.tag == "Effector")
            {
                toMove = true;
                toResize = false;
                Cursor.SetCursor(cursorMove, cursorSpot, CursorMode.Auto);
                return;
            }

            if (hitInformation.collider.tag == "EffectorExterior")
            {
                toMove = false;
                toResize = true;
                effectorHovered = hitInformation.collider.gameObject;
                Cursor.SetCursor(cursorResize, cursorSpot, CursorMode.Auto);
                return;
            }

        toMove = false;
        toResize = false;
        effectorHovered = null;
        Cursor.SetCursor(null, cursorSpot, CursorMode.Auto);
    }
}
