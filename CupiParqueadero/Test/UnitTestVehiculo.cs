using Xunit;
using CupiParqueadero.Controllers;
using ParkingMentoring.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CupiParqueadero.Models;

namespace CupiParqueadero.Test
{
    public class UnitTestVehiculo
    {

        [Fact]
        public async Task CalcularPrecio_DosHorasDiaNormal_DeberiaRetornarPrecioCorrecto40Async()
        {
            // Arrange
            var calculator = new Vehicle("", "", "");
            double payment = calculator.PaymentParking(DateTime.Parse("2023-09-29T13:00:00"), DateTime.Parse("2023-09-29T15:00:00"));

            // Assert
            var paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            double resultadoEsperado = 40;
            Assert.Equal(resultadoEsperado, payment);
        }

        [Fact]
        public async Task CalcularPrecio_DosHorasDiaEconomico_DeberiaRetornarPrecioCorrecto20Async()
        {
            // Arrange
            var calculator = new Vehicle("", "", "");
            double payment = calculator.PaymentParking(DateTime.Parse("2023-09-27T13:00:00"), DateTime.Parse("2023-09-27T15:00:00"));

            // Assert
            var paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            double resultadoEsperado = 20;
            Assert.Equal(resultadoEsperado, payment);
        }

        [Fact]
        public async Task CalcularPrecio_DosHorasDiaFinDeSemana_DeberiaRetornarPrecioCorrecto50Async()
        {
            // Arrange
            var calculator = new Vehicle("", "", "");
            double payment = calculator.PaymentParking(DateTime.Parse("2023-09-30T13:00:00"), DateTime.Parse("2023-09-30T15:00:00"));

            // Assert
            var paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            double resultadoEsperado = 50;
            Assert.Equal(resultadoEsperado, payment);
        }

        [Fact]
        public async Task CalcularPrecio_MasOchoHorasMenosUnDia_DeberiaRetornarPrecioCorrecto200Async()
        {
            // Arrange
            var calculator = new Vehicle("", "", "");
            double payment = calculator.PaymentParking(DateTime.Parse("2023-09-30T03:00:00"), DateTime.Parse("2023-09-30T15:00:00"));

            // Assert
            var paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            double resultadoEsperado = 200;
            Assert.Equal(resultadoEsperado, payment);

            // -------------------------------------------------------------------------
            // Arrange
            calculator = new Vehicle("", "", "");
            payment = calculator.PaymentParking(DateTime.Parse("2023-09-15T03:00:00"), DateTime.Parse("2023-09-15T15:00:00"));

            // Assert
            paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            resultadoEsperado = 200;
            Assert.Equal(resultadoEsperado, payment);

            // -------------------------------------------------------------------------
            // Arrange
            calculator = new Vehicle("", "", "");
            payment = calculator.PaymentParking(DateTime.Parse("2023-09-13T03:00:00"), DateTime.Parse("2023-09-13T15:00:00"));

            // Assert
            paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            resultadoEsperado = 200;
            Assert.Equal(resultadoEsperado, payment);
        }

        [Fact]
        public async Task CalcularPrecio_TresDias_DeberiaRetornarPrecioCorrectoAsync()
        {
            // Arrange
            var calculator = new Vehicle("", "", "");
            double payment = calculator.PaymentParking(DateTime.Parse("2023-09-27T13:00:00"), DateTime.Parse("2023-09-30T15:00:00"));

            // Assert
            var paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            double resultadoEsperado = 650;
            Assert.Equal(resultadoEsperado, payment);

            //---------------------------------------------------------------------------------

            // Arrange
            calculator = new Vehicle("", "", "");
            payment = calculator.PaymentParking(DateTime.Parse("2023-09-11T19:00:00"), DateTime.Parse("2023-09-14T23:00:00"));

            // Assert
            paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            resultadoEsperado = 740;
            Assert.Equal(resultadoEsperado, payment);

            //---------------------------------------------------------------------------------

            // Arrange
            calculator = new Vehicle("", "", "");
            payment = calculator.PaymentParking(DateTime.Parse("2023-09-11T01:00:00"), DateTime.Parse("2023-09-14T05:00:00"));

            // Assert
            paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            resultadoEsperado = 740;
            Assert.Equal(resultadoEsperado, payment);
        }

        [Fact]
        public async Task CalcularPrecio_NocturnoMenosOchoHoras_DeberiaRetornarPrecioCorrectoAsync()
        {
            // Arrange
            var calculator = new Vehicle("", "", "");
            double payment = calculator.PaymentParking(DateTime.Parse("2023-09-30T19:00:00"), DateTime.Parse("2023-09-30T21:20:00"));

            // Assert
            var paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            double resultadoEsperado = 105;
            Assert.Equal(resultadoEsperado, payment);
        }

        [Fact]
        public async Task CalcularPrecio_DiurnoNocturnoMenosOchoHoras_DeberiaRetornarPrecioCorrectoAsync()
        {
            // Arrange
            var calculator = new Vehicle("", "", "");
            double payment = calculator.PaymentParking(DateTime.Parse("2023-09-30T16:00:00"), DateTime.Parse("2023-09-30T21:20:00"));

            // Assert
            var paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            double resultadoEsperado = 190;
            Assert.Equal(resultadoEsperado, payment);
        }

        [Fact]
        public async Task CalcularPrecio_NocturnoOchoHoras_DeberiaRetornarPrecioCorrectoAsync()
        {
            // Arrange
            var calculator = new Vehicle("", "", "");
            double payment = calculator.PaymentParking(DateTime.Parse("2023-09-29T23:30:00"), DateTime.Parse("2023-09-30T06:20:00"));

            // Assert
            var paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            double resultadoEsperado = 245;
            Assert.Equal(resultadoEsperado, payment);
        }

        [Fact]
        public async Task CalcularPrecio_NocturnoDiurnoMenosOchoHoras_DeberiaRetornarPrecioCorrectoAsync()
        {
            // Arrange
            var calculator = new Vehicle("", "", "");
            double payment = calculator.PaymentParking(DateTime.Parse("2023-09-29T04:30:00"), DateTime.Parse("2023-09-29T08:00:00"));

            // Assert
            var paymentType = Assert.IsType<double>(payment);

            // Comparar el valorPay con el resultado esperado
            double resultadoEsperado = 95;
            Assert.Equal(resultadoEsperado, payment);
        }
    }
}
