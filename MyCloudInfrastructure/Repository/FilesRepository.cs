using Microsoft.EntityFrameworkCore;
using MyCloudDomain.Files;
using MyCloudDomain.Interfaces;
using Npgsql;

namespace MyCloudInfrastructure.Repository
{
    public class FilesRepository(MyDbContext myDbContext) : IFilesRepository
    {
        private readonly MyDbContext _myDbContext = myDbContext;
        public async Task<List<FileRecordEntity>> GetGroupFiles(int groupId)
        {
            try
            {
                return await _myDbContext.files
                    .Where(file => file.GroupId == groupId)
                    .ToListAsync();
            }
            catch
            {
                throw new Exception($"Eroare la obtinerea fisierelor pentru grupul: {groupId}");
            }
        }

        public async Task<List<FileRecordEntity>> GetUserFiles(int userId)
        {
            try
            {
                return await _myDbContext.files
                    .Where(file => file.UserId == userId)
                    .ToListAsync();
            }
            catch
            {
                throw new Exception($"Eroare la obtinerea fisierelor pentru utilizatorul: {userId}");
            }
        }

        public async Task UploadFile(FileRecordEntity fileRecord)
        {
            try
            {
                _myDbContext.files.Add(fileRecord);
                await _myDbContext.SaveChangesAsync();
            }
            catch(PostgresException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch
            {
                throw new Exception($"Eroare la salvare fisier in baza de date: Fisier: {fileRecord.FileName} \n Utilizator: {fileRecord.UserId}");
            }
        }
    }
}
