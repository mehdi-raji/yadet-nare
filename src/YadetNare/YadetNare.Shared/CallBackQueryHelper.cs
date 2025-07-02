namespace YadetNare.Shared;

public class CallBackQueryHelper
{
    public static string GenerateShowData<T>(object id) => $"{nameof(T)}:{id}";
    public static string GetEntityId( string query) => query.Split(":")[1] ;
}