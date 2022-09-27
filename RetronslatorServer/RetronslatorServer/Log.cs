using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetronslatorServer
{
    internal class Log
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public void LogWriter(string log)
        {
            switch (log[0])
            {
                case 'E':
                    {
                        logger.Error(log);
                        break;
                    }
                case 'W':
                    {
                        logger.Warn(log);
                        break;
                    }
                case 'F':
                    {
                        logger.Fatal(log);
                        break;
                    }
                case 'I':
                    {
                        logger.Info(log);
                        break;
                    }
                default:
                    break;
            }
        }
        public void LogWriter(char Key, string log)
        {
            switch (Key)
            {
                case 'E':
                    {
                        logger.Error(log);
                        break;
                    }
                case 'W':
                    {
                        logger.Warn(log);
                        break;
                    }
                case 'F':
                    {
                        logger.Fatal(log);
                        break;
                    }
                case 'I':
                    {
                        logger.Info(log);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
