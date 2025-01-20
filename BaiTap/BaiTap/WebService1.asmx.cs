using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BaiTap
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WorldDbConnection"].ConnectionString;

        // Lấy tất cả các quốc gia
        [WebMethod]
        public List<Country> GetAllCountries()
        {
            var countries = new List<Country>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Code, Name, Continent, Region, Population FROM Country", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    countries.Add(new Country
                    {
                        Code = reader.GetString(0),
                        Name = reader.GetString(1),
                        Continent = reader.GetString(2),
                        Region = reader.GetString(3),
                        Population = reader.GetInt32(4)
                    });
                }
            }
            return countries;
        }

        // Lấy các thành phố theo mã quốc gia
        [WebMethod]
        public List<City> GetCitiesByCountry(string countryCode)
        {
            var cities = new List<City>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Id, Name, CountryCode, Population, District FROM City WHERE CountryCode = @CountryCode", connection);
                command.Parameters.AddWithValue("@CountryCode", countryCode);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new City
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        CountryCode = reader.GetString(2),
                        Population = reader.GetInt32(3),
                        District = reader.GetString(4)
                    });
                }
            }
            return cities;
        }

        // Lấy tên quốc gia theo mã quốc gia
        [WebMethod]
        public Country GetCountryByCode(string countryCode)
        {
            Country result = null;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Code, Name, Continent, Region, Population FROM Country WHERE Code = @CountryCode", connection);
                command.Parameters.AddWithValue("@CountryCode", countryCode);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = new Country
                    {
                        Code = reader.GetString(0),       
                        Name = reader.GetString(1),       
                        Continent = reader.GetString(2),  
                        Region = reader.GetString(3),     
                        Population = reader.GetInt32(4)  
                    };
                }
            }
            return result;
        }

        // Lấy tất cả thành phố theo tên thành phố
        [WebMethod]
        public List<City> GetCityByName(string cityName)
        {
            var cities = new List<City>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Id, Name, CountryCode, Population, District FROM City WHERE Name LIKE @CityName", connection);
                command.Parameters.AddWithValue("@CityName", "%" + cityName + "%"); // Tìm kiếm tên gần đúng
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new City
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        CountryCode = reader.GetString(2),
                        Population = reader.GetInt32(3),
                        District = reader.GetString(4)
                    });
                }
            }
            return cities;
        }
    }
}
