using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Interfaces;
using Dashboard.WebApi.Controllers;
using NSubstitute;
using NUnit;
using NUnit.Framework;

namespace Dashboard.UnitTests.Controllers
{
    [TestFixture]
    public class DashboardDataControllerTests
    {
        private DashboardDataController _controller;

        [SetUp]
        public void Init()
        {
            var projectServiceMoq = Substitute.For<IProjectService>();
            var ciDataProviderFactoryMoq = Substitute.For<ICiDataProviderFactory>();

            var fakeProvider = Substitute.For<ICiDataProvider>();
            fakeProvider.Name.Returns("TestGitLabProvider");

            ciDataProviderFactoryMoq.GetSupportedProviders.Returns(new List<ICiDataProvider> { fakeProvider });

            _controller = new DashboardDataController(projectServiceMoq, ciDataProviderFactoryMoq);
        }

        [Test]
        public void SupportedProviders_ReturnsAllDataProviders()
        {
            //Act
            var result = _controller.SupportedProviders();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo("TestGitLabProvider"));
        }
    }
}
