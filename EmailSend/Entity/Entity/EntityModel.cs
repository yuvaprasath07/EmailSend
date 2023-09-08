using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class EntityModel
    {
        [Key]
        public int Num { get; set; }
        public  string ?Name { get; set; }

        public string ? Emaill { get; set; }
    }
}
