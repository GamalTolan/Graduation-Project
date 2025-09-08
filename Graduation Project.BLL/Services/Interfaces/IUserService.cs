using Graduation_Project.BLL.ViewModels.UserVM;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserVM> GetAll();
        UserVM? GetById(int id);

        void Add(CreateUserVM vm);
        void Update(EditUserVM vm);
        void Delete(int id);

        EditUserVM? GetForEdit(int id);

        bool IsEmailExists(string email);
    }
}
