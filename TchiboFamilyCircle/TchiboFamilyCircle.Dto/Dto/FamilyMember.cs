using System;
using System.Collections.Generic;

namespace TchiboFamilyCircle.Dto
{
    public class FamilyMember
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public FamilyMemberType Type { get; set; }

        public Occasion Occasion { get; set; }

        public IList<string> Sizes { get; set; }

        public IList<string> Interests { get; set; }

        public string CustomerNumber { get; set; }
    }
}
