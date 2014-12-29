using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZooKeeperNet;

namespace ZookeeperDemo
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            ZooKeeper _zk;
            _zk = new ZooKeeper("127.0.0.1:2181", TimeSpan.FromSeconds(5), new Watcher());
       
                var stat = _zk.Exists("/DemoV1", true);

                byte[] buff = System.Text.Encoding.Default.GetBytes(DateTime.Now.ToString("HH:mm:ss"));
                byte[] getbuf;
                string getstr;


                if (_zk.State == ZooKeeper.States.CONNECTED)
                {

                    var _exists = _zk.Exists(@"/DemoV1", false);

                    if (_exists == null)
                    {
                        _zk.Create(@"/DemoV1", buff, Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
                    }

                    _exists = _zk.Exists(@"/DemoV1/RTData", false);

                    if (_exists == null)
                    {
                        _zk.Create(@"/DemoV1/RTData", buff, Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
                    }
                }
                else {
                    Console.WriteLine("没有连接上");
                }

                while (true) {

                    buff = System.Text.Encoding.Default.GetBytes(DateTime.Now.ToString("HH:mm:ss"));
                    _zk.SetData("/DemoV1/RTData", buff, -1);
                    Thread.Sleep(1000);


                    getbuf = _zk.GetData("/DemoV1/RTData", true, null);
                    getstr = "begin@ " + System.Text.Encoding.Default.GetString(getbuf) + "  @end";
                    Console.WriteLine(getstr);

                   
                }
                
            
        }
    }
}
