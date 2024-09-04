using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class OpenInputFieldOnKey : MonoBehaviour
{
    public TMP_InputField inputField;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            EventSystem.current.SetSelectedGameObject(inputField.gameObject);

            inputField.ActivateInputField();
        }
    }
}
