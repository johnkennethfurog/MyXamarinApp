using MyXamarinApp.Interfaces;
using MyXamarinApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyXamarinApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {


        private string _display;
        public string Display
        {
            get { return _display; }
            set { SetPropertyValue(ref _display, value); }
        }

        private double _result=0;
        public double Result
        {
            get { return _result; }
            set { SetPropertyValue(ref _result, value); }
        }

        private double _tempResult = 0;

        public double TempResult
        {
            get { return _tempResult; }
            set { SetPropertyValue(ref _tempResult, value); }
        }



        private string _currentNumberString = "";
        private double _currentNumber = 0;
        private Operation _currentOperation;
        private ICalculate _calculateService;

        public ICommand ButtonPressCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand EqualsCommand { get; set; }

        public  MainPageViewModel()
        {
            ButtonPressCommand = new Command<string>(ButtonPressAction);
            ClearCommand = new Command(ClearAction);
            EqualsCommand = new Command(EqualAction);
            _calculateService = new CalculateService();
            _currentOperation = Operation.none;
        }

        private void EqualAction()
        {
            Result = TempResult;
            Display = TempResult.ToString();
            _currentNumber = TempResult;
            _currentNumberString = Display;
            TempResult = 0;
        }

        private void ClearAction()
        {
            Display = "";
        }

        void ButtonPressAction(string text)
        {
            bool calculate = false;
            if(text != "+" && text != "-" && text != "*" && text != "/")
            {
                _currentNumberString += text;
                calculate = true;
            }

            _currentNumber = Double.Parse(_currentNumberString);
            _currentOperation = GetOperationToUse(text);

            if(calculate)
                TempResult = _calculateService.Calculate(_currentOperation, _result, number: _currentNumber);
            
            Display += text;
        }

        Operation GetOperationToUse(string text)
        {
            switch(text)
            {
                case "+":
                    _currentNumberString = "";
                    Result = _tempResult;
                    return Operation.add;
                case "-":
                    _currentNumberString = "";
                    Result = _tempResult;
                    return Operation.subtract;
                case "/":
                    _currentNumberString = "";
                    Result = _tempResult;
                    return Operation.divide;
                case "*":
                    _currentNumberString = "";
                    Result = _tempResult;
                    return Operation.multiply;
                default:
                    return _currentOperation;

            }
        }
    }

    public enum Operation
    {
        add, subtract, divide, multiply, none
    }

}
