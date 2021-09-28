using System;
using System.Collections.Generic;
using System.Text;

namespace Platonic_Reader
{
    class MasterPageItem
    {
        public string Title { get; set; }

        public string FirstLetter { get; set; }

        public string IconSource { get; set; }
        
        public int Book { get; set; }

        public Type TargetType { get; set; }
    }


}
