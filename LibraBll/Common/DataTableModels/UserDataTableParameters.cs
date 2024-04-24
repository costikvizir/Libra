using System;

namespace LibraBll.Common.DataTableModels
{
    public class UserDataTableParameters : DataTablesParameters
    {
        public string UserName { get; set; }
        public DateTime? UploadDate { get; set; }
        //public Comparator UploadProblemDateComparator { get; set; }
    }
}