using System.Linq.Expressions;

namespace MvcApp.Models
{
    public class GroupModel
    {
        public int GroupId { get; set; }
        public int? CourseId { get; set; }
        public string Name { get; set; } = null!;


        public static Expression<Func<Core.Entities.Group, GroupModel>> GroupSelector
        {
            get
            {
                return group => new GroupModel()
                {
                    GroupId = group.Id,
                    CourseId = group.CourseId,
                    Name = group.Name.Value
                };
            }
        }

        public static Expression<Func<Core.Entities.Group, GroupModel>> GroupNameSelector
        {
            get
            {
                return group => new GroupModel()
                {
                    GroupId = group.Id,
                    Name = group.Name.Value
                };
            }
        }
    }
}
