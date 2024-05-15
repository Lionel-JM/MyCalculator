using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;


namespace WpfApp2 {
    /// <summary>
    /// first windows app, simple calculator
    /// stores expression in a string and then pass it into  calc dunction
    /// </summary>
    public partial class MainWindow : Window {

        private CultureInfo culture;

        public MainWindow() {
            InitializeComponent();
        }
        
        string expression, ansver;


        private void one_Click(object sender, RoutedEventArgs e) { expression += "1"; resultBox.Text = expression; }
        private void two_Click(object sender, RoutedEventArgs e) { expression += "2"; resultBox.Text = expression; }
        private void three_Click(object sender, RoutedEventArgs e) { expression += "3"; resultBox.Text = expression; }
        private void four_Click(object sender, RoutedEventArgs e) { expression += "4"; resultBox.Text = expression; }
        private void five_Click(object sender, RoutedEventArgs e) { expression += "5"; resultBox.Text = expression; }
        private void six_Click(object sender, RoutedEventArgs e) { expression += "6"; resultBox.Text = expression; }
        private void seven_Click(object sender, RoutedEventArgs e) { expression += "7"; resultBox.Text = expression; }
        private void eight_Click(object sender, RoutedEventArgs e) { expression += "8"; resultBox.Text = expression; }
        private void nine_Click(object sender, RoutedEventArgs e) { expression += "9"; resultBox.Text = expression; }
        private void zero_Click(object sender, RoutedEventArgs e) { expression += "0"; resultBox.Text = expression; }


        private void dot_Click(object sender, RoutedEventArgs e) {
            if (culture == null) { culture = CultureInfo.CurrentCulture; }
            string decimalSeparator = culture.NumberFormat.NumberDecimalSeparator;

            expression += decimalSeparator; 
            resultBox.Text = expression; 
        }
        private void op_brace_Click(object sender, RoutedEventArgs e) { expression += " ( "; resultBox.Text = expression; }
        private void cl_brace_Click(object sender, RoutedEventArgs e) { expression += " ) "; resultBox.Text = expression; }
        private void pow_Click(object sender, RoutedEventArgs e) { expression += " ^ "; resultBox.Text = expression; }
        private void plus_Click(object sender, RoutedEventArgs e) { expression += " + "; resultBox.Text = expression; }
        private void minus_Click(object sender, RoutedEventArgs e) { expression += " - "; resultBox.Text = expression; }
        private void mult_Click(object sender, RoutedEventArgs e) { expression += " * "; resultBox.Text = expression; }
        private void divide_Click(object sender, RoutedEventArgs e) { expression += " / "; resultBox.Text = expression; }
        private void ac_Click(object sender, RoutedEventArgs e) { expression = ""; resultBox.Text = expression; }
        private void ans_Click(object sender, RoutedEventArgs e) { expression += ansver; resultBox.Text = expression; }
        private void del_Click(object sender, RoutedEventArgs e) {
            if (expression == "") { return; }
            char lChar = expression[expression.Length - 1];//the last character in the expression
            if (lChar == '1' || lChar == '2' || lChar == '3' || lChar == '4' || lChar == '5' || lChar == '6' || lChar == '7' ||
                    lChar == '8' || lChar == '9' || lChar == '0' || lChar == '.') {
                expression = expression.Substring(0, expression.Length - 1);
            }
            else {
                expression = expression.Substring(0, expression.Length - 3);
            }
            resultBox.Text = expression;
        }
        private void equal_Click(object sender, RoutedEventArgs e) {
            try {
                //MessageBox.Show(expression);
                ansver = MyCalc(expression).ToString();
                if (ansver == "300") { resultBox.Text = "\n\t Отсоси у тракториста!"; }
                else { resultBox.Text = ansver; }
            }
            catch (Exception ex) {
                if (ex.Message.Contains("Index was out of range")) { return; }
                else if (ex.Message.Contains("Input string was not in a correct format.")) { resultBox.Text = "SYNTAX ERROR"; }
                else if (ex.Message.Contains("Index was outside the bounds of the array.")) { resultBox.Text = "Bracket did not closed"; }
                else { resultBox.Text = ex.Message; }
            }
            expression = "";
        }



        private static double MyCalc(string expr){
            expr += " ";
            if (expr.Contains(" / 0 ")){
                Console.WriteLine("Must not divide by zero!\n");
                return 0;
            }
            
            List<double> nums = new List<double>();
            List<char> sign = new List<char>();
            string temp = "";
            

            for (int el = 0; el < expr.Length; el++) {
                if (expr[el] == '(') {
                    el += 2;
                    while (expr[el] != ')')
                    {
                        temp += expr[el];
                        el++;
                    }
                    nums.Add(MyCalc(temp));
                    temp = "";
                }
                else if (expr[el] == ' '){
                    if (temp == "+" || temp == "-" || temp == "*" || temp == "/" || temp == "^") {
                        sign.Add(char.Parse(temp));
                    }
                    else{
                        if(temp.Length == 0) {  continue; }//check for an empty input
                        nums.Add(double.Parse(temp));
                    }
                    temp = "";
                }
                else if (expr[el] == '+' || expr[el] == '-' || expr[el] == '*' || expr[el] == '/'){
                    temp = expr[el].ToString();
                }
                else{
                    temp += expr[el].ToString();
                }
            }

            for (int i = 0; i < sign.Count; i++)
            {
                if (sign[i] == '^')
                {
                    nums[i] = Math.Pow(nums[i], nums[i + 1]);
                    sign.RemoveAt(i);
                    nums.RemoveAt(i + 1);
                    i--;
                }
            }
            for (int i = 0; i < sign.Count; i++)
            {
                if (sign[i] == '*')
                {
                    nums[i] *= nums[i + 1];
                    sign.RemoveAt(i);
                    nums.RemoveAt(i + 1);
                    i--;
                }
                else if (sign[i] == '/')
                {
                    nums[i] /= nums[i + 1];
                    sign.RemoveAt(i);
                    nums.RemoveAt(i + 1);
                    i--;
                }
            }
            for (int i = 0; i < sign.Count; i++)
            {
                if (sign[i] == '+')
                {
                    nums[i] += nums[i + 1];
                }
                else if (sign[i] == '-')
                {
                    nums[i] -= nums[i + 1];
                }
                sign.RemoveAt(i);
                nums.RemoveAt(i + 1);
                i--;
            }
            return nums[0];
        }
    }










}
