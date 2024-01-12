using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanStyleGroomers.Model
{
    public class UsersInsert
    {
        public string id { get; set; }
        public string group_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string calling_code { get; set; }
        public string mobile { get; set; }
        public string mobile_verified { get; set; }
        public string password { get; set; }
        public string image { get; set; }
        public string remember_token { get; set; }
    }
}
