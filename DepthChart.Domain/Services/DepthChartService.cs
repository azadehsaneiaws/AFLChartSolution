using DepthChart.Domain.Entities;
using DepthChart.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace DepthChart.Domain.Services
{
    public class DepthChartService
    {
        private readonly IDepthChartRepository _depthChartRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly Dictionary<string, List<Player>> _depthChart;

        public DepthChartService(IDepthChartRepository depthChartRepository, IPlayerRepository playerRepository)
        {
            _depthChartRepository = depthChartRepository;
            _playerRepository = playerRepository;
            _depthChart = new Dictionary<string, List<Player>>();
        }

        public void AddPlayerToDepthChart(string position, Player player, int? positionDepth = null)
        {
            if (!_depthChart.ContainsKey(position))
            {
                _depthChart[position] = new List<Player>();
            }

            if (positionDepth.HasValue && positionDepth.Value < _depthChart[position].Count)
            {
                _depthChart[position].Insert(positionDepth.Value, player);
            }
            else
            {
                _depthChart[position].Add(player);
            }
        }

        public Player RemovePlayerFromDepthChart(string position, Player player)
        {
            if (!_depthChart.ContainsKey(position))
            {
                return null;
            }

            _depthChart[position].Remove(player);
            return player;
        }

        public List<Player> GetBackups(string position, Player player)
        {
            if (!_depthChart.ContainsKey(position) || !_depthChart[position].Contains(player))
            {
                return new List<Player>();
            }

            int playerIndex = _depthChart[position].IndexOf(player);
            return _depthChart[position].Skip(playerIndex + 1).ToList();
        }

        public Dictionary<string, List<Player>> GetFullDepthChart()
        {
            return _depthChart.ToDictionary(entry => entry.Key, entry => entry.Value.ToList());
        }
    }
}
