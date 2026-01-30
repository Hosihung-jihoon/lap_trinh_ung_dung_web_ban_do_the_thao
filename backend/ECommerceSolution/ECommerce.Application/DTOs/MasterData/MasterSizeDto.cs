namespace ECommerce.Application.DTOs.MasterData
{
    public class MasterSizeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Type { get; set; }
        public int SortOrder { get; set; }
    }

    public class CreateMasterSizeDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Type { get; set; }
        public int SortOrder { get; set; }
    }

    public class UpdateMasterSizeDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Type { get; set; }
        public int SortOrder { get; set; }
    }
}