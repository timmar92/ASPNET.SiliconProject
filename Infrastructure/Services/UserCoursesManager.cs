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

    public async Task<IEnumerable<UserCoursesEntity>> GetUserCourses(string userId)
    {
        try
        {
            var userCourses = await _userCoursesRepository.GetAllAsync(x => x.UserId == userId);
            if (userCourses != null)
            {
                return userCourses;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
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

    public async Task<bool> RemoveAllUserCourses(string userId)
    {
        try
        {
            var userCourses = await _userCoursesRepository.GetAllAsync(x => x.UserId == userId);
            if (userCourses != null)
            {
                var result = await _userCoursesRepository.DeleteAllAsync(x => x.UserId == userId);
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
