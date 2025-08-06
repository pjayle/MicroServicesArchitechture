using Microsoft.EntityFrameworkCore;
using service.userapi.Data;
using service.userapi.Models;

namespace service.userapi.Service
{
    public interface IUserService
    {
        Task<List<MUser>> getall();
        Task<MUser> getbyid(int pkid);
        Task<MUser> add(MUser employee);
        Task<MUser> update(MUser employee);
        Task<MUser> delete(int pkid);
    }

    public class UserService : IUserService
    {
        private readonly UserDbContext _db;

        public UserService(UserDbContext db)
        {
            _db = db;
        }

        public async Task<List<MUser>> getall()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<MUser> getbyid(int pkid)
        {
            return await _db.Users.FirstAsync(u => u.Id == pkid);
        }

        public async Task<MUser> add(MUser mUser)
        {
            var result = _db.Users.Add(mUser);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<MUser> update(MUser mUser)
        {
            var result = _db.Users.Update(mUser);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<MUser> delete(int pkid)
        {
            MUser mUser = _db.Users.First(u => u.Id == pkid);
            _db.Users.Remove(mUser);
            await _db.SaveChangesAsync();
            return mUser;
        }
    }
}
