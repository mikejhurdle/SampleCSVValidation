using SampleCSVValidation.AppServices;
using SampleCSVValidation.DTOs;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace SampleCSVValidation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Post action for the Form on the Index page. Used to pull in the CSV file, type of delimitation, and number of columns
        /// </summary>
        /// <param name="file">Posted file from user</param>
        /// <param name="columnCount">Number of expected columns - from user input</param>
        /// <param name="fileSchema">Determines if format should be CSV or TAB</param>
        /// <returns>ProcessResultDTO to the ViewData</returns>
        /// <see cref="ProcessResultDTO"/>
        [HttpPost]
        public ActionResult Index(int columnCount)
        {
            var _fileServ = new FileServices();
            _fileServ.DoThings();
            //var result = new ProcessResultDTO();
            //try
            //{
            //    // verify file is present
            //    if (file.ContentLength > 0)
            //    {
            //        var _fileServ = new FileServices();

            //        // Archive the original file for reading and audit purposes
            //        var filePath = _fileServ.ArchiveFile(file);

            //        result = _fileServ.ProcessFile(file.FileName, filePath, columnCount, fileSchema);
            //        result.Message = "File Successfully processed";
            //        ViewData["Result"] = result;
            //    }
            //    else
            //    {
            //        result.Message = "An error occured uploading the file";
            //        ViewData["Result"] = result;
            //    }

            return View();
            //}
            //catch (Exception Ex)
            //{
            //    result.Message = "File upload failed";
            //    ViewData["Result"] = result;
            //    return View();
            //    throw Ex;

            //}
        }
    }
}