using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;

public class ConsoleDisplay : MonoBehaviour
{
    public TMP_Text consoleText;
    public ScrollRect scrollRect;

    StringBuilder logBuilder = new StringBuilder();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logBuilder.AppendLine(logString);

        if (type == LogType.Error || type == LogType.Exception)
        {
            logBuilder.AppendLine(stackTrace);
        }

        consoleText.text = logBuilder.ToString();

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    public void ClearConsole()
    {
        logBuilder.Clear();
        consoleText.text = string.Empty;
    }
}
