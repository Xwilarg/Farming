using System;

public struct ConsoleCommand
{
    public int argumentCount;
    public Action<string[]> callback;
}
