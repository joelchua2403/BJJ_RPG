using System.Text.Json.Serialization;

namespace VS_RPG.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        GuardPlayer = 1,
        Wrestler = 2,
        GuardPasser = 3,
        Scrambler = 4,
        NoGi = 5,
        LegLocker = 6,
        OldSchool = 7,
    }
}