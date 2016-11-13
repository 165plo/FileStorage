using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileStorageMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStorageMVC
{
    [RouteAttribute("[controller]/[action]")]
    public class FileController : Controller
    {
        private FileContext _fileContext;

        public FileController(FileContext fileContext)
        {
            _fileContext = fileContext;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult AddForm()
        {
            return View();
        }

        public IActionResult Add(IList<IFormFile> FileUpload)
        {
            //var files = Request.Form.Files;
            foreach(var f in FileUpload){
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    f.CopyTo(memoryStream);
                    _fileContext.Files.Add(
                        new File
                        {
                            Name = f.FileName, 
                            Payload = memoryStream.ToArray(),
                            Deleted = false 
                        });
                        _fileContext.SaveChanges();
                }
            }
            
            // foreach(var f in files){
            //     using (MemoryStream memoryStream = new MemoryStream())
            //     {
            //         f.CopyTo(memoryStream);
            //         _fileContext.Files.Add(
            //             new File
            //             {
            //                 Name = f.FileName, 
            //                 Payload = memoryStream.ToArray(),
            //                 Deleted = false 
            //             });
            //             _fileContext.SaveChanges();
            //     }
            // }
            
            return Redirect("Index");
        }

        public IActionResult Delete(int Key)
        {
            var dbFile = _fileContext.Files.First(x=>x.Key == Key);
            _fileContext.Files.Remove(dbFile);
            _fileContext.SaveChanges();
            return Redirect("Index");
        }

        public IActionResult Index()
        {
            return View(_fileContext.Files.Select(x=>x.ToViewModel()));
        }

        public IActionResult Download(int Key)
        {
            var dbFile = _fileContext.Files.First(x=>x.Key == Key);
            return File(dbFile.Payload, "application/octet-stream", dbFile.Name);
        }

        public IActionResult Edit(int Key)
        {
            var dbFile = _fileContext.Files.First(x=>x.Key == Key);
            //return File(dbFile.Payload, "application/octet-stream", dbFile.Name);
            return View(dbFile.ToViewModel());
        }
    }
}