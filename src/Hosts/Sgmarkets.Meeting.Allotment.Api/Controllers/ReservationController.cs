using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sgmarkets.Meeting.Allotment.Api.Mappings;
using Sgmarkets.Meeting.Allotment.Api.Models;
using Sgmarkets.Meeting.Allotment.Domain.Entities;
using Sgmarkets.Meeting.Allotment.Domain.Exceptions;
using Sgmarkets.Meeting.Allotment.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Sgmarkets.Meeting.Allotment.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        #region :  Variables & Fields
        private readonly IRepositoryRooms _repositoryRooms;
        private readonly IRepositoryReservations _repositoryReservations;
        #endregion

        #region : Ctor
        public ReservationController(IRepositoryReservations repositoryReservations, IRepositoryRooms repositoryRooms)
        {
            _repositoryRooms = repositoryRooms ?? throw new ArgumentNullException(nameof(repositoryRooms));
            _repositoryReservations = repositoryReservations ?? throw new ArgumentNullException(nameof(repositoryReservations));
        }
        #endregion

        /// <summary>
        /// Return all reservations for the specific day
        /// </summary>
        /// <param name="day">DateTime</param>
        /// <param name="room">string</param>
        /// <response code="200">When everything is OK</response>
        [HttpGet]
        [Route("List")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ReservationModel>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<ReservationModel>> GetReservations(string room, DateTime day)
        {
            var reservations = Task.Run(() => _repositoryReservations.GetReservations(room, day).Select(s => s.MapToModel()));
            return await reservations;
        }

        /// <summary>
        /// Return all available slots for the specific day
        /// </summary>
        /// <param name="day">DateTime</param>
        /// <param name="room">string</param>
        /// <response code="200">When everything is OK</response>
        [HttpGet]
        [Route("FreeSlots")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Slot>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<Slot>> GetSlots(string room, DateTime day)
        {
            var slots = Task.Run(() =>
            {
                var arr = _repositoryReservations.GetSlots(room, day).OrderBy(s => s.Start);
                return arr.Skip(1).Concat(arr.Take(1));
            });
            return await slots;
        }

        /// <summary>
        /// Delete the specific reseration
        /// </summary>
        /// <param name="model">ReservationModel</param>
        /// <response code="200">When everything is OK</response>
        /// <response code="400">
        /// - When the model is not valid (empty, null, required property not specified, ... )<br/>
        /// - When the reservation not found with criteria of model
        /// </response>
        /// <response code="500">When the internal exception occurred</response>
        [HttpPost]
        [Route("Delete")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteReservation(ReservationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var beginTime = TimeSpan.Parse(model.BeginTime);
                var endTime = TimeSpan.Parse(model.EndTime);
                var reservation = _repositoryReservations.Find(model.Day, beginTime, endTime, model.RoomName);

                if (reservation == null)
                {
                    ModelState.AddModelError("ReservationNotFound", "No reservations found to delete with these criteria");
                    return BadRequest();
                }

                await Task.Run(() => _repositoryReservations.Delete(reservation));
            }
            catch
            {
                return Ok(new StatusCodeResult(StatusCodes.Status500InternalServerError));
            }
            return Ok(true);
        }

        /// <summary>
        /// Create a new reservation
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">When everything is OK</response>
        /// <response code="400">
        /// - When the model is not valid (empty, null, required property not specified, ... )<br/>
        /// - When the reservation did not specify a room<br/>
        /// - When a business rule not respected
        /// </response>
        /// <response code="500">When the internal exception occurred</response>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateReservation(ReservationModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var room = _repositoryRooms.Items().FirstOrDefault(r => r.Name == model.RoomName);

            if (room == null)
            {
                ModelState.AddModelError("RoomNotFound", $"No rooms found with this name : {model.RoomName}");
                return BadRequest();
            }

            try
            {
                var reservation = model.MapToEntity();
                reservation.Room = room;
                await Task.Run(() => _repositoryReservations.Add(reservation));
            }
            catch (DomainException e)
            {
                ModelState.AddModelError("DomainError", e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                return Ok(new StatusCodeResult(StatusCodes.Status500InternalServerError));
            }

            return Ok(true);
        }

    }
}
