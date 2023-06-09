﻿using ApiEstoque.Data;
using ApiEstoque.Dtos.Shop;
using ApiEstoque.Dtos.User;
using ApiEstoque.Models;
using ApiEstoque.Repository.Exceptions;
using ApiEstoque.Repository.interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ApiContext _dbContext;
        private readonly IMapper _mapper;
        private IShopRepository _shopRepository;
        public UserRepository(ApiContext dbContext, IMapper mapper, IShopRepository shopRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _shopRepository = shopRepository;
        }
        public async Task<UserDto> Create(UserDtoCreate user)
        {
            UserModel findEmail = await _dbContext.Users.SingleOrDefaultAsync(p => p.Email.Equals(user.Email));
            ShopModel shopName = await _dbContext.Shop.SingleOrDefaultAsync(p => p.Name.Equals(user.storeName));
            if (findEmail != null)
            {
                throw new CreateUserException(404, $"Email:{user.Email} já cadastrado.");
            }
            if (shopName != null)
            {
                throw new CreateUserException(404, $"Shop:{user.storeName} já cadastrado.");
            }
            var model = _mapper.Map<UserModel>(user);
            model.Status = "Active";
            model.CreateAt = DateTime.UtcNow;
            model.SetPasswordHash();
            await _dbContext.Users.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            //Model Shop
            var modelShop = new ShopDtoCreate();
            modelShop.Name = user.storeName;
            modelShop.UserId = model.Id;
            await _shopRepository.Create(modelShop);
            return _mapper.Map<UserDto>(model);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                UserModel user = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (user == null)
                {
                    throw new ArgumentException($"Usuario para o ID: {id} não foi encontrado no banco de dados.");
                }
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<UserDto>> GetAll()
        {
            try
            {
                var listUser = await _dbContext.Users.ToListAsync();
                return _mapper.Map<List<UserDto>>(listUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<UserModel> GetByEmail(string email)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserDto> FindEmail(string email)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
                return _mapper.Map<UserDto>(user); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<UserDto> GetById(int id)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<UserDto> Update(UserDtoUpdate user, int id)
        {
            try
            {

                UserModel findUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (findUser == null)
                {
                    throw new Exception($"Usuario para o ID: {id} não foi encontrado no banco de dados.");
                }
                findUser.Name = user.Name;
                findUser.Email = user.Email;
                if (user.Status != null || user.Status != "")
                {
                    findUser.Status = user.Status;
                }
                findUser.UpdateAt = DateTime.UtcNow;

                _dbContext.Users.Update(findUser);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<UserDto>(findUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
