using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Application.Mappers
{
    public static class RegionMapper
    {
        public static readonly Dictionary<string, string> RegionMap = new()
        {
            { "euw1", "EUW" },
            { "eun1", "EUNE" },
            { "na1", "NA" },
            { "kr", "KR" },
            { "br1", "BR" },
            { "la1", "LAN" },
            { "la2", "LAS" },
            { "oc1", "OCE" },
            { "tr1", "TR" },
            { "ru", "RU" },
            { "jp1", "JP" }
        };

        public static readonly Dictionary<string, string> RegionToRoutingMap = new()
        {
            { "na1", "americas" },
            { "br1", "americas" },
            { "la1", "americas" },
            { "la2", "americas" },
            { "kr", "asia" },
            { "jp1", "asia" },
            { "eun1", "europe" },
            { "euw1", "europe" },
            { "me1", "europe" },
            { "tr1", "europe" },
            { "ru", "europe" },
            { "oc1", "sea" },
            { "sg2", "sea" },
            { "tw2", "sea" },
            { "vn2", "sea" }
        };

        public static string GetRouting(string region) =>
            RegionToRoutingMap.TryGetValue(region.ToLower(), out var routing) ? routing : "europe";

        public static string GetRegion(string region) =>
            RegionMap.TryGetValue(region.ToLower(), out var readable) ? readable : region.ToUpper();
    }
}
