# Console Command System for Unity

## Overview

This package provides a simple console command system for Unity that allows you to register, execute, and display console commands in your game. It consists of several components that work together to handle commands, display output, and capture user input.

## Dependencies

- Text Mesh Pro

## How to Use

1. Add the "ConsoleCanvas" prefab to your scene.
2. Go to the function that you want to activate with a command in the console.
3. Add "[ConsoleCommand]" to the line before your function.
4. Run the project and type your function name followed by any parameters specified in your function.

## Example Code

using UnityEngine;
public class PlayerDebugging : MonoBehaviour
{
    [ConsoleCommand]
    public void DebugMessage(string message)
    {
        Debug.Log(message);
    }
}

## Example Console Command

DebugMessage myMessageThatIWantToPrint

## Example Console Output

myMessageThatIWantToPrint
