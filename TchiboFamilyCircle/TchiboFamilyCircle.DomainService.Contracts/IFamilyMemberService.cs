using System.Collections.Generic;
using TchiboFamilyCircle.Dto;

namespace TchiboFamilyCircle.DomainService.Contracts
{
    public interface IFamilyMemberService
    {
        IEnumerable<FamilyMember> GetAll();

        void AddFamilyMemeber(FamilyMember familyMember);

        void UpdateFamilyMember(FamilyMember familyMember);

        void DeleteFamilyMember(int id);
    }
}
