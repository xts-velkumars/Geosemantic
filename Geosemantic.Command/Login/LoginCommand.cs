using System.Collections.Generic;
using System.Security.Claims;
using Newtonsoft.Json;
using Xen.Command;
using Xen.Common;

namespace Ste.Command.Login
{
    public class LoginCommand : XenCommand<XenResult<IList<Claim>>>
    {
        public string UserName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
