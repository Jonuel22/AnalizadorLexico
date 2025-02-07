using System;
using System.Windows.Forms;

namespace AnalizadorLexico
{
    public class MainForm : Form
    {
        private TextBox inputBox;
        private Button analyzeButton;
        private ListBox outputList;

        public MainForm()
        {
            this.Text = "Analizador Léxico";
            this.Width = 500;
            this.Height = 400;

            inputBox = new TextBox { Left = 10, Top = 10, Width = 460 };
            analyzeButton = new Button { Text = "Analizar", Left = 200, Top = 50 };
            outputList = new ListBox { Left = 10, Top = 100, Width = 460, Height = 200 };

            analyzeButton.Click += (sender, e) => AnalyzeText();

            Controls.Add(inputBox);
            Controls.Add(analyzeButton);
            Controls.Add(outputList);
        }

        private void AnalyzeText()
        {
            outputList.Items.Clear();
            Lexer lexer = new Lexer();
            List<Token> tokens = lexer.Analyze(inputBox.Text);
            foreach (var token in tokens)
            {
                outputList.Items.Add(token.ToString());
            }
        }
    }
}
