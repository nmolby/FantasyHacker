using System.Collections.Generic;
using System.Text.Json.Serialization;
using FantasyHacker.Interface;

namespace FantasyHacker.BoxScoreResponse
{
    
    public class Venue
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }

    public class SpringVenue
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

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

    public class Division
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

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class LeagueRecord
    {
        [JsonPropertyName("wins")]
        public int Wins { get; set; }

        [JsonPropertyName("losses")]
        public int Losses { get; set; }

        [JsonPropertyName("pct")]
        public string Pct { get; set; }
    }

    public class Records
    {
    }

    public class Record
    {
        [JsonPropertyName("gamesPlayed")]
        public int GamesPlayed { get; set; }

        [JsonPropertyName("wildCardGamesBack")]
        public string WildCardGamesBack { get; set; }

        [JsonPropertyName("leagueGamesBack")]
        public string LeagueGamesBack { get; set; }

        [JsonPropertyName("springLeagueGamesBack")]
        public string SpringLeagueGamesBack { get; set; }

        [JsonPropertyName("sportGamesBack")]
        public string SportGamesBack { get; set; }

        [JsonPropertyName("divisionGamesBack")]
        public string DivisionGamesBack { get; set; }

        [JsonPropertyName("conferenceGamesBack")]
        public string ConferenceGamesBack { get; set; }

        [JsonPropertyName("leagueRecord")]
        public LeagueRecord LeagueRecord { get; set; }

        [JsonPropertyName("records")]
        public Records Records { get; set; }

        [JsonPropertyName("divisionLeader")]
        public bool DivisionLeader { get; set; }

        [JsonPropertyName("wins")]
        public int Wins { get; set; }

        [JsonPropertyName("losses")]
        public int Losses { get; set; }

        [JsonPropertyName("winningPercentage")]
        public string WinningPercentage { get; set; }
    }

    public class SpringLeague
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }
    }

    public class Team
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("season")]
        public int Season { get; set; }

        [JsonPropertyName("venue")]
        public Venue Venue { get; set; }

        [JsonPropertyName("springVenue")]
        public SpringVenue SpringVenue { get; set; }

        [JsonPropertyName("teamCode")]
        public string TeamCode { get; set; }

        [JsonPropertyName("fileCode")]
        public string FileCode { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonPropertyName("teamName")]
        public string TeamName { get; set; }

        [JsonPropertyName("locationName")]
        public string LocationName { get; set; }

        [JsonPropertyName("firstYearOfPlay")]
        public string FirstYearOfPlay { get; set; }

        [JsonPropertyName("league")]
        public League League { get; set; }

        [JsonPropertyName("division")]
        public Division Division { get; set; }

        [JsonPropertyName("sport")]
        public Sport Sport { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("record")]
        public Record Record { get; set; }

        [JsonPropertyName("franchiseName")]
        public string FranchiseName { get; set; }

        [JsonPropertyName("clubName")]
        public string ClubName { get; set; }

        [JsonPropertyName("springLeague")]
        public SpringLeague SpringLeague { get; set; }

        [JsonPropertyName("allStarStatus")]
        public string AllStarStatus { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }

    public class Fielding
    {
        [JsonPropertyName("assists")]
        public int Assists { get; set; }

        [JsonPropertyName("putOuts")]
        public int PutOuts { get; set; }

        [JsonPropertyName("errors")]
        public int Errors { get; set; }

        [JsonPropertyName("chances")]
        public int Chances { get; set; }

        [JsonPropertyName("caughtStealing")]
        public int CaughtStealing { get; set; }

        [JsonPropertyName("passedBall")]
        public int PassedBall { get; set; }

        [JsonPropertyName("stolenBases")]
        public int StolenBases { get; set; }

        [JsonPropertyName("stolenBasePercentage")]
        public string StolenBasePercentage { get; set; }

        [JsonPropertyName("pickoffs")]
        public int Pickoffs { get; set; }

        [JsonPropertyName("fielding")]
        public string FieldingPct { get; set; }

        [JsonPropertyName("gamesStarted")]
        public int GamesStarted { get; set; }
    }

    public class TeamStats
    {
        [JsonPropertyName("batting")]
        public Batting Batting { get; set; }

        [JsonPropertyName("pitching")]
        public Pitching Pitching { get; set; }

        [JsonPropertyName("fielding")]
        public Fielding Fielding { get; set; }
    }

    public class Person
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }

    public class Position
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

    public class Stats
    {
        [JsonPropertyName("batting")]
        public Batting Batting { get; set; }

        [JsonPropertyName("pitching")]
        public Pitching Pitching { get; set; }

        [JsonPropertyName("fielding")]
        public Fielding Fielding { get; set; }
    }

    public class Status
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class SeasonStats
    {
        [JsonPropertyName("batting")]
        public Batting Batting { get; set; }

        [JsonPropertyName("pitching")]
        public Pitching Pitching { get; set; }

        [JsonPropertyName("fielding")]
        public Fielding Fielding { get; set; }
    }

    public class GameStatus
    {
        [JsonPropertyName("isCurrentBatter")]
        public bool IsCurrentBatter { get; set; }

        [JsonPropertyName("isCurrentPitcher")]
        public bool IsCurrentPitcher { get; set; }

        [JsonPropertyName("isOnBench")]
        public bool IsOnBench { get; set; }

        [JsonPropertyName("isSubstitute")]
        public bool IsSubstitute { get; set; }
    }

    public class AllPosition
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


    public class FieldList
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class Info
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("fieldList")]
        public List<FieldList> FieldList { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class Away : ITeamGame
    {
        [JsonPropertyName("team")]
        public Team Team { get; set; }

        [JsonPropertyName("teamStats")]
        public TeamStats TeamStats { get; set; }

        [JsonPropertyName("players")]
        public Dictionary<string, Player> Players { get; set; }

        [JsonPropertyName("batters")]
        public List<int> Batters { get; set; }

        [JsonPropertyName("pitchers")]
        public List<int> Pitchers { get; set; }

        [JsonPropertyName("bench")]
        public List<int> Bench { get; set; }

        [JsonPropertyName("bullpen")]
        public List<int> Bullpen { get; set; }

        [JsonPropertyName("battingOrder")]
        public List<int> BattingOrder { get; set; }

        [JsonPropertyName("info")]
        public List<Info> Info { get; set; }

        [JsonPropertyName("note")]
        public List<Note> Note { get; set; }
    }

    public class Note
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class Home : ITeamGame
    {
        [JsonPropertyName("team")]
        public Team Team { get; set; }

        [JsonPropertyName("teamStats")]
        public TeamStats TeamStats { get; set; }

        [JsonPropertyName("players")]
        public Dictionary<string, Player> Players { get; set; }

        [JsonPropertyName("batters")]
        public List<int> Batters { get; set; }

        [JsonPropertyName("pitchers")]
        public List<int> Pitchers { get; set; }

        [JsonPropertyName("bench")]
        public List<int> Bench { get; set; }

        [JsonPropertyName("bullpen")]
        public List<int> Bullpen { get; set; }

        [JsonPropertyName("battingOrder")]
        public List<int> BattingOrder { get; set; }

        [JsonPropertyName("info")]
        public List<Info> Info { get; set; }

        [JsonPropertyName("note")]
        public List<Note> Note { get; set; }
    }

    public class Teams
    {
        [JsonPropertyName("away")]
        public Away Away { get; set; }

        [JsonPropertyName("home")]
        public Home Home { get; set; }
    }


    public class OfficialDetails
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }

    public class Official
    {
        [JsonPropertyName("official")]
        public OfficialDetails OfficialDetails { get; set; }

        [JsonPropertyName("officialType")]
        public string OfficialType { get; set; }
    }


}
