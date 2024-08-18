using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace מירוץ_מכוניות_task
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Car car1 = new Car("Car 1");
            Car car2 = new Car("Car 2");
            Car car3 = new Car("Car 3");

            int meters = 100;
            List<Car> ranking = new List<Car>();

            Task task1 = Task.Run(() => MoveCar(car1, meters, ranking));
            Task task2 = Task.Run(() => MoveCar(car2, meters, ranking));
            Task task3 = Task.Run(() => MoveCar(car3, meters, ranking));

            await Task.WhenAll(task1, task2, task3);

            Console.WriteLine("\nסדר הזכייה:");
            for (int i = 0; i < ranking.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ranking[i].Name}");
            }
        }

        public static async Task MoveCar(Car car, int meters, List<Car> ranking)
        {
            Random random = new Random();
            int distanceCovered = 0;

            while (distanceCovered < meters)
            {
                int speed = random.Next(1, 11);
                distanceCovered += speed;

                car.Position = Math.Min(100, (distanceCovered * 100) / meters);

                Console.WriteLine($"{car.Name} - מיקום נוכחי: {car.Position}%");

                await Task.Delay(1000);
            }

            lock (ranking)
            {
                ranking.Add(car);
            }

            Console.WriteLine($"{car.Name} סיימה את המרוץ!");
        }

        public class Car
        {
            public string Name { get; }
            public int Position { get; set; } 

            public Car(string name)
            {
                Name = name;
                Position = 0;
            }
        }
    }
}
