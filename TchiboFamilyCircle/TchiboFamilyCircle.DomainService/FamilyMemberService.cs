using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TchiboFamilyCircle.DataContext;
using TchiboFamilyCircle.Dto;
using TchiboFamilyCircle.Entities;
using TchiboFamilyCircle.Settings;

namespace TchiboFamilyCircle.DomainService
{
    public class FamilyMemberService : IFamilyMemberService
    {
        private readonly TchiboFamillyCircleDataContext _context;
        private readonly IMapper _mapper;

        public FamilyMemberService(IOptions<AppSettings> settings, IMapper mapper)
        {
            _context = new TchiboFamillyCircleDataContext(settings);
            _mapper = mapper;
        }
        public void Add(FamilyMember familyMember)
        {
            var familyMemberEntity = _mapper.Map<FamilyMember, FamilyMemberEntity>(familyMember);

            _context.FamilyMemberEntities.InsertOne(familyMemberEntity);
        }

        public void Delete(string id)
        {
            _context.FamilyMemberEntities.DeleteOne(member => member.Id == id);
        }        
        public IEnumerable<FamilyMember> GetAll()
        {
            var familyMemeberEnities = _context.FamilyMemberEntities.Find(member => true).ToList();

            var familyMembers = _mapper.Map<IEnumerable<FamilyMemberEntity>, IEnumerable<FamilyMember>>(familyMemeberEnities);

            return familyMembers;
        }
        public void Update(FamilyMember familyMember)
        {
            var familyMemberEntity = _mapper.Map<FamilyMember, FamilyMemberEntity>(familyMember);

            _context.FamilyMemberEntities.ReplaceOne(member => member.Id == familyMemberEntity.Id, familyMemberEntity);           
        }
    }
}
