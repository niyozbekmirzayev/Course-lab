using AutoMapper;
using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Courses;
using Courselab.Domain.Enums;
using Courselab.Service.DTOs.Courses;
using Courselab.Service.Extensions;
using Courselab.Service.Interfaces;
using EduCenterWebAPI.Data.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courselab.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;

        public CourseService(
            IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
        }

        public async Task<BaseResponse<Course>> CreateAsync(CourseForCreationDto courseCreationDto)
        {
            var response = new BaseResponse<Course>();

            var exsitAuthor = await unitOfWork.Authors.GetAsync(
                author => author.Id.Equals(courseCreationDto.AuthorId) &&
                author.Status != ObjectStatus.Deleted
                );

            //checking if author exsists
            if (exsitAuthor == null)
            {
                response.Error = new BaseError(code: 404, message: "Author not found");

                return response;
            }

            Course exsistCourse = await unitOfWork.Courses.GetAsync(
                course => course.Name.Equals(courseCreationDto.Name) &&
                course.Status != ObjectStatus.Deleted &&
                course.AuthorId.Equals(courseCreationDto.AuthorId)
                );

            // checking if course is not unique for author
            if (exsistCourse != null)
            {
                response.Error = new BaseError(code: 409, message: "Author already has the course");

                return response;
            }

            //mapping
            Course newCourse = mapper.Map<Course>(courseCreationDto);

            //updating database
            newCourse.Create();
            var createdNewcourse = await unitOfWork.Courses.InsertAsync(newCourse);
            await unitOfWork.SaveChangesAsync();

            response.Data = createdNewcourse;
            response.Code = 200;

            return response;
        }
        
        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            var response = new BaseResponse<bool>();

            Course course = await unitOfWork.Courses.GetAsync(course => course.Id.Equals(id) &&
                                                       course.Status != ObjectStatus.Deleted);

            //checking if author to delete does not exsist
            if (course == null)
            {
                response.Error = new BaseError(code: 404, message: "Course not found");

                return response;
            }

            //updating database
            course.Delete();
            await unitOfWork.SaveChangesAsync();

            response.Code = 200;
            response.Data = true;

            return response;
        }
        
        public BaseResponse<IEnumerable<Course>> GetAll(PaginationParams @params)
        {
            var response = new BaseResponse<IEnumerable<Course>>();

            var courses = unitOfWork.Courses.GetAll().Include("Author");
            var paginatedCourses = courses.ToPagesList(@params);

            response.Data = paginatedCourses;
            response.Code = 200;

            return response;
        }
        
        public async Task<BaseResponse<Course>> GetByIdAsync(Guid id)
        {
            var response = new BaseResponse<Course>();
            
            Course course = await unitOfWork.Courses.GetAsync(course => course.Id == id &&
            course.Status != ObjectStatus.Deleted);

            //checking if course does not exsist
            if (course == null)
            {
                response.Error = new BaseError(code: 404, message: "Course not found");

                return response;
            }

            response.Data = course;
            response.Code = 200;

            return response;
        }
        
        public async Task<BaseResponse<Course>> UpdateAsync(CourseForUpdateDto courseToUpdate)
        {
            var response = new BaseResponse<Course>();

            var exsistCourse = await unitOfWork.Courses.GetAsync(course => course.Id.Equals(courseToUpdate.Id) &&
                                                       course.Status != ObjectStatus.Deleted);

            //checking if course to update does not exsist
            if (exsistCourse == null)
            {
                response.Error = new BaseError(code: 404, message: "Course not found");

                return response;
            }

            var exsitAuthor = await unitOfWork.Authors.GetAsync(
                author => author.Id.Equals(courseToUpdate.AuthorId) &&
                author.Status != ObjectStatus.Deleted
                );

            //checking if author does not exsist
            if (exsitAuthor == null)
            {
                response.Error = new BaseError(code: 404, message: "Author not found");

                return response;
            }

            //mapping
            exsistCourse.Name = courseToUpdate.Name;
            exsistCourse.Description = courseToUpdate.Description;
            exsistCourse.Price = courseToUpdate.Price;
            exsistCourse.Duration = courseToUpdate.Duration;
            exsistCourse.Type = courseToUpdate.Type;
            exsistCourse.AuthorId = courseToUpdate.AuthorId;

            //updating database
            exsistCourse.Modify();
            await unitOfWork.SaveChangesAsync();

            response.Data = exsistCourse;
            response.Code = 200;

            return response;
        }

    }
}
