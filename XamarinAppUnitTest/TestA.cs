using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MyXamarinApp.ViewModels;
namespace XamarinAppUnitTest
{
    public class TestA
    {
        public TestA()
        {

        }

        [Theory]
        [InlineData("1","2","-","5",7)]
        [InlineData("0", "2", "*", "5",10)]
        [InlineData("2", "2", "+", "5",25)]
        public void CheckAdd(string num1,string num2,string operation,string num3,double expected)
        {
            var calcu = new MainPageViewModel();
            calcu.ButtonPressCommand.Execute(num1);
            calcu.ButtonPressCommand.Execute(num2);

            calcu.ButtonPressCommand.Execute(operation);
            calcu.ButtonPressCommand.Execute(num3);

            Assert.Equal(expected, calcu.TempResult, 0);
         
        }

        

        [Fact]
        public void Pass()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task failed()
        {
            await Task.Run(() =>
            {
                throw new Exception("boom");
            });
        }
    }
}
