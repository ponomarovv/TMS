using AutoMapper;
using TMS.BLL.Models;
using TMS.DAL.Entities;

namespace TMS.BLL.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskEntity, TaskModel>().ReverseMap();
    }
}