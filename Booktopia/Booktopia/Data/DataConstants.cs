namespace Booktopia.Data
{
    public class DataConstants
    {
        public const int TitleMaxLength = 40;
        public const int TitleMinLength = 5;
        public class Book
        {
            public const int AnnotationMaxLength = 1000;
            public const int AnnotationMinLength = 600;
        }

        public class Chapter
        {
            public const int MaxText = 2500;
            public const int MinText = 1000;
        }

        public class Review
        {
            public const int MaxText = 400;
            public const int MinText = 100;
            public const int MaxRating = 5;
            public const int MinRating = 1;
        }

        public class Quote
        {
            public const int MinText = 20;
            public const int MaxText = 200;
        }

        public class Author
        {
            public const int MaxName = 30;
            public const int MinName = 3;
            public const int MaxDescription = 300;
            public const int MinDescription = 30;
        }

        public class Category
        {
            public const int MaxType = 20;
        }

        public class User
        {
            public const int MinFullName = 3;
            public const int MaxFullName = 40;
            public const int MinPassword = 6;
            public const int MaxPassword = 100;
        }
        
    }
}
