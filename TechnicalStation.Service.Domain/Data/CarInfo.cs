using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalStation.Service.Domain.Data
{
    public class CarInfo
    {
        private int id;
        private string carModel;

        public CarInfo()
        {
        }

        public CarInfo(int id,
            string carModel)
        {

            this.id = id;
            this.carModel = carModel;
        }

        public int Id { get => id; set => id = value; }
        public string CarModel { get => carModel; set => carModel = value; }
    }
}
