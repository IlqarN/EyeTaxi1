using AdminPanel.Models;
using System;
using System.IO;
using UserPanel.Services;

namespace AdminPanel.Services
{
    public class CreateStatisticService
    {

        public static void SetStatistic(float price)
        {
            string[] dir = Directory.GetCurrentDirectory().Split('\\');
            string path = "";
            foreach (var item in dir)
            {
                if (item.ToLower() == "eyetaxi")
                    break;
                path += item + "\\";
            }


            Statistics statistic = JsonSaveService<Statistics>.Load(path + @"\EyeTaxi\AdminPanel\bin\Debug\statistic");

            if (statistic == null)
                statistic = new Statistics();


            if (statistic.Month == DateTime.Now.Month)
                statistic.MonthlyIncome += price;
            else
            {
                statistic.Month = DateTime.Now.Month;
                statistic.MonthlyIncome = price;
            }

            if (statistic.Day == DateTime.Now.Day)
                statistic.DailyIncome += price;
            else
            {
                statistic.DailyIncome = price;
                statistic.Day = DateTime.Now.Day;
            }

            statistic.IncomeAfterLWD += price;
            statistic.Interest = (statistic.IncomeAfterLWD * 0.15f).ToString("0.##");

            JsonSaveService<Statistics>.Save(statistic, path + @"\EyeTaxi\AdminPanel\bin\Debug\statistic");

        }

    }
}
