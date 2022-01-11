using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnrollmentServices.Dtos;
using EnrollmentServices.Models;

namespace EnrollmentServices.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, DtoCourseGet>();

            CreateMap<DtoCourseInsert, Course>();
        }
    }
}