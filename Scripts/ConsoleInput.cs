using TMPro;
using UnityEngine;

public class ConsoleInput : MonoBehaviour
{
    public TMP_InputField consoleInputField;
    public ConsoleSystem consoleSystem;

    void Start()
    {
        consoleInputField.onEndEdit.AddListener(OnConsoleInputSubmit);
    }

    void OnConsoleInputSubmit(string input)
    {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(input))
        {
            consoleSystem.ExecuteCommand(input);
            consoleInputField.text = string.Empty;
        }
    }
}
