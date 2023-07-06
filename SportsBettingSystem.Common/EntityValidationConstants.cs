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
            public const int NameMaxLength = 50;
        }
    }
}
