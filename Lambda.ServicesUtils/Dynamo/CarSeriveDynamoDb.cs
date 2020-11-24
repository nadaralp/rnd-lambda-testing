using Lambda.ServicesUtils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace Lambda.ServicesUtils.Dynamo
{
    public class CarSeriveDynamoDb : DynamoBaseService, ICarService
    {
        public async Task AddCarAsync(Car car)
        {
            car.Id = car.Id ?? Guid.NewGuid().ToString();
            if (car.CreationDate == null || car.CreationDate == new DateTime())
                car.CreationDate = DateTime.Now;

            await context.SaveAsync(car);
        }

        public async Task DeleteCar(string carId)
        {
            await context.DeleteAsync(carId);
        }

        public async Task<IEnumerable<Car>> FilterCars(string manufacturer = null, double? price = null)
        {
            List<ScanCondition> scanConditions = new List<ScanCondition>();
            if (manufacturer == null && price == null)
            {
                throw new InvalidOperationException("both predicates (manufacturer, price) are null, can't filter");
            }

            if (manufacturer != null)
            {
                var scanCondition = new ScanCondition(nameof(Car.Manufacturer), ScanOperator.Equal, manufacturer);
                scanConditions.Add(scanCondition);
            }

            if (price != null && price != 0)
            {
                var scanCondition = new ScanCondition(nameof(Car.Price), ScanOperator.Equal, price);
                scanConditions.Add(scanCondition);
            }

            var filteredCars = await context.ScanAsync<Car>(scanConditions).GetRemainingAsync();
            return filteredCars;
        }

        public async Task<Car> GetCarByIdAsync(string hashKey)
        {
            var carByHashKey = await context.LoadAsync<Car>(hashKey);
            return carByHashKey;
        }

        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            List<ScanCondition> conditions = new List<ScanCondition>();
            List<Car> allCars = await context.ScanAsync<Car>(conditions).GetRemainingAsync();
            return allCars;
        }

        public Task UpdateCar(int carIdToUpdate, Car newCar)
        {
            throw new NotImplementedException();
        }
    }
}