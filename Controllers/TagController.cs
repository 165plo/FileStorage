using FileStorageMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileStorageMVC
{
    [RouteAttribute("[controller]/[action]")]
    public class TagController : Controller
    {
        private FileContext _fileContext;

        public TagController(FileContext fileContext)
        {
            _fileContext = fileContext;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult AddTag()
        {
            return View();
        }

        public IActionResult Add(string tag)
        {
            _fileContext.Tags.Add(new Tag{Value = tag});
            _fileContext.SaveChanges();
            return Redirect("Index");
        }

        public IActionResult Index()
        {
            return View(_fileContext.Tags);
        }

        
        public IActionResult Delete(Tag tag)
        {
            _fileContext.Tags.Remove(tag);
            _fileContext.SaveChanges();
            return Redirect("Index");
        }

    }
}