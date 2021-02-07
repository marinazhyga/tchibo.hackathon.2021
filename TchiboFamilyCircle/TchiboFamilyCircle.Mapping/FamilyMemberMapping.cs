using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TchiboFamilyCircle.Dto;
using TchiboFamilyCircle.Entities;

namespace TchiboFamilyCircle.Mapping
{
    public class FamilyMemberMapping : Profile
    {
        public FamilyMemberMapping()
        {
            CreateMap<FamilyMemberEntity, FamilyMember>()
                .ForMember(destination => destination.Id, option => option.MapFrom(source => source.Id))
                .ForMember(destination => destination.Name, option => option.MapFrom(source => source.Name))
                .ForMember(destination => destination.Type, option => option.MapFrom(source => source.Type))
                .ForMember(destination => destination.DateOfBirth, option => option.MapFrom(source => source.DateOfBirth))
                .ForMember(destination => destination.Occasions, option => option.MapFrom(source => source.Occasions))
                .ForMember(destination => destination.Sizes, option => option.MapFrom(source => source.Sizes))
                .ForMember(destination => destination.Interests, option => option.MapFrom(source => source.Interests))
                .ForMember(destination => destination.Budget, option => option.MapFrom(source => source.Budget))
                .ForMember(destination => destination.CustomerNumber, option => option.MapFrom(source => source.CustomerNumber));

            CreateMap<FamilyMember, FamilyMemberEntity>()
                .ForMember(destination => destination.Id, option => option.MapFrom(source => source.Id))
                .ForMember(destination => destination.Name, option => option.MapFrom(source => source.Name))
                .ForMember(destination => destination.Type, option => option.MapFrom(source => source.Type))
                .ForMember(destination => destination.DateOfBirth, option => option.MapFrom(source => source.DateOfBirth))
                .ForMember(destination => destination.Occasions, option => option.MapFrom(source => source.Occasions))
                .ForMember(destination => destination.Sizes, option => option.MapFrom(source => source.Sizes.Select(x => x.Trim())))
                .ForMember(destination => destination.Interests, option => option.MapFrom(source => source.Interests.Select(x => x.Trim())))
                .ForMember(destination => destination.Budget, option => option.MapFrom(source => source.Budget))
                .ForMember(destination => destination.CustomerNumber, option => option.MapFrom(source => source.CustomerNumber));
        }        
    }
}
