using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataStructures
{
   internal class DoublyLinkedListNode<T>
   {
      public T data;
      public DoublyLinkedListNode<T> next;
      public DoublyLinkedListNode<T> previous;

      public DoublyLinkedListNode(T data)
      {
         this.data = data;
         next = null;
         previous = null;
      }

      public DoublyLinkedListNode(T data, DoublyLinkedListNode<T> prevNode)
      {
         this.data = data;
         previous = prevNode;
         prevNode.next = this;
      }

      public T Data
      {
         get { return data; }
         set { data = value; }
      }

      public DoublyLinkedListNode<T> Next
      {
         get { return next; }
         set { next = value; }
      }

      public DoublyLinkedListNode<T> Previous
      {
         get { return previous; }
         set { previous = value; }
      }
   }

   internal class DoublyLinkedList<T>
   {
      internal int count;

      internal DoublyLinkedListNode<T> head { get; set; } 
      internal DoublyLinkedListNode<T> tail { get; set; } 

      public DoublyLinkedList()
      {
         head = null;
         tail = null;
         count = 0;
      }

      public void AddToEnd(T data)
      {
         if (head == null)
         {
            head = new DoublyLinkedListNode<T>(data);
            tail = head;
         }
         else
         {
            DoublyLinkedListNode<T> newItem = new DoublyLinkedListNode<T>(data, tail);
            tail.Next = newItem;
            newItem.Previous = tail;
            tail = newItem;
         }
         count++;
      }

      public void Insert(T data, int index)
      {
         count++;
         if (index >= count || index < 0)
         {
            throw new ArgumentOutOfRangeException("Out of range!");
         }
         DoublyLinkedListNode<T> newItem = new DoublyLinkedListNode<T>(data);
         int currentIndex = 0;
         DoublyLinkedListNode<T> currentItem = head;
         DoublyLinkedListNode<T> prevItem = null;
         while (currentIndex < index)
         {
            prevItem = currentItem;
            currentItem = currentItem.Next;
            currentIndex++;
         }
         if (index == 0)
         {
            newItem.Previous = head.Previous;
            newItem.Next = head;
            head.Previous = newItem;
            head = newItem;
         }
         else if (index == count - 1)
         {
            newItem.Previous = tail;
            tail.Next = newItem;
            newItem = tail;
         }
         else
         {
            newItem.Next = prevItem.Next;
            prevItem.Next = newItem;
            newItem.Previous = currentItem.Previous;
            currentItem.Previous = newItem;
         }
      }

      public void RemoveByValue(T data)
      {
         int currentIndex = 0;
         DoublyLinkedListNode<T> currentItem = head;
         DoublyLinkedListNode<T> prevItem = null;
         while (currentItem != null)
         {
            if ((currentItem.Data != null &&
            currentItem.Data.Equals(data)) ||
            (currentItem.Data == null) && (data == null))
            {
               break;
            }
            prevItem = currentItem;
            currentItem = currentItem.Next;
            currentIndex++;
         }
         if (currentItem != null)
         {
            count--;
            if (count == 0)
            {
               head = null;
            }
            else if (prevItem == null)
            {
               head = currentItem.Next;
               head.Previous = null;
            }
            else if (currentItem == tail)
            {
               currentItem.Previous.Next = null;
               tail = currentItem.Previous;
            }
            else
            {
               currentItem.Previous.Next = currentItem.Next;
               currentItem.Next.Previous = currentItem.Previous;
            }
         }

      }

      public void RemoveByReference(DoublyLinkedListNode<T> nodeRef)
      {
         if (nodeRef == head)
         {
            if (nodeRef.next != null)
            {
               DoublyLinkedListNode<T> nextNode = nodeRef.Next;
               nextNode.Previous = null;
               head = nextNode;
               count--;
            }
            else
            {
               head = tail = null;
               count--;
            }
         }
         else if (nodeRef != tail)
         {
            DoublyLinkedListNode<T> previousNode, nextNode;

            previousNode = nodeRef.Previous;
            nextNode = nodeRef.Next;

            if(previousNode != null) previousNode.Next = nextNode; 
            if(nextNode != null) nextNode.previous = previousNode; 

            count--; 
         }
         else //is tail
         {
            DoublyLinkedListNode<T> previousNode;

            previousNode = nodeRef.Previous;
            previousNode.Next = null;
            nodeRef.Previous = null;
            tail = previousNode;
            count--;
         }
      }

      public override string ToString()
      {
         StringBuilder sb = new StringBuilder();
         DoublyLinkedListNode<T> tmp = head;

         while (tmp != null)
         {
            sb.AppendLine(tmp.Data.ToString());
            tmp = tmp.Next;
         }
         return sb.ToString();
      }

      //indexer
      public DoublyLinkedListNode<T> this[DoublyLinkedListNode<T> node]
      {
         get
         {
            DoublyLinkedListNode<T> currentNode = this.head;
            
            while (currentNode != null && currentNode != node)
            {
               currentNode = currentNode.Next;
            }

            return currentNode;
         }

      }

      public void RemoveTail()
      {
         if (head == null || head.next == null) RemoveHead();
         else
         {
            DoublyLinkedListNode<T> tmp = tail.Previous;
            tail.Previous = null;
            tmp.next = null;
            tail = tmp;
            count--;
         }
      }

      public void RemoveHead()
      {
         if (count > 0) count--;
         head = head.next;
         
         if (head == null) tail = null;
         else head.Previous = null;
      }

      public void MoveToEnd(DoublyLinkedListNode<T> nodeRef)
      {
         if (nodeRef == head)
         {
            if (nodeRef.next != null)
            {
               DoublyLinkedListNode<T> nextNode = nodeRef.Next;
               nextNode.Previous = null;
               head = nextNode;
               count--; //because AddToEnd performs count++
               AddToEnd(nodeRef.Data);
            }
         }
         else if (nodeRef != tail)
         {
            DoublyLinkedListNode<T> previousNode, nextNode;

            previousNode = nodeRef.Previous;
            nextNode = nodeRef.Next;

            previousNode.Next = nextNode; 
            nextNode.previous = previousNode; 

            count--; //because AddToEnd performs count++
            AddToEnd(nodeRef.Data);
         }
      }
   }
}
