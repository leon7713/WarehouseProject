using BusinessLogic.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataValue
{
   internal class YValue : IComparable<YValue>
   {
      //data members
      internal double _y;
      private static int _maxCount = 10;
      internal static int refillValue = 2;
      internal int returnedBoxesVal = 0;
      internal DoublyLinkedListNode<TimeData> listNodeRef;

      //properties
      public int Count { get; set; }

      //c'tors
      public YValue(double y, int count, DoublyLinkedListNode<TimeData> nodeRef)
      {
         _y = y;
         UpdateAndCheckCount(count);
         listNodeRef = nodeRef;
      }

      public YValue(double y) //dummy
      {
         _y = y;
      }

      //methods
      internal int UpdateAndCheckCount(int count)
      {
         if (Count + count >= _maxCount)
         {
            returnedBoxesVal = (Count + count) - _maxCount;
            Count = _maxCount;
         }
         else Count += count;

         return returnedBoxesVal;
      }

      internal bool CheckRefillStatus()
      {
         return Count <= refillValue;
      }

      public int CompareTo(YValue other)
      {
         return _y.CompareTo(other._y);
      }
   }
}
