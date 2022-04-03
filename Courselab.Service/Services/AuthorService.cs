using AutoMapper;
using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Authors;
using Courselab.Domain.Enums;
using Courselab.Service.DTOs.Authors;
using Courselab.Service.Extensions;
using Courselab.Service.Helpers;
using Courselab.Service.Interfaces;
using EduCenterWebAPI.Data.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Courselab.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IConfiguration config;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;

        public AuthorService(
            IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.config = config;
            this.env = env;
        }

        public async Task<BaseResponse<Author>> CreateAsync(AuthorForCreationDto authorCreationDto)
        {
            BaseResponse<Author> response = new BaseResponse<Author>();

            var exsistAuthorLogin = await unitOfWork.Authors.GetAsync(
                Author => Author.Login.Equals(authorCreationDto.Login) &&
                Author.Status != ObjectStatus.Deleted
                );

            // checking if login is not unique
            if (exsistAuthorLogin != null)
            {
                response.Error = new BaseError(code: 409, message: "Login of author already exsists");

                return response;
            }

            Author exsistAuthorPhoneNumber = await unitOfWork.Authors.GetAsync(
                Author => Author.PhoneNumber.Equals(authorCreationDto.PhoneNumber) &&
                Author.Status != ObjectStatus.Deleted
                );

            // checking if phone number exsists
            if (exsistAuthorPhoneNumber != null)
            {
                response.Error = new BaseError(code: 409, message: "Phone number of author already exsists");

                return response;
            }

            Author exsistAuthorEmail = await unitOfWork.Authors.GetAsync(
                Author => Author.PhoneNumber.Equals(authorCreationDto.Email) &&
                Author.Status != ObjectStatus.Deleted
                );

            // checking if email exsists
            if (exsistAuthorEmail != null)
            {
                response.Error = new BaseError(code: 409, message: "Email of author already exsists");

                return response;
            }

            //mapping
            Author newAuthor = mapper.Map<Author>(authorCreationDto);
            newAuthor.Password = newAuthor.Password.EncodeInSha256();

            if (authorCreationDto.Image != null)
                newAuthor.Image = await SaveFileAsync(authorCreationDto.Image.OpenReadStream(), authorCreationDto.Image.FileName);

            //inserting data
            newAuthor.Create();
            var createdNewAuthor = await unitOfWork.Authors.InsertAsync(newAuthor);
            await unitOfWork.SaveChangesAsync();

            //providing return content
            if (authorCreationDto.Image != null)
                createdNewAuthor.Image = HttpContextHelper.Context.Request.Scheme + "://" +
                                        HttpContextHelper.Context.Request.Host.Value + "/Images/" +
                                        createdNewAuthor.Image;

            response.Data = createdNewAuthor;
            response.Code = 200;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            var response = new BaseResponse<bool>();

            var author = await unitOfWork.Authors.GetAsync(author => author.Id.Equals(id) &&
                                                       author.Status != ObjectStatus.Deleted);

            //checking if author to delete does not exsist
            if (author == null)
            {
                response.Error = new BaseError(code: 404, message: "Author to delete not found");

                return response;
            }

            author.Delete();
            await unitOfWork.SaveChangesAsync();

            response.Code = 200;
            response.Data = true;

            return response;
        }

        public BaseResponse<IEnumerable<Author>> GetAll(PaginationParams @params)
        {
            var response = new BaseResponse<IEnumerable<Author>>();

            var authors = unitOfWork.Authors.GetAll();
            var paginatedAuthors = authors.ToPagesList(@params);

            //setting image
            foreach (var author in paginatedAuthors)
                if (author.Image != null)
                    author.Image = HttpContextHelper.Context.Request.Scheme + "://" +
                                            HttpContextHelper.Context.Request.Host.Value + "/Images/" +
                                            author.Image;

            response.Data = paginatedAuthors;
            response.Code = 200;

            return response;
        }

        public async Task<BaseResponse<Author>> GetByIdAsync(Guid id)
        {
            var response = new BaseResponse<Author>();
            var author = await unitOfWork.Authors.GetAsync(author => author.Id == id &&
            author.Status != ObjectStatus.Deleted);

            //checking if Author does not exsist
            if (author == null)
            {
                response.Error = new BaseError(code: 404, message: "Author not found");

                return response;
            }

            //providing return data
            if (author.Image != null)
                author.Image = HttpContextHelper.Context.Request.Scheme + "://" +
                                        HttpContextHelper.Context.Request.Host.Value + "/Images/" +
                                        author.Image;

            response.Data = author;
            response.Code = 200;

            return response;
        }

        public async Task<BaseResponse<Author>> UpdateAsync(AuthorForUpdateDto authorToUpdate)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(author => author.Id.Equals(authorToUpdate.Id) &&
                                                       author.Status != ObjectStatus.Deleted);

            //checking if author to update does not exsist
            if (author == null)
            {
                response.Error = new BaseError(code: 404, message: "Author not found");

                return response;
            }

            //mapping
            author.FirstName = authorToUpdate.FirstName;
            author.LastName = authorToUpdate.LastName;
            author.Gender = authorToUpdate.Gender;
            author.BrithDate = authorToUpdate.BrithDate;
            author.PhoneNumber = authorToUpdate.PhoneNumber;
            author.Email = authorToUpdate.Email;
            author.Login = authorToUpdate.Login;
            author.Password = authorToUpdate.Password.EncodeInSha256();

            //saving image of photo if user uploaded
            if (authorToUpdate.Image != null)
                author.Image = await SaveFileAsync(authorToUpdate.Image.OpenReadStream(), authorToUpdate.Image.FileName);

            author.Modify();
            await unitOfWork.SaveChangesAsync();

            //providing return content
            if (author.Image != null)
                author.Image = HttpContextHelper.Context.Request.Scheme + "://" +
                                        HttpContextHelper.Context.Request.Host.Value + "/Images/" +
                                        author.Image;

            response.Data = author;
            response.Code = 200;

            return response;
        }

        //extension services
        public async Task<string> SaveFileAsync(Stream file, string fileName)
        {
            //provideing names for file and storage
            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;
            string storagePath = config.GetSection("Storage:ImagesUrl").Value;
            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");
            Console.WriteLine(filePath);

            //creating stream with given path to copy file from input 
            FileStream mainFile = File.Create(filePath);
            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return fileName;
        }

    }
}
