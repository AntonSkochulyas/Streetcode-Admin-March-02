using Moq;
using Streetcode.DAL.Entities.Locations;
using Streetcode.DAL.Repositories.Interfaces.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.XUnitTest.Repositories.Mocks
{
    internal class RepositoryMocker
    {
        public static Mock<ILocationRepository> GetLocationRepositoryMock()
        {
            var locations = new List<Location>()
            {
                new Location() { Id = 1, Streetname = "First location", TableNumber = 1 },
                new Location() { Id = 2, Streetname = "Second location", TableNumber = 2 },
                new Location() { Id = 3, Streetname = "Third location", TableNumber = 3 },
                new Location() { Id = 4, Streetname = "Fourth location", TableNumber = 4 },
            }
        }
    }
    }
