using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DeriveAndStrategyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var afr = new AsyncFileReader();
            afr.BeginExecute( (result) =>
                                  {
                                      Console.WriteLine(result); 
                                      Console.WriteLine("press key to continue");
                                  });
            Console.ReadKey();
        }
    }

    public abstract class AsyncCommandBase
    {
        private IAsyncResult ar;
        private Action<object> onWorkDone;

        public void BeginExecute(Action<object> notifyMeWhenDone)
        {
            this.onWorkDone = notifyMeWhenDone;
            this.ar = this.OnBeginOperation(this.OperationCompleted);
        }

        private void OperationCompleted(IAsyncResult ar)
        {
            object result = this.OnOperationCompleted(ar);
            if (null != this.onWorkDone)
            {
                this.onWorkDone(result);
            }
        }

        protected abstract IAsyncResult OnBeginOperation(AsyncCallback callback);
        protected abstract object OnOperationCompleted(IAsyncResult ar);
    }

    public class AsyncFileReader : AsyncCommandBase
    {
        private FileStream fs;
        private byte[] buffer;

        protected override IAsyncResult OnBeginOperation(AsyncCallback callback)
        {
            this.fs = new FileStream("Content.txt",
                                     FileMode.Open,
                                     FileAccess.Read,
                                     FileShare.Read,
                                     512,
                                     // otworz plik w trybie asynchronicznym!
                                     FileOptions.Asynchronous);
            this.buffer = new byte[fs.Length];
            return fs.BeginRead(buffer, 0, buffer.Length, callback, null);
        }

        protected override object OnOperationCompleted(IAsyncResult ar)
        {
            int bytesRead = this.fs.EndRead(ar);

            try
            {
                // odczyt danych i wydruk na konsole
                using (var ms = new MemoryStream(this.buffer))
                using (var str = new StreamReader(ms))
                {
                    return str.ReadToEnd();
                }
            }
            finally
            {
                this.fs.Dispose();
            }
        }
    }
}
