using System.Collections.Generic;

namespace eHallTools
{
    public class ServerList
    {
        public List<Server> Info { get; set; }
    }

    public class Server
    {
        public string University { get; set; }
        public string AuthserverHttp { get; set; }
        public string EhallHttp { get; set; }
    }

}
