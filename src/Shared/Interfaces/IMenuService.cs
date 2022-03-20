namespace Shared.Interfaces
{
    public class MenuClass
    {
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? Path { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool Expanded { get; set; }
        public IEnumerable<MenuClass>? Children { get; set; }
        public IEnumerable<string>? Tags { get; set; }
    }

    public interface IMenuService
    {
        IEnumerable<MenuClass> Examples { get; }
        IEnumerable<MenuClass> Filter(string term);

    }
}
