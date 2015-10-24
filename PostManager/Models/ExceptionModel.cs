using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostManager.Models
{
    public class ExceptionModel
    {
        public int ExId { get; set; }

        public DateTime ExDate { get; set; }

        public string ExMessage { get; set; }

        public string StackTrace { get; set; }

        public string PostLink { get; set; }

        public Int32 SourceId { get; set; }
    }
}