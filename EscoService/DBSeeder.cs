﻿namespace EscoService
{
    public class DBSeeder
    {
        public static void AddEscos(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<ApiContext>();

            db?.Escos.Add(
                new Esco()
                {
                    ID = 1,
                    Name = "Company A",
                    Size = 25
                });

            db?.Escos.Add(
                new Esco()
                {
                    ID = 2,
                    Name = "Company B",
                    Size = 56
                });

            db?.Escos.Add(
                new Esco()
                {
                    ID = 3,
                    Name = "Company C",
                    Size = 12
                });

            db?.Escos.Add(
                new Esco()
                {
                    ID = 4,
                    Name = "Company D",
                    Size = 205
                });

            db?.Customers.Add(
                new Customer()
                {
                    ID = 1,
                    CustomerId = 1,
                    Name = "Customer A",
                    Price = 10
                });

            db?.Customers.Add(
                new Customer()
                {
                    ID = 2,
                    CustomerId = 1,
                    Name = "Customer B",
                    Price = 35
                });

            db?.Customers.Add(
                new Customer()
                {
                    ID = 3,
                    CustomerId = 2,
                    Name = "Customer C",
                    Price = 22
                });

            db?.Customers.Add(
                new Customer()
                {
                    ID = 4,
                    CustomerId = 2,
                    Name = "Customer D",
                    Price = 15
                });

            db?.Customers.Add(
                new Customer()
                {
                    ID = 5,
                    CustomerId = 3,
                    Name = "Customer E",
                    Price = 103
                });

            db?.Customers.Add(
                new Customer()
                {
                    ID = 6,
                    CustomerId = 3,
                    Name = "Customer F",
                    Price = 135
                });

            db?.Customers.Add(
                new Customer()
                {
                    ID = 7,
                    CustomerId = 4,
                    Name = "Customer G",
                    Price = 76
                });

            db?.Customers.Add(
                new Customer()
                {
                    ID = 8,
                    CustomerId = 4,
                    Name = "Customer H",
                    Price = 33
                });

            db?.SaveChanges();
        }

    }
}
