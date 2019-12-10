using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace ZPlayerStreamHost.Controllers
{
    public class ZFileController : Controller
    {
        [HttpGet]
        public FileResult Open([FromQuery]string file)
        {
            using (MemoryStream result = new MemoryStream())
            {


                using (FileStream zipToOpen = new FileStream(file, FileMode.Open))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                    {
                        var entry = archive.Entries[0];
                        var stream = entry.

                        stream.CopyTo(result);
                    }
                }

                result.Seek(0, SeekOrigin.Begin);
                return File(result, "audio/mpeg3");
            }
        }
    }
}
