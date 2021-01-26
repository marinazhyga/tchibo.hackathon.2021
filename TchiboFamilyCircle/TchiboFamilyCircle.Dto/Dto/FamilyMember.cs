using System;
using System.Collections.Generic;

namespace TchiboFamilyCircle.Dto
{
    public class FamilyMember
    {
        public string Id { get; set; } 

        public string Name { get; set; }

        public FamilyMemberType Type { get; set; }

        public DateTime DateOfBirth { get; set; }

        public IEnumerable<Occasion> Occasions { get; set; }

        public string Sizes { get; set; }

        public string Interests { get; set; }

        public string CustomerNumber { get; set; }
    }
}
