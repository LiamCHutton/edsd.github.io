using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EDStationDatabase.Models.Category;

namespace EDStationDatabase.Models
{
    public class EDAssetsContext : IdentityDbContext<User>
    {
        public DbSet<StationBookmark> StationBookmarks { get; set; } = null!;
        public DbSet<Economy> Economies { get; set; } = null!;
        public DbSet<Allegiance> Allegiances { get; set; } = null!;
        public DbSet<StationType> StationType { get; set; } = null!;
        public DbSet<Superpower> Superpowers { get; set; } = null!;

        public EDAssetsContext(DbContextOptions<EDAssetsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // List of economies
            modelBuilder.Entity<Economy>().HasData(
                new Economy() { EconomyId = 1, EconomyName = "Agriculture" },
                new Economy() { EconomyId = 2, EconomyName = "Colony" },
                new Economy() { EconomyId = 3, EconomyName = "Extraction" },
                new Economy() { EconomyId = 4, EconomyName = "High Tech" },
                new Economy() { EconomyId = 5, EconomyName = "Industrial" },
                new Economy() { EconomyId = 6, EconomyName = "Military" },
                new Economy() { EconomyId = 7, EconomyName = "Refinery" },
                new Economy() { EconomyId = 8, EconomyName = "Service" },
                new Economy() { EconomyId = 9, EconomyName = "Terraforming" },
                new Economy() { EconomyId = 10, EconomyName = "Tourism" },
                new Economy() { EconomyId = 11, EconomyName = "Prison Colony" }
            );

            // List of allegiances
            modelBuilder.Entity<Allegiance>().HasData(
                new Allegiance() { AllegianceId = 1, AllegianceName = "Aisling Duval" },
                new Allegiance() { AllegianceId = 2, AllegianceName = "Arissa Lavingy-Duval" },
                new Allegiance() { AllegianceId = 3, AllegianceName = "Denton Patreus" },
                new Allegiance() { AllegianceId = 4, AllegianceName = "Zemina Torval" },
                new Allegiance() { AllegianceId = 5, AllegianceName = "Jerome Archer" },
                new Allegiance() { AllegianceId = 6, AllegianceName = "Felicia Winters" },
                new Allegiance() { AllegianceId = 7, AllegianceName = "Edmund Mahon" },
                new Allegiance() { AllegianceId = 8, AllegianceName = "Nakato Kaine" },
                new Allegiance() { AllegianceId = 9, AllegianceName = "Archon Delaine" },
                new Allegiance() { AllegianceId = 10, AllegianceName = "Li Yong-Rui" },
                new Allegiance() { AllegianceId = 11, AllegianceName = "Parnav Antal" },
                new Allegiance() { AllegianceId = 12, AllegianceName = "Yuri Grom" },
                new Allegiance() { AllegianceId = 13, AllegianceName = "Independent" },
                new Allegiance() { AllegianceId = 14, AllegianceName = "Pilot's Federation" }
            );

            // List of station types
            modelBuilder.Entity<StationType>().HasData(
                new StationType() { StationTypeId = 1, StationTypeName = "Coriolis" },
                new StationType() { StationTypeId = 2, StationTypeName = "Orbis" },
                new StationType() { StationTypeId = 3, StationTypeName = "Ocellus" },
                new StationType() { StationTypeId = 4, StationTypeName = "Outpost" },
                new StationType() { StationTypeId = 5, StationTypeName = "Planetary Outpost" },
                new StationType() { StationTypeId = 6, StationTypeName = "Planetary Port" },
                new StationType() { StationTypeId = 7, StationTypeName = "Planetary Surface Port" },
                new StationType() { StationTypeId = 8, StationTypeName = "Planetary Settlement" },
                new StationType() { StationTypeId = 9, StationTypeName = "Asteroid Base" }
            );

            // List of superpowers
            modelBuilder.Entity<Superpower>().HasData(
                new Superpower() { SuperpowerId = 1, SuperpowerName = "Empire" },
                new Superpower() { SuperpowerId = 2, SuperpowerName = "Federation" },
                new Superpower() { SuperpowerId = 3, SuperpowerName = "Alliance" },
                new Superpower() { SuperpowerId = 4, SuperpowerName = "Independent" }
            );

            //Pre-populated database with some station bookmarks
            modelBuilder.Entity<StationBookmark>().HasData(
                new StationBookmark() { StationBookmarkId = 1, StationName = "Low City", SystemName = "HR 1980", EconomyId = 7, AllegianceId = 13, StationTypeId = 3 },
                new StationBookmark() { StationBookmarkId = 2, StationName = "Ray Gateway", SystemName = "Diaguandri", EconomyId = 4, AllegianceId = 10, StationTypeId = 1 },
                new StationBookmark() { StationBookmarkId = 3, StationName = "Copernicus Observatory", SystemName = "Asterope", EconomyId = 6, AllegianceId = 13, StationTypeId = 1 }
            );
        }
    }
}
