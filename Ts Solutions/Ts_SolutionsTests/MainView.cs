using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ts_Solutions.IView;
using Ts_Solutions.Model;

namespace Ts_SolutionsTests
{
    public class MainView : IMainView
    {
        public bool Success { get; set; }

        public void CallClicked(string phone)
        {
        }

        public void CallNumber(string phone)
        {
        }

        public void DirectionsClicked(ServicePoint point)
        {
        }

        public void HideStatus()
        {
        }

        public bool IsOnline()
        {
            return true;
        }

        public Task OnConnected()
        {
            return Task.Delay(0);
        }

        public void OpenDirections(ServicePoint point)
        {
        }

        public void SetList(List<ServicePoint> points)
        {
        }

        public void SetLoading(bool isLoading)
        {
        }

        public void SetMarkers(List<ServicePoint> points)
        {
            if (points != null && points.Count > 0)
                Success = true;
            else
                Success = false;
        }

        public void ShowMessage(string message)
        {
        }

        public void ShowStatus(string status)
        {
        }
        
    }
}
