using System;
using System.Collections.Generic;
using System.Linq;
using TchiboFamilyCircle.Dto;

namespace TchiboFamilyCircle.DomainService
{
    public class OccasionService : IOccasionService
    {
        private static IList<Occasion> _occasions = new List<Occasion>
        {
            new Occasion { Id = 1, Name = "Birthday", Date = new DateTime() },
            new Occasion { Id = 2, Name = "Christmas", Date = new DateTime(2021, 12, 25)  },
            new Occasion { Id = 3, Name = "Easter", Date = new DateTime(2021, 4, 4) },
            new Occasion { Id = 4, Name = "Mother'sDay", Date = new DateTime(2021, 5, 9)  },
            new Occasion { Id = 5, Name = "Father'sDay", Date = new DateTime(2021, 5, 13)  },
            new Occasion { Id = 6, Name = "Anniversary", Date = new DateTime() },
            new Occasion { Id = 7, Name = "Wedding", Date = new DateTime() },
            new Occasion { Id = 8, Name = "Housewarming", Date = new DateTime() },
            new Occasion { Id = 9, Name = "Graduation", Date = new DateTime() },
        };
        public IList<Occasion> GetAllOccasions()
        {
            return _occasions;
        }

        public Occasion GetOccasionById(int id)
        {
            return _occasions.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
