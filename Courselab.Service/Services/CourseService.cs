using AutoMapper;
using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Courses;
using Courselab.Domain.Enums;
using Courselab.Service.DTOs.Courses;
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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Course>> CreateAsync(CourseForCreationDto courseCreationDto)
        {
            var response = new BaseResponse<Course>();

            var exsitAuthor = await unitOfWork.Users.GetAsync(author => author.Id.Equals(courseCreationDto.AuthorId) &&
                                                               author.Status != ObjectStatus.Deleted);

            // Checking if user exsists
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

            // Checking if course is not unique for user
            if (exsistCourse != null)
            {
                response.Error = new BaseError(code: 409, message: "Author already has the course");

                return response;
            }

            // Mapping
            Course newCourse = mapper.Map<Course>(courseCreationDto);

            // Updating database
            newCourse.Create();
            var createdNewcourse = await unitOfWork.Courses.InsertAsync(newCourse);
            await unitOfWork.SaveChangesAsync();

            response.Data = createdNewcourse;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            var response = new BaseResponse<bool>();

            Course course = await unitOfWork.Courses.GetAsync(course => course.Id.Equals(id) &&
                                                              course.Status != ObjectStatus.Deleted);

            // Checking if course to delete does not exsist
            if (course == null)
            {
                response.Error = new BaseError(code: 404, message: "Course not found");

                return response;
            }

            // Updating database
            course.Delete();
            await unitOfWork.SaveChangesAsync();
            
            response.Data = true;

            return response;
        }

        public BaseResponse<IEnumerable<Course>> GetAll(PaginationParams @params)
        {
            var response = new BaseResponse<IEnumerable<Course>>();

            var courses = unitOfWork.Courses.GetAll().Include("Author");
            var paginatedCourses = courses.ToPagesList(@params);

            response.Data = paginatedCourses;

            return response;
        }

        public async Task<BaseResponse<Course>> GetByIdAsync(Guid id)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAll(course => course.Id == id &&
                                                          course.Status != ObjectStatus.Deleted)
                                                         .Include("Author")
                                                         .FirstOrDefaultAsync();

            // Checking if course does not exsist
            if (course == null)
            {
                response.Error = new BaseError(code: 404, message: "Course not found");

                return response;
            }

            response.Data = course;

            return response;
        }

        public async Task<BaseResponse<Course>> UpdateAsync(CourseForUpdateDto courseToUpdate)
        {
            var response = new BaseResponse<Course>();

            var fileHelper = new FileHelper(config, env);

            var exsistCourse = await unitOfWork.Courses.GetAsync(course => course.Id.Equals(courseToUpdate.Id) &&
                                                                 course.Status != ObjectStatus.Deleted);

            // Checking if course to update does not exsist
            if (exsistCourse == null)
            {
                response.Error = new BaseError(code: 404, message: "Course not found");

                return response;
            }

            var exsitAuthor = await unitOfWork.Users.GetAsync(author => author.Id.Equals(courseToUpdate.AuthorId) &&
                                                              author.Status != ObjectStatus.Deleted);

            // Checking if author does not exsist
            if (exsitAuthor == null)
            {
                response.Error = new BaseError(code: 404, message: "Author not found");

                return response;
            }

            // Mapping
            exsistCourse.Name = courseToUpdate.Name;
            exsistCourse.Description = courseToUpdate.Description;
            exsistCourse.Type = courseToUpdate.Type;
            exsistCourse.Level = exsistCourse.Level;
            exsistCourse.YouTubePlayListLink = courseToUpdate.YouTubePlayListLink;
            exsistCourse.AuthorId = courseToUpdate.AuthorId;

            // Checking if media file uploaded
            string imageSection = "ImagesUrl:Images";
            string videoSection = "VideosUrl:Videos";
            if (courseToUpdate.Image != null)
                exsistCourse.Image = await fileHelper.SaveFileAsync(courseToUpdate.Image.OpenReadStream(), courseToUpdate.Image.FileName, imageSection);

            if (courseToUpdate.GuidVideo != null)
                exsistCourse.GuidVideo = await fileHelper.SaveFileAsync(courseToUpdate.GuidVideo.OpenReadStream(), courseToUpdate.GuidVideo.FileName, videoSection);


            // Updating database
            exsistCourse.Modify();
            await unitOfWork.SaveChangesAsync();

            //uploading link of media instead of name
            if (exsistCourse.Image != null)
                RefitImage(exsistCourse);

            if (exsistCourse.GuidVideo != null)
                RefitVideo(exsistCourse);

            response.Data = exsistCourse;

            return response;
        }

        //Helper methods
        public void RefitImage(Course course)
        {
            course.Image = HttpContextHelper.Context.Request.Scheme + "://" +
                           HttpContextHelper.Context.Request.Host.Value + "/Images/" +
                           course.Image;
        }

        public void RefitVideo(Course course)
        {
            course.GuidVideo = HttpContextHelper.Context.Request.Scheme + "://" +
                               HttpContextHelper.Context.Request.Host.Value + "/Videos/" +
                               course.GuidVideo;
        }

    }
}
