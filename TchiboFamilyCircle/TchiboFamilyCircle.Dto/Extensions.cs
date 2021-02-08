namespace TchiboFamilyCircle.Dto
{
    public static class Extensions
    {
        public static bool IsFemale(this FamilyMemberType familyMemberType)
        {
            return (familyMemberType == FamilyMemberType.Wife ||
               familyMemberType == FamilyMemberType.Dauther ||
               familyMemberType == FamilyMemberType.Mommy ||
               familyMemberType == FamilyMemberType.Sister ||
               familyMemberType == FamilyMemberType.CousinFemale ||
               familyMemberType == FamilyMemberType.MotherInLaw ||
               familyMemberType == FamilyMemberType.Girlfriend ||
               familyMemberType == FamilyMemberType.FriendFemale ||
               familyMemberType == FamilyMemberType.Nice ||
               familyMemberType == FamilyMemberType.Aunt ||
               familyMemberType == FamilyMemberType.Grandma ||
               familyMemberType == FamilyMemberType.RelativeFemale);
        }

        public static bool IsMale(this FamilyMemberType familyMemberType)
        {
            return (familyMemberType == FamilyMemberType.Husband ||
               familyMemberType == FamilyMemberType.Son ||
               familyMemberType == FamilyMemberType.Daddy ||
               familyMemberType == FamilyMemberType.Brother ||
               familyMemberType == FamilyMemberType.CousinMale ||
               familyMemberType == FamilyMemberType.FatherInLaw ||
               familyMemberType == FamilyMemberType.Boyfriend ||
               familyMemberType == FamilyMemberType.FriendMale ||
               familyMemberType == FamilyMemberType.Nephew ||
               familyMemberType == FamilyMemberType.Uncle ||
               familyMemberType == FamilyMemberType.Grandpa ||
               familyMemberType == FamilyMemberType.RelativeMale);
        }

        public static bool CanBeFlowers(this Occasion occasion)
        {
            if (occasion.Name == "Birthday" ||
                occasion.Name == "Mother'sDay" ||
                occasion.Name == "Anniversary" ||
                occasion.Name == "Wedding" ||
                occasion.Name == "Graduation")                
                return true;

            return false;
        }
    }
}
