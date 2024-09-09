using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ConsoleSystem : MonoBehaviour
{
    Dictionary<string, MethodInfo> _commands = new Dictionary<string, MethodInfo>();

    void Start()
    {
        RegisterCommands();
    }

    void RegisterCommands()
    {
        var methods = Assembly.GetExecutingAssembly()
            .GetTypes()
            .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            .Where(m => m.GetCustomAttributes(typeof(ConsoleCommandAttribute), false).Length > 0);

        foreach (var method in methods)
        {
            var attribute = (ConsoleCommandAttribute)method.GetCustomAttributes(typeof(ConsoleCommandAttribute), false)[0];
            var commandName = attribute.CommandName ?? method.Name;
            _commands[commandName.ToLower()] = method;
        }
    }

    public void ExecuteCommand(string commandInput)
    {
        if (string.IsNullOrEmpty(commandInput))
        {
            Debug.LogError("No command entered.");
            return;
        }

        string[] inputParts = commandInput.Split(' ');
        string commandName = inputParts[0].ToLower();
        string[] args = inputParts.Skip(1).ToArray();

        if (!_commands.TryGetValue(commandName, out var method))
        {
            Debug.LogError($"Command '{commandName}' not found.");
            return;
        }

        var parameters = method.GetParameters();
        if (args.Length != parameters.Length)
        {
            Debug.LogError($"Command '{commandName}' expects {parameters.Length} argument(s), but {args.Length} were provided.");
            return;
        }

        try
        {
            object[] convertedArgs = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                convertedArgs[i] = Convert.ChangeType(args[i], parameters[i].ParameterType);
            }

            if (method.IsStatic)
            {
                method.Invoke(null, convertedArgs);
            }
            else
            {
                var instance = FindObjectOfType(method.DeclaringType);
                if (instance != null)
                {
                    method.Invoke(instance, convertedArgs);
                }
                else
                {
                    Debug.LogError($"Instance of '{method.DeclaringType.Name}' not found in the scene.");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error executing command '{commandName}': {ex.Message}");
        }
    }
}
