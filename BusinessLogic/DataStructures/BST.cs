using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataStructures
{
   internal class BST<T> where T : IComparable<T>
   {
      Node root;

      public void Add(T item)
      {
         if (root == null)
         {
            root = new Node(item);
            return;
         }
         Node tmp = root;
         Node parent = null;

         while (tmp != null)
         {
            parent = tmp;
            if (item.CompareTo(tmp.data) < 0) tmp = tmp.left;
            else tmp = tmp.right;
         }

         if (item.CompareTo(parent.data) < 0) parent.left = new Node(item);
         else parent.right = new Node(item);
      }

      public bool Remove(T itemToRemove, out T removedItem)
      {
         Node tmp = root;
         Node parent = tmp;

         while (tmp != null)
         {
            int cmp = itemToRemove.CompareTo(tmp.data);
            if (cmp == 0)
            {
               if (tmp.left == null && tmp.right == null) //leaf (no children)
               {
                  removedItem = tmp.data;
                  if (parent.left == tmp) parent.left = null;
                  else parent.right = null;
                  if (tmp == root) root = null; 
                  return true;
               }
               else if (tmp.left == null || tmp.right == null) //has one child
               {
                  removedItem = tmp.data;
                  if (parent == tmp) //test!!!
                  {
                     if (tmp.left == null) root = tmp.right;
                     else root = tmp.left;
                     break;
                  }
                  if (parent.left == tmp)
                  {
                     if (tmp.left == null) parent.left = tmp.right;
                     else parent.left = tmp.left;
                  }
                  else
                  {
                     if (tmp.left == null) parent.right = tmp.right;
                     else parent.right = tmp.left;
                  }
                  return true;
               }
               else if (tmp.left != null && tmp.right != null) //has two children
               {
                  Node current = tmp;
                  removedItem = current.data;
                  Node parentTwo = current;
                  tmp = tmp.right;
                  if (tmp.left == null)
                  {
                     if (tmp.right == null)
                     {
                        current.data = tmp.data;
                        current.right = null;
                        return true;
                     }
                     current.data = tmp.data;
                     current.right = tmp.right;
                     return true;
                  }
                  while (tmp.left != null)
                  {
                     parentTwo = tmp;
                     tmp = tmp.left;
                  }

                  current.data = tmp.data;
                  if (tmp.right != null) parentTwo.left = tmp.right;
                  else parentTwo.left = null;

                  return true;
               }
            }

            parent = tmp;
            if (cmp < 0) tmp = tmp.left;
            else tmp = tmp.right;
         }

         removedItem = default(T);
         return false;
      }

      public bool Search(T itemToSearch, out T foundItem)
      {
         Node tmp = root;
         while (tmp != null)
         {
            int cmp = itemToSearch.CompareTo(tmp.data);
            if (cmp == 0)
            {
               foundItem = tmp.data;
               return true;
            }
            if (cmp < 0) tmp = tmp.left;
            else tmp = tmp.right;
         }

         foundItem = default(T);
         return false;
      }

      public void FindBestMatch(T itemToSearch, out T foundItem)
      {
         Node tmp = root;
         Node lastBiggest = null;
         foundItem = default(T);

         while (tmp != null)
         {
            int cmp = itemToSearch.CompareTo(tmp.data);
            if (cmp == 0)
            {
               foundItem = tmp.data;
               return;
            }
            if (cmp < 0)
            {
               lastBiggest = tmp;
               tmp = tmp.left;
            }
            else tmp = tmp.right;
         }

         if (lastBiggest != null) foundItem = lastBiggest.data;
      }

      public void PrintInOrder()
      {
         PrintInOrder(root);
      }

      private void PrintInOrder(Node tmp)
      {
         if (tmp != null)
         {
            PrintInOrder(tmp.left);
            Console.WriteLine(tmp.data);
            PrintInOrder(tmp.right);
         }
      }

      public IEnumerable<T> InOrder()
      {
         return PrivateScanInOrder(root);
      }

      private IEnumerable<T> PrivateScanInOrder(Node root)
      {
         if (root != null)
         {
            if (root.left != null)
            {
               foreach (var item in PrivateScanInOrder(root.left))
               {
                  yield return item;
               }
            }

            yield return root.data;

            if (root.right != null)
            {
               foreach (var item in PrivateScanInOrder(root.right))
               {
                  yield return item;
               }
            }
         }
      }

      class Node
      {
         public Node left;
         public Node right;
         public T data;

         public Node(T data)
         {
            this.data = data;
            left = right = null;
         }
      }

   }
}
