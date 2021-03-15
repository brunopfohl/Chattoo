using System;

namespace Chattoo.Domain.Enums
{
    [Flags]
    public enum UserGroupPermission
    {
        Admin = 0,

        Post = 2,

        Invite = 4,

        ManageUsers = 8
    }
}
