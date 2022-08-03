using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorManager : MonoBehaviour
{
    public Texture2D cursorMove;
    public Texture2D cursorResize;

    public Vector2 cursorSpot;

    GameObject effectorHovered = null;
    GameObject effectorSelected = null;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hitInformation = Physics2D.GetRayIntersection(ray, 10);

        if (Input.GetMouseButtonDown(0))
        {
            if (effectorSelected == null)
            {
                effectorSelected = effectorHovered;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (effectorSelected != null)
            {
                effectorSelected = null;
            }
        }

        if (effectorSelected != null)
        {
            effectorSelected.transform.position = hitInformation.point;
        }

        if (hitInformation.collider == null)
        {
            Cursor.SetCursor(null, cursorSpot, CursorMode.Auto);
            effectorHovered = null;
            return;
        }

            if (hitInformation.collider.tag == "Effector")
            {
                Cursor.SetCursor(cursorMove, cursorSpot, CursorMode.Auto);
                return;
            }

            if (hitInformation.collider.tag == "EffectorExterior")
            {
            effectorHovered = hitInformation.collider.gameObject;
            Cursor.SetCursor(cursorResize, cursorSpot, CursorMode.Auto);
                return;
            }

        effectorHovered = null;
        Cursor.SetCursor(null, cursorSpot, CursorMode.Auto);
    }
}
