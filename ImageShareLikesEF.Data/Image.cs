using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShareLikesEF.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public DateTime DateUploaded { get; set; }
        public int NumberOfLikes { get; set; }
    }
}
