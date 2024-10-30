using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Referencias
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;


namespace ndmmPruebaUI
{
    [TestClass]
    public class GoogleSearchTest
    {
        private IWebDriver driver;

        public GoogleSearchTest()
        {
            // Inicializa el controlador de Chrome
            driver = new ChromeDriver();
        }

        [TestMethod]
        public void SearchInGoogle()
        {
            // Navega a Google
            driver.Navigate().GoToUrl("https://www.google.com");

            // Encuentra el cuadro de búsqueda
            var searchBox = driver.FindElement(By.Name("q"));

            // Ingresa la búsqueda
            searchBox.SendKeys("OpenAI");

            // Enviar la búsqueda
            searchBox.Submit();

            // Espera un momento para que se carguen los resultados
            System.Threading.Thread.Sleep(2000);

            // Verifica que el título de la página contenga "OpenAI"
            Assert.IsTrue(driver.Title.Contains("OpenAI"), "El título no contiene 'OpenAI'");
        }

        public void Dispose()
        {
            // Cierra el navegador
            driver.Quit();
        }
    }
}