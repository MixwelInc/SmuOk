using System;
using System.DirectoryServices.AccountManagement;


namespace SmuOk.Common
{
  public static class Win32
  {
    public static bool MyUserExistsLocal(string UserId)
    {
      return MyUserExists(UserId, ContextType.Machine);
    }

    public static bool MyUserExistsAd(string UserId)
    {
      return MyUserExists(UserId, ContextType.Domain);
    }
    
    private static bool MyUserExists(string UserId, ContextType ct)
    {
      bool userExists = false;
      try
      {
        using (var ctx = new PrincipalContext(ct))
        {
          using (var user = UserPrincipal.FindByIdentity(ctx, UserId))
          {
            if (user != null)
            {
              userExists = true;
              user.Dispose();
            }
          }
        }
      }
      catch (Exception ex) { }
      return userExists;
    }
  }
}
