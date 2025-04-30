using EDStationDatabase.Models.Category;
using EDStationDatabase.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace EDStationDatabase.Models
{
    public class StationBookmark
    {
        public int StationBookmarkId { get; set; }

        [Required(ErrorMessage = "Please enter a station name")]
        public string StationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a system name")]
        public string SystemName { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Please select an economy")]
        public int? EconomyId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select an allegiance")]
        public int? AllegianceId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a station type")]
        public int? StationTypeId { get; set; }

        public int? SuperpowerId { get; set; }

        [ValidateNever]
        public Superpower Superpower { get; set; } = null!;

        [ValidateNever]
        public Economy Economy { get; set; } = null!;

        [ValidateNever]
        public Allegiance Allegiance { get; set; } = null!;

        [ValidateNever]
        public StationType StationType { get; set; } = null!;

        public string Slug =>
            StationName?.Replace(' ', '-').ToLower() + '-' + SystemName?.Replace(' ', '-').ToLower();
    }
}