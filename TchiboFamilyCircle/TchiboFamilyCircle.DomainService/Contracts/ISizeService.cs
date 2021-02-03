using System;
using System.Collections.Generic;
using TchiboFamilyCircle.Dto;

namespace TchiboFamilyCircle.DomainService
{
    public interface ISizeService
    {
        IList<string> GetAllSizes();
        IList<string> GetSizesByFamilyMember(FamilyMemberType familyMemberType, DateTime? birthDay);

    }
}
