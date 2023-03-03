using Volo.Abp.Reflection;

namespace WePi.Permissions;

public class WePiPermissions
{
    public const string GroupName = "WePi";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(WePiPermissions));
    }
}
