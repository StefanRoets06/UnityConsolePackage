using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenInputFieldOnKey : MonoBehaviour
{
    public TMP_InputField inputField;
    public bool lockCursor = true;

    bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote) || (Input.GetKeyDown(KeyCode.Return) && isActive))
        {
            isActive = !isActive;
        }

        if (isActive)
        {
            EventSystem.current.SetSelectedGameObject(inputField.gameObject);
            inputField.ActivateInputField();

            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(inputField.gameObject);
            inputField.DeactivateInputField();
            inputField.text = "";

            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
