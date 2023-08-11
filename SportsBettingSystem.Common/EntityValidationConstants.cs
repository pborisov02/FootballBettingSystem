namespace SportsBettingSystem.Common
{
    public class EntityValidationConstants
    {
        public static class Team
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 40;
        }

        public static class League
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 16;

            public const int CountryNameMinLength = 2;
            public const int CountryNameMaxLength = 56;
        }
    }
}
