public class ArticleModelConstants
{
    public class Article
    {
        public const int MinTitleLength = 1;
        public const int MaxTitleLength = 50;
        public const int MinSubtitleLength = 1;
        public const int MaxSubtitleLength = 50;
        public const int MinTextLength = 1;
        public const int MaxTextLength = 50;

    }

    public class Price
    {
        public const int MaxAmountDigits = 10; // Assuming a maximum of 10 digits for the price amount
        public const int MaxCurrencyLength = 3; // Assuming a maximum currency code length of 3 characters
    }

    public class Weight
    {
        public const int MaxValueDigits = 10; // Assuming a maximum of 10 digits for the weight value
        public const int MaxUnitLength = 10; // Assuming a maximum length of 10 characters for the weight unit
    }
}