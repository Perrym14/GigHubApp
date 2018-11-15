using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTest
    {
        private AttendancesController _controller;
        private Mock<IUnitOfWork> _mockUoW;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;

        public AttendancesControllerTest()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            _mockUoW = new Mock<IUnitOfWork>();
            _mockUoW.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);
            _controller = new AttendancesController(_mockUoW.Object);

            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_UserAttemptsToAttendAGigTheyAlreadyAttend_ShouldReturnBadRequest()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto { GigId = 1 });
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void CancelAttendance_AttemptingToCancelNonExistingGig_ShouldReturnNotFound()
        {
            var result = _controller.CancelAttendance(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void ValidRequest_UserAddsAttendance_ShouldReturnOk()
        {
            var result = _controller.Attend(new AttendanceDto { GigId = 1 });
            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void CancelValidRequest_UserCancelsAttendance_ShouldReturnOk()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(attendance);

            var result = (OkNegotiatedContentResult<int>)_controller.CancelAttendance(1);
            result.Content.Should().Be(1);
        }


    }
}
