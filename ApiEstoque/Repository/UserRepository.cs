using ApiEstoque.Data;
using ApiEstoque.Dtos.User;
using ApiEstoque.Models;
using ApiEstoque.Repository.interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ApiContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(ApiContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<UserModel> Create(UserDtoCreate user)
        {
            var model = _mapper.Map<UserModel>(user);
            model.Status = "Active";
            model.CreateAt = DateTime.UtcNow;
            await _dbContext.Users.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            UserModel user = await GetById(id);
            if (user == null)
            {
                throw new Exception($"Usuario para o ID: {id} não foi encontrado no banco de dados.");
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> GetById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserModel> Update(UserDtoUpdate user, int id)
        {
            UserModel findUser = await GetById(id);
            if (findUser == null)
            {
                throw new Exception($"Usuario para o ID: {id} não foi encontrado no banco de dados.");
            }
            findUser.Name = user.Name;
            findUser.Email = user.Email;
            findUser.Password = user.Password;
            if (user.Status != null || user.Status != "")
            {
                findUser.Status = user.Status;
            }
           
            _dbContext.Users.Update(findUser);
            await _dbContext.SaveChangesAsync();
            return findUser;
        }
    }
}
