using System;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultVisualGameobject;
    [SerializeField]
    private GameObject selectedVisualGameobject;

    public void Select()
    {
        defaultVisualGameobject.SetActive(false);
        selectedVisualGameobject.SetActive(true);
    }

    public void Deselect()
    {
        defaultVisualGameobject.SetActive(true);
        selectedVisualGameobject.SetActive(false);
    }

    public void SetTransform(Vector3 position, Vector3 rotation, Vector3 scale)
    {
        transform.position = position;
        transform.localEulerAngles = rotation;
        transform.localScale = scale;
    }

    public Transform GetTransform()         // Use this instead of gameobject.transform
    {
        return transform;
    }

    public void Move(Vector3 moveVector)
    {
        transform.position += moveVector;
    }
}
