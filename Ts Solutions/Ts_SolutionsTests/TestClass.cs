using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ts_Solutions.Presenter;

namespace Ts_SolutionsTests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public async Task ShouldLoadServicePointsSuccessfully()
        {
            var view = new MainView();
            var presenter = new MainPresenter(view);

            await presenter.LoadServicePoints();
            Assert.IsTrue(view.SuccessList);
        }

        [Test]
        public async Task ShouldLoadServiceTimeout()
        {
            var view = new MainView();
            var presenter = new MainPresenter(view);

            presenter.Timeout = 10;

            await presenter.LoadServicePoints();
            Assert.IsTrue(view.SuccessTimeout);
        }
    }
}
