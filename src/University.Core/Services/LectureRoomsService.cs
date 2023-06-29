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

        public LectureRoomsService(ILectureRoomsRepository lectureRoomsRepository)
        {
            _lectureRoomsRepository = lectureRoomsRepository;
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

        public async Task<IBaseResponse<LectureRoom>> GetLectureRoomById(int id)
        {
            try
            {
                var lectureRoomDetails = await _lectureRoomsRepository.GetLectureRoomByIdAsync(id);
                if (lectureRoomDetails == null)
                {
                    return new BaseResponse<LectureRoom>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                return new BaseResponse<LectureRoom>()
                {
                    StatusCode = StatusCode.OK,
                    Data = lectureRoomDetails
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<LectureRoom>()
                {
                    Description = $"[GetSubjectById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<LectureRoom>>> GetLectureRoomsList()
        {
            try
            {
                var subjects = await _lectureRoomsRepository.GetAllAsync(n => n.Faculty);
                if (!subjects.Any())
                {
                    return new BaseResponse<IEnumerable<LectureRoom>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<IEnumerable<LectureRoom>>()
                {
                    Data = subjects,
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
                var dropdownsValues = await _lectureRoomsRepository.GetNewLectureRoomDropdownsValuesAsync();

                return new BaseResponse<NewLectureRoomDropdownsVM>()
                {
                    Data = dropdownsValues,
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
    }
}
