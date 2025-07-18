using System.Collections.Generic;
using JetBrains.Annotations;

namespace YadetNare.Core.Infrastructure;

public record UserState(State State, string AffectedColumn, int? EntityId, EntityType EntityType);
public static class ChatInfo
{
    /// <summary>
    /// Key is Chat Id
    /// </summary>
    public static Dictionary<long, UserState> States { get; set; }= new();

    [CanBeNull]
    public static UserState GetState(long id)
    {
        return States.GetValueOrDefault(id);
    }
}

public enum State : byte
{
    None,
    Edit,
    Delete
}

public enum EntityType : byte
{
    Activity,
    Alarm
}