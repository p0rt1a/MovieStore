using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace MovieApp.UnitTests.TestSetup
{
    public static class Directors
    {
        public static void AddDirectors(this MovieStoreDbContext context)
        {
            context.Directors.AddRange(
                new Director()
                {
                    Name = "Steven",
                    Surname = "Spielberg"
                },
                new Director()
                {
                    Name = "Alfred",
                    Surname = "Hitchcock"
                },
                new Director()
                {
                    Name = "Akira",
                    Surname = "Kurosawa"
                }
            );
        }
    }
}
