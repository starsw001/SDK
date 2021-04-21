using Synctool.HttpFramework;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace LightNovel.SDK
{
    public class Class1
    {
        //https://github.com/0x7FFFFF/wenku8downloader/blob/master/src/user.py
        private const string Host = "https://www.wenku8.net/login.php";

        public Class1()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public void T()
        {
            var json = @"{'username': kilydoll365, 'password': sion8550, 'usercookie': 315360000, 'action': 'login',
                      'submit': '%26%23160%3B%B5%C7%26%23160%3B%26%23160%3B%C2%BC%26%23160%3B'}";
            var bytes = HttpMultiClient.HttpMulti.AddNode(Host,json,RequestType.POST).Build().RunBytes().FirstOrDefault();
            using StreamReader reader = new StreamReader(new MemoryStream(bytes), Encoding.GetEncoding("GBK"));
            var xx = reader.ReadToEnd();
            Console.WriteLine(xx);
        }
    }
}
