using System.Collections.Generic;
using TchiboFamilyCircle.Dto;

namespace TchiboFamilyCircle.DomainService
{
    public interface IOccasionService
    {
        IList<Occasion> GetAllOccasions();
    }
}
