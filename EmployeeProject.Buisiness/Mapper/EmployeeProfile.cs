using AutoMapper;
using EmployeeProject.Buisiness.Models.Employees;

namespace EmployeeProject.Buisiness.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<InsertEmployeeRequest, Employee>();
            CreateMap<UpdateEmployeeRequest, Employee>();
        }
    }
}
