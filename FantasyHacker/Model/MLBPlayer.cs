using System;
using System.Collections.Generic;
using FantasyHacker.Interface;
using System.Linq;
namespace FantasyHacker.Model
{
    /// <summary>
    /// Represents the stats of a specific player for a specific game. Also can injest season stats of that player for the season of the game.
    /// </summary>
    public class MLBPlayer : IFantasyPlayer
    {
        public int PlayerId { get; set; }
        public BoxScoreResponse.BoxScore BoxScore { get; private set; }
        public BoxScoreResponse.Player PlayerGameStats { get; private set; }
        public PersonResponse.Person SeasonStats { get; private set; }
        public BoxScoreResponse.Person Person { get => PlayerGameStats?.Person; }
        public BoxScoreResponse.Position Position { get => PlayerGameStats?.Position; }
        public BoxScoreResponse.Stats Stats { get => PlayerGameStats?.Stats; }

        public MLBPlayer(int playerId)
        {
            PlayerId = playerId;
        }

        /// <summary>
        /// Injests a game box score and extracts and stores the stats for this player.
        /// </summary>
        /// <param name="boxScore"></param>
        public void InjestBoxScore(BoxScoreResponse.BoxScore boxScore)
        {
            BoxScore = boxScore;
            foreach (var team in new List<ITeamGame>() { boxScore.Teams.Away, boxScore.Teams.Home})
            {
                foreach (var player in team.Players.Values)
                {
                    if(player.Person.Id == PlayerId)
                    {
                        PlayerGameStats = player;
                        break;
                    }
                }
                if(PlayerGameStats != null)
                {
                    break;
                }
            }
            if(PlayerGameStats == null)
            {
                throw new Exception("Player not found in box score");
            }
        }

        public void InjestSeasonStats(PersonResponse.PersonResponseRoot personResponseRoot)
        {
            SeasonStats = personResponseRoot.People.Where(x => x.Id == PlayerId).FirstOrDefault();
            if(SeasonStats == null)
            {
                throw new Exception("Player not found in season stats");
            }
        }

        public decimal Score()
        {
            return PlayerGameStats.Score();
        }
    }
}

