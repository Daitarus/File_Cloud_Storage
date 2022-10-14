using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public enum TypeMessage : byte
    {
        UNKNOW = 0,
        GET_FILES_LIST = 1,
        GET_FILE = 2,
        ADD_FILE = 3,
        DELETE_FILE = 4
    }
}
