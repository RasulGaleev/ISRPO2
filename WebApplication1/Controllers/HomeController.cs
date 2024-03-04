using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private int _counter = 0;
        public IActionResult TaskFirst()
        {
            return View();
        }


        [HttpPost]
        public IActionResult TaskFirst(string num1, string num2, string num3, string num4)
        {
            double n1 = double.Parse(num1);
            double n2 = double.Parse(num2);
            double n3 = double.Parse(num3);
            double n4 = double.Parse(num4);

            int negativeCount = 0;

            if (n1 < 0)
            {
                negativeCount++;
            }
            if (n2 < 0)
            {
                negativeCount++;
            }
            if (n3 < 0)
            {
                negativeCount++;
            }
            if (n4 < 0)
            {
                negativeCount++;
            }

            ViewBag.NegativeCount = negativeCount;
            return View();
        }


        public IActionResult TaskSecond()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TaskSecond(string Text)
        {
            ViewBag.Text = Text.Replace(" ", "_");
            return View();
        }

        public static List<Book> catalog = new List<Book>
    {
        new Book { Id = Guid.NewGuid(), Author = "John Doe", Title = "The Great Novel", Year = 2020 },
        new Book { Id = Guid.NewGuid(), Author = "Jane Smith", Title = "Adventure Time", Year = 2018 },
    };

        public IActionResult TaskThird()
        {
            return View(catalog);
        }

        public IActionResult TaskThirdAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TaskThirdAdd(Book book)
        {
            if (ModelState.IsValid)
            {
                // Генерация уникального ID с использованием GUID
                book.Id = Guid.NewGuid();

                catalog.Add(book);

                return RedirectToAction("TaskThird");
            }

            // Если данные некорректны, возвращаем представление с ошибками валидации
            return View("TaskThirdAdd", book);
        }

        public IActionResult RemoveBook(Guid id)
        {
            var bookToRemove = catalog.Find(b => b.Id == id);

            if (bookToRemove != null)
            {
                catalog.Remove(bookToRemove);
            }

            return RedirectToAction("TaskThird");
        }

        public IActionResult Search(string searchTerm)
        {
            List<Book> results;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // Если строка поиска пуста, вернуть все записи из каталога
                results = catalog;
            }
            else
            {
                // Иначе выполнить поиск по заданному критерию
                results = catalog.FindAll(book =>
                    book.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    book.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    book.Year.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            return View("TaskThird", results);
        }

    }
}