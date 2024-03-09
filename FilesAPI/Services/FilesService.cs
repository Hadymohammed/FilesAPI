using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilesAPI.Models;
using FilesAPI.Repos;

namespace FilesAPI.Services
{
    public class FilesService
    {
        private readonly FilesRepo _filesRepo;
        public FilesService(FilesRepo filesRepo)
        {
            _filesRepo = filesRepo;
        }
        //upload file
        public async Task<string> UploadFile(IFormFile file)
        {
            //get file name
            var fileName = file.FileName;
            //get file extension
            var fileExtension = Path.GetExtension(fileName);
            //generate new file name
            var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
            //get path to upload file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", newFileName);
            Console.WriteLine(filePath);
            try
            {
                //upload file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                //get api url
                var apiUrl = "https://localhost:5001";
                //save file details to database
                var fileEntity = new FileEntity
                {
                    Title = fileName,
                    FilePath = filePath,
                    Url = $"{apiUrl}/files/{newFileName}",
                    Type = file.ContentType
                };
                await _filesRepo.Add(fileEntity);

                fileEntity.Url = $"{apiUrl}/files/download/{fileEntity.Id}";
                await _filesRepo.Update(fileEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return newFileName;
        }
        //get all files
        public IEnumerable<FileEntity> GetAllFiles()
        {
            return _filesRepo.GetAll();
        }
        //get file by id
        public FileEntity GetFileById(int id)
        {
            return _filesRepo.GetById(id);
        }
        //download file
        public async Task<byte[]> DownloadFile(int id)
        {
            var file = _filesRepo.GetById(id);
            var filePath = file.FilePath;
            var fileName = file.Title;
            byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
            return fileBytes;
        }
        //delete file
        public async Task DeleteFile(int id)
        {
            var file = _filesRepo.GetById(id);
            var filePath = file.FilePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            await _filesRepo.Delete(id);
        }
    }
}