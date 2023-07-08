namespace SportsBettingSystem.Common
{
    public class EntityValidationConstants
    {
        public static class Player
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 50;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 50;

            public const int KitNumberMin = 1;
            public const int KitNumberMax = 99;

        }
        public static class Team
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 40;

            public const int BadgeUrlMinLength = 1;
            public const int BadgeUrlMaxLength = 2048;

            public const int StadiumNameMinLength = 3;
            public const int StadiumNameMaxLength = 40;
        }

        public static class League
        {
            public const int CountryNameMinLength = 2;
            public const int CountryNameMaxLength = 56;

            public const int LogoUrlMinLength = 1;
            public const int LogoUrlMaxLength = 2048;
        }
    }
}
