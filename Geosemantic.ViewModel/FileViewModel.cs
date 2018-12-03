using Newtonsoft.Json;

namespace Geosemantic.ViewModel
{
    public class FileViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public long Length { get; set; }

        [JsonIgnore]
        public byte[] Data { get; set; }
    }
}
