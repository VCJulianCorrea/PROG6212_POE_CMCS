using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PROG_POE_CMCS;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace PROG_POE_CMCS.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PROG_POE_CMCSContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid())
                .Options;
            _context = new PROG_POE_CMCSContext(options);
            _envMock = new Mock<IWebHostEnvironment>();
            _envMock.Setup(e => e.WebRootPath).Returns(Path.GetTempPath());
        }

        [Test]
        public async Task Create_SetsConditionToGettingReviewed()
        {
            var controller = new ClaimsController(_context, _envMock.Object);
            var claim = new Claim { Description = "Test Claim", HoursWorked = 5, HourlyRate = 100 };
            await controller.Create(claim, new List<IFormFile>());
            var createdClaim = _context.Claim.FirstOrDefault();
            Assert.That(createdClaim, Is.Not.Null);
            Assert.That(createdClaim.Condition, Is.EqualTo("Getting Reviewed"));
        }
        [Test]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var controller = new ClaimsController(_context, _envMock.Object);
            var result = await controller.Details(null);
            Assert.That(result, Is.InstanceOf<Microsoft.AspNetCore.Mvc.NotFoundResult>());
        }
        [Test]
        public async Task Reject_ChangesConditionToRejected()
        {
            var claim = new Claim { Id = 2, Condition = "Getting Reviewed" };
            _context.Claim.Add(claim);
            await _context.SaveChangesAsync();
            var controller = new ClaimsController(_context, _envMock.Object);
            await controller.Reject(2);
            var updated = await _context.Claim.FindAsync(2);
            Assert.That(updated.Condition, Is.EqualTo("Rejected"));
        }
        [Test]
        public async Task DeleteConfirmed_RemovesClaim()
        {
            var claim = new Claim { Id = 10, Description = "To be deleted" };
            _context.Claim.Add(claim);
            await _context.SaveChangesAsync();
            var controller = new ClaimsController(_context, _envMock.Object);
            await controller.DeleteConfirmed(10);
            var deleted = await _context.Claim.FindAsync(10);
            Assert.That(deleted, Is.Null);
        }
        [Test]
        public async Task Verify_ChangesConditionToVerified()
        {
            var claim = new Claim { Id = 1, Condition = "Getting Reviewed" };
            _context.Claim.Add(claim);
            await _context.SaveChangesAsync();
            var controller = new ClaimsController(_context, _envMock.Object);
            var result = await controller.Verify(1);
            var updated = await _context.Claim.FindAsync(1);
            Assert.That(updated.Condition, Is.EqualTo("Verified"));
        }
    }
}
