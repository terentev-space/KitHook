namespace KitHook.Services.ConfigService.Interfaces
{
    public interface IConfig
    {
        public string GetPath();
        public void FillDefault();
        public bool IsValid();
    }
}