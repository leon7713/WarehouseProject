using BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace WarehouseProject
{
   public sealed partial class MainPage : Page
   {
      //data members
      public static Manager mng;
      private DispatcherTimer removalTimer;

      //c'tor
      public MainPage()
      {
         this.InitializeComponent();
         removalTimer = new DispatcherTimer();
         removalTimer.Interval = new TimeSpan(0, 0, 0, 120, 0); //removal timer speed - X seconds
         removalTimer.Tick += timer_TickRemove; 
         removalTimer.Start(); 
      }

      //static c'tor
      static MainPage()
      {
         mng = new Manager();
      }

      //methods
      private void timer_TickRemove(object sender, object e)
      {
         StatusTB.Text = string.Empty;
         MainTB.Text = string.Empty;
         XTB.Background = new SolidColorBrush(Colors.White);
         YTB.Background = new SolidColorBrush(Colors.White);
         AmountTB.Background = new SolidColorBrush(Colors.White);

         if (mng.RemoveOldBoxes())
         {
            StatusTB.Text = "Old boxes have been removed!";
            XTB.Text = string.Empty;
            YTB.Text = string.Empty;
         }
      }

      private void EnterBtn_Click(object sender, RoutedEventArgs e)
      {
         StatusTB.Text = string.Empty;
         MainTB.Text = string.Empty;
         XTB.Background = new SolidColorBrush(Colors.White);
         YTB.Background = new SolidColorBrush(Colors.White);
         AmountTB.Background = new SolidColorBrush(Colors.White);

         if (XTB.Text.Equals(string.Empty)) XTB.Background = new SolidColorBrush(Colors.Red);
         else if (YTB.Text.Equals(string.Empty)) YTB.Background = new SolidColorBrush(Colors.Red);
         else if (AmountTB.Text.Equals(string.Empty)) AmountTB.Background = new SolidColorBrush(Colors.Red);
         else
         {
            double x = double.Parse(XTB.Text);
            double y = double.Parse(YTB.Text);
            int amount = int.Parse(AmountTB.Text);

            int returnedBoxes = mng.Add(x, y, amount);
            if (returnedBoxes != 0) StatusTB.Text = $"Not all the boxes were accepted! Returned boxes: {returnedBoxes}";

            XTB.Text = string.Empty;
            YTB.Text = string.Empty;
            AmountTB.Text = string.Empty;
         }
      }

      private void FindBtn_Click(object sender, RoutedEventArgs e)
      {
         StatusTB.Text = string.Empty;
         MainTB.Text = string.Empty;
         XTB.Background = new SolidColorBrush(Colors.White);
         YTB.Background = new SolidColorBrush(Colors.White);
         AmountTB.Background = new SolidColorBrush(Colors.White);

         if (XTB.Text.Equals(string.Empty)) XTB.Background = new SolidColorBrush(Colors.Red);
         else if (YTB.Text.Equals(string.Empty)) YTB.Background = new SolidColorBrush(Colors.Red);
         else
         {
            double x = double.Parse(XTB.Text);
            double y = double.Parse(YTB.Text);

            if (mng.GetData(x, y) != null) MainTB.Text = mng.GetData(x, y);
            else StatusTB.Text = "There are no boxes with this size!";

            XTB.Text = string.Empty;
            YTB.Text = string.Empty;
            AmountTB.Text = string.Empty;
         }
      }

      private void FindPresentBtn_Click(object sender, RoutedEventArgs e)
      {
         StatusTB.Text = string.Empty;
         MainTB.Text = string.Empty;
         XTB.Background = new SolidColorBrush(Colors.White);
         YTB.Background = new SolidColorBrush(Colors.White);
         AmountTB.Background = new SolidColorBrush(Colors.White);

         if (XTB.Text.Equals(string.Empty)) XTB.Background = new SolidColorBrush(Colors.Red);
         else if (YTB.Text.Equals(string.Empty)) YTB.Background = new SolidColorBrush(Colors.Red);
         else
         {
            double x = double.Parse(XTB.Text);
            double y = double.Parse(YTB.Text);

            if (mng.FindPresentBox(x, y, out double xRes, out double yRes, out bool refillStatus))
            {
               StatusTB.Text = $"The present box has been found! X size: {xRes}, Y size: {yRes}";
               if (refillStatus) StatusTB.Text += "\n\nAlert! Needs to be refilled!";
               if (mng.isLastBox) StatusTB.Text += "\n\nAlert! It was the last box of this size!";
            }
            else StatusTB.Text = "There is no a correct size, sorry!";
           
            XTB.Text = string.Empty;
            YTB.Text = string.Empty;
            AmountTB.Text = string.Empty;
         }
      }
   }
}
