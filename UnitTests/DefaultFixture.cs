using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;

namespace ProjectManager.UnitTests
{
    public class DefaultFixture
    {
        public IFixture ProjectManagerFixture { get; set; }

        public DefaultFixture()
        {
            ProjectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        }


    }
}
