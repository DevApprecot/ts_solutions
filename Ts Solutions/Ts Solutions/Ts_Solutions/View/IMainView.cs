using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ts_Solutions.Model;

namespace Ts_Solutions.View
{
    public interface IMainView
    {
        void SetMarkers(List<ServicePoint> points);
        void ShowStatus();
        void SwitchView();
        void ShowNoNet();
    }
}
