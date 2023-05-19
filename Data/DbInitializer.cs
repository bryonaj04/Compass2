using System;
using System.Diagnostics;
using Compass.Models;
using Microsoft.EntityFrameworkCore;

namespace Compass.Data
{
    public class DbInitializer
    {
        public static class SeedData
        {
            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var context = new SchedulingContext(
                    serviceProvider.GetRequiredService<
                        DbContextOptions<SchedulingContext>>()))
                {
                    if (context == null || context.Rooms == null)
                    {
                        throw new ArgumentNullException("Null SchedulingContext");
                    }

                    // Look for any movies.
                    if (context.Rooms.Any())
                    {
                        return;   // DB has been seeded
                    }
                    var people = new Person[]
                    {
                        new Person{Id = 1, Name = "Andy"},
                        new Person{Id = 2, Name = "Richard"},
                        new Person{Id = 3, Name = "Elwood"},
                        new Person{Id = 4, Name = "Rebecca"},
                        new Person{Id = 5, Name = "Rosanna"},
                        new Person{Id = 6 , Name = "Beth"},

                    };
                    foreach (var person in people)
                    {
                        context.People.Add(person);
                    }
                    context.SaveChanges();

                    var rooms = new Room[]
                    {
                        new Room{ Id = 7, Name = "St Winifreds"},
                        new Room{ Id = 8, Name = "St Augustines"},
                        new Room{ Id = 9, Name = "St Anthonys"},
                        new Room{ Id = 10, Name = "Dean Park"},
                        new Room{ Id = 11, Name = "St Valerie"}
                    };
                    foreach (var room in rooms)
                    {
                        context.Rooms.Add(room);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}

