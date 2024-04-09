using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class UserCoursesManager(UserCoursesRepository userCoursesRepository)
{
    private readonly UserCoursesRepository _userCoursesRepository = userCoursesRepository;

    public async Task<bool> AddUserCourse(string userId, int courseId)
    {
        try
        {
            var userCourseEntity = new UserCoursesEntity
            {
                UserId = userId,
                CourseId = courseId
            };
            userCourseEntity = await _userCoursesRepository.CreateOneAsync(userCourseEntity);
            if (userCourseEntity != null)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false!;
    }

    public async Task<bool> IsUserJoined(string userId, int courseId)
    {
        try
        {
            var userCourseEntity = await _userCoursesRepository.GetOneAsync(x => x.UserId == userId && x.CourseId == courseId);
            if (userCourseEntity != null)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false!;

    }

    public async Task<bool> RemoveUserCourse(string userId, int courseId)
    {
        try
        {
            var userCourseEntity = await _userCoursesRepository.GetOneAsync(x => x.UserId == userId && x.CourseId == courseId);
            if (userCourseEntity != null)
            {
                var result = await _userCoursesRepository.DeleteOneAsync(userCourseEntity);
                if (result == true)
                {
                    return true;
                }

            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;

    }
}
