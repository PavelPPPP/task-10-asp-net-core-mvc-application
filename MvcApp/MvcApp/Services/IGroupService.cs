using MvcApp.Models;

namespace MvcApp.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupModel>> GetGroupsOfCourse(int? courseId);
        Task<GroupModel> GetGroupModel(int? id);
        Task<GroupModel> GetOnlyIdAndName(int? groupId);
        Task CreateGroup(GroupModel groupModel);
        Task UpdateGroup(GroupModel groupModel);
        Task DeleteGroup(int? id);
    }
}
