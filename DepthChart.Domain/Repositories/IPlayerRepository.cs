using DepthChart.Domain.Entities;

namespace DepthChart.Domain.Repositories
{
    public interface IPlayerRepository
    {
        Player GetPlayerById(int number);
        void AddPlayer(Player player);
        void RemovePlayer(Player player);
    }
}
