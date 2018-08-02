using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepeaterDemo.Models {
    public class SampleData {
        public SampleData(int value, int max) {
            value = Math.Max(value, 50);

            Value = value;
            Max = max;

            Size = value / 6;
            MaxSize = max / 6;
        }
        public int Value { get; set; }
        public int Max { get; set; }

        public double Size { get; set; }
        public double MaxSize { get; set; }
    }
}
