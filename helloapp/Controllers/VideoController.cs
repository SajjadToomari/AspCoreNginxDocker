using Microsoft.AspNetCore.Mvc;

namespace helloapp.Controllers
{
    [Route("[controller]")]
    public class VideoController : Controller
    {
        static readonly object lockObject = new object();
        private static void WriteLog(string message, IWebHostEnvironment hostingEnvironment, string fileName = "ServiceLog.txt", bool AddTime = true)
        {

            try
            {

                if (Monitor.TryEnter(lockObject, 300))
                {
                    try
                    {
                        var path = Path.Combine(hostingEnvironment.WebRootPath, fileName);
                        var sw = new StreamWriter(path, true);
                        if (AddTime)
                        {
                            sw.WriteLine(DateTime.Now + ": " + message);
                        }
                        else
                        {
                            sw.WriteLine(message);
                        }
                        sw.Flush();
                        sw.Close();
                    }
                    finally
                    {
                        Monitor.Exit(lockObject);
                    }
                }
                else
                {
                    // Code to execute if the attempt times out.  
                }

            }
            catch (Exception)
            {
                // ignored
            }
        }

        private IWebHostEnvironment _hostingEnvironment;

        public VideoController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpGet("Get/{video}")]
        public IActionResult Get(string video)
        {
            WriteLog(video, _hostingEnvironment);

            var path = Path.Combine(_hostingEnvironment.WebRootPath, "videos", video);
            var file = new StreamReader(path);
           
            return File(file.BaseStream, "video/mp4");
        }
    }
}
