using Bogus;
using Microsoft.EntityFrameworkCore;
using ParkingManagement.src.domain.Entities;
using ParkingManagement.src.infrastructure;

namespace ParkingManagement.src.Seeding
{
    public class Seeder
    {
        private readonly AppDbContext _dbContext;

        public Seeder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.ParkingSpaces.Any())
            {
                _dbContext.ParkingSpaces.ExecuteDelete();
                _dbContext.Vehicles.ExecuteDelete();
                _dbContext.SaveChanges();
            }

            var faker = new Faker();

            for (int i = 1; i <= 15; i++)
            {
                if (i < 10)
                {
                    var freeParkingPlace = new ParkingSpace
                    {
                        PlaceNumber = i
                    };

                    _dbContext.ParkingSpaces.Add(freeParkingPlace);
                    _dbContext.SaveChanges();
                }
                else
                {
                    var occupiedParkingPlace = new ParkingSpace
                    {
                        PlaceNumber = i,                        
                        Vehicle = new Vehicle
                        {
                            LicensePlate = faker.Vehicle.Vin().Substring(0, 6),
                            VehicleType = faker.PickRandom<VehicleType>(),
                            EntryTime = faker.Date.Recent(1)
                        }
                    };

                    _dbContext.ParkingSpaces.Add(occupiedParkingPlace);
                }
            }

            _dbContext.SaveChanges();
        }
    }
}
