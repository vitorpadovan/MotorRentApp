using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MotorRentApp.Core.Model;
using System.Net.Http.Json;
using BFF_MotorRentApp.Controllers.Requests.Vehicle;
using Moq;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http.Headers;
using System.Diagnostics.Contracts;

namespace MotorRentApp.IntegrationTests.Tests
{
    public class VehicleIntegrationTests : MotorRentScenariosBase
    {

        [Theory]
        [InlineData(1, false)]
        [InlineData(3, false)]
        [InlineData(80, false)]
        [InlineData(-1, true)]
        [InlineData(0, true)]
        public async Task InvalidYear(int quantity, bool valid)
        {
            // Arrange
            using MotorRentTestServer server = CreateServer();
            using HttpClient httpClient = server.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var year = DateTime.Now.Year + quantity;

            // Act
            HttpResponseMessage response = await httpClient.PostAsJsonAsync<VehicleRegistration>("api/v1/Vehicle/registration", new()
            {
                LicensePlate = "FFX-1212",
                Year = year
            });

            // Assert
            if (valid)
            {
                Assert.True(response.IsSuccessStatusCode);
            }
            else
            {
                Assert.True(!response.IsSuccessStatusCode);
                Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
            }
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("FFF-12X4", false)]
        [InlineData("FFF-12X4ASDASD", false)]
        [InlineData("FFF1215", false)]
        [InlineData("FF1-1216", false)]
        [InlineData("FFF-1X17", true)]
        [InlineData("FFF-1217", true)]
        public async Task InvalidLicensePlate(string license, bool valid)
        {
            // Arrange
            using MotorRentTestServer server = CreateServer();
            using HttpClient httpClient = server.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var year = DateTime.Now.Year;

            // Act
            HttpResponseMessage response = await httpClient.PostAsJsonAsync<VehicleRegistration>("api/v1/Vehicle/registration", new()
            {
                LicensePlate = license,
                Year = year
            });

            // Assert
            if (valid)
                Assert.True(response.IsSuccessStatusCode);
            else
            {
                Assert.True(!response.IsSuccessStatusCode);
                Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
            }
        }

        [Theory(DisplayName = "Eu como usuário admin quero cadastrar uma nova moto")]
        [InlineData("FXX-1K99", 2024)]
        [InlineData("IKP-1L88", 2024)]
        [InlineData("YUI-8X75", 2024)]
        public async Task RegisterLicensePlateByAdmin(string license, int year)
        {
            // Arrange
            using MotorRentTestServer server = CreateServer();
            using HttpClient httpClient = server.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            HttpResponseMessage response = await httpClient.PostAsJsonAsync<VehicleRegistration>("api/v1/Vehicle/registration", new()
            {
                LicensePlate = license,
                Year = year
            });

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        public async Task RegisterLicensePlateByUser()
        {
            throw new NotImplementedException();
        }

        [Fact(DisplayName = "Eu como usuário admin quero consultar as motos existentes na plataforma e conseguir filtrar pela placa.")]
        public async Task ASdasd()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task ChangeLicensePlate()
        {
            throw new NotImplementedException();
        }

        public async Task Asdasd()
        {
        }
    }
}
