using System.Collections.Generic;
using YadetNare.Domain.Activity;

namespace YadetNare.Core.Activity.Queries;

public interface IActivityQueryService
{
    // refactor: IList, IEnumerable , ... ?
    Task<IList<ActivityModel>> GetAllAsync(long chatId);
    Task<ActivityModel> GetAsync(string id);
    Task<ActivityModel> GetForEditAsync(int id);
    Task<ActivityModel> GetAsync(int id);
}