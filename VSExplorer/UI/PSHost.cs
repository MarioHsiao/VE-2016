
namespace WinExplorer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    
    using System.Text;
    
    using System.Threading.Tasks;
    using System.Collections;
    

    public class NavigatorExt
    {
        public ArrayList N { get; set; }

        public int act = -1;

        public int maxsize = 10;

        public NavigatorExt()
        {
            N = new ArrayList();
        }

        public void Add(string c)
        {
            N.Add(c);
            act++;
            if (act > maxsize)
            {
                N.RemoveAt(0);
                act--;
            }
        }

        public string Prev()
        {
            if (N == null)
                return "";
            if (act <= 0)
                return "";

            act--;

            string c = N[act] as string;

            return c;
        }
        public string Next()
        {
            if (N == null)
                return "";
            if (act <= 0)
                return "";

            act++;

            if (act >= N.Count)
                act = N.Count - 1;

            if (act < 0)
                act = 0;

            string s = N[act] as string;

            return s;
        }
    }
}