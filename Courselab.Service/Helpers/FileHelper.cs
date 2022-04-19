using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Courselab.Service.Helpers
{
    public class FileHelper
    {
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;
        public FileHelper(IConfiguration config, IWebHostEnvironment env)
        {
            this.config = config;
            this.env = env;
        }

        public async Task<string> SaveFileAsync(Stream file, string fileName, string section)
        {
            // Provideing names for file and storage
            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;
            string storagePath = config.GetSection(section).Value;
            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");

            // Creating stream with given path to copy file from input 
            FileStream mainFile = File.Create(filePath);
            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return fileName;
        }
    }
}
