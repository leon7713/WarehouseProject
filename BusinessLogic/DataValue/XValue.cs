using BusinessLogic.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataValue
{
   internal class XValue : IComparable<XValue>
   {
      //data members
      internal double _x;
      private LinkedList<YValue> _yValuesList;

      //c'tors
      public XValue(double x, YValue yValue)
      {
         _yValuesList = new LinkedList<YValue>();
         _x = x;
         _yValuesList.AddFirst(yValue);
      }

      public XValue(double x) //dummy x
      {
         _x = x;
      }

      //methods
      internal bool CheckYList(double y)
      {
         foreach (var item in _yValuesList)
         {
            if (y == item._y) return true;
         }
         return false;
      }

      internal YValue GetYValue(double y)
      {
         foreach (var item in _yValuesList)
         {
            if (y == item._y) return item;
         }
         return null;
      }

      internal YValue GetClosestY(double y)
      {
         YValue res = null;

         foreach (var item in _yValuesList)
         {
            if (y == item._y) return item;
            if (item._y > y)
            {
               if (res == null) res = item;
               else if (item._y < res._y) res = item;
            }
         }

         return res;
      }

      internal LinkedList<YValue> GetAllYValues()
      {
         return _yValuesList;
      }

      internal void RemoveYValue(YValue yValue)
      {
         _yValuesList.Remove(yValue);
      }

      internal void AddListMember(YValue yValue)
      {
         _yValuesList.AddFirst(yValue);
      }

      public int CompareTo(XValue other)
      {
         return _x.CompareTo(other._x);
      }
   }
}
