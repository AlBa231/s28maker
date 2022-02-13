using System;
using System.Collections.Generic;
using System.Text;

namespace S28Maker.Models
{
    public interface IS28FieldRow
    {
        public string Name { get; }
        public string Description { get; }
        public string Value { get; set; }
        public string PreviousValue { get; }
        public string ReceivedValue { get; set; }

        public string CalculatedValue { get; }

        void CopyFrom(IS28FieldRow itemFrom);
    }
}
