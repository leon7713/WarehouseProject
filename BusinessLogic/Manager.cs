using BusinessLogic.DataStructures;
using BusinessLogic.DataValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
   public class Manager
   {
      //data members
      private BST<XValue> _mainTree;
      internal DoublyLinkedList<TimeData> _datesList;
      DateTime removeDate;
      private int percentBorder;

      //properties
      public bool isLastBox { get; set; }

      //c'tor
      public Manager()
      {
         _mainTree = new BST<XValue>();
         _datesList = new DoublyLinkedList<TimeData>();
         removeDate = DateTime.Now + new TimeSpan(72, 0, 0); //3 days ago
         percentBorder = 30; //Search with this limitation in percents (border)!!!
      }

      //methods
      public int Add(double x, double y, int count)
      {
         XValue xValueDummy = new XValue(x);
         int returnedBoxesValue;

         if (_mainTree.Search(xValueDummy, out XValue xValueRes)) //x is matching
         {
            if (xValueRes.GetYValue(y) != null) //y is matching
            {
               returnedBoxesValue = xValueRes.GetYValue(y).UpdateAndCheckCount(count);
            }
            else //y doesn't match
            {
               _datesList.AddToEnd(new TimeData(DateTime.Now, x, y));
               YValue yValue = new YValue(y, count, _datesList.tail);
               xValueRes.AddListMember(yValue);
               returnedBoxesValue = yValue.returnedBoxesVal;
            }
         }
         else //x doesn't match
         {
            _datesList.AddToEnd(new TimeData(DateTime.Now, x, y));
            YValue yValue = new YValue(y, count, _datesList.tail);
            XValue xValue = new XValue(x, yValue);
            _mainTree.Add(xValue);
            returnedBoxesValue = yValue.returnedBoxesVal;
         }

         return returnedBoxesValue;
      }

      public string GetData(double x, double y) 
      {
         XValue xValueDummy = new XValue(x);

         if (_mainTree.Search(xValueDummy, out XValue xValueRes))
         {
            YValue yValueRes = xValueRes.GetYValue(y);

            if (yValueRes != null) return $"Count: {yValueRes.Count}, last purchase date: {yValueRes.listNodeRef.Data.LastOperationDate}";
            else return null;
         }
         else return null;
      }

      public bool FindPresentBox(double x, double y, out double xRes, out double yRes, out bool refillStatus)
      {
         XValue xValueDummy;
         xRes = x;
         yRes = y;
         refillStatus = false;
         isLastBox = false;
         double borderY = (y + ((y * percentBorder) / 100));
         double borderX = (x + ((x * percentBorder) / 100));

         for (double i = x ; i < borderX; i += 0.1) //percent border
         {
            xValueDummy = new XValue(i);
            _mainTree.FindBestMatch(xValueDummy, out XValue xValueFoundItem);

            if (xValueFoundItem != null && xValueFoundItem._x <= borderX)
            {
               YValue yValue = xValueFoundItem.GetClosestY(y);
               if (yValue != null && yValue._y <= borderY) //found y
               {
                  xRes = xValueFoundItem._x;
                  yRes = yValue._y;
                  yValue.Count--;
                  if (yValue.Count == 0) DeleteUnnecessaryData(xValueFoundItem, yValue);
                  else
                  {
                     _datesList.MoveToEnd(yValue.listNodeRef);
                     yValue.listNodeRef.Data.LastOperationDate = DateTime.Now;
                  }
                  if (yValue.Count <= YValue.refillValue) refillStatus = true;
                  return true;
               }
            }
            else return false;
         }
         return false;
      }


      private void DeleteUnnecessaryData(XValue xValue, YValue yValue)
      {
         if (yValue.Count == 0)
         {
            _datesList.RemoveByReference(yValue.listNodeRef);
            xValue.RemoveYValue(yValue);
            isLastBox = true;
         }
         if (xValue.GetAllYValues().Count == 0) //last x size
         {
            _mainTree.Remove(xValue, out XValue removedXValiey);
            isLastBox = true;
         }
      }

      public bool RemoveOldBoxes()
      {
         if (_datesList.head != null)
         {
            if (_datesList.head.Data.LastOperationDate < removeDate)
            {
               YValue yValue = new YValue(_datesList.head.Data.ySizeVal);
               XValue xValue = new XValue(_datesList.head.Data.xSizeVal, yValue);

               _mainTree.Search(xValue, out XValue xValueRes);
               xValueRes.RemoveYValue(xValueRes.GetYValue(yValue._y));
               if (xValueRes.GetAllYValues().Count == 0) //last x size
               {
                  _mainTree.Remove(xValueRes, out XValue removedXValiey);
               }

               _datesList.RemoveHead();
               return true;
            } 
         }
         return false;
      }
   }
}
