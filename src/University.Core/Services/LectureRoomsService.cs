﻿using System.Text.RegularExpressions;
using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.LectureRoomVM;

namespace University.Core.Services
{
    public class LectureRoomsService : ILectureRoomsService
    {
        private readonly ILectureRoomsRepository _lectureRoomsRepository;
        private readonly IFacultiesRepository _facultiesRepository;

        public LectureRoomsService(ILectureRoomsRepository lectureRoomsRepository, IFacultiesRepository facultiesRepository)
        {
            _lectureRoomsRepository = lectureRoomsRepository;
            _facultiesRepository = facultiesRepository;
        }

        public async Task<IBaseResponse<LectureRoom>> AddNewLectureRoom(NewLectureRoomModel model)
        {
            try
            {
                var newLectureRoom = new LectureRoom()
                {
                    Name = model.Name,
                    Capacity = model.Capacity,
                    FacultyId = model.FacultyId
                };
                await _lectureRoomsRepository.AddAsync(newLectureRoom);

                return new BaseResponse<LectureRoom>()
                {
                    Data = newLectureRoom,
                    Description = "New Lecture Room successfully added",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<LectureRoom>()
                {
                    Description = $"[LectureRoomsService.AddNewLectureRoom] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewLectureRoomModel>> GetLectureRoomById(int id)
        {
            try
            {
                var lectureRoomDetails = await _lectureRoomsRepository.GetByIdAsync(id);
                if (lectureRoomDetails == null || lectureRoomDetails.FacultyId == null)
                {
                    return new BaseResponse<NewLectureRoomModel>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                var data = new NewLectureRoomModel()
                {
                    Id = lectureRoomDetails.Id,
                    Name = lectureRoomDetails.Name,
                    Capacity = lectureRoomDetails.Capacity,
                    FacultyId = (int)lectureRoomDetails.FacultyId
                };

                return new BaseResponse<NewLectureRoomModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewLectureRoomModel>()
                {
                    Description = $"[LectureRoomsService.GetLectureRoomById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<LectureRoom>> GetLectureRoomWithIncludePropertiesById(int id)
        {
            try
            {
                var lectureRoomDetails = await _lectureRoomsRepository.GetByIdAsync(id, f => f.Faculty);
                if (lectureRoomDetails == null || lectureRoomDetails.FacultyId == null)
                {
                    return new BaseResponse<LectureRoom>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                return new BaseResponse<LectureRoom>()
                {
                    Data = lectureRoomDetails,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<LectureRoom>()
                {
                    Description = $"[LectureRoomsService.GetLectureRoomWithIncludePropertiesById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<LectureRoom>>> GetSortedLectureRoomsList(string sortOrder, string searchString)
        {
            try
            {
                var lectureRooms = (await _lectureRoomsRepository.GetAllAsync(n => n.Faculty)).Where(f => f.FacultyId != null);
                if (!lectureRooms.Any())
                {
                    return new BaseResponse<IEnumerable<LectureRoom>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    lectureRooms = lectureRooms.Where(lr => lr.Name.ToLower().Contains(searchString.ToLower()));
                }

                lectureRooms = sortOrder switch
                {
                    "name_desc" => lectureRooms.OrderByDescending(lr => lr.Name),
                    "Capacity" => lectureRooms.OrderBy(lr => lr.Capacity),
                    "capacity_desc" => lectureRooms.OrderByDescending(lr => lr.Capacity),
                    "Faculty" => lectureRooms.OrderBy(lr => lr.Faculty.Name),
                    "faculty_desc" => lectureRooms.OrderByDescending(lr => lr.Faculty.Name),
                    _ => lectureRooms.OrderBy(lr => lr.Name),
                };
                return new BaseResponse<IEnumerable<LectureRoom>>()
                {
                    Data = lectureRooms.ToList(),
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<LectureRoom>>()
                {
                    Description = $"[LectureRoomsService.GetLectureRoomsList] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewLectureRoomDropdownsModel>> GetNewLectureRoomDropdownsValues()
        {
            try
            {
                var lectureRoomDropdownsValues = new NewLectureRoomDropdownsModel()
                {
                    Faculties = (await _facultiesRepository.GetAllAsync()).OrderBy(n => n.Name).ToList()
                };

                return new BaseResponse<NewLectureRoomDropdownsModel>()
                {
                    Data = lectureRoomDropdownsValues,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewLectureRoomDropdownsModel>()
                {
                    Description = $"[LectureRoomsService.GetNewLectureRoomDropdownsValues] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<LectureRoom>> UpdateLectureRoom(NewLectureRoomModel model)
        {
            try
            {
                var dbLectureRoom = await _lectureRoomsRepository.GetByIdAsync(model.Id);
                if (dbLectureRoom == null || dbLectureRoom.FacultyId == null)
                {
                    return new BaseResponse<LectureRoom>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                dbLectureRoom.Id = model.Id;
                dbLectureRoom.Name = model.Name;
                dbLectureRoom.Capacity = model.Capacity;
                dbLectureRoom.FacultyId = model.FacultyId;

                await _lectureRoomsRepository.UpdateAsync(dbLectureRoom.Id, dbLectureRoom);

                return new BaseResponse<LectureRoom>()
                {
                    Description = "Lecture room successfully updated",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<LectureRoom>()
                {
                    Description = $"[LectureRoomsService.UpdateLectureRoom] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteLectureRoom(int id)
        {
            try
            {
                var lectureRoom = await _lectureRoomsRepository.GetByIdAsync(id);
                if (lectureRoom == null) 
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                await _lectureRoomsRepository.DeleteAsync(id);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Lecture room successfully deleted",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[LectureRoomsService.DeleteLectureRoom] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
