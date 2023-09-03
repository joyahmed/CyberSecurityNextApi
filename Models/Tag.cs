using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSecurityNextApi.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string TagName { get; set; } = string.Empty;

        public int PostId { get; set; }
        public Post Post { get; set; }

        public Tag()
        {
            Post = new Post();
        }
    }
}