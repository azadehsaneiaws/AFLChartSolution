using DepthChart.Domain.Services;

namespace DepthChart.Domain.Repositories
{
    public class InMemoryDepthChartRepository : IDepthChartRepository
    {
        private DepthChartService _depthChart;

        public InMemoryDepthChartRepository(IPlayerRepository playerRepository)
        {
            // I will Create a new DepthChartService with the required dependencies
            _depthChart = new DepthChartService(this, playerRepository);
        }

        public DepthChartService GetDepthChart()
        {
            return _depthChart;
        }

        public void Save(DepthChartService depthChart)
        {
            _depthChart = depthChart;
        }
    }
}
