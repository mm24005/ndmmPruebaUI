using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Referencias
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace ndmmPruebaUI
{
    [TestClass]
    public class Test_guia5
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private const string BaseUrl = "https://localhost:7054";

        public Test_guia5()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void TestCrearProducto()
        {
            try
            {
                driver.Navigate().GoToUrl($"{BaseUrl}/Customer");


                // Navegar a la página inicial de productos
                driver.Navigate().GoToUrl($"{BaseUrl}/Customer/Create");

                // Esperar a que el formulario esté visible y completar los campos
                wait.Until(d => d.FindElement(By.Id("Name")))
                    .SendKeys("Nery");

                driver.FindElement(By.Id("LastName"))
                    .SendKeys("Mendez");

                driver.FindElement(By.Id("Address"))
                    .SendKeys("villanf");

                // Encontrar y enviar el formulario
                var form = driver.FindElement(By.TagName("form"));
                form.Submit();

                // Esperar a que redirija al índice y verificar que aparezca el producto
                wait.Until(d => d.Url.Contains("/Customer"));

                // Verificar que el producto fue creado buscando en la tabla
                var productCell = wait.Until(d =>
                    d.FindElement(By.XPath("//td[contains(text(), 'Nery')]")));

                Assert.IsNotNull(productCell,
                    "El producto no aparece en la lista después de ser creado");

                Console.WriteLine("Producto creado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante la prueba: {ex.Message}");
                TakeScreenshot();
                throw;
            }
        }

        private void TakeScreenshot()
        {
            try
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var fileName = $"ErrorScreenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                screenshot.SaveAsFile(fileName);
                Console.WriteLine($"Screenshot guardado como {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al tomar screenshot: {ex.Message}");
            }
        }

        public void Dispose()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}