namespace Quizzario.Data.Abstracts
{
    public interface IJSONRepository
    {
        void Save(string filename, string json);
        string Load(string filename);
    }
}