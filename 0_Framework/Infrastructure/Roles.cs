namespace _0_Framework.Infrastructure;
public static class Roles
{
    public const string Administrator = "9";
    public const string SystemUser = "10";
    public const string ContentUploader = "11";
    public const string ColleagueUser = "12";

    public static string GetRoleBy(long id) => id switch
    {
        9 => "مدیر سیستم",
        11 => "محتوا گذار",
        12 => "کاربر همکار",
        _ => "کاربر عادی"
    };
}
