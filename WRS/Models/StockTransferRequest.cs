using System.Text.Json.Serialization;

namespace WRS
{
    public class StockTransferRequest
    {
        [JsonPropertyNameAttribute("storeid")]
        public string Store { get; set; }
        public StockTransferItem[] Items { get; set; }
    }

    public class StockTransferItem
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
        public string[] Modifiers { get; set; }
    }
}
