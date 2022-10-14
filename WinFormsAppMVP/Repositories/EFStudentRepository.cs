using WinFormsAppMVP.Models;
using WinFormsAppMVP.Repositories.Contexts;

namespace WinFormsAppMVP.Repositories;


public class EFStudentRepository : IStudentRepository
{

    private readonly EFContext _context;

    public EFStudentRepository()
    {
        _context = new EFContext();
    }


    public void Add(Student entity)
    {
        _context.Students?.Add(entity);
        _context.SaveChanges();
    }


    public Student? Get(Func<Student, bool> predicate)
        => _context.Students?.FirstOrDefault(predicate);



    public Student? GetById(Guid id)
        => _context.Students?.Find(id);


    public List<Student>? GetList(Func<Student, bool>? predicate = null)
        => (predicate == null) switch
        {
            true => _context.Students?.ToList(),
            false => _context.Students?.Where(predicate).ToList()
        };




    public void Remove(Student entity)
    {
        _context.Students?.Remove(entity);
        _context.SaveChanges();
    }

    public void Update(Student entity)
    {
        _context.Students?.Update(entity);
        _context.SaveChanges();
    }
}
