namespace Api.Services;

public class BufferUploadService
{
    private readonly string _dir = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Media"));
    public async Task<bool> Upload(IFormFile file)
    {
        if (file.Length > 0) {
            try {
                if (!Directory.Exists(_dir))
                    Directory.CreateDirectory(_dir);

                var path = Path.Combine(_dir, file.FileName);
                using (var fileStream = new FileStream(path, FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }
        return false;
    }
}