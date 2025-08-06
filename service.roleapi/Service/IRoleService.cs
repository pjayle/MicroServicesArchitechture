using Microsoft.EntityFrameworkCore;
using service.roleapi.Data;
using service.roleapi.Models;

namespace service.roleapi.Service
{
    public interface IRoleService
    {
        Task<List<Roles>> getall();
        Task<Roles> getbyid(int pkid);
        Task<Roles> add(Roles mRoles);
        Task<Roles> update(Roles mRoles);
        Task<Roles> delete(int pkid);
    }

    public class RoleService : IRoleService
    {
        private readonly RoleDbContext _db;

        public RoleService(RoleDbContext db)
        {
            _db = db;
        }

        public async Task<List<Roles>> getall()
        {
            return await _db.Roles.ToListAsync();
        }

        public async Task<Roles> getbyid(int pkid)
        {
            return await _db.Roles.FirstAsync(u => u.Id == pkid);
        }

        public async Task<Roles> add(Roles mRoles)
        {
            var result = _db.Roles.Add(mRoles);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Roles> update(Roles mRoles)
        {
            var result = _db.Roles.Update(mRoles);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Roles> delete(int pkid)
        {
            Roles mRoles = _db.Roles.First(u => u.Id == pkid);
            _db.Roles.Remove(mRoles);
            await _db.SaveChangesAsync();
            return mRoles;
        }
    }
}
