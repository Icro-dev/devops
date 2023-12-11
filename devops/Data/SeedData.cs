using devops.Data;
using devops.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devops;

public class SeedData
{
    public static async void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        if (context.Articles != null && !context.Articles.Any())
            context.Articles.AddRange(
                new Article
                {
                    Title = "Devops23",
                    Author = "S",
                    Category = "Docker",
                    Subjects = "Docker client, Rest API, server docker daemon",
                    PostedOn = DateTime.Now,
                    LastEditedOn = DateTime.Now,
                    Body = "test"
                }
                );
        await context.SaveChangesAsync();
    }
}
