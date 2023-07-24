﻿using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.StudentVM;

namespace University.Core.Services.Interfaces
{
    public interface IStudentsService
    {
        Task<IBaseResponse<IEnumerable<Student>>> GetSortedStudentsList(string sortOrder, string searchString);
        Task<IBaseResponse<NewStudentVM>> GetStudentById(int id);
        Task<IBaseResponse<Student>> GetStudentWithIncludePropertiesById(int id);
        Task<IBaseResponse<Student>> AddNewStudent(NewStudentVM model);
        Task<IBaseResponse<Student>> UpdateStudent(NewStudentVM model);
        Task<IBaseResponse<bool>> DeleteStudent(int id);
    }
}
