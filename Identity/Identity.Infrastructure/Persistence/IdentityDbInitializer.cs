using Azure.Core;
using Microsoft.AspNetCore.Identity;

internal class IdentityDbInitializer : DbInitializer
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public IdentityDbInitializer(
        IdentityDbContext db,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        : base(db)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public override void Initialize()
    {
        base.Initialize();

        SeedAdministrator();
    }

    private void SeedAdministrator()
        => Task
            .Run(async () =>
            {
                if (await roleManager.FindByNameAsync(CommonModelConstants.Common.AdministratorRoleName) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(CommonModelConstants.Common.AdministratorRoleName));
                }

                if (await roleManager.FindByNameAsync(CommonModelConstants.Common.UserRoleName) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(CommonModelConstants.Common.UserRoleName));
                }

                if(await roleManager.FindByNameAsync(CommonModelConstants.Common.AuthorRoleName) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(CommonModelConstants.Common.AuthorRoleName));
                }

                //var adminUser = new UserData().GetData();
                //await userManager.CreateAsync(adminUser, "5a5ec25b-1b60-44dc-88b4-f1013eb56832");
                //await userManager.AddToRoleAsync(adminUser, CommonModelConstants.Common.AdministratorRoleName);
            })
            .GetAwaiter()
            .GetResult();
}