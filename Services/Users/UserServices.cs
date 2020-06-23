using DataAccess.Interfaces;
using Entities;
using Entities.Dto.User;
using Services.Helpers;
using Services.Logs;
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
            if (SharedServices.IsObjectNull(changePasswordDto))
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
            LogService.Add("User Deleted", id);
        }

        public void BlockUser(Guid id)
        {
            usersRepository.Block(id);
            LogService.Add("User Blocked", id);
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
            LogService.Add("Passwrod Changed", changePasswordDto.Id);

            return true;
        }


        public User Register(RegisterDto user)
        {
            if (SharedServices.IsObjectNull(user))
                return null;

            if (!SharedServices.IsValid(user))
                return null;

            var userToCreate = usersRepository.Register(user);

            if (SharedServices.IsObjectNull(userToCreate))
                return null;

            LogService.Add("User Created", userToCreate.Id);

            return userToCreate;
        }

        public User Login(LoginDto user)
        {
            if (SharedServices.IsObjectNull(user))
                return null;

            if (!SharedServices.IsValid(user))
                return null;

            var userToLogin = usersRepository.Login(user);

            if (SharedServices.IsObjectNull(userToLogin))
                return null;

            LogService.Add("User Logged In", userToLogin.Id);

            return userToLogin;
        }

        public User Edit(EditProfileDto editProfileDto)
        {
            if (SharedServices.IsObjectNull(editProfileDto))
                return null;

            if (!SharedServices.IsValid(editProfileDto))
                return null;

            if (!IsUserAuthorized(editProfileDto.Id))
                return null;

            var user = repository.GetById(editProfileDto.Id);

            var userToEdit = new User() { Username = user.Username, Email = user.Email, Phonenumber = user.Phonenumber };

            repository.Update(userToEdit);

            LogService.Add("User Profile Edited", user.Id);

            return user;

        }




    }
}
