﻿using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class Driver
    {
        public float Balance { get; set; }

        public float Rating { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public Location LastLocation { get; set; }

        public Car Car { get; set; }

        public Driver(float balance, float rating, string name, string surname, string phoneNumber, Location lastLocation, Car car)
        {
            Balance = balance;
            Rating = rating;
            Name = name;
            PhoneNumber = phoneNumber;
            LastLocation = lastLocation;
            Car = car;
            Surname = surname;
        }

        public Driver()
        {
            LastLocation = new Location();
            Car = new Car();
            Rating = 5;
        }
        public bool isEmpty()
        {
            return string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Surname) && string.IsNullOrEmpty(PhoneNumber) && Car.IsEmpty();
        }

        public bool IsWhiteSpace()
        {
            return string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Surname) && string.IsNullOrWhiteSpace(PhoneNumber) && Car.IsWhiteSpace();
        }
    }
}
