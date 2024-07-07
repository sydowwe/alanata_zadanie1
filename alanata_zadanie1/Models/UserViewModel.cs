using System.Collections.Generic;

namespace alanata_zadanie1.Models
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        public int NumOfPages { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public string OrderBy { get; set; }
        public string OrderByDirection { get; set; }
        public int[] PerPageOptions { get; set; }
        public List<string> OrderByOptions { get; set; }
        public string[] OrderByDirOptions { get; set; }
    }
}