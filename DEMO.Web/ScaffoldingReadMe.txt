Scaffolding has generated all the files and added the required dependencies.

However the Application's Startup code may require additional changes for things to work end to end.
Add the following code to the Configure method in your Application's Startup class if not already done:

        app.UseEndpoints(endpoints =>
        {
          endpoints.MapControllerRoute(
            name : "areas",
            pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
        });

WRONG VERSION OF THE COMMAND
Scaffold-DbContext "Server=LAPTOP-RULCTAQ5\SQLEXPRESS;Database=db_theloanmallm2n;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context ApplicationDbContext -Project DEMO.Data -Force

RIGHT VERSION OF THE COMMAND
Scaffold-DbContext "Server=LAPTOP-RULCTAQ5\SQLEXPRESS;Database=db_theloanmallm2n;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context ApplicationDbContext -Project DEMO.Data -StartupProject DEMO.Data -Force