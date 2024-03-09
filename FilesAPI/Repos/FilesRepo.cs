using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilesAPI.AppDbContext;
using FilesAPI.Models;

namespace FilesAPI.Repos
{
    public class FilesRepo
    {
        private readonly ApplicationDbContext _context;
        public FilesRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        //add
        public async Task Add(FileEntity file)
        {
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();
        }
        //get all
        public IEnumerable<FileEntity> GetAll()
        {
            return _context.Files.ToList();
        }
        //get by id
        public FileEntity GetById(int id)
        {
            return _context.Files.FirstOrDefault(x => x.Id == id);
        }
        //delete
        public async Task Delete(int id)
        {
            var file = GetById(id);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }
        //update
        public async Task Update(FileEntity file)
        {
            _context.Files.Update(file);
            await _context.SaveChangesAsync();
        }
    }
}