namespace YadetNare.Core;

public static class CallBackQueryHelper
{
    // BUG: Fix type of T name!
    [Obsolete]
    public static string GenerateShowData<T>(object id) => $"{typeof(T)}:{id}";
    
    [Obsolete]
    public static string GenerateEditData<T>(object id) => $"{typeof(T)}:{id}";
    
    [Obsolete]
    public static string GetEntityId(string query)
    {
        var splited = Split(query);
        
        return splited.Length == 2 ? splited[1] : string.Empty;
    }
    
    private static string[] Split(string query) => query.Split(":");
  
}