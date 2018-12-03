using System.Collections.Generic;

namespace Ste.ViewModel
{
    public class UploadDataViewModel
    {
        public List<KeyValuePair<string, string>> FormData { get; set; }
        public FileViewModel FileViewModel { get; set; }
    }
}
