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
    [TestFixture]
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
            var sut = CreateSUT();
            List<Label> labels = new List<Label>
            {
                new Label
                {
                    LabelName = "Important",
                    CreatedOnDate = new DateTime()
                },
            };
            _labelRepository.GetAll().Returns(labels);

            //Act
            var actual = await sut.GetAllLabels();

            //Assert
            Assert.AreEqual(labels, actual);
        }


        [Test]
        public async Task GetLabelById_ShouldReturnLabel()
        {
            //Arrange
            var sut = CreateSUT();
            var labelId = 1;
            var label = new Label
            {
                LabelName = "Important",
                CreatedOnDate = new DateTime()
            };
            _labelRepository.GetById(labelId).Returns(label);

            //Act
            var actual = await sut.GetLabelById(labelId);

            //Assert
            Assert.AreEqual(label, actual);
        }

        private LabelService CreateSUT()
        {
            return new LabelService(_labelRepository);
        }
    }
}
