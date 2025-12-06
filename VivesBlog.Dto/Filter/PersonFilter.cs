namespace VivesBlog.Dto.Filter
{
    public class PersonFilter
    {
        public string? Search { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public bool UseEmailFilter { get; set; }
        public string? Email { get; set; }

    }
}
