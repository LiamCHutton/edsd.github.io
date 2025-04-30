using EDStationDatabase.Models;
using EDStationDatabase.Models.ViewModels;
using EDStationDatabase.Models.StationSearch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EDStationDatabase.Controllers
{
    public class EdsmController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "4c2cb075a98b48e005ea56e2b8a81b27bfe0b74a"; // Move to config in production

        public EdsmController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> SearchSystem(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
            {
                return View("Error", "System name is required.");
            }

            var url = $"https://www.edsm.net/api-v1/system?systemName={Uri.EscapeDataString(systemName)}&apiKey={_apiKey}&showCoordinates=1&showInformation=1";

            try
            {
                var json = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<SystemInfoViewModel>(json);
                return View("SystemResult", result);
            }
            catch (HttpRequestException ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<IActionResult> GetStationsInSystem(
           string systemName = null,
           string stationName = null,
           bool? hasMarket = null,
           bool? hasShipyard = null,
           bool? hasOutfitting = null,
           string stationType = null,
           string government = null,
           string economy = null,
           long? maxDistance = null,
           bool includeFleetCarriers = false,
           int radius = 100,
           bool includeNearbyStations = false)
        {
            if (maxDistance.HasValue && maxDistance < 0)
            {
                ModelState.AddModelError("maxDistance", "Maximum distance must be between 0 and 10,000 light-years.");
                return View("Error", "Invalid maximum distance value.");
            }

            // If both system name and station name are empty, show an error
            if (string.IsNullOrWhiteSpace(systemName) && string.IsNullOrWhiteSpace(stationName))
            {
                return View("Error", "Either system name or station name is required.");
            }

            StationListViewModel viewModel = new StationListViewModel
            {
                Stations = new List<Station>()
            };

            try
            {
                // Case 1: Station name search without system name (search across systems)
                if (!string.IsNullOrWhiteSpace(stationName) && string.IsNullOrWhiteSpace(systemName))
                {
                    viewModel = await SearchStationsAcrossSystems(stationName, radius, hasMarket, hasShipyard, hasOutfitting, stationType, government, economy, maxDistance, includeFleetCarriers);
                }
                // Case 2: System name provided (with or without station name)
                else
                {
                    if (includeNearbyStations)
                    {
                        viewModel = await GetStationsInSystemAndNearby(systemName, stationName, radius, hasMarket, hasShipyard, hasOutfitting, stationType, government, economy, maxDistance, includeFleetCarriers);
                    }
                    else
                    {
                        viewModel = await GetStationsInSpecificSystem(systemName, stationName, hasMarket, hasShipyard, hasOutfitting, stationType, government, economy, maxDistance, includeFleetCarriers);
                    }
                }

                return View("~/Views/Edsm/StationResult.cshtml", viewModel);
            }
            catch (HttpRequestException ex)
            {
                return View("Error", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing API response: {ex.Message}");
                return View("Error", $"Error processing API response: {ex.Message}");
            }
        }

        private async Task<StationListViewModel> GetStationsInSystemAndNearby(
            string systemName,
            string stationName = null,
            int radius = 100,
            bool? hasMarket = null,
            bool? hasShipyard = null,
            bool? hasOutfitting = null,
            string stationType = null,
            string government = null,
            string economy = null,
            long? maxDistance = null,
            bool includeFleetCarriers = false)
        {
            // First get stations in the specified system
            var primarySystemViewModel = await GetStationsInSpecificSystem(
                systemName, stationName, hasMarket, hasShipyard, hasOutfitting,
                stationType, government, economy, maxDistance, includeFleetCarriers);

            // Find nearby systems
            var nearbySystems = await GetNearbySystems(systemName, radius);
            var allStations = new List<Station>(primarySystemViewModel.Stations);

            // Process each nearby system, but limit processing to maintain performance
            // We'll process systems until we have around 50 stations or hit our system limit
            int processedSystems = 0;
            foreach (var system in nearbySystems)
            {
                // Stop if we already have 50 stations or processed 15 systems
                if (allStations.Count >= 50 || processedSystems >= 15)
                    break;

                processedSystems++;

                try
                {
                    var nearbySystemName = system.name.ToString();
                    var stationsUrl = $"https://www.edsm.net/api-system-v1/stations?systemName={Uri.EscapeDataString(nearbySystemName)}&apiKey={_apiKey}";
                    var stationsJson = await _httpClient.GetStringAsync(stationsUrl);
                    dynamic responseObj = JsonConvert.DeserializeObject<dynamic>(stationsJson);

                    if (responseObj.stations == null)
                        continue;

                    // Process stations in this nearby system
                    foreach (var stationData in responseObj.stations)
                    {
                        var station = new Station
                        {
                            Name = stationData.name,
                            Type = stationData.type,
                            Government = stationData.government,
                            Allegiance = stationData.allegiance,
                            Economy = stationData.economy,
                            HasMarket = stationData.haveMarket ?? false,
                            HasShipyard = stationData.haveShipyard ?? false,
                            HasOutfitting = stationData.haveOutfitting ?? false,
                            DistanceToArrival = stationData.distanceToArrival,
                            SystemName = nearbySystemName,
                            DistanceFromReference = system.distance
                        };

                        // Apply filters
                        bool includeStation = true;

                        if (!string.IsNullOrEmpty(stationName) &&
                            !station.Name.Contains(stationName, StringComparison.OrdinalIgnoreCase))
                            includeStation = false;

                        if (hasMarket == true && !station.HasMarket)
                            includeStation = false;

                        if (hasShipyard == true && !station.HasShipyard)
                            includeStation = false;

                        if (hasOutfitting == true && !station.HasOutfitting)
                            includeStation = false;

                        if (!string.IsNullOrEmpty(stationType) && station.Type != stationType)
                            includeStation = false;

                        if (!string.IsNullOrEmpty(government) && station.Government != government)
                            includeStation = false;

                        if (!string.IsNullOrEmpty(economy) && station.Economy != economy)
                            includeStation = false;

                        if (maxDistance.HasValue && station.DistanceToArrival.HasValue && station.DistanceToArrival > maxDistance)
                            includeStation = false;

                        if (!includeFleetCarriers && station.Type == "Fleet Carrier")
                            includeStation = false;

                        if (includeStation)
                        {
                            allStations.Add(station);

                            // Stop adding stations if we hit our limit
                            if (allStations.Count >= 50)
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Skip systems that cause errors
                    Console.WriteLine($"Error processing system {system.name}: {ex.Message}");
                    continue;
                }
            }

            // Sort by distance from reference system
            allStations = allStations
                .OrderBy(s => s.DistanceFromReference ?? 0)
                .ThenBy(s => s.SystemName)
                .ThenBy(s => s.Name)
                .Take(50)
                .ToList();

            return new StationListViewModel
            {
                Name = primarySystemViewModel.Name,
                IsMultiSystemSearch = true,
                IncludesNearbyStations = true,
                SearchRadius = radius,
                TotalStationCount = allStations.Count,
                Stations = allStations
            };
        }

        private async Task<List<dynamic>> GetNearbySystems(string systemName, int radius)
        {
            var systemsUrl = $"https://www.edsm.net/api-v1/sphere-systems?systemName={Uri.EscapeDataString(systemName)}&radius={radius}&apiKey={_apiKey}";
            var systemsJson = await _httpClient.GetStringAsync(systemsUrl);
            var systems = JsonConvert.DeserializeObject<List<dynamic>>(systemsJson);

            // Remove the reference system itself and sort by distance
            return systems
                .Where(s => s.name.ToString() != systemName)
                .OrderBy(s => (double)s.distance)
                .ToList();
        }

        private async Task<StationListViewModel> GetStationsInSpecificSystem(
            string systemName,
            string stationName = null,
            bool? hasMarket = null,
            bool? hasShipyard = null,
            bool? hasOutfitting = null,
            string stationType = null,
            string government = null,
            string economy = null,
            long? maxDistance = null,
            bool includeFleetCarriers = false)
        {
            var url = $"https://www.edsm.net/api-system-v1/stations?systemName={Uri.EscapeDataString(systemName)}&apiKey={_apiKey}";
            var json = await _httpClient.GetStringAsync(url);
            dynamic responseObj = JsonConvert.DeserializeObject<dynamic>(json);

            // Check if the system exists
            if (responseObj == null || responseObj.name == null)
            {
                throw new Exception($"The system '{systemName}' does not exist or could not be found.");
            }

            var viewModel = new StationListViewModel
            {
                Name = responseObj.name,
                IsMultiSystemSearch = false,
                IncludesNearbyStations = false,
                Stations = new List<Station>()
            };

            // Process and filter the stations
            foreach (var stationData in responseObj.stations)
            {
                var station = new Station
                {
                    Name = stationData.name,
                    Type = stationData.type,
                    Government = stationData.government,
                    Allegiance = stationData.allegiance,
                    Economy = stationData.economy,
                    HasMarket = stationData.haveMarket ?? false,
                    HasShipyard = stationData.haveShipyard ?? false,
                    HasOutfitting = stationData.haveOutfitting ?? false,
                    DistanceToArrival = stationData.distanceToArrival,
                    SystemName = responseObj.name,
                    DistanceFromReference = 0 // This is the reference system
                };

                // Apply filters
                bool includeStation = true;

                if (!string.IsNullOrEmpty(stationName) &&
                    !station.Name.Contains(stationName, StringComparison.OrdinalIgnoreCase))
                    includeStation = false;

                if (hasMarket == true && !station.HasMarket)
                    includeStation = false;

                if (hasShipyard == true && !station.HasShipyard)
                    includeStation = false;

                if (hasOutfitting == true && !station.HasOutfitting)
                    includeStation = false;

                if (!string.IsNullOrEmpty(stationType) && station.Type != stationType)
                    includeStation = false;

                if (!string.IsNullOrEmpty(government) && station.Government != government)
                    includeStation = false;

                if (!string.IsNullOrEmpty(economy) && station.Economy != economy)
                    includeStation = false;

                if (maxDistance.HasValue && station.DistanceToArrival.HasValue && station.DistanceToArrival > maxDistance)
                    includeStation = false;

                if (!includeFleetCarriers && station.Type == "Fleet Carrier")
                    includeStation = false;

                if (includeStation)
                {
                    viewModel.Stations.Add(station);
                }
            }

            return viewModel;
        }

        private async Task<StationListViewModel> SearchStationsAcrossSystems(
            string stationName,
            int radius = 100,
            bool? hasMarket = null,
            bool? hasShipyard = null,
            bool? hasOutfitting = null,
            string stationType = null,
            string government = null,
            string economy = null,
            long? maxDistance = null,
            bool includeFleetCarriers = false)
        {

            var refSystemUrl = $"https://www.edsm.net/api-v1/sphere-systems?systemName=Sol&radius={radius}&apiKey={_apiKey}";
            var refJson = await _httpClient.GetStringAsync(refSystemUrl);
            var systems = JsonConvert.DeserializeObject<List<dynamic>>(refJson);

            var viewModel = new StationListViewModel
            {
                Name = $"Stations matching '{stationName}'",
                IsMultiSystemSearch = true,
                IncludesNearbyStations = true,
                SearchRadius = radius,
                Stations = new List<Station>()
            };

            // For each system, check if it has stations matching our criteria
            int processedSystems = 0;
            foreach (var system in systems)
            {
                // Limit the number of systems we process to improve performance
                // and stop once we have enough stations
                if (viewModel.Stations.Count >= 50 || processedSystems >= 15)
                    break;

                processedSystems++;

                try
                {
                    string systemName = system.name;
                    var stationsUrl = $"https://www.edsm.net/api-system-v1/stations?systemName={Uri.EscapeDataString(systemName)}&apiKey={_apiKey}";
                    var stationsJson = await _httpClient.GetStringAsync(stationsUrl);
                    dynamic responseObj = JsonConvert.DeserializeObject<dynamic>(stationsJson);

                    if (responseObj.stations == null)
                        continue;

                    foreach (var stationData in responseObj.stations)
                    {
                        var station = new Station
                        {
                            Name = stationData.name,
                            Type = stationData.type,
                            Government = stationData.government,
                            Allegiance = stationData.allegiance,
                            Economy = stationData.economy,
                            HasMarket = stationData.haveMarket ?? false,
                            HasShipyard = stationData.haveShipyard ?? false,
                            HasOutfitting = stationData.haveOutfitting ?? false,
                            DistanceToArrival = stationData.distanceToArrival,
                            SystemName = systemName,  // Include the system name
                            DistanceFromReference = system.distance  // Distance from reference system (Sol)
                        };

                        // Apply filters
                        bool includeStation = true;

                        // Station name filter is mandatory for cross-system search
                        if (!station.Name.Contains(stationName, StringComparison.OrdinalIgnoreCase))
                            includeStation = false;

                        if (hasMarket == true && !station.HasMarket)
                            includeStation = false;

                        if (hasShipyard == true && !station.HasShipyard)
                            includeStation = false;

                        if (hasOutfitting == true && !station.HasOutfitting)
                            includeStation = false;

                        if (!string.IsNullOrEmpty(stationType) && station.Type != stationType)
                            includeStation = false;

                        if (!string.IsNullOrEmpty(government) && station.Government != government)
                            includeStation = false;

                        if (!string.IsNullOrEmpty(economy) && station.Economy != economy)
                            includeStation = false;

                        if (maxDistance.HasValue && station.DistanceToArrival.HasValue && station.DistanceToArrival > maxDistance)
                            includeStation = false;

                        if (!includeFleetCarriers && station.Type == "Fleet Carrier")
                            includeStation = false;

                        if (includeStation)
                        {
                            viewModel.Stations.Add(station);

                            // Stop adding stations if we hit our limit
                            if (viewModel.Stations.Count >= 50)
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Skip systems that cause errors
                    Console.WriteLine($"Error processing system {system.name}: {ex.Message}");
                    continue;
                }
            }

            // Sort by distance from reference
            viewModel.Stations = viewModel.Stations
                .OrderBy(s => s.DistanceFromReference)
                .ThenBy(s => s.SystemName)
                .ThenBy(s => s.Name)
                .Take(50)
                .ToList();

            viewModel.TotalStationCount = viewModel.Stations.Count;

            return viewModel;
        }

        [HttpGet]
        public async Task<JsonResult> GetStationSuggestions(string systemName, string query)
        {
            if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
            {
                return Json(new List<string>());
            }

            List<string> suggestions = new List<string>();

            // If system name is provided, get suggestions from that system
            if (!string.IsNullOrWhiteSpace(systemName))
            {
                var url = $"https://www.edsm.net/api-system-v1/stations?systemName={Uri.EscapeDataString(systemName)}&apiKey={_apiKey}";

                try
                {
                    var json = await _httpClient.GetStringAsync(url);
                    dynamic responseObj = JsonConvert.DeserializeObject<dynamic>(json);

                    foreach (var stationData in responseObj.stations)
                    {
                        string stationName = stationData.name;
                        if (stationName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            suggestions.Add(stationName);
                        }
                    }
                }
                catch
                {
                    // Return empty list on error
                }
            }
            // If no system name, search across popular systems
            else
            {
                // For demonstration, we'll search in a few popular systems
                string[] popularSystems = { "Sol", "Shinrarta Dezhra", "Jameson Memorial", "Achenar", "Alioth", "LHS 20" };

                foreach (var system in popularSystems)
                {
                    try
                    {
                        var url = $"https://www.edsm.net/api-system-v1/stations?systemName={Uri.EscapeDataString(system)}&apiKey={_apiKey}";
                        var json = await _httpClient.GetStringAsync(url);
                        dynamic responseObj = JsonConvert.DeserializeObject<dynamic>(json);

                        foreach (var stationData in responseObj.stations)
                        {
                            string stationName = stationData.name;
                            if (stationName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                suggestions.Add($"{stationName} ({system})");
                            }
                        }
                    }
                    catch
                    {
                        // Skip systems that cause errors
                        continue;
                    }
                }
            }

            return Json(suggestions.Take(10));
        }
    }
}