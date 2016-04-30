public partial class Startup
{
  public void ConfigureAuth(IAppBuilder app)
  {
    // Configure the Db Context, user manager and role
    // manager to use a single instance per request
    
    app.CreatePerOwinContext(ApplicationDbContext.Create);
    app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
    app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
    
    // Enable the Application to use a cookie to store information for the 
    // signed in user and to use a cookie to temporarily store information
    // about a user loggin in with a third party login provider 
    // Configure the sign in cookie
    
    app.UseCookieAuthentication(new CookieAuthenticationOptions{
      AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
      LoginPath = new PathString("/Account/Login");
      Provider = new CookieAuthenticationProvider{
      
         // Enables the application to validate the security stamp when the user 
                // logs in. This is a security feature which is used when you 
                // change a password or add an external login to your account.  
                OnValidateIdentity = SecurityStampValidator
                    .OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                    validateInterval: TimeSpan.FromMinutes(30),
                    regenerateIdentity: (manager, user) 
                    => user.GenerateUserIdentityAsync(manager))
      
      }
    });
    
    app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
  
        // Enables the application to temporarily store user information when 
        // they are verifying the second factor in the two-factor authentication process.
        app.UseTwoFactorSignInCookie(
            DefaultAuthenticationTypes.TwoFactorCookie, 
            TimeSpan.FromMinutes(5));
  
  }
}
