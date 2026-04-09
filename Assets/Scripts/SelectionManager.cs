using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance  { get; private set; }

    [SerializeField]
    private Camera userCamera;
    [SerializeField]
    private LayerMask furnitureLayerMask;
    [SerializeField]
    private LayerMask uiLayerMask;
    [SerializeField]
    private GameObject transformUIGamobject;
    [SerializeField]
    private GameObject moveGizmoPrefab;
    [SerializeField]
    private LayerMask gizmoLayerMask;


    private GameObject selectedFurnitureGameobject;
    private bool clickRequested;
    private Vector2 mousePosition;
    private Vector2 mouseDelta;
    private bool clickedMouseLastFrame;
    private Furniture furniture;
    private GameObject moveGizmoGameobject;
    private GameObject hitArrowGameobject;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InputHandler.Instance.OnMouseClick += InputHandler_OnMouseClick;
        transformUIGamobject.SetActive(false);
    }

    private void Update()
    {
        if (!clickRequested) return;

        HandleMouseClick();

        clickRequested = false;
    }

    private void InputHandler_OnMouseClick(object sender, InputHandler.MouseClickEventArgs mouseClickEventArgs)
    {
        clickRequested = true;
        mousePosition = mouseClickEventArgs.MousePosition;
        mouseDelta = mouseClickEventArgs.MouseDelta;
        clickedMouseLastFrame = mouseClickEventArgs.ClickedMouseLastFrame;
    }

    private void HandleMouseClick()
    {
        Ray ray = userCamera.ScreenPointToRay(mousePosition);

        if (EventSystem.current.IsPointerOverGameObject()) return;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, gizmoLayerMask) || (clickedMouseLastFrame && hitArrowGameobject != null))
        {
            HandleMoveGizmo(hit);
        }
        else if (Physics.Raycast(ray, out hit, 100f, furnitureLayerMask))
        {
            hitArrowGameobject = null;
            HandleSelect(hit);
        }
        else
        {
            if (selectedFurnitureGameobject != null)
            {
                HandleDeselect();
            }
        }
    }

    private void HandleMoveGizmo(RaycastHit hit)
    {
        if (!clickedMouseLastFrame || hitArrowGameobject == null)
        {
            hitArrowGameobject = hit.collider.gameObject;
        }

        Vector3 arrowDirection = hitArrowGameobject.transform.up;
        Gizmo gizmo = moveGizmoGameobject.GetComponent<Gizmo>();   

        gizmo.MoveGizmoAndFurniture(arrowDirection, mouseDelta, Time.deltaTime, furniture);
        transformUIGamobject.GetComponent<TransformUI>().UpdateTransformValues(furniture.GetTransform());
    }

    public void HandleDeselect()
    {
        if (selectedFurnitureGameobject == null) return;

        furniture = selectedFurnitureGameobject.GetComponent<Furniture>();
        furniture.Deselect();
        selectedFurnitureGameobject = null;
        furniture = null;
        transformUIGamobject.SetActive(false);
        Destroy(moveGizmoGameobject);
    }

    private void HandleSelect(RaycastHit hit)
    {
        if (selectedFurnitureGameobject != null && selectedFurnitureGameobject == hit.collider.gameObject) return;

        HandleDeselect();

        selectedFurnitureGameobject = hit.collider.gameObject;

        furniture = selectedFurnitureGameobject.GetComponent<Furniture>();
        furniture.Select();
        
        transformUIGamobject.SetActive(true);
        transformUIGamobject.GetComponent<TransformUI>().UpdateTransformValues(furniture.GetTransform());

        Renderer renderer = selectedFurnitureGameobject.GetComponentInChildren<Renderer>();
        Vector3 center = renderer.bounds.center;

        moveGizmoGameobject = Instantiate(moveGizmoPrefab, center, selectedFurnitureGameobject.transform.rotation);
    }

    public void UpdateSelectedTransform(Vector3 position, Vector3 rotation, Vector3 scale)
    {
        if (furniture != null)
        {
            furniture.SetTransform(position, rotation, scale);

            if (moveGizmoGameobject != null)
            {
                Renderer renderer = selectedFurnitureGameobject.GetComponentInChildren<Renderer>();
                Vector3 center = renderer.bounds.center;
                Gizmo gizmo = moveGizmoGameobject.GetComponent<Gizmo>();
                gizmo.SetTransform(center, rotation); 
            }
        }
    }

    public void HandleDelete()
    {
        if (selectedFurnitureGameobject != null)
        {
            furniture = null;
            transformUIGamobject.SetActive(false);
            Destroy(moveGizmoGameobject);
            Destroy(selectedFurnitureGameobject);
        }
    }

}
