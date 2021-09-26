using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataValue
{
   internal class TimeData
   {
      //data members
      internal double xSizeVal;
      internal double ySizeVal;

      //properties
      public DateTime LastOperationDate { get; set; }

      //c'tor
      public TimeData(DateTime lastOper, double xSize, double ySize)
      {
         LastOperationDate = lastOper;
         xSizeVal = xSize;
         ySizeVal = ySize;
      }
   }
}
