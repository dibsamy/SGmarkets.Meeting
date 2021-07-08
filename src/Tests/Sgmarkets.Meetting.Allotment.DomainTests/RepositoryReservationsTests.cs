using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sgmarkets.Meeting.Allotment.Domain.Entities;
using Sgmarkets.Meeting.Allotment.Domain.Exceptions;
using Sgmarkets.Meeting.Allotment.Domain.Repositories;
using System;
using System.Linq;

namespace Sgmarkets.Meetting.Allotment.DomainTests
{
    [TestClass]
    public class RepositoryReservationsTests
    {
        RepositoryReservations _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new RepositoryReservations();
        }

        [TestMethod]
        public void Add_Must_Be_Throw_ArgumentNullException_When_Reservation_Is_Null()
        {
            //Arrange
            var expectedMessage = "The reservation cannot be null. (Parameter 'reservation')";
            var reservation = (Reservation)null;

            // Act
            void action() => _sut.Add(reservation);

            //Assert
            var caughtException = Assert.ThrowsException<ArgumentNullException>(action);
            Assert.AreEqual(expectedMessage, caughtException.Message);
        }

        [TestMethod]
        public void Add_Must_Be_Throw_ArgumentNullException_When_Room_Is_Null()
        {
            //Arrange
            var expectedMessage = "The room of Reservation cannot be null. (Parameter 'Room')";
            var reservation = new Reservation();

            // Act
            void action() => _sut.Add(reservation);

            //Assert
            var caughtException = Assert.ThrowsException<ArgumentNullException>(action);
            Assert.AreEqual(expectedMessage, caughtException.Message);
        }

        [TestMethod]
        public void Add_Must_Be_Throw_InPasteMeetingException()
        {
            //Arrange
            var expectedMessage = "Unable to create a meeting in the past.";
            var reservation = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = DateTime.Now.AddDays(-1)
            };

            // Act
            void action() => _sut.Add(reservation);

            //Assert
            var caughtException = Assert.ThrowsException<InPasteMeetingException>(action);
            Assert.AreEqual(expectedMessage, caughtException.Message);
        }

        [TestMethod]
        public void Add_Must_Be_Throw_EndTimeLessOrEqualStartTimeException()
        {
            //Arrange
            var expectedMessage = "The beginning time must be greater than the end time.";
            var reservation = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = DateTime.Now,
                BeginTime = new TimeSpan(12, 0, 0),
                EndTime = new TimeSpan(10, 0, 0)
            };

            // Act
            void action() => _sut.Add(reservation);

            //Assert
            var caughtException = Assert.ThrowsException<EndTimeLessOrEqualStartTimeException>(action);
            Assert.AreEqual(expectedMessage, caughtException.Message);
        }

        [TestMethod]
        public void Add_Must_Be_Create_When_Meeting_Is_First_In_Day()
        {
            //Arrange
            var now = DateTime.Now;
            var reservation = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = now,
                BeginTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(11, 0, 0)
            };

            // Act
            _sut.Add(reservation);
            var reservations = _sut.GetReservations(now);

            //Assert
            Assert.IsTrue(reservations.Count() == 1);
        }

        [TestMethod]
        public void Add_Must_Be_Throw_Overlap_On_BeginTime()
        {
            //Arrange
            var expectedMessage = "The reservation is overlap with another reservation.";
            var now = DateTime.Now;
            var reservation = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = now,
                BeginTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(10, 0, 0)
            };
            _sut.Add(reservation);

            var overlap = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = now,
                BeginTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(11, 0, 0)
            };
            // Act
            void action() => _sut.Add(overlap);

            //Assert
            var caughtException = Assert.ThrowsException<OverlapMeetingException>(action);
            Assert.AreEqual(expectedMessage, caughtException.Message);
        }

        [TestMethod]
        public void Add_Must_Be_Throw_Overlap_On_EndTime()
        {
            //Arrange
            var expectedMessage = "The reservation is overlap with another reservation.";
            var now = DateTime.Now;
            var reservation = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = now,
                BeginTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(10, 0, 0)
            };
            _sut.Add(reservation);

            var overlap = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = now,
                BeginTime = new TimeSpan(7, 0, 0),
                EndTime = new TimeSpan(9, 0, 0)
            };
            // Act
            void action() => _sut.Add(overlap);

            //Assert
            var caughtException = Assert.ThrowsException<OverlapMeetingException>(action);
            Assert.AreEqual(expectedMessage, caughtException.Message);
        }

        [TestMethod]
        public void Add_Must_Be_Throw_Overlap_When_Wrapping()
        {
            //Arrange
            var expectedMessage = "The reservation is overlap with another reservation.";
            var now = DateTime.Now;
            var reservation = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = now,
                BeginTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(10, 0, 0)
            };
            _sut.Add(reservation);

            var overlap = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = now,
                BeginTime = new TimeSpan(7, 0, 0),
                EndTime = new TimeSpan(11, 0, 0)
            };

            // Act
            void action() => _sut.Add(overlap);

            //Assert
            var caughtException = Assert.ThrowsException<OverlapMeetingException>(action);
            Assert.AreEqual(expectedMessage, caughtException.Message);
        }

        [TestMethod]
        public void Add_Must_Be_Added_Reservations()
        {
            //Arrange
            var now = DateTime.Now;
            var r1 = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = now,
                BeginTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(10, 0, 0)
            };
            var r2 = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = now,
                BeginTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(11, 0, 0)
            };

            _sut.Add(r1);
            _sut.Add(r2);

            // Act
            var act = _sut.GetReservations(now);

            //Assert
            Assert.IsTrue(2 == act.Count());
        }



        [TestMethod]
        public void Add_Must_Be_Build_Slots()
        {
            //Arrange
            var expectedSlots = 24;
            var expectedFreeSlots = 21;
            var expectedOccupedSlots = 3;
            var now = DateTime.Now;
            var r1 = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = now,
                BeginTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(10, 0, 0)
            };
            var r2 = new Reservation
            {
                Room = new Room { Name = "Room0" },
                Day = now,
                BeginTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(11, 0, 0)
            };

            _sut.Add(r1);
            _sut.Add(r2);

            // Act
            var act = _sut.GetSlots(now);

            //Assert
            Assert.IsTrue(expectedSlots == act.Count());
            Assert.IsTrue(expectedFreeSlots == act.Where(s => s.Free).Count());
            Assert.IsTrue(expectedOccupedSlots == act.Where(s => !s.Free).Count());
        }


        [TestMethod]
        public void Add_Must_Be_Return_All_Slots()
        {
            //Arrange
            var expectedSlots = 24;
            var now = DateTime.Now;

            // Act
            var act = _sut.GetSlots(now);

            //Assert
            Assert.IsTrue(expectedSlots == act.Count());
        }
    }
}
