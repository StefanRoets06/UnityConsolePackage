using System;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class ConsoleCommandAttribute : Attribute
{
    public string CommandName { get; }

    public ConsoleCommandAttribute(string commandName = null)
    {
        CommandName = commandName;
    }
}
