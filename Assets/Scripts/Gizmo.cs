using System;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    private float sizeOnScreen = 0.2f;
    private float allowedWorldRadius = 100.0f;
    private float sensitivity = 2.0f;

    void Update()
    {
        KeepSizeSameForDisplay();
    }


    // Change this function from delta based to axis-plane projection
    public void MoveGizmoAndFurniture(Vector3 directionVector, Vector2 mouseDelta, float deltaTime, Furniture furniture)
    {
        directionVector.Normalize();

        Vector3 furnitureOriginScreenPos = Camera.main.WorldToScreenPoint(furniture.GetTransform().position);
        Vector3 arrowDirectionScreenPos = Camera.main.WorldToScreenPoint(furniture.GetTransform().position + directionVector);

        Vector2 projectedArrow = new Vector2(arrowDirectionScreenPos.x - furnitureOriginScreenPos.x, arrowDirectionScreenPos.y - furnitureOriginScreenPos.y);

        float movingMagnitude = sensitivity * sizeOnScreen * Screen.width / projectedArrow.magnitude;

        float movement = Vector2.Dot(mouseDelta, projectedArrow.normalized);

        Vector3 translationVector = directionVector * movement * movingMagnitude * deltaTime;

        if ((transform.position + translationVector).magnitude <= allowedWorldRadius)
        {
            transform.position += translationVector;
            furniture.Move(translationVector);
        }
    }

    public void SetTransform(Vector3 position, Vector3 rotation)
    {
        transform.position = position;
        transform.localEulerAngles = rotation;
    }

    private void KeepSizeSameForDisplay()
    {
        Camera cam = Camera.main;

        float distance = Vector3.Distance(transform.position, cam.transform.position);

        float fov = cam.fieldOfView * Mathf.Deg2Rad;

        float scale = 2f * distance * Mathf.Tan(fov / 2f);

        transform.localScale = Vector3.one * scale * sizeOnScreen;
    }
}
