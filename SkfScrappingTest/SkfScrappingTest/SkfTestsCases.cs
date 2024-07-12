using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace SkfScrappingTest
{
    [TestClass]
    public class SkfTestsCases
    {
      
        private readonly IWebDriver _driver;
        private readonly SkfHome _skfHome;
        private readonly CookiesPopUp _cookiesPopUp;
        private readonly SkfProducto _skfProducto;

        public SkfTestsCases()
        {
            _driver = new ChromeDriver();
            _cookiesPopUp = new CookiesPopUp(_driver);
            _skfHome = new SkfHome(_driver);
            //_skfHome.OpenHome();
            //_cookiesPopUp.AceptarCookies();
            _skfProducto = new SkfProducto(_driver);
        }

        [TestMethod]
        public async Task GetSkusTest()
        {
            var links = getLinks();
            List<string> skus = new List<string>();
           
            foreach (var link in links)
            {
                try
                {
                    var producto = await _skfProducto.GetProductos(link);
                    skus.Add(producto);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocurrió un error: " + ex.Message);
                }
            }

            escribirResultado(skus);
        }

        private void escribirResultado(List<string> skus)
        {
            // Ruta del archivo donde deseas escribir los datos
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Subir tres niveles para salir de bin\Debug\net7.0
            string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;

            // Combinar con la carpeta Data
            string filePath = Path.Combine(projectDirectory, "Data", "resultado.txt");

            try
            {
                // Escribir cada string en una nueva línea en el archivo de texto
                File.WriteAllLines(filePath, skus);
                Console.WriteLine("Los datos se han escrito correctamente en el archivo: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al escribir en el archivo: " + ex.Message);
            }
        }

        private List<string> getLinks()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Subir tres niveles para salir de bin\Debug\net7.0
            string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;

            // Combinar con la carpeta Data
            string filePath = Path.Combine(projectDirectory, "Data", "links.txt");
            if (File.Exists(filePath))
            {
                // Leer todas las líneas del archivo
                string[] lines = File.ReadAllLines(filePath);

                // Procesar cada línea
                return lines.ToList();
            }
            else
            {
                Console.WriteLine("El archivo no existe: " + filePath);
                throw new Exception();
            }
        }
    }
}
