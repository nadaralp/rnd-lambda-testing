using Lambda.ServicesUtils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils.Dynamo
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetCarsAsync();

        Task<IEnumerable<Car>> FilterCars(string manufacturer = null, double? price = null);

        Task<Car> GetCarByIdAsync(string hashKey);

        Task AddCarAsync(Car car);

        Task UpdateCar(int carIdToUpdate, Car newCar);

        Task DeleteCar(string carId);
    }
}