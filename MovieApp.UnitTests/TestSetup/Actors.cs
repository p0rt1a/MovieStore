using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace MovieApp.UnitTests.TestSetup
{
    public static class Actors
    {
        public static void AddActors(this MovieStoreDbContext context)
        {
            context.Actors.AddRange(
                new Actor()
                {
                    Name = "James",
                    Surname = "Cagney",
                },
                new Actor()
                {
                    Name = "James",
                    Surname = "Stewart"
                },
                new Actor()
                {
                    Name = "Cary",
                    Surname = "Grant"
                },
                new Actor()
                {
                    Name = "Henry",
                    Surname = "Fonda"
                },
                new Actor()
                {
                    Name = "Marlon",
                    Surname = "Brondo"
                }
            );
        }
    }
}
