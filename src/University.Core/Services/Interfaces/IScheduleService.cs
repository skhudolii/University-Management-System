﻿using University.Core.Entities;
using University.Core.Response.Interfeces;

namespace University.Core.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<IBaseResponse<IEnumerable<Lecture>>> GetScheduleForFaculty(int id, string sortOrder, string searchString);
        Task<IBaseResponse<IEnumerable<Lecture>>> GetScheduleForTeacher(int id, DateTime lastDateOfPeriod, string sortOrder, string searchString);
        Task<IBaseResponse<IEnumerable<Lecture>>> GetScheduleForStudent(int id, DateTime lastDateOfPeriod, string sortOrder, string searchString);
    }
}
