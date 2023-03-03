using WePi.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace WePi.Permissions;

public class WePiPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WePiPermissions.GroupName, L("Permission:WePi"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WePiResource>(name);
    }
}
