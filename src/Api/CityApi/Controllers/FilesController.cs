﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityinfoAPI.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        FileExtensionContentTypeProvider fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            this.fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }


        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            string pathToFile = "wwwroot/SIV.rar";

            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            var bytes = System.IO.File.ReadAllBytes(pathToFile);

            if (!fileExtensionContentTypeProvider.TryGetContentType(pathToFile,
                out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}
