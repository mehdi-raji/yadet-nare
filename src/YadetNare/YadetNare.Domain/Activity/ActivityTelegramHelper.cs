using YadetNare.Shared;

namespace YadetNare.Domain.Activity;

public static class ActivityTelegramHelper
{

    #region CallBack Pattern
    public const string AddCallBack = "activity:add";
    public static string ToShowCallBackData (this ActivityModel activity) => $"activity:show:{activity.Id}";  
    
    #endregion

    #region Text

    public const string AddButtonText = $"{Emoji.Pushpin}افزودن";
    

    #endregion
    
    #region Button
    public static string GetListButtonText(this ActivityModel activity) => $"{Emoji.Pushpin} {activity.Title}"; 
    
    #endregion
    
    
}