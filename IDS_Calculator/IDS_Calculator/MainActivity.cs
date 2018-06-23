using System;
using Android.App;
using Android.Widget;
using Android.OS;

/// <summary>
/// This file implements a simple calculator for Android 
/// using the Xamarin development environment running in
/// Visual Studio 2015 (Community Edition).
/// - - - -
/// Written by Geoff Gariepy (geoff.gariepy@gmail.com) 23-JUN-2018
/// 
/// </summary>
namespace IDS_Calculator
{
    [Activity(Label = "IDS Calculator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string Register = "0";
        decimal firstVal = 0.0M;
        decimal secondVal = 0.0M;
        bool EqualsPerformed = false;
        string Operation = "";
        TextView RegisterViewer;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            RegisterViewer = FindViewById<TextView>(Resource.Id.txtOutput);

            RegisterViewer.Text = Register;


            // Get our button from the layout resource,
            // and attach an event to it
            #region Digits

            Button button0 = FindViewById<Button>(Resource.Id.button0);
            button0.Click += delegate { AddDigit(0); };

            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += delegate { AddDigit(1); };

            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += delegate { AddDigit(2); };

            Button button3 = FindViewById<Button>(Resource.Id.button3);
            button3.Click += delegate { AddDigit(3); };

            Button button4 = FindViewById<Button>(Resource.Id.button4);
            button4.Click += delegate { AddDigit(4); };

            Button button5 = FindViewById<Button>(Resource.Id.button5);
            button5.Click += delegate { AddDigit(5); };

            Button button6 = FindViewById<Button>(Resource.Id.button6);
            button6.Click += delegate { AddDigit(6); };

            Button button7 = FindViewById<Button>(Resource.Id.button7);
            button7.Click += delegate { AddDigit(7); };

            Button button8 = FindViewById<Button>(Resource.Id.button8);
            button8.Click += delegate { AddDigit(8); };

            Button button9 = FindViewById<Button>(Resource.Id.button9);
            button9.Click += delegate { AddDigit(9); };

            #endregion

            #region Operations
            Button buttonPlus = FindViewById<Button>(Resource.Id.buttonPlus);
            buttonPlus.Click += delegate { QueueOperation("+"); };

            Button buttonMinus = FindViewById<Button>(Resource.Id.buttonMinus);
            buttonMinus.Click += delegate { QueueOperation("-"); };

            Button buttonDiv = FindViewById<Button>(Resource.Id.buttonDiv);
            buttonDiv.Click += delegate { QueueOperation("/"); };

            Button buttonTimes = FindViewById<Button>(Resource.Id.buttonTimes);
            buttonTimes.Click += delegate { QueueOperation("*"); };

            Button buttonSquare = FindViewById<Button>(Resource.Id.buttonSquare);
            buttonSquare.Click += delegate { DoSquared(); };

            Button buttonEquals = FindViewById<Button>(Resource.Id.buttonEquals);
            buttonEquals.Click += delegate { DoEquals(); };

            #endregion

            #region Edit Functions
            Button buttonPlusMinus = FindViewById<Button>(Resource.Id.buttonPlusMinus);
            buttonPlusMinus.Click += delegate { DoPlusMinus(); };

            Button buttonClear = FindViewById<Button>(Resource.Id.buttonC);
            buttonClear.Click += delegate { DoClear(); };

            Button buttonClearEntry = FindViewById<Button>(Resource.Id.buttonCE);
            buttonClearEntry.Click += delegate { DoClearEntry(); };

            Button buttonDecimal = FindViewById<Button>(Resource.Id.buttonDot);
            buttonDecimal.Click += delegate { DoDecimal(); };
            #endregion

        }

        /// <summary>
        /// Adds a decimal point where the cursor is
        /// to the number currently being entered
        /// </summary>
        private void DoDecimal()
        {
            if (Register == "" || Register == "0" || EqualsPerformed == true)
                Register = "0.";
            else if (!Register.Contains("."))
            {
                Register = Register + ".";
            }
            RegisterViewer.Text = Register;

            if (EqualsPerformed)
                EqualsPerformed = false;
       }

        /// <summary>
        /// Enqueues an operation (+, -, /, *) and
        /// makes the register ready for the next
        /// numeric entry.
        /// </summary>
        /// <param name="Op"></param>
        private void QueueOperation(string Op)
        {
            if (Operation != "")
            {
                DoEquals();
                Operation = Op;
                return;
            }
            firstVal = Decimal.Parse(Register);
            Register = "";
            RegisterViewer.Text = "";
            Operation = Op;
        }

        /// <summary>
        /// Multiplies the current value being displayed
        /// by itself.  Checks for overflow and
        /// displays an Err message if overflow is
        /// encountered.
        /// </summary>
        private void DoSquared()
        {
            try
            {
                decimal val = decimal.Parse(Register);

                checked
                {
                    val = val * val;
                }

                Register = val.ToString();
                RegisterViewer.Text = Register;
                
            }
            catch
            {
                DoClear();
                Register = "Err";
                RegisterViewer.Text = Register;
            }
        }
        
        /// <summary>
        /// Multiplies the current value displayed
        /// on the calculator by -1, changing its
        /// sign.
        /// </summary>
        private void DoPlusMinus()
        {
            decimal valRegister;
            decimal.TryParse(Register, out valRegister);
            if (valRegister != 0)
                valRegister *= -1;
            Register = valRegister.ToString();
            RegisterViewer.Text = Register;
        }

        /// <summary>
        /// Implements the clear function familiar to 
        /// calculator users
        /// </summary>
        private void DoClear()
        {
            Register = "0";
            firstVal = 0.0M;
            secondVal = 0.0M;
            RegisterViewer.Text = Register;
            Operation = "";
        }

        /// <summary>
        /// Clears the most recently entered digits,
        /// preserving any that were entered before the
        /// last operator.
        /// </summary>
        private void DoClearEntry()
        {
            Register = "0";
            RegisterViewer.Text = Register;
        }

        /// <summary>
        /// Calculates the result of the previous number
        /// entered (or previously calculated) by applying the last entered
        /// operator to the currently entered string of digits.
        /// Results are rounded after 8 digits past the decimal point.
        /// </summary>
        private void DoEquals()
        {
            decimal result = 0.0M;
            try
            {
                secondVal = Convert.ToDecimal(Register);
                switch (Operation)
                {
                    case "+":
                        result = firstVal + secondVal;
                        break;
                    case "-":
                        result = firstVal - secondVal;
                        break;
                    case "/":
                        result = firstVal / secondVal;
                        break;
                    case "*":
                        result = firstVal * secondVal;
                        break;
                }

                firstVal = result;
                secondVal = 0.0M;
                result = Math.Round(result, 8);
                Register = (result.ToString("##.########") != "") ? result.ToString("##.########") : "0";
                RegisterViewer.Text = Register;
                Operation = "";
                EqualsPerformed = true;
            }
            catch
            {
                firstVal = 0;
                secondVal = 0;
                Register = "Err";
                Operation = "";
                RegisterViewer.Text = Register;
            }
        }

        /// <summary>
        /// Appends a numeric digit to the right of the currently
        /// displayed number.
        /// Replaces the default "0" value with the digit, if applicable
        /// </summary>
        /// <param name="digit">digit corresponding to button pressed</param>
        private void AddDigit(int digit)
        {
            // Clear display after equals performed and first digit pressed
            if (EqualsPerformed)
            {
                EqualsPerformed = false; 
                Register = "";
            }

            if (Register == "0" || Register == "Err")
                Register = "";



            Register = Register + digit.ToString();
            RegisterViewer.Text = Register;            


            //decimal OldRegisterVal = 0;
            //if (Register != "")
            //    OldRegisterVal = decimal.Parse(Register);
            //if (firstVal == 0 && secondVal == 0 && OldRegisterVal == 0)
            //    Register = string.Empty;


            //if (OldRegisterVal == 0.0M)
            //    decimal.TryParse(Register, out firstVal);
            //else if (secondVal == 0.0M)
            //    decimal.TryParse(Register, out secondVal);
        }
    }
}

