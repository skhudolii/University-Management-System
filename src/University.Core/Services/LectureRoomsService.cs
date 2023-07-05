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

        public async Task<IBaseResponse<LectureRoom>> AddNewLectureRoom(NewLectureRoomVM model)
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
                    Description = $"[AddNewLectureRoom] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewLectureRoomVM>> GetLectureRoomById(int id)
        {
            try
            {
                var lectureRoomDetails = await _lectureRoomsRepository.GetByIdAsync(id);
                if (lectureRoomDetails == null)
                {
                    return new BaseResponse<NewLectureRoomVM>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                var data = new NewLectureRoomVM()
                {
                    Id = lectureRoomDetails.Id,
                    Name = lectureRoomDetails.Name,
                    Capacity = lectureRoomDetails.Capacity,
                    FacultyId = (int)lectureRoomDetails.FacultyId
                };

                return new BaseResponse<NewLectureRoomVM>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewLectureRoomVM>()
                {
                    Description = $"[GetSubjectById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<LectureRoom>> GetLectureRoomWithIncludePropertiesById(int id)
        {
            try
            {
                var lectureRoomDetails = await _lectureRoomsRepository.GetByIdAsync(id, f => f.Faculty);
                if (lectureRoomDetails == null || lectureRoomDetails.Faculty == null)
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
                    Description = $"[GetLectureRoomWithIncludePropertiesById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<LectureRoom>>> GetLectureRoomsList()
        {
            try
            {
                var lectureRooms = await _lectureRoomsRepository.GetAllAsync(n => n.Faculty);
                var filteredLectureRooms = lectureRooms.Where(f => f.FacultyId != null);

                if (!filteredLectureRooms.Any())
                {
                    return new BaseResponse<IEnumerable<LectureRoom>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<IEnumerable<LectureRoom>>()
                {
                    Data = filteredLectureRooms,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<LectureRoom>>()
                {
                    Description = $"[GetLectureRoomsList] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewLectureRoomDropdownsVM>> GetNewLectureRoomDropdownsValues()
        {
            try
            {
                var lectureRoomDropdownsValues = new NewLectureRoomDropdownsVM()
                {
                    Faculties = (await _facultiesRepository.GetAllAsync()).OrderBy(n => n.Name).ToList()
                };

                return new BaseResponse<NewLectureRoomDropdownsVM>()
                {
                    Data = lectureRoomDropdownsValues,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewLectureRoomDropdownsVM>()
                {
                    Description = $"[GetNewLectureRoomDropdownsValues] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<LectureRoom>> UpdateLectureRoom(NewLectureRoomVM model)
        {
            try
            {
                var dbLectureRoom = await _lectureRoomsRepository.GetByIdAsync(model.Id);
                if (dbLectureRoom == null)
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
                    Description = $"[UpdateLectureRoom] : {ex.Message}",
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
                    Description = $"[DeleteLectureRoom] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
