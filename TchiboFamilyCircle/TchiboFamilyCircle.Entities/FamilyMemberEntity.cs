using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using TchiboFamilyCircle.Dto;

namespace TchiboFamilyCircle.Entities
{
    public class FamilyMemberEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        public string Name { get; set; }

        public FamilyMemberType Type { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Occasion Occasion { get; set; }

        public IList<string> Sizes { get; set; }

        public IList<string> Interests { get; set; }

        public string CustomerNumber { get; set; }
    }
}
