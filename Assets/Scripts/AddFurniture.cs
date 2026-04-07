using UnityEngine;
using UnityEngine.UI;

public class AddFurniture : MonoBehaviour
{
    [SerializeField]
    private FurnitureSO[] furnitureSOArray;
    [SerializeField]
    private Button spawnFurnitureButton;
    [SerializeField]
    private Button deleteFurnitureButton;
    [SerializeField]
    private Button[] furnitureTypeButtonArray;
    [SerializeField]
    private Image[] furnitureTypeImageArray;
    [SerializeField]
    private Image[] furnitureBackgroundImageArray;

    private GameObject selectedFurniture;

    private void Start()
    {
        spawnFurnitureButton.onClick.AddListener(SpawnFurniture);
        deleteFurnitureButton.onClick.AddListener(DeleteFurniture);

        SetDefaultButtonBackgroundColor();

        for (int i = 0; i < furnitureSOArray.Length; i++)
        {
            int index = i;
            furnitureTypeImageArray[i].sprite = furnitureSOArray[i].furnitureSprite;

            furnitureTypeButtonArray[i].onClick.AddListener(() =>
            {
                selectedFurniture = furnitureSOArray[index].furniturePrefab;
                HighlightSelectedFurnitureImageByIndex(index);
            });
        }
    }
    
    private void HighlightSelectedFurnitureImageByIndex(int index)
    {
        SetDefaultButtonBackgroundColor();

        furnitureBackgroundImageArray[index].color = Color.skyBlue;
    }

    private void SetDefaultButtonBackgroundColor()
    {
        foreach (Image background in furnitureBackgroundImageArray)
        {
            background.color = Color.lightGray;
        }
    }

    private void SpawnFurniture()
    {
        if (selectedFurniture != null)
        {
            GameObject.Instantiate(selectedFurniture);
            SelectionManager.Instance.HandleDeselect();
        }
    }

    private void DeleteFurniture()
    {
        SelectionManager.Instance.HandleDelete();
    }


}
