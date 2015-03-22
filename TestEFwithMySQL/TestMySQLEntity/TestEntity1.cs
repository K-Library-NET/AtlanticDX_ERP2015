using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMySQLEntity
{
    [Table("t_entity1")]
    public class TestEntity1
    {
        public int TestEntity1Id
        {
            get;
            set;
        }

        public string Property1
        {
            get;
            set;
        }

        public string Property2
        {
            get;
            set;
        }

        public string Property3
        {
            get;
            set;
        }
    }
}
