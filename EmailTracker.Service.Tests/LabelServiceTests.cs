using EmailTracker.Core.Models;
using EmailTracker.Repository.IRepositories;
using EmailTracker.Service.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTracker.Service.Tests
{
    public class LabelServiceTests
    {
        private ILabelRepository _labelRepository;

        [SetUp]
        public void SetUp()
        {
            _labelRepository = Substitute.For<ILabelRepository>();
        }

        [Test]
        public async Task GetAllLabels_ShouldReturnAllLabels()
        {
            //Arrange
            var service = GetLabelService();

            //Act
            List<Label> labels = new List<Label>
            {
                new Label
                {
                    LabelName = "Important",
                    CreatedOnDate = new DateTime()
                },
            };

            _labelRepository.GetAll().Returns(labels);
            var actualResult = await service.GetAllLabels();

            //Assert
            Assert.AreEqual(labels, actualResult);
        }


        [Test]
        public async Task GetLabelById_ShouldReturnLabel()
        {
            //Arrange
            var service = new LabelService(_labelRepository);

            //Act
            var labelId = 1;
            var label = new Label
            {
                LabelName = "Important",
                CreatedOnDate = new DateTime()
            };

            _labelRepository.GetById(labelId).Returns(label);
            var actualResult = await service.GetLabelById(labelId);

            //Assert
            Assert.AreEqual(label, actualResult);
        }

        private LabelService GetLabelService()
        {
            return new LabelService(_labelRepository);
        }
    }
}
