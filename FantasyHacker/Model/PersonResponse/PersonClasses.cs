using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FantasyHacker.PersonResponse
{
    public class PrimaryPosition
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }
    }

    public class Type
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }

    public class Group
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }

    public class Stat
    {
        [JsonPropertyName("gamesPlayed")]
        public int GamesPlayed { get; set; }

        [JsonPropertyName("groundOuts")]
        public int GroundOuts { get; set; }

        [JsonPropertyName("airOuts")]
        public int AirOuts { get; set; }

        [JsonPropertyName("runs")]
        public int Runs { get; set; }

        [JsonPropertyName("doubles")]
        public int Doubles { get; set; }

        [JsonPropertyName("triples")]
        public int Triples { get; set; }

        [JsonPropertyName("homeRuns")]
        public int HomeRuns { get; set; }

        [JsonPropertyName("strikeOuts")]
        public int StrikeOuts { get; set; }

        [JsonPropertyName("baseOnBalls")]
        public int BaseOnBalls { get; set; }

        [JsonPropertyName("intentionalWalks")]
        public int IntentionalWalks { get; set; }

        [JsonPropertyName("hits")]
        public int Hits { get; set; }

        [JsonPropertyName("hitByPitch")]
        public int HitByPitch { get; set; }

        [JsonPropertyName("avg")]
        public string Avg { get; set; }

        [JsonPropertyName("atBats")]
        public int AtBats { get; set; }

        [JsonPropertyName("obp")]
        public string Obp { get; set; }

        [JsonPropertyName("slg")]
        public string Slg { get; set; }

        [JsonPropertyName("ops")]
        public string Ops { get; set; }

        [JsonPropertyName("caughtStealing")]
        public int CaughtStealing { get; set; }

        [JsonPropertyName("stolenBases")]
        public int StolenBases { get; set; }

        [JsonPropertyName("stolenBasePercentage")]
        public string StolenBasePercentage { get; set; }

        [JsonPropertyName("groundIntoDoublePlay")]
        public int GroundIntoDoublePlay { get; set; }

        [JsonPropertyName("groundIntoTriplePlay")]
        public int GroundIntoTriplePlay { get; set; }

        [JsonPropertyName("numberOfPitches")]
        public int NumberOfPitches { get; set; }

        [JsonPropertyName("plateAppearances")]
        public int PlateAppearances { get; set; }

        [JsonPropertyName("totalBases")]
        public int TotalBases { get; set; }

        [JsonPropertyName("rbi")]
        public int Rbi { get; set; }

        [JsonPropertyName("leftOnBase")]
        public int LeftOnBase { get; set; }

        [JsonPropertyName("sacBunts")]
        public int SacBunts { get; set; }

        [JsonPropertyName("sacFlies")]
        public int SacFlies { get; set; }

        [JsonPropertyName("babip")]
        public string Babip { get; set; }

        [JsonPropertyName("groundOutsToAirouts")]
        public string GroundOutsToAirouts { get; set; }

        [JsonPropertyName("catchersInterference")]
        public int CatchersInterference { get; set; }

        [JsonPropertyName("atBatsPerHomeRun")]
        public string AtBatsPerHomeRun { get; set; }
    }

    public class Team
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }

    public class Player
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }

    public class League
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }

    public class Sport
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }
    }

    public class Opponent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }

    public class PositionsPlayed
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }
    }

    public class Content
    {
        [JsonPropertyName("link")]
        public string Link { get; set; }
    }

    public class Game
    {
        [JsonPropertyName("gamePk")]
        public int GamePk { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("content")]
        public Content Content { get; set; }

        [JsonPropertyName("gameNumber")]
        public int GameNumber { get; set; }
    }

    public class Split
    {
        [JsonPropertyName("season")]
        public string Season { get; set; }

        [JsonPropertyName("stat")]
        public Stat Stat { get; set; }

        [JsonPropertyName("team")]
        public Team Team { get; set; }

        [JsonPropertyName("player")]
        public Player Player { get; set; }

        [JsonPropertyName("league")]
        public League League { get; set; }

        [JsonPropertyName("sport")]
        public Sport Sport { get; set; }

        [JsonPropertyName("opponent")]
        public Opponent Opponent { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("gameType")]
        public string GameType { get; set; }

        [JsonPropertyName("isHome")]
        public bool IsHome { get; set; }

        [JsonPropertyName("isWin")]
        public bool IsWin { get; set; }

        [JsonPropertyName("positionsPlayed")]
        public List<PositionsPlayed> PositionsPlayed { get; set; }

        [JsonPropertyName("game")]
        public Game Game { get; set; }
    }

    public class StatGroup
    {
        [JsonPropertyName("type")]
        public Type Type { get; set; }

        [JsonPropertyName("group")]
        public Group Group { get; set; }

        [JsonPropertyName("exemptions")]
        public List<object> Exemptions { get; set; }

        [JsonPropertyName("splits")]
        public List<Split> Splits { get; set; }
    }

    public class BatSide
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class PitchHand
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class Person
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("primaryNumber")]
        public string PrimaryNumber { get; set; }

        [JsonPropertyName("birthDate")]
        public string BirthDate { get; set; }

        [JsonPropertyName("currentAge")]
        public int CurrentAge { get; set; }

        [JsonPropertyName("birthCity")]
        public string BirthCity { get; set; }

        [JsonPropertyName("birthStateProvince")]
        public string BirthStateProvince { get; set; }

        [JsonPropertyName("birthCountry")]
        public string BirthCountry { get; set; }

        [JsonPropertyName("height")]
        public string Height { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("primaryPosition")]
        public PrimaryPosition PrimaryPosition { get; set; }

        [JsonPropertyName("useName")]
        public string UseName { get; set; }

        [JsonPropertyName("middleName")]
        public string MiddleName { get; set; }

        [JsonPropertyName("boxscoreName")]
        public string BoxscoreName { get; set; }

        [JsonPropertyName("nickName")]
        public string NickName { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("isPlayer")]
        public bool IsPlayer { get; set; }

        [JsonPropertyName("isVerified")]
        public bool IsVerified { get; set; }

        [JsonPropertyName("draftYear")]
        public int DraftYear { get; set; }

        [JsonPropertyName("pronunciation")]
        public string Pronunciation { get; set; }

        [JsonPropertyName("stats")]
        public List<StatGroup> Stats { get; set; }

        [JsonPropertyName("mlbDebutDate")]
        public string MlbDebutDate { get; set; }

        [JsonPropertyName("batSide")]
        public BatSide BatSide { get; set; }

        [JsonPropertyName("pitchHand")]
        public PitchHand PitchHand { get; set; }

        [JsonPropertyName("nameFirstLast")]
        public string NameFirstLast { get; set; }

        [JsonPropertyName("nameSlug")]
        public string NameSlug { get; set; }

        [JsonPropertyName("firstLastName")]
        public string FirstLastName { get; set; }

        [JsonPropertyName("lastFirstName")]
        public string LastFirstName { get; set; }

        [JsonPropertyName("lastInitName")]
        public string LastInitName { get; set; }

        [JsonPropertyName("initLastName")]
        public string InitLastName { get; set; }

        [JsonPropertyName("fullFMLName")]
        public string FullFMLName { get; set; }

        [JsonPropertyName("fullLFMName")]
        public string FullLFMName { get; set; }

        [JsonPropertyName("strikeZoneTop")]
        public double StrikeZoneTop { get; set; }

        [JsonPropertyName("strikeZoneBottom")]
        public double StrikeZoneBottom { get; set; }
    }

    public class PersonResponseRoot
    {
        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }

        [JsonPropertyName("people")]
        public List<Person> People { get; set; }
    }


}
