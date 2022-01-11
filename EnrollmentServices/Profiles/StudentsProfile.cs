using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnrollmentServices.Dtos;
using EnrollmentServices.Models;

namespace EnrollmentServices.Profiles
{
    public class StudentsProfile : Profile
    {
        public StudentsProfile()
        {
            CreateMap<Student, DtoStudentGet>()
                .ForMember(dst => dst.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<DtoStudentInsert, Student>();
        }
    }
}