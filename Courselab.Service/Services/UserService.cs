using AutoMapper;
using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Registraions;
using Courselab.Domain.Entities.Users;
using Courselab.Domain.Enums;
using Courselab.Service.DTOs.Commons;
using Courselab.Service.DTOs.Users;
using Courselab.Service.Extensions;
using Courselab.Service.Helpers;
using Courselab.Service.Interfaces;
using EduCenterWebAPI.Data.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courselab.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration config;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly string imageSection = "Storage:ImagesUrl";

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.config = config;
            this.env = env;
        }

        public async Task<BaseResponse<User>> BuyCourseAsync(Guid userId, Guid courseId)
        {
            var response = new BaseResponse<User>();

            var exsistUser = await unitOfWork.Users.GetAll(user => user.Id.Equals(userId) &&
                                                            user.Status != ObjectStatus.Deleted)
                                                            .Include("Registrations.Course")
                                                            .FirstAsync();

            // Checking if user does not exsist
            if (exsistUser == null)
            {
                response.Error = new BaseError(code: 404, message: "User not found");

                return response;
            }

            var exsistCourse = await unitOfWork.Courses.GetAsync(course => course.Id.Equals(courseId) &&
                                                                 course.Status != ObjectStatus.Deleted);

            // Checking if course does not exsist
            if (exsistCourse == null)
            {
                response.Error = new BaseError(code: 404, message: "Course not found");

                return response;
            }

            // Checking if user has not already bought current course
            ICollection<Registration> registrations = exsistUser.Registrations;

            foreach (var registration in registrations)
            {
                if (registration.Course.Id == courseId)
                {
                    response.Error = new BaseError(code: 409, message: "Course already registrated");

                    return response;
                };
            }

            // Registring for course
            var newRegistration = new Registration();
            newRegistration.Start(exsistCourse);

            var createdRegistration = await unitOfWork.Registrations.InsertAsync(newRegistration);
            exsistUser.Registrations.Add(createdRegistration);

            // Updating database
            await unitOfWork.SaveChangesAsync();

            response.Data = exsistUser;

            return response;
        }

        public async Task<BaseResponse<User>> CreateAsync(UserForCreationDto userCreationDto)
        {
            var response = new BaseResponse<User>();

            var fileHelper = new FileHelper(config, env);

            var exsistUsername = await unitOfWork.Users.GetAsync(user => user.Username.Equals(userCreationDto.Username) &&
                                                                 user.Status != ObjectStatus.Deleted);

            // Checking if username is not unique
            if (exsistUsername != null)
            {
                response.Error = new BaseError(code: 409, message: "Username already exsists");

                return response;
            }


            var exsistEmail = await unitOfWork.Users.GetAsync(user => user.Email.Equals(userCreationDto.Email) &&
                                                              user.Status != ObjectStatus.Deleted);

            // Checking if email exsists
            if (exsistEmail != null)
            {
                response.Error = new BaseError(code: 409, message: "Email already exsists");

                return response;
            }

            // Mapping
            var newUser = mapper.Map<User>(userCreationDto);
            newUser.Password = newUser.Password.EncodeInSha256();

            // Checking if media file uploaded
            if (userCreationDto.Image != null)
                newUser.Image = await fileHelper.SaveFileAsync(userCreationDto.Image.OpenReadStream(), userCreationDto.Image.FileName, imageSection);

            // Updating database
            newUser.Create();
            var createdNewUser = await unitOfWork.Users.InsertAsync(newUser);
            await unitOfWork.SaveChangesAsync();

            // Uploading link of media instead of name
            if (userCreationDto.Image != null)
                RefitImage(createdNewUser);

            response.Data = createdNewUser;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            var response = new BaseResponse<bool>();

            var user = await unitOfWork.Users.GetAsync(user => user.Id.Equals(id) &&
                                                       user.Status != ObjectStatus.Deleted);

            // Checking if user to delete does not exsist
            if (user == null)
            {
                response.Error = new BaseError(code: 404, message: "User not found");

                return response;
            }

            // Updating database
            user.Delete();
            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public BaseResponse<IEnumerable<User>> GetAll(PaginationParams @params)
        {
            var response = new BaseResponse<IEnumerable<User>>();

            var users = unitOfWork.Users.GetAll().Include("Registrations.Course");
            var paginatedUsers = users.ToPagesList(@params);

            // Uploading link of media instead of name
            foreach (var user in paginatedUsers)
                if (user.Image != null)
                    RefitImage(user);

            response.Data = paginatedUsers;

            return response;
        }

        public async Task<BaseResponse<User>> GetByIdAsync(Guid id)
        {
            var response = new BaseResponse<User>();
            var user = await unitOfWork.Users.GetAll(user => user.Id.Equals(id) &&
                                                     user.Status != ObjectStatus.Deleted)
                                                     .Include("Registrations.Course")
                                                     .FirstOrDefaultAsync();

            // Checking if user does not exsist
            if (user == null)
            {
                response.Error = new BaseError(code: 404, message: "User not found");

                return response;
            }

            // Uploading link of media instead of name
            if (user.Image != null)
                RefitImage(user);

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<User>> SetImageAsync(SetImageDto setImageDto)
        {
            var response = new BaseResponse<User>();
            var fileHelper = new FileHelper(config, env);

            var user = await unitOfWork.Users.GetAsync(student => student.Id.Equals(setImageDto.UserId) &&
                                                       student.Status != ObjectStatus.Deleted);

            // Checking if user to update does not exsist
            if (user == null)
            {
                response.Error = new BaseError(code: 404, message: "User not found");

                return response;
            }

            user.Image = await fileHelper.SaveFileAsync(setImageDto.Image.OpenReadStream(), setImageDto.Image.FileName, imageSection);
            user.Modify();

            // Updating database
            await unitOfWork.SaveChangesAsync();

            RefitImage(user);
            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<User>> UpdateAsync(UserForUpdateDto userToUpdate)
        {
            var response = new BaseResponse<User>();

            var fileHelper = new FileHelper(config, env);

            var user = await unitOfWork.Users.GetAsync(user => user.Id.Equals(userToUpdate.Id) &&
                                                       user.Status != ObjectStatus.Deleted);

            // Checking if user to update does not exsist
            if (user == null)
            {
                response.Error = new BaseError(code: 404, message: "User not found");

                return response;
            }

            // Mapping
            user.FirstName = userToUpdate.FirstName;
            user.LastName = userToUpdate.LastName;
            user.Gender = userToUpdate.Gender;
            user.DateOfBirth = userToUpdate.DataOfBirth;
            user.About = userToUpdate.About;
            user.Email = userToUpdate.Email;
            user.Username = userToUpdate.Username;
            user.Password = userToUpdate.Password.EncodeInSha256();

            // Checking if media file uploaded
            if (userToUpdate.Image != null)
                user.Image = await fileHelper.SaveFileAsync(userToUpdate.Image.OpenReadStream(), userToUpdate.Image.FileName, imageSection);

            else user.Image = null;

            // Updating database
            user.Modify();
            await unitOfWork.SaveChangesAsync();

            // Uploading link of media instead of name
            if (user.Image != null)
                RefitImage(user);

            response.Data = user;

            return response;
        }


        //Helper methods
        public void RefitImage(User user)
        {
            user.Image = HttpContextHelper.Context.Request.Scheme + "://" +
                           HttpContextHelper.Context.Request.Host.Value + "/Images/" +
                           user.Image;
        }
    }
}
