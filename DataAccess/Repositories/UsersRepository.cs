﻿using Context;
using DataAccess.Interfaces;
using Entities;
using Entities.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private IRepository<User> repository;
        public UsersRepository(IRepository<User> repository)
        { this.repository = repository; }

        protected readonly Database context;
        public UsersRepository(Database context)
        {
            this.context = context;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private bool IsExisit(string email, string phonenumber)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == email || x.Phonenumber == phonenumber);
            if (user == null)
                return false;

            return true;
        }

        public void Block(Guid id)
        {
            var userFromDb = repository.GetById(id);

            userFromDb.IsBlocked = true;

            repository.Update(userFromDb);
        }

        public void Delete(Guid id)
        {
            var userFromDb = repository.GetById(id);

            userFromDb.IsDeleted = true;

            repository.Update(userFromDb);
        }

        public bool IsAuthorized(Guid id)
        {
            var user = repository.GetById(id);

            if (user == null)
                return false;

            if (user.IsBlocked == true || user.IsDeleted == true)
                return false;

            return true;
        }

        public User GetUserByPhonenumber(string phonenumber)
        {
            var user = context.Users.FirstOrDefault(x => x.Phonenumber == phonenumber);

            if (user == null)
                return null;

            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
                return null;

            return user;
        }

        public User Login(LoginDto loginDto)
        {
            var user = GetUserByPhonenumber(loginDto.Phonenumber);

            if (user == null)
                return null;

            if (!IsAuthorized(user.Id))
                return null;

            if (!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            user.DeviceToken = loginDto.DeviceToken;
            user.IsActive = true;

            repository.Update(user);

            return user;
        }

        public User Register(RegisterDto user)
        {
            if (IsExisit(user.Email, user.Phonenumber))
                return null;

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            var userToCreate = new User() { Username = user.Username, Phonenumber = user.Phonenumber, Email = user.Email, DeviceToken = user.DeviceToken, PasswordHash = passwordHash, PasswordSalt = passwordSalt};

            repository.Insert(userToCreate);

            return userToCreate;
        }

        public bool ValidateUserPassword(Guid id, string password)
        {
            var user = repository.GetById(id);

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return false;

            return true;
        }

        public void ChangePassword(Guid Id, string password)
        {
            var user = repository.GetById(Id);

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            repository.Update(user);
        }
    }
}
