namespace Quizzario.Data.Abstracts
{
    public interface IJSONRepository
    {
        void Save(string filename, string json);
        string Load(string filename);
        void SaveWithAbsolutePath(string filepath, string json);
        string LoadWithAbsolutePath(string filepath);
        string BuildAbsolutePath(string filename);
    }
}