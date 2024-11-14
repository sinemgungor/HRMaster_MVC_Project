namespace HRMaster_MVC_Project.Areas.CEOPanel.Models
{
    public static class MimeMapping
    {
        private static readonly Dictionary<string, string> MimeMappings = new Dictionary<string, string>
    {
        { ".pdf", "application/pdf" },
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png", "image/png" },
        // Diğer dosya uzantıları ve MIME türleri burada listelenebilir
    };

        public static string GetMimeMapping(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return MimeMappings.TryGetValue(ext, out var mime) ? mime : "application/octet-stream";
        }
    }
}
