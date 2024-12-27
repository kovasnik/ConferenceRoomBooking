using AutoMapper;
using ConferenceRoomBooking.BLL.Interfaces;
using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ConferenceRoomBooking.BLL.Services
{
    public class ConferenceRoomService : IConferenceRoomService
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;
        private readonly IRoomServiceRepository _roomServiceRepository;
        private readonly IMapper _mapper;
        public ConferenceRoomService(IConferenceRoomRepository roomRepository, IRoomServiceRepository roomServiceRepository, IMapper mapper)
        {
            _conferenceRoomRepository = roomRepository;
            _roomServiceRepository = roomServiceRepository;
            _mapper = mapper;
        }

        public async Task<int> AddConfereceRoom(CreateRoomDto roomWithServices)
        {

            var serviceIds = roomWithServices.ServiceIds;

            // Pass the checked values ​​to the model
            var room = _mapper.Map<ConferenceRoom>(roomWithServices);
            await _conferenceRoomRepository.AddAsync(room);

            // If not empty add to the RoomServices all connections
            room.RoomServices ??= new List<RoomService>();
            foreach (var serviceId in serviceIds)
            {
                await _roomServiceRepository.AddAsync(new RoomService { ServiceId = serviceId, RoomId = room.Id });
            }

            return room.Id;
        }

        public async Task<bool> DeleteConferenceRoom(int roomId)
        {
            // Search for a conference room by id
            var room = await _conferenceRoomRepository.GetRoomByIdAsync(roomId);
            if (room == null)
                return false;
            
            await _conferenceRoomRepository.DeleteAsync(room);
            return true;
        }

        public async Task<bool> UpdateConfirenceRoom(UpdateRoomDto dtoModel)
        {
            // Search for a conference room by id
            var existingRoom = await _conferenceRoomRepository.GetRoomByIdAsync(dtoModel.Id);

            if (existingRoom == null)
                return false;

            // Pass the checked values ​​to the model
            existingRoom = _mapper.Map<ConferenceRoom>(dtoModel);

            await _conferenceRoomRepository.UpdateAsync(existingRoom);
            return true;
        }

        public async Task<IEnumerable<AvailableRoomsDto>> GetAvailableConfirenceRoom(DateTime startTime, DateTime endTime, int capasity)
        {
            // booking checks
            if (startTime.Date != endTime.Date)
            {
               throw new InvalidOperationException("Booking must be made on the same day");
            }
            TimeSpan startLimit = new TimeSpan(6, 0, 0);  // 06:00
            TimeSpan endLimit = new TimeSpan(23, 0, 0);   // 23:00

            if (startTime.TimeOfDay < startLimit || endTime.TimeOfDay > endLimit)
            {
                throw new InvalidOperationException("Booking time must be between 6:00 AM and 11:00 PM");
            }

            // create an IEnumerable object and put all the suitable conference room into it
            IEnumerable<ConferenceRoom> availableRooms = await _conferenceRoomRepository.GetAvailableRoomAsync(startTime, endTime, capasity);

            // transfer data to the viewmodel to correctly issue service IDs (if we don't transfer it, it will be loop)
            var viewModels = _mapper.Map<IEnumerable<AvailableRoomsDto>>(availableRooms);

            if (viewModels is null)
            {
                throw new InvalidOperationException("No available rooms with that time and capasity");
            }

            return viewModels;
        }
    }
}
