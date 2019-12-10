using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace ZPlayerStreamServer.Controllers
{
    public class ZFileController : Controller
    {
        [HttpGet]
        public FileResult Open([FromQuery]string file)
        {
            using (FileStream zipToOpen = new FileStream(file, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                {
                    return this.File(archive.Entries[0].Open(), "audio/x-wav ");
                }
            }
        }
    }
}
