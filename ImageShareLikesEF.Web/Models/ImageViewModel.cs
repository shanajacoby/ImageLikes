using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageShareLikesEF.Data;

namespace ImageShareLikesEF.Web.Models
{
    public class ImageViewModel
    {
        public Image Image { get; set; }
        public List<int> ImagesViewed { get; set; }
    }
}
