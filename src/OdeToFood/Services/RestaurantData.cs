﻿using System;
using System.Collections.Generic;
using OdeToFood.Entities;
using System.Linq;

namespace OdeToFood.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();

        Restaurant Get(int id);

        Restaurant Add(Restaurant newRestaurant);
    }


    public class SqlRestaurantData : IRestaurantData
    {
        private OdeToFoodDbContext _context;
        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
            ///may not want SaveChnages() to be here, want to make sure all changes are ready to be saved
            ///may
            _context.SaveChanges();
            return newRestaurant;
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        static List<Restaurant> _restaurants;
        //this is NOT THREAD SAFE, this could not be used if there were multiple concurrent users
        //there is only one list of the restaurants across the entire app, this is only acceptable for pre-database testing
        static InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "The House of Kobe" },
                new Restaurant { Id = 2, Name = "LJ's and the Kat" },
                new Restaurant { Id = 3, Name = "King's Contrivance" }
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants;
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(newRestaurant);

            return newRestaurant;
        }
    }
}
