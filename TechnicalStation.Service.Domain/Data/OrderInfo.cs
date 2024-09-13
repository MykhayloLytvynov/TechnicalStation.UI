using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalStation.Service.Domain.Data
{
    public class OrderInfo
    {

        private int id;
        private DateTime start_date;
        private DateTime end_date;
        private string car_produser;
        private DateTime car_year_of_creation;
        private string firstname_of_client;
        private string secondname_of_client;
        private string patronymic_of_client;
        private int customerId = 1;
        int carId = 1;

        public OrderInfo()
        {
        }

        public OrderInfo(int id,
        DateTime start_date,
        DateTime end_date,
        string car_produser,
        DateTime car_year_of_creation,
        string firstname_of_client,
        string secondname_of_client,
        string patronymic_of_client,
        int customerId,
        int carId)
        {
            this.start_date = start_date;
            this.id = id;
            this.end_date = end_date;
            this.car_produser = car_produser;
            //this.car_model = car_model;
            this.car_year_of_creation = car_year_of_creation;
            this.firstname_of_client = firstname_of_client;
            this.secondname_of_client = secondname_of_client;
            this.patronymic_of_client = patronymic_of_client;
            this.customerId = customerId;
            this.carId = carId;
        }

        public int Id { get => id; set => id = value; }
        public int CustomerId { get => customerId; set => this.customerId = value; }
        public int CarId { get => carId; set => this.carId = value; }
        public DateTime Start_date { get => start_date; set => start_date = value; }
        public DateTime End_date { get => end_date; set => end_date = value; }
        public string Car_produser { get => car_produser; set => car_produser = value; }
        //public string Car_model { get => car_model; set => car_model = value; }
        public DateTime Car_year_of_creation { get => car_year_of_creation; set => car_year_of_creation = value; }
        public string Firstname_of_client { get => firstname_of_client; set => firstname_of_client = value; }
        public string Secondname_of_client { get => secondname_of_client; set => secondname_of_client = value; }
        public string Patronymic_of_client { get => patronymic_of_client; set => patronymic_of_client = value; }
    }
}
