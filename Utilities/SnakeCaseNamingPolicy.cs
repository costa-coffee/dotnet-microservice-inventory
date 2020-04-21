using System.Text.Json;

namespace Inventory
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) =>
            name.ToSnakeCase();
    }

}
