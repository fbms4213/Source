using WinFormsAppMVP.Models;

namespace WinFormsAppMVP.Repositories;

public interface IStudentRepository : IRepository<Student>
{
    Student? GetById(Guid id);
}