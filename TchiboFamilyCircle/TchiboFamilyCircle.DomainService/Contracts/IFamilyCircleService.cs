using System.Collections.Generic;
using TchiboFamilyCircle.Dto;
using TchiboFamilyCircle.Entities;

namespace TchiboFamilyCircle.DomainService
{
    public interface IFamilyCircleService
    {
        IList<string> GetArticles(IList<FamilyMember> familyMembers);

        IList<Article> GetArticlesPerFamilyMember(FamilyMember familyMembers, int occasionId);
    }
}
