using Shared.Interfaces;

namespace Server.Implementations
{
    public class MenuService
        : IMenuService
    {
        MenuClass[] allExamples = new[]
        {
            new MenuClass()
            {
                Name = "Home",
                Path = "/",
                Icon = "&#xe88a"
            },
            new MenuClass()
            {
                Name = "counter",
                Path = "/counter",
                Icon = "&#xe88a"
            },
            new MenuClass()
            {
                Name = "fetchdata",
                Path = "/fetchdata",
                Icon = "&#xe88a"
            }
        };

        public IEnumerable<MenuClass> Examples
        {
            get { return allExamples; }
        }

        public IEnumerable<MenuClass> Filter(string term)
        {
            if (string.IsNullOrEmpty(term))
                return allExamples;


            return Examples.Where(category => category.Children?.Any(deepFilter) == true)
                .Select(category => new MenuClass
                {
                    Name = category.Name,
                    Expanded = true,
                    Children = category.Children.Where(deepFilter).Select(example => new MenuClass
                        {
                            Name = example.Name,
                            Path = example.Path,
                            Icon = example.Icon,
                            Expanded = true,
                            Children = example.Children
                        }
                    ).ToArray()
                }).ToList();
        }


        public bool contains(string value)
        {
            return value.Contains(value, StringComparison.OrdinalIgnoreCase);
        }

        public bool filter(MenuClass example)
        {
            return contains(example.Name) || (example.Tags != null && example.Tags.Any(contains));
        }

        public bool deepFilter(MenuClass example)
        {
            return filter(example) || example.Children?.Any(filter) == true;
        }
    }
}
