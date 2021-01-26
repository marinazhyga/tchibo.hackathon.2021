using System.Collections.Generic;
using TchiboFamilyCircle.Dto;

namespace TchiboFamilyCircle.DomainService
{
    public interface IFamilyMemberService
    {
        IEnumerable<FamilyMember> GetAll();

        void Add(FamilyMember familyMember);

        void Update(FamilyMember familyMember);

        void Delete(string id);
    }
}
