/// <summary>
/// This class contains unit tests for the DepthChartService class.
//Step 1: TDD: I created a new test class called DepthChartServiceTests
/// </summary>
using DepthChart.Domain.Entities;
using DepthChart.Domain.Repositories;
using DepthChart.Domain.Services;
using Xunit;
using System.Collections.Generic;

namespace DepthChart.Tests.Services
{
    public class DepthChartServiceTests
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IDepthChartRepository _depthChartRepository;
        private readonly DepthChartService _service;

        public DepthChartServiceTests()
        {
            // Initialize my InMemoryPlayerRepository and InMemoryDepthChartRepository
            _playerRepository = new InMemoryPlayerRepository();
            _depthChartRepository = new InMemoryDepthChartRepository(_playerRepository);

            // Initialize my DepthChartService with the repositories
            _service = new DepthChartService(_depthChartRepository, _playerRepository);
        }

        [Fact]
        public void AddPlayerToDepthChart_ShouldAddPlayerToPosition()
        {
            // Arrange
            var player = new Player(12, "Tom Brady", "QB");

            // Act
            _service.AddPlayerToDepthChart("QB", player);

            // Assert
            var depthChart = _service.GetFullDepthChart();
            Assert.Single(depthChart["QB"]);
            Assert.Equal("Tom Brady", depthChart["QB"][0].Name);
        }

        [Fact]
        public void RemovePlayerFromDepthChart_ShouldRemovePlayer()
        {
            // Arrange
            var player = new Player(12, "Tom Brady", "QB");
            _service.AddPlayerToDepthChart("QB", player);

            // Act
            var removedPlayer = _service.RemovePlayerFromDepthChart("QB", player);

            // Assert
            var depthChart = _service.GetFullDepthChart();
            Assert.Empty(depthChart["QB"]);
            Assert.Equal("Tom Brady", removedPlayer.Name);
        }

        [Fact]
        public void GetBackups_ShouldReturnBackups()
        {
            // Arrange
            var player1 = new Player(12, "Tom Brady", "QB");
            var player2 = new Player(11, "Blaine Gabbert", "QB");
            var player3 = new Player(2, "Kyle Trask", "QB");
            _service.AddPlayerToDepthChart("QB", player1);
            _service.AddPlayerToDepthChart("QB", player2);
            _service.AddPlayerToDepthChart("QB", player3);

            // Act
            var backups = _service.GetBackups("QB", player1);

            // Assert
            Assert.Equal(2, backups.Count);
            Assert.Equal("Blaine Gabbert", backups[0].Name);
            Assert.Equal("Kyle Trask", backups[1].Name);
        }

        [Fact]
        public void GetBackups_ShouldReturnEmptyList_WhenPlayerHasNoBackups()
        {
            // Arrange
            var player1 = new Player(12, "Tom Brady", "QB");
            _service.AddPlayerToDepthChart("QB", player1);

            // Act
            var backups = _service.GetBackups("QB", player1);

            // Assert
            Assert.Empty(backups);
        }

        [Fact]
        public void GetFullDepthChart_ShouldReturnTheCorrectDepthChart()
        {
            // Arrange
            var player1 = new Player(12, "Tom Brady", "QB");
            var player2 = new Player(13, "Mike Evans", "WR");
            _service.AddPlayerToDepthChart("QB", player1);
            _service.AddPlayerToDepthChart("WR", player2);

            // Act
            var depthChart = _service.GetFullDepthChart();

            // Assert
            Assert.Equal(2, depthChart.Count);
            Assert.Single(depthChart["QB"]);
            Assert.Single(depthChart["WR"]);
            Assert.Equal("Tom Brady", depthChart["QB"][0].Name);
            Assert.Equal("Mike Evans", depthChart["WR"][0].Name);
        }
    }
}

