using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransformUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField[] inputFieldArray;

    private void Start()
    {
        foreach (TMP_InputField inputField in inputFieldArray)
        {
            inputField.onValueChanged.AddListener(InputField_onValueChanged);
        }
    }

    private void InputField_onValueChanged(string value)
    {
        float[] valueArray = new float[inputFieldArray.Length];

        for (int i = 0; i < inputFieldArray.Length; i++)
        { 
            string inputText = inputFieldArray[i].text;
            if (float.TryParse(inputText, NumberStyles.Float, CultureInfo.CurrentCulture, out float floatValue))
            {
                valueArray[i] = floatValue;
            }
        }

        Vector3 position = new Vector3(valueArray[0], valueArray[1], valueArray[2]);
        Vector3 rotation = new Vector3(valueArray[3], valueArray[4], valueArray[5]);
        Vector3 scale = new Vector3(valueArray[6], valueArray[7], valueArray[8]);

        SelectionManager.Instance.UpdateSelectedTransform(position, rotation, scale);
    }

    public void UpdateTransformValues(Transform transform)
    {
        float[] valueArray = new float[inputFieldArray.Length];
        valueArray[0] = transform.position.x;
        valueArray[1] = transform.position.y;
        valueArray[2] = transform.position.z;

        valueArray[3] = transform.localEulerAngles.x;
        valueArray[4] = transform.localEulerAngles.y;
        valueArray[5] = transform.localEulerAngles.z;

        valueArray[6] = transform.localScale.x;
        valueArray[7] = transform.localScale.y;
        valueArray[8] = transform.localScale.z;

        for (int i = 0; i < inputFieldArray.Length; i++)
        { 
            valueArray[i] = Mathf.Round(valueArray[i] * 1000) / 1000.0f;
            inputFieldArray[i].text = valueArray[i].ToString();
        }
    }

}
