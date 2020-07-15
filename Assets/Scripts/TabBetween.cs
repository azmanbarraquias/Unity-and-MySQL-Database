using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class TabBetween : MonoBehaviour
{
    public TMP_InputField nextField;

    private TMP_InputField currentField;

    public bool startThis = false;

    private void Start()
    {
        if (nextField == null)
        {
            Destroy(this);
            return;
        }

        currentField = GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (currentField.isFocused && Input.GetKeyDown (KeyCode.Tab))
        {
            nextField.ActivateInputField();
        }

        if (startThis && !currentField.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            currentField.ActivateInputField();
        }
    }

}
