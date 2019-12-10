using Microsoft.AspNetCore.Mvc;
using MimeMapping;
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
            MemoryStream result = new MemoryStream();
            string mime = null;

            using (FileStream zipToOpen = new FileStream(file, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                {
                    var entry = archive.Entries[0];
                    mime = MimeUtility.GetMimeMapping(entry.FullName);
                    var stream = entry.Open();

                    stream.CopyTo(result);
                }
            }

            result.Seek(0, SeekOrigin.Begin);
            return File(result, mime);
        }
    }
}
