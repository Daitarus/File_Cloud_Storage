using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetronslatorServer
{
    public enum TypeMessage : byte
    {
        UNKNOW = 0,
        GET_FILES_LIST = 1,
        GET_FILE = 2,
        SEND_FILE = 3
    }
}
