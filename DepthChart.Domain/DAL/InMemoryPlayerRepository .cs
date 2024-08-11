using DepthChart.Domain.Entities;
using System.Collections.Generic;

namespace DepthChart.Domain.Repositories
{
    // I used InMemoryRepository, trying to get the basic functionality working, However in real world If I wanted to do this, I would Make My service 
    // Multi DB Compatible, by creating specific implementations of these interfaces for each database(eg. DynamoDB, SQL, InMemory, etc)
    //eg for SQL     public class SqlServerPlayerRepository : IPlayerRepository & Would set up Connection string in my Constructor

    public class InMemoryPlayerRepository : IPlayerRepository
    {
        private readonly Dictionary<int, Player> _players = new Dictionary<int, Player>();

        public Player GetPlayerById(int number)
        {
            return _players.TryGetValue(number, out var player) ? player : null;
        }

        public void AddPlayer(Player player)
        {
            if (!_players.ContainsKey(player.Number))
            {
                _players[player.Number] = player;
            }
        }

        public void RemovePlayer(Player player)
        {
            _players.Remove(player.Number);
        }
    }
}
