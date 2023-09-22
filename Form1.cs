using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Timed_Math_Quiz_Assignment
{
    public partial class Form1 : Form
    {
        Random randomizer = new Random();

        int addend1, addend2, minuend, subtrahend, multiplicand, multiplier, dividend, divisor, timeLeft;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                if (timeLeft <= 5) timeLabel.ForeColor = Color.Red;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                timeLabel.ForeColor = Color.Black;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToString("dd MMMM yyyy");
        }

        private void sum_KeyPress(object sender, KeyPressEventArgs keyPressEvent)
        {
            if (keyPressEvent.KeyChar == (char)Keys.Enter) difference.Focus();
        }

        private void difference_KeyPress(object sender, KeyPressEventArgs keyPressEvent)
        {
            if (keyPressEvent.KeyChar == (char)Keys.Enter) product.Focus();
        }

        private void product_KeyPress(object sender, KeyPressEventArgs keyPressEvent)
        {
            if (keyPressEvent.KeyChar == (char)Keys.Enter) quotient.Focus();
        }

        private void quotient_KeyPress(object sender, KeyPressEventArgs keyPressEvent)
        {
            if (keyPressEvent.KeyChar == (char)Keys.Enter) sum.Focus();
        }

        private void sum_ValueChanged(object sender, EventArgs e)
        {
            List<Label> additionLabels = new List<Label>() { plusLeftLabel, plusLabel, plusRightLabel, plusEquales };
            if (addend1 + addend2 == sum.Value)
            {
                turnLabelsGreen(additionLabels);
            } else
            {
                turnLabelsBlack(additionLabels);
            }
        }

        private void difference_ValueChanged(object sender, EventArgs e)
        {
            List<Label> subtractionLabels = new List<Label>() { minusLeftLabel, minusLabel, minusRightLabel, minusEquales };
            if (minuend - subtrahend == difference.Value)
            {
                turnLabelsGreen(subtractionLabels);
            } else
            {
                turnLabelsBlack(subtractionLabels);
            }
        }

        private void product_ValueChanged(object sender, EventArgs e)
        {
            List<Label> multiplicationLabels = new List<Label>() { timesLeftLabel, timesLabel, timesRightLabel, timesEquales };
            if (multiplicand * multiplier == product.Value)
            {
                turnLabelsGreen(multiplicationLabels);
            } else
            {
                turnLabelsBlack(multiplicationLabels);
            }
        }

        private void quotient_ValueChanged(object sender, EventArgs e)
        {
            List<Label> divisionLabels = new List<Label>() { dividedLeftLabel, dividedLabel, dividedRightLabel, dividedEquales };
            if (dividend / divisor == quotient.Value)
            {
                turnLabelsGreen(divisionLabels);
            } else
            {
                turnLabelsBlack(divisionLabels);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (plusLabel.ForeColor != Color.Black)
            {
                resetLabelColors();
            }
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void StartTheQuiz()
        {
            // Addition problem code.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
            sum.Value = 0;

            // Subtraction problem code.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Multiplication problem code.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Division problem code.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the countdown timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private bool CheckTheAnswer()
        {
            if (plusLabel.ForeColor == Color.Green
                && minusLabel.ForeColor == Color.Green
                && timesLabel.ForeColor == Color.Green
                && dividedLabel.ForeColor == Color.Green)
                return true;
            else
                return false;
        }

        private void turnLabelsGreen(List<Label> labels)
        {
            foreach (Label label in labels) 
            {
                label.ForeColor = Color.Green;
            }
        }

        private void turnLabelsBlack(List<Label> labels)
        {
            foreach (Label label in labels)
            {
                label.ForeColor = Color.Black;
            }
        }

        private void resetLabelColors()
        {
            List<Label> labels = new List<Label>() { plusLeftLabel, plusLabel, plusRightLabel, plusEquales, minusLeftLabel, minusLabel, minusRightLabel, minusEquales, timesLeftLabel, timesLabel, timesRightLabel, timesEquales, dividedLeftLabel, dividedLabel, dividedRightLabel, dividedEquales };
            foreach (Label label in labels)
            {
                label.ForeColor = Color.Black;
            }

        }
    }
}
