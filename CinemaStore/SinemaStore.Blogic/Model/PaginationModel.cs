using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Blogic.Model
{
    public interface IPagination
    {
        int CountOnPage { get; set; }
        int TotalItems { get; set; }
        int Page { get; set; }
        int Total { get; set; }
    }
    public class PaginationModel
    {
        public PaginationModel()
        {
            CountOnPage = 8;
            Page = 1;
        }
        public int CountOnPage { get; set; }
        public int TotalItems { get; set; }//total elements from  data
        public int Page { get; set; } //pages
        public int Total
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / CountOnPage); }
        }

        public int[] CountOnPageArray
        {
            get
            {
                return new int[] { 12, 24, 48, 96 };
            }
        }
    }
}
