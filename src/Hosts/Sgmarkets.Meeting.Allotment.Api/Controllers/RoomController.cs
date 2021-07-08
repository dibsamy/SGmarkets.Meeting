using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sgmarkets.Meeting.Allotment.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sgmarkets.Meeting.Allotment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        #region : Variables & Fields
        private readonly IRepositoryRooms _repositoryRooms;
        #endregion

        #region : Ctor
        public RoomController(IRepositoryRooms repositoryRooms)
        {
            _repositoryRooms = repositoryRooms ?? throw new ArgumentNullException(nameof(repositoryRooms));
        }
        #endregion

        /// <summary>
        /// Gets all meeting rooms name.
        /// </summary>
        /// <response code="200">When everything is OK</response>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        public IEnumerable<string> Get()
         => _repositoryRooms.Items().Select(r => r.Name);

    }
}
