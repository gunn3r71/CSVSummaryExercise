namespace CSVSummaryExercise.Extension
{
    public static class StringExtensions
    {
        public static string ToDirectory(this string myString)
        {
            return @"\" + myString;
        }
    }
}