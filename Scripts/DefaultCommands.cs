using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCommands : MonoBehaviour
{
    private bool fpsCounterEnabled = false;
    private float fps;

    void Start()
    {
        if (!PlayerPrefs.HasKey("HelpDisabled") || PlayerPrefs.GetInt("HelpDisabled") == 0)
        {
            Help();
        }

        // Set V-Sync from PlayerPrefs
        if (PlayerPrefs.HasKey("VSync"))
        {
            QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSync");
        }
    }

    void Update()
    {
        // Calculate FPS
        if (fpsCounterEnabled)
        {
            fps = 1.0f / Time.deltaTime;
        }
    }

    void OnGUI()
    {
        if (fpsCounterEnabled)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 12;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.UpperRight;

            GUI.Label(new Rect(Screen.width - 110, 10, 100, 30), $"FPS: {Mathf.RoundToInt(fps)}", style);
        }
    }

    [ConsoleCommand]
    void Help()
    {
        Debug.Log("Default Command List:\n\nHelp  \nStartupHelpActive bool \nClearConsole \nQuit \nSay string \nVSync \nFullscreen \nResolution int int bool \nFPSCounter");
    }

    [ConsoleCommand]
    void ClearConsole()
    {
        for (int i = 0; i < 100; i++)
        {
            Debug.Log("");
        }
    }

    [ConsoleCommand]
    void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    [ConsoleCommand]
    void Say(string message)
    {
        Debug.Log(message);
    }

    [ConsoleCommand]
    void StartupHelpActive(bool active)
    {
        PlayerPrefs.SetInt("HelpDisabled", active ? 0 : 1);
        PlayerPrefs.Save();
        Debug.Log(!active ? "Startup help disabled." : "Startup help enabled.");
    }

    [ConsoleCommand]
    void VSync()
    {
        int currentVSyncCount = QualitySettings.vSyncCount;
        int newVSyncCount = (currentVSyncCount == 0) ? 1 : 0;
        QualitySettings.vSyncCount = newVSyncCount;
        PlayerPrefs.SetInt("VSync", newVSyncCount);
        PlayerPrefs.Save();
        Debug.Log("V-Sync " + (newVSyncCount == 1 ? "enabled." : "disabled."));
    }

    [ConsoleCommand]
    void Fullscreen()
    {
        bool isFullscreen = Screen.fullScreen;
        Screen.fullScreen = !isFullscreen;
        Debug.Log("Fullscreen mode: " + (Screen.fullScreen ? "Disabled" : "Enabled"));
    }

    [ConsoleCommand]
    void Resolution(int width, int height, bool fullscreen)
    {
        Screen.SetResolution(width, height, fullscreen);
        Debug.Log($"Resolution set to {width}x{height}");
    }

    [ConsoleCommand]
    void FPSCounter()
    {
        fpsCounterEnabled = !fpsCounterEnabled;
        if (fpsCounterEnabled)
        {
            Debug.Log("FPS Counter enabled.");
        }
        else
        {
            Debug.Log("FPS Counter disabled.");
        }
    }
}
