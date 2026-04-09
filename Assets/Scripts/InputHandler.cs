using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    public event EventHandler<MouseClickEventArgs> OnMouseClick;
    public event EventHandler<DefaultVector2EventArgs> OnWASDKeyPress;
    public event EventHandler<DefaultVector2EventArgs> OnMouseScroll;
    public event EventHandler<DefaultVector2EventArgs> OnMouseDelta;

    public class MouseClickEventArgs : EventArgs
    {
        public Vector2 MousePosition { get; }
        public Vector2 MouseDelta { get; }
        public bool ClickedMouseLastFrame { get; }

        public MouseClickEventArgs(Vector2 mousePosition, Vector2 mouseDelta, bool clickedMouseLastFrame)
        {
            MousePosition = mousePosition;
            MouseDelta = mouseDelta;
            ClickedMouseLastFrame = clickedMouseLastFrame;
        }
    }

    public class DefaultVector2EventArgs : EventArgs
    {
        public Vector2 Vector { get; }

        public DefaultVector2EventArgs(Vector2 vector)
        {
            Vector = vector;
        }
    }

    private InputSystem_Actions inputSystemActions;
    private bool clickedMouseLastFrame;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        inputSystemActions = new InputSystem_Actions();
        inputSystemActions.User.Enable();
    }

    private void Update()
    {
        Select();
        WASDKeyPress();
        MouseScroll();
        MouseDelta();
    }

    private void Select()
    {
        if (inputSystemActions.User.Select.IsPressed() && !inputSystemActions.User.Alt.IsPressed())
        {
            Vector2 mouseDelta = inputSystemActions.User.MouseDelta.ReadValue<Vector2>();
            Vector2 mousePosition = inputSystemActions.User.MousePosition.ReadValue<Vector2>();

            OnMouseClick?.Invoke(this, new MouseClickEventArgs(mousePosition, mouseDelta, clickedMouseLastFrame));
            clickedMouseLastFrame = true;
        }
        else
        {
            clickedMouseLastFrame = false;
        }
    }

    private void WASDKeyPress()
    {
        Vector2 inputVector = inputSystemActions.User.WASD.ReadValue<Vector2>();
        OnWASDKeyPress?.Invoke(this, new DefaultVector2EventArgs(inputVector));
    }

    private void MouseScroll()
    {
        Vector2 inputVector = inputSystemActions.User.Zoom.ReadValue<Vector2>();
        OnMouseScroll?.Invoke(this, new DefaultVector2EventArgs(inputVector));
    }

    private void MouseDelta()
    {
        Vector2 inputVector = inputSystemActions.User.Turn.ReadValue<Vector2>();
        OnMouseDelta?.Invoke(this, new DefaultVector2EventArgs(inputVector));
    }
}
