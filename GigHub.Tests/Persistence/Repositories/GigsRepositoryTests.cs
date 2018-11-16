using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class GigsRepositoryTests
    {
        private GigsRepository _repository;
        private Mock<DbSet<Gig>> _MockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            _MockGigs = new Mock<DbSet<Gig>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_MockGigs.Object);

            _repository = new GigsRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUserUpcomingGigs_GigInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };

            _MockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUserUpcomingGigs("1");
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUserUpcomingGigs_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };
            gig.Cancel();

            _MockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUserUpcomingGigs("1");
            gigs.Should().BeEmpty();
        }
    }
}
