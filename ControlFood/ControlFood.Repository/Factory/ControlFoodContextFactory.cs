using ControlFood.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ControlFoodContextFactory : IDesignTimeDbContextFactory<ControlFoodContext>
{
    public ControlFoodContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ControlFoodContext>();
        optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=ControlFoodDb;Uid=root;Pwd=root;");

        return new ControlFoodContext(optionsBuilder.Options);
    }
}