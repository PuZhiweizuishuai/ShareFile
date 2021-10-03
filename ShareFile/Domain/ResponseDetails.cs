using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Domain
{
    public class ResponseDetails : Hashtable
    {
        private ResponseDetails()
        {
            Add("status", 200);
            Add("message", "success!");
            Add("timestamp", DateTime.Now);
        }

        public static ResponseDetails Ok()
        {
            return new ResponseDetails();
        }

        public static ResponseDetails Ok(int code, string msg)
        {
            ResponseDetails r = Ok();
            r.Add("status", code);
            r.Add("message", msg);
            return r;
        }

        public static ResponseDetails Ok(String msg)
        {
            ResponseDetails r = Ok();
            r.Add("message", msg);
            return r;
        }


        public ResponseDetails Add(string key, object value)
        {
            if (base.Contains(key))
            {
                base.Remove(key);
            }
            base.Add(key, value);
            return this;
        }
    }
}
