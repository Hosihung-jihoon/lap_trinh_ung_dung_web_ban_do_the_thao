namespace ECommerce.Application.DTOs.MasterData
{
    public class MasterColorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HexCode { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int SortOrder { get; set; }
    }

    public class CreateMasterColorDto
    {
        public string Name { get; set; } = string.Empty;
        public string HexCode { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int SortOrder { get; set; }
    }

    public class UpdateMasterColorDto
    {
        public string Name { get; set; } = string.Empty;
        public string HexCode { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int SortOrder { get; set; }
    }
}