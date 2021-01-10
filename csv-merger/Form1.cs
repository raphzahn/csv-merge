using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csv_merger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string path = textBox1.Text;
            string pathOut = textBox2.Text;
            string[] files = Directory.GetFiles(path, "*.csv", SearchOption.AllDirectories);
            int count = 0;
            Encoding utf8WithoutBom = new UTF8Encoding(true);
            path = path +"\\";

            File.Delete(pathOut);

            using (var writer = new StreamWriter(new FileStream(pathOut, FileMode.CreateNew), utf8WithoutBom)) { 

                foreach (var file in files)
                {

                    using (var reader = new StreamReader(file, Encoding.Default))
                    {
                        if (count == 0) {
                            writer.WriteLine(reader.ReadLine());
                        }
                        
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith("VM") != true)
                            {
                                writer.WriteLine(line);
                            }

                        }
                    }

                    count++;

                }
            }
        }

    }
}
