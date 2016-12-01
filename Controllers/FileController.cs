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
            var files = _fileContext.Files.ToList();
            files.Sort(FileStorageMVC.File.CompareByName);
            return View(files.Select(x=>x.ToViewModel()));
        }

        public IActionResult Download(int Key)
        {
            var dbFile = _fileContext.Files.First(x=>x.Key == Key);
            return File(dbFile.Payload, "application/octet-stream", dbFile.Name);
        }

        public IActionResult Edit(int Key)
        {
            if(Key==0)
                return null;
            var dbFile = _fileContext.Files.First(x=>x.Key == Key);
            //return File(dbFile.Payload, "application/octet-stream", dbFile.Name);
            return View(new EditViewModel
            {
                Key = dbFile.Key, 
                Name = dbFile.Name, 
                SelectTags = _fileContext.Tags.Select(x=>x.Value).ToList() 
            });
        }
    }
}