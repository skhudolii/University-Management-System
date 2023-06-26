using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;

namespace University.Core.Services
{
    public class LectureRoomsService : ILectureRoomsService
    {
        private readonly ILectureRoomsRepository _lectureRoomsRepository;

        public LectureRoomsService(ILectureRoomsRepository lectureRoomsRepository)
        {
            _lectureRoomsRepository = lectureRoomsRepository;
        }

        public async Task<IBaseResponse<LectureRoom>> GetLectureRoomById(int id)
        {
            var baseResponse = new BaseResponse<LectureRoom>();
            try
            {
                var lectureRoomDetails = await _lectureRoomsRepository.GetLectureRoomByIdAsync(id);
                if (lectureRoomDetails == null)
                {
                    baseResponse.Description = "Not found";
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }

                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = lectureRoomDetails;
                return baseResponse;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetSubjectById] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
                return baseResponse;
            }
        }

        public async Task<IBaseResponse<IEnumerable<LectureRoom>>> GetLectureRoomsList()
        {
            var baseResponse = new BaseResponse<IEnumerable<LectureRoom>>();
            try
            {
                var lectureRooms = await _lectureRoomsRepository.GetAllAsync(n => n.Faculty);
                if (!lectureRooms.Any())
                {
                    baseResponse.Description = "0 items found";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = lectureRooms;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<LectureRoom>>()
                {
                    Description = $"[GetLectureRoomsList] : {ex.Message}"
                };
            }
        }
    }
}
