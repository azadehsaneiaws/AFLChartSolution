using DepthChart.Domain.Entities;
using DepthChart.Domain.Services;

namespace DepthChart.Domain.Repositories
{
    public interface IDepthChartRepository
    {
        DepthChartService GetDepthChart();
        void Save(DepthChartService depthChart);
    }
}
