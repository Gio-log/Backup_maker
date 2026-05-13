namespace Backup_Maker.Models
{
    class Backup : IFileEntry
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Extension { get; set; }
    }
}
