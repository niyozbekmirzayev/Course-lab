using AutoMapper;
using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Students;
using Courselab.Domain.Enums;
using Courselab.Service.DTOs.Students;
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
    public class StudentService : IStudentService
    {
        private readonly IConfiguration config;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;

        public StudentService(
            IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.config = config;
            this.env = env;
        }

        public async Task<BaseResponse<Student>> CreateAsync(StudentForCreationDto studentCreationDto)
        {
            BaseResponse<Student> response = new BaseResponse<Student>();

            var exsistStudentLogin = await unitOfWork.Students.GetAsync(
                Student => Student.Login.Equals(studentCreationDto.Login) &&
                Student.Status != ObjectStatus.Deleted
                );

            // checking if login is not unique
            if (exsistStudentLogin != null)
            {
                response.Error = new BaseError(code: 409, message: "Login of student already exsists");

                return response;
            }

            Student exsistStudentPhoneNumber = await unitOfWork.Students.GetAsync(
                Student => Student.PhoneNumber.Equals(studentCreationDto.PhoneNumber) &&
                Student.Status != ObjectStatus.Deleted
                );

            // checking if phone number exsists
            if (exsistStudentPhoneNumber != null)
            {
                response.Error = new BaseError(code: 409, message: "Phone number of student already exsists");

                return response;
            }

            Student exsistStudentEmail = await unitOfWork.Students.GetAsync(
                Student => Student.PhoneNumber.Equals(studentCreationDto.Email) &&
                Student.Status != ObjectStatus.Deleted
                );

            // checking if email exsists
            if (exsistStudentEmail != null)
            {
                response.Error = new BaseError(code: 409, message: "Email of student already exsists");

                return response;
            }

            //mapping
            Student newStudent = mapper.Map<Student>(studentCreationDto);
            newStudent.Password = newStudent.Password.EncodeInSha256();

            if (studentCreationDto.Image != null)
                newStudent.Image = await SaveFileAsync(studentCreationDto.Image.OpenReadStream(), studentCreationDto.Image.FileName);

            //inserting data
            newStudent.Create();
            var createdNewStudent = await unitOfWork.Students.InsertAsync(newStudent);
            await unitOfWork.SaveChangesAsync();

            //providing return content
            if (studentCreationDto.Image != null)
                createdNewStudent.Image = HttpContextHelper.Context.Request.Scheme + "://" +
                                        HttpContextHelper.Context.Request.Host.Value + "/Images/" +
                                        createdNewStudent.Image;

            response.Data = createdNewStudent;
            response.Code = 200;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            var response = new BaseResponse<bool>();

            var student = await unitOfWork.Students.GetAsync(student => student.Id.Equals(id) &&
                                                       student.Status != ObjectStatus.Deleted);

            //checking if student to delete does not exsist
            if (student == null)
            {
                response.Error = new BaseError(code: 404, message: "student to delete not found");

                return response;
            }

            student.Delete();
            await unitOfWork.SaveChangesAsync();

            response.Code = 200;
            response.Data = true;

            return response;
        }

        public BaseResponse<IEnumerable<Student>> GetAll(PaginationParams @params)
        {
            var response = new BaseResponse<IEnumerable<Student>>();

            var students = unitOfWork.Students.GetAll();
            var paginatedStudents = students.ToPagesList(@params);

            //setting image
            foreach (var student in paginatedStudents)
                if (student.Image != null)
                    student.Image = HttpContextHelper.Context.Request.Scheme + "://" +
                                            HttpContextHelper.Context.Request.Host.Value + "/Images/" +
                                            student.Image;

            response.Data = paginatedStudents;
            response.Code = 200;

            return response;
        }

        public async Task<BaseResponse<Student>> GetByIdAsync(Guid id)
        {
            var response = new BaseResponse<Student>();
            var student = await unitOfWork.Students.GetAsync(student => student.Id == id &&
            student.Status != ObjectStatus.Deleted);

            //checking if student does not exsist
            if (student == null)
            {
                response.Error = new BaseError(code: 404, message: "student not found");

                return response;
            }

            //providing return data
            if (student.Image != null)
                student.Image = HttpContextHelper.Context.Request.Scheme + "://" +
                                        HttpContextHelper.Context.Request.Host.Value + "/Images/" +
                                        student.Image;

            response.Data = student;
            response.Code = 200;

            return response;
        }

        public async Task<BaseResponse<Student>> UpdateAsync(StudentForUpdateDto studentToUpdate)
        {
            var response = new BaseResponse<Student>();

            var student = await unitOfWork.Students.GetAsync(student => student.Id.Equals(studentToUpdate.Id) &&
                                                       student.Status != ObjectStatus.Deleted);

            //checking if student to update does not exsist
            if (student == null)
            {
                response.Error = new BaseError(code: 404, message: "student not found");

                return response;
            }

            //mapping
            student.FirstName = studentToUpdate.FirstName;
            student.LastName = studentToUpdate.LastName;
            student.Gender = studentToUpdate.Gender;
            student.BrithDate = studentToUpdate.BrithDate;
            student.PhoneNumber = studentToUpdate.PhoneNumber;
            student.Email = studentToUpdate.Email;
            student.Login = studentToUpdate.Login;
            student.Password = studentToUpdate.Password.EncodeInSha256();

            //saving image of photo if user uploaded
            if (studentToUpdate.Image != null)
                student.Image = await SaveFileAsync(studentToUpdate.Image.OpenReadStream(), studentToUpdate.Image.FileName);

            student.Modify();
            await unitOfWork.SaveChangesAsync();

            //providing return content
            if (student.Image != null)
                student.Image = HttpContextHelper.Context.Request.Scheme + "://" +
                                        HttpContextHelper.Context.Request.Host.Value + "/Images/" +
                                        student.Image;

            response.Data = student;
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
