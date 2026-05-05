using Core.DataSource;
using Core.Entities;
using Core.Repositoties;
using Core.ValueObjects;
using MvcApp.Models;

namespace MvcApp.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository<Group> _groupRepository;

        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _groupRepository = unitOfWork.Groups;
        }

        public async Task<IEnumerable<GroupModel>> GetGroupsOfCourse(int? courseId)
        {
            if (courseId is null)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            var groups = await _groupRepository.GetGroupsOfCourse(courseId, GroupModel.GroupSelector);

            return groups;
        }

        public async Task<GroupModel> GetGroupModel(int? groupId)
        {
            if (groupId is null)
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            var groupModel = await _groupRepository.GetWithProjection(groupId, GroupModel.GroupSelector);

            return groupModel;
        }

        public async Task<GroupModel> GetOnlyIdAndName(int? groupId)
        {
            if (groupId is null)
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            var group = await _groupRepository.GetWithProjection(groupId, GroupModel.GroupNameSelector);

            return group;
        }

        public async Task CreateGroup(GroupModel groupModel)
        {
            if (groupModel is null)
            {
                throw new ArgumentNullException(nameof(groupModel));
            }

            var group = new Group(groupModel.CourseId, new Name(groupModel.Name));

            await _groupRepository.Create(group);
            await _unitOfWork.Save();
        }

        public async Task UpdateGroup(GroupModel groupModel)
        {
            if (groupModel is null)
            {
                throw new ArgumentNullException(nameof(groupModel));
            }

            var group = await _groupRepository.Get(groupModel.GroupId);
            group.Change(groupModel.CourseId, new Name(groupModel.Name));

            _groupRepository.Update(group);
            await _unitOfWork.Save();
        }

        public async Task DeleteGroup(int? groupId)
        {
            if (groupId is null)
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            var group = await _groupRepository.GetWithDetail(groupId);

            if (group.Students.Count() == 0)
            {
                _groupRepository.Delete(group);
                await _unitOfWork.Save();
            }
        }
    }
}
