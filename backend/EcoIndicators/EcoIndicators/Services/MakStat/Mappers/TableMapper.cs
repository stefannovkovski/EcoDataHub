namespace EcoIndicators.Services.MakStat.Mappers {
    using System.Reflection;

    public static class TableMapper {
        public static object Map<T>(List<T> data) {
            if (!data.Any())
                return new { columns = Array.Empty<string>(), rows = Array.Empty<object>() };

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var columns = props.Select(p => p.Name).ToList();

            var rows = data.Select(item =>
            {
                var dict = new Dictionary<string, object?>();
                foreach (var prop in props) {
                    dict[prop.Name] = prop.GetValue(item);
                }
                return dict;
            });

            return new {
                columns,
                rows
            };
        }
    }

}
