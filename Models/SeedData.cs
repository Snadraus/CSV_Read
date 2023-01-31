using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CSV_Read.Data;
using System;
using System.Linq;

namespace CSV_Read.Models;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new CSVContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<CSVContext>>()))
        {
            // Look for any CSV_Headerss.
            if (context.CSV_Headers.Any())
            {
                return;   // DB has been seeded
            }
            context.CSV_Headers.AddRange(
                new CSV_Headers
                {
                     index  = 1,
                     organization = "test",
                     name = "another test",
                     website = "third test",
                     country = "fourth test",
                     description = "fifth test",
                     founded = 2,
                     industry = "this and that",
                     numEmp = 9
                },
                new CSV_Headers
                {
                    index = 1,
                    organization = "test",
                    name = "another test",
                    website = "third test",
                    country = "fourth test",
                    description = "fifth test",
                    founded = 2,
                    industry = "this and that",
                    numEmp = 9
                },
                new CSV_Headers
                {
                    index = 1,
                    organization = "test",
                    name = "another test",
                    website = "third test",
                    country = "fourth test",
                    description = "fifth test",
                    founded = 2,
                    industry = "this and that",
                    numEmp = 9
                },
                new CSV_Headers
                {
                    index = 1,
                    organization = "test",
                    name = "another test",
                    website = "third test",
                    country = "fourth test",
                    description = "fifth test",
                    founded = 2,
                    industry = "this and that",
                    numEmp = 9
                }
            );
            context.SaveChanges();
        }
    }
}