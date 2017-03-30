﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ts_Solutions.Model;

namespace Ts_Solutions.IView
{
	public interface IMainView : IBaseView
    {
        void SetMarkers(List<ServicePoint> points);
		void SetList(List<ServicePoint> points);
        void ShowStatus(string result);
    }
}
