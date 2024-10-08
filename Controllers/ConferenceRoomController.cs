﻿using AutoMapper;
using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.DTO.Repositories;
using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferenceRoomController : Controller
    {
        // Connecting repositories using dependency injection
        private readonly IConferenceRoomRepository _conferenceRoomRepository;
        private readonly IRoomServiceRepository _roomServiceRepository;
        private readonly IMapper _mapper;
        public ConferenceRoomController(IConferenceRoomRepository roomRepository, IRoomServiceRepository roomServiceRepository, IMapper mapper) 
        { 
            _conferenceRoomRepository = roomRepository;
            _roomServiceRepository = roomServiceRepository;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddConfereceRoom([FromBody] CreateRoomDto roomWithServices)
        {

            var serviceIds = roomWithServices.ServiceIds;
            // Model checks
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Pass the checked values ​​to the model
            var room = _mapper.Map<ConferenceRoom>(roomWithServices);
            //var room = new ConferenceRoom
            //{
            //    Name = roomWithServices.Name,
            //    Description = roomWithServices.Description,
            //    Capacity = roomWithServices.Capacity,
            //    CostPerHour = roomWithServices.CostPerHour
            //};


            await _conferenceRoomRepository.AddAsync(room);

            // If not empty add to the RoomServices all connections
            if (room.RoomServices == null)
            {
                room.RoomServices = new List<RoomService>();
            }
            foreach (var serviceId in serviceIds)
            {
                await _roomServiceRepository.AddAsync(new RoomService { ServiceId = serviceId, RoomId = room.Id });
            }

            return Ok(room.Id);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteConferenceRoom(int roomId)
        {
            // Search for a conference room by id
            var room = await _conferenceRoomRepository.GetRoomByIdAsync(roomId);
            if (room != null)
            {
                await _conferenceRoomRepository.DeleteAsync(room);
                return NoContent(); // 204
            }

                return BadRequest("Id does not exist");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateConfirenceRoom([FromBody] UpdateRoomDto dtoModel)
        {
            // Model checks
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Search for a conference room by id
            var existingRoom = await _conferenceRoomRepository.GetRoomByIdAsync(dtoModel.Id);

            if (existingRoom == null)
            {
                return NotFound("Conference room not found"); //404
            }
            // Pass the checked values ​​to the model
            existingRoom = _mapper.Map<ConferenceRoom>(dtoModel);
            //existingRoom.Name = dtoModel.Name;
            //existingRoom.CostPerHour = dtoModel.CostPerHour;
            //existingRoom.Capacity = dtoModel.Capacity;
            //existingRoom.Description = dtoModel.Description;

            await _conferenceRoomRepository.UpdateAsync(existingRoom);
            return Ok();
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableConfirenceRoom(DateTime startTime, DateTime endTime, int capasity)
        {
            // booking checks
            if (startTime.Date != endTime.Date)
            {
                return BadRequest("Booking must be made on the same day");
            }
            TimeSpan startLimit = new TimeSpan(6, 0, 0);  // 06:00
            TimeSpan endLimit = new TimeSpan(23, 0, 0);   // 23:00

            if (startTime.TimeOfDay < startLimit || endTime.TimeOfDay > endLimit)
            {
                return BadRequest("Booking time must be between 6:00 AM and 11:00 PM");
            }

            // create an IEnumerable object and put all the suitable conference room into it
            IEnumerable<ConferenceRoom> availableRooms = await _conferenceRoomRepository.GetAvailableRoomAsync(startTime, endTime, capasity);

            // transfer data to the viewmodel to correctly issue service IDs (if we don't transfer it, it will be loop)
            var viewModels = _mapper.Map < IEnumerable<AvailableRoomsDto>>(availableRooms);
            //var viewModels = availableRooms.Select(r => new AvailableRoomsDto
            //{
            //    Id = r.Id,
            //    Name = r.Name,
            //    Capacity = r.Capacity,
            //    ServiceIds = r.RoomServices.Select(rs => rs.ServiceId).ToList()
            //});

            if (viewModels is null)
            {
                return BadRequest("No available rooms with that time and capasity");
            }

            return Ok(viewModels);
        }
    }
}
