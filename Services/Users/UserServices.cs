using DataAccess.Interfaces;
using Entities;
using Entities.Dto.User;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Users
{
    public class UserServices
    {
        private static IRepository<User> repository;
        private static IUsersRepository usersRepository;
        public UserServices(IRepository<User> repository, IUsersRepository usersRepository)
        { UserServices.repository = repository; UserServices.usersRepository = usersRepository; }

        private bool IsUserPasswordValid(ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto == null)
                return false;

            if (!SharedServices.IsValid(changePasswordDto))
                return false;

            if (!usersRepository.ValidateUserPassword(changePasswordDto.Id, changePasswordDto.OldPassword))
                return false;

            return true;
        }

        public void DeleteUser(Guid id)
        {
            usersRepository.Delete(id);
        }

        public void BlockUser(Guid id)
        {
            usersRepository.Block(id);
        }

        public bool IsUserAuthorized(Guid id)
        {
            if (!usersRepository.IsAuthorized(id))
                return false;

            return true;
        }

        public bool ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (!IsUserAuthorized(changePasswordDto.Id))
                return false;

            if (!IsUserPasswordValid(changePasswordDto))
                return false;

            usersRepository.ChangePassword(changePasswordDto.Id, changePasswordDto.NewPassword);

            return true;
        }


        public User Register(RegisterDto user)
        {
            if (user == null)
                return null;

            if (!SharedServices.IsValid(user))
                return null;

            var userToCreate = usersRepository.Register(user);

            if (userToCreate == null)
                return null;

            return userToCreate;
        }

        public User Login(LoginDto user)
        {
            if (user == null)
                return null;

            if (!SharedServices.IsValid(user))
                return null;

            var userToLogin = usersRepository.Login(user);

            if (userToLogin == null)
                return null;

            return userToLogin;
        }

        public User Edit(EditProfileDto editProfileDto)
        {
            if (editProfileDto == null)
                return null;

            if (!SharedServices.IsValid(editProfileDto))
                return null;

            if (!IsUserAuthorized(editProfileDto.Id))
                return null;

            var user = repository.GetById(editProfileDto.Id);

            var userToEdit = new User() { Username = user.Username, Email = user.Email, Phonenumber = user.Phonenumber };

            repository.Update(userToEdit);

            return user;

        }




    }
}
