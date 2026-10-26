using Microsoft.EntityFrameworkCore;
using PostService.Models;

namespace PostService.DataAccess;

public class GenericEfRepository<TModel> : IGenericRepository<TModel> where TModel : ModelBase
{
    private readonly DbContext _context;

    public GenericEfRepository(DbContext context)
    {
        _context = context;
    }

    public List<TModel> GetList()
    {
        return _context.Set<TModel>().ToList();
    }

    public TModel? GetById(int id)
    {
        return _context.Set<TModel>().FirstOrDefault(model => model.Id == id);
    }

    public int Create(TModel model)
    {
        _context.Set<TModel>().Add(model);
        _context.SaveChanges();
        return model.Id;
    }

    public int Update(TModel model)
    {
        var tracked = _context.Set<TModel>().Local.FirstOrDefault(entity => entity.Id == model.Id);
        if (tracked != null && !ReferenceEquals(tracked, model))
        {
            _context.Entry(tracked).CurrentValues.SetValues(model);
            _context.Entry(tracked).State = EntityState.Modified;
        }
        else
        {
            _context.Set<TModel>().Update(model);
        }
        return _context.SaveChanges();
    }

    public int Delete(int id)
    {
        var entity = _context.Set<TModel>().Find(id);
        if (entity == null)
        {
            return 0;
        }
        _context.Set<TModel>().Remove(entity);
        return _context.SaveChanges();
    }
}