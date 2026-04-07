using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private void Start()
    {
        InputHandler.Instance.OnWASDKeyPress += InputHandler_OnWASDKeyPress;
        InputHandler.Instance.OnMouseScroll += InputHandler_OnMouseScroll;
        InputHandler.Instance.OnMouseDelta += InputHandler_OnMouseDelta;
    }

    private void InputHandler_OnWASDKeyPress(object sender, InputHandler.DefaultVector2EventArgs eventArgs)
    {
        MoveCamera(eventArgs.Vector);
    }

    private void InputHandler_OnMouseScroll(object sender, InputHandler.DefaultVector2EventArgs eventArgs)
    {
        ZoomCamera(eventArgs.Vector);
    }

    private void InputHandler_OnMouseDelta(object sender, InputHandler.DefaultVector2EventArgs eventArgs)
    {
        CameraRotate(eventArgs.Vector);
    }

    private void MoveCamera(Vector2 inputVector)
    {
        float speed = 2f;
        Vector3 updatedPosition = transform.position;
        updatedPosition += speed * inputVector.x * transform.right * Time.deltaTime;
        updatedPosition += speed * inputVector.y * transform.forward * Time.deltaTime;

        transform.position = updatedPosition;
    }

    private void ZoomCamera(Vector2 inputVector)
    {
        float speed = 100f;
        Vector3 updatedPosition = transform.position + transform.forward * inputVector.y * speed * Time.deltaTime;

        transform.position = updatedPosition;
    }

    private void CameraRotate(Vector2 inputVector)
    {
        float rotationSpeed = 2f;
        float distance = 5f;
        Vector3 pivot = transform.position + distance * transform.forward;

        transform.RotateAround(pivot, Vector3.up, inputVector.x * rotationSpeed);

        Vector3 right = transform.right;
        transform.RotateAround(pivot, right, -inputVector.y * rotationSpeed);
    }
}
