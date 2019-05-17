using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int a, b;
        //This function is for calculating the weight function using Hebb's rule
        int weightmat(List<int[]>mat1 , int i, int j)
        {
            int w=0;
            for(int n=0;n<a;n++)
            {
                if (mat1[n][i] == 0)
                    mat1[n][i] = -1;
                if (mat1[n][j] == 0)
                    mat1[n][j] = -1;
                w = w + mat1[n][i] * mat1[n][j];
            }
            return w;
        }
        int unit(int[] test,int[,] w_matrix,int i)
        {
            int j = 0,u=0;
            for(j = 0; j < b; j++)
            {
                if (test[j] == 0)
                    test[j] = -1;
                u = u + test[j] * w_matrix[j,i];
            }
            return u;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int lineCounter = 0;

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename.Text = openFileDialog1.FileName;
                TextReader reader = new StreamReader(openFileDialog1.FileName);
                Console.Write("I am Bob".Count(c => !Char.IsWhiteSpace(c)));
                richTextBox1.Text = reader.ReadToEnd();
                reader.Close();
                TextReader countline = new StreamReader(openFileDialog1.FileName);
                while (countline.ReadLine() != null)
                {
                    lineCounter++;
                }
                textBox1.Text = lineCounter.ToString();
                countline.Close();
                string line1 = File.ReadLines(openFileDialog1.FileName).First();
                int countcoulmn = line1.Count(c => !Char.IsWhiteSpace(c));
                textBox2.Text = countcoulmn.ToString();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
            {
                a = Convert.ToInt32(textBox1.Text);
                b = Convert.ToInt32(textBox2.Text);
            
            // Here it creates the list of Dynamic arrays
            List<int[]> mat1 = new List<int[]>();
                // Initializing the weight matrix variable
                int[,] w_matrix = new int[b, b];
                // This is the rich text box funtion which gets the input vectors
                string[] Rt1 = richTextBox1.Lines;
                // Validation for the input format(rows)
                if (Rt1.Length != a)
                {
                    MessageBox.Show("No of rows do not match");
                    return;
                }
                int i = 0, j = 0;
                foreach (string line in Rt1)
                {
                    int[] a1 = Array.ConvertAll(line.Split(' ').ToArray(), Int32.Parse);
                
                    // Validation for the input format (columns)
                    if (a1.Length != b)
                    {
                        MessageBox.Show("No of Columns do not match");
                        return;
                    }
                    // After getting the inputs it appends to the array mat1
                    mat1.Add(a1);
                   // Console.WriteLine(mat1);
                }
                // Calculating weight matrix
                for (i = 0; i < b; i++)
                {
                    for (j = 0; j < b; j++)
                    {
                        if (i == j)
                        {
                            w_matrix[i, j] = 0;
                        }
                        else
                        {
                            w_matrix[i, j] = weightmat(mat1, i, j);
                        }
                        richTextBox2.Text += w_matrix[i, j].ToString() + ' ';
                    }
                    richTextBox2.Text += '\n';
                }

            int[] test = Array.ConvertAll(textBox3.Text.Split(' ').ToArray(), Int32.Parse);
            int[] temp = test;
            long[,] mat_row = new long[a, b];
            long[,] check_mat = new long[b,b];
            string tf = "";
            var Check = "";
            var ft = "";
            var tv = "";
            int flag = 0;
            // This will checks the network converge or not
            for (i = 0; i < b; i++)
            {
                int t = 0, k = 0;
                k = unit(test, w_matrix, i);
                t = test[i] + k;
                temp[i] = t;
                if (t >= 0)
                    temp[i] = 1;
                else
                    temp[i] = -1;
                tf = "";
                for (j = 0; j < b; j++)
                {
                    tv = test[j].ToString();
                    tf = tf + tv;

                }
                //Console.WriteLine($"======================= {tf}");
                for (int m = 0; m < a; m++)
                {
                    Check = "";
                    for (int n = 0; n < b; n++)
                    {
                        ft = mat1[m][n].ToString();
                        Check = Check + ft;
                    }
                    if (tf == Check)
                        flag = 1;
                    

                 
                }
                
            }
            if (flag == 1)
            {
                label7.Text = ($" This Network has \n  been Converged \n Founded Vector \n    {tf}");
                Console.WriteLine("con");
            }
            else
                label7.Text = ($"No Vector found \nThis Network resulted \n in Loop");


        }
    }
}
