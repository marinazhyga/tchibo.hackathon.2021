using System;
using System.Collections.Generic;
using System.Linq;
using TchiboFamilyCircle.Dto;

namespace TchiboFamilyCircle.DomainService
{
    public class SizeService : ISizeService
    {
        private static IList<string> _sizesFemaleAdults = new List<string> { "34", "36", "38", "40", "42", "44", "46", "48", "50", "52", "54", "XS 32/34", "S 36/38", "M 40/42", "L 44/46", "XL 48/50", "XXL 52/54" };

        private static IList<string> _sizesMaleAdults = new List<string> { "48", "50", "52", "54", "56", "XS", "S", "M", "L", "XL", "XXL", "S 44/46", "M 48/50", "L 52/54", "XL 56/58", "XXL 60/62" };

        private static IList<string> _sizesKids = new List<string> { "32", "33", "34", "35", "36", "37", "38", "39", "40", "110", "116", "134", "140", "146", "152", "158", "164", "170", "50/56", "62/68", "74/80", "86/92", "98/104", "110/116", "98/104", "122/128", "134/140", "146/152", "158/164", "170/176" };

        private Dictionary<FamilyMemberType, IList<string>> _sizes = new Dictionary<FamilyMemberType, IList<string>>
        {
            { FamilyMemberType.Husband, _sizesMaleAdults },
            { FamilyMemberType.Wife, _sizesFemaleAdults },
            { FamilyMemberType.Son, _sizesKids},
            { FamilyMemberType.Dauther, _sizesKids },
            { FamilyMemberType.Daddy,_sizesMaleAdults },
            { FamilyMemberType.Mommy, _sizesFemaleAdults },
            { FamilyMemberType.Brother, _sizesMaleAdults },
            { FamilyMemberType.Sister, _sizesFemaleAdults },
            { FamilyMemberType.CousinMale, _sizesMaleAdults },
            { FamilyMemberType.CousinFemale, _sizesFemaleAdults },
            { FamilyMemberType.FatherInLaw, _sizesMaleAdults },
            { FamilyMemberType.MotherInLaw, _sizesFemaleAdults },
            { FamilyMemberType.Boyfriend, _sizesMaleAdults },
            { FamilyMemberType.Girlfriend, _sizesFemaleAdults },
            { FamilyMemberType.FriendMale, _sizesMaleAdults },
            { FamilyMemberType.FriendFemale, _sizesFemaleAdults },
            { FamilyMemberType.Nephew,_sizesKids},
            { FamilyMemberType.Nice, _sizesKids },
            { FamilyMemberType.Uncle, _sizesMaleAdults },
            { FamilyMemberType.Aunt, _sizesFemaleAdults },
            { FamilyMemberType.Grandpa, _sizesMaleAdults },
            { FamilyMemberType.Grandma, _sizesFemaleAdults },
            { FamilyMemberType.RelativeMale, _sizesMaleAdults },
            { FamilyMemberType.RelativeFemale, _sizesFemaleAdults }
        };

        public IList<string> GetAllSizes()
        {
            var result = new List<string>();

            foreach (var x in _sizes)
            {
                result.AddRange(x.Value);
            }

            return result.Distinct().ToList();
        }

        public IList<string> GetSizesByFamilyMember(FamilyMemberType familyMemberType, DateTime? birthDay)
        {
            if (!birthDay.HasValue)
            {
                return _sizes[familyMemberType].ToList();
            }
            else
            {
                TimeSpan difference = DateTime.Now - birthDay.Value;

                var years = difference.TotalDays / 365;

                if (years <= 12)
                {
                    return _sizesKids;
                }
                else if (familyMemberType.IsMale())
                {
                    return _sizesMaleAdults;
                }
                else if (familyMemberType.IsFemale())
                {
                    return _sizesFemaleAdults;
                }
            }

            return GetAllSizes();
        }
    }
}
