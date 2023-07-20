﻿using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.LectureVM;

namespace University.Core.Services.Interfaces
{
    public interface ILecturesService
    {
        Task<IBaseResponse<IEnumerable<Lecture>>> GetSortedLecturesList(string sortOrder);
        Task<IBaseResponse<NewLectureVM>> GetLectureById(int id);
        Task<IBaseResponse<Lecture>> GetLectureWithIncludePropertiesById(int id);
        Task<IBaseResponse<Lecture>> AddNewLecture(NewLectureVM newLectureVM);
        Task<IBaseResponse<Lecture>> UpdateLecture(NewLectureVM newLectureVM);
        Task<IBaseResponse<bool>> DeleteLecture(int id);
        Task<IBaseResponse<IEnumerable<Lecture>>> Filter(string searchString);
    }
}
