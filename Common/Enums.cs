namespace Advocate.Common
{
    public enum Roles
    {
        SuperAdmin,
        Admin,
        User,
        Operator
    }

    public enum AppRights
    {
        Read,
        Write,
        View
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum NotificationType
    {
        error,
        success,
        warning,
        info
    }    
    public enum ManageType
    {
        New,
        Amended,
        Repealed
    }

}