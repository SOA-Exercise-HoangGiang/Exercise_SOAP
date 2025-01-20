using ServiceReference1;
using System.Data;

namespace BTUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new WebService1SoapClient(WebService1SoapClient.EndpointConfiguration.WebService1Soap);

                var response = await client.GetAllCountriesAsync();
                var countries = response.Body.GetAllCountriesResult;

                if (countries != null && countries.Any())
                {
                    var dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn("Code", typeof(string)),
                        new DataColumn("Name", typeof(string)),
                        new DataColumn("Continent", typeof(string)),
                        new DataColumn("Region", typeof(string)),
                        new DataColumn("Population", typeof(int))
                    });

                    foreach (var country in countries)
                    {
                        dt.Rows.Add(
                            country.Code,
                            country.Name,
                            country.Continent,
                            country.Region,
                            country.Population
                        );
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoResizeColumns();
                }
                else
                {
                    MessageBox.Show("No countries found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please enter a country code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var client = new WebService1SoapClient(WebService1SoapClient.EndpointConfiguration.WebService1Soap);
                string countryCode = textBox1.Text.Trim();

                // Gọi phương thức GetCountryByCodeAsync
                var response = await client.GetCountryByCodeAsync(countryCode);
                var countryDB = response.Body.GetCountryByCodeResult; // Trả về một đối tượng Country

                if (countryDB != null)
                {
                    var dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn("Code", typeof(string)),
                        new DataColumn("Name", typeof(string)),
                        new DataColumn("Continent", typeof(string)),
                        new DataColumn("Region", typeof(string)),
                        new DataColumn("Population", typeof(int))
                    });

                    // Thêm dữ liệu quốc gia vào DataTable
                    dt.Rows.Add(
                        countryDB.Code,
                        countryDB.Name,
                        countryDB.Continent,
                        countryDB.Region,
                        countryDB.Population
                    );

                    // Gán DataTable cho DataGridView
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoResizeColumns();
                }
                else
                {
                    MessageBox.Show("No country found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please enter a city name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var client = new WebService1SoapClient(WebService1SoapClient.EndpointConfiguration.WebService1Soap);
                string cityName = textBox2.Text.Trim();

                var response = await client.GetCityByNameAsync(cityName);
                var cities = response.Body.GetCityByNameResult;

                if (cities != null && cities.Any())
                {
                    var dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn("Id", typeof(int)),
                        new DataColumn("Name", typeof(string)),
                        new DataColumn("CountryCode", typeof(string)),
                        new DataColumn("Population", typeof(int)),
                        new DataColumn("District", typeof(string))
                    });

                    foreach (var city in cities)
                    {
                        dt.Rows.Add(
                            city.Id,
                            city.Name,
                            city.CountryCode,
                            city.Population,
                            city.District
                        );
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoResizeColumns();
                }
                else
                {
                    MessageBox.Show("No cities found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Please enter a country code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var client = new WebService1SoapClient(WebService1SoapClient.EndpointConfiguration.WebService1Soap);
                string countryCode = textBox3.Text.Trim();

                var response = await client.GetCitiesByCountryAsync(countryCode);
                var cities = response.Body.GetCitiesByCountryResult;

                if (cities != null && cities.Any())
                {
                    var dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn("Id", typeof(int)),
                        new DataColumn("Name", typeof(string)),
                        new DataColumn("CountryCode", typeof(string)),
                        new DataColumn("Population", typeof(int)),
                        new DataColumn("District", typeof(string))
                    });

                    foreach (var city in cities)
                    {
                        dt.Rows.Add(
                            city.Id,
                            city.Name,
                            city.CountryCode,
                            city.Population,
                            city.District
                        );
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoResizeColumns();
                }
                else
                {
                    MessageBox.Show("No cities found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}