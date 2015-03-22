using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TestEntity3
{
    [Table("Tb_Core_Config")]
    public class CoreConfig
    {
        public int CoreConfigId
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

        public string Comments
        {
            get;
            set;
        }

        public int CommentId
        {
            get;
            set;
        }

        public string CommentName
        {
            get;
            set;
        }
    }
}
