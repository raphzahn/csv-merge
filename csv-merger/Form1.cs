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
            int longestLine = 0;
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

            var totalcols = 0;
            var lines = File.ReadAllLines(pathOut);
            foreach (var line in lines)
            {
                var lineCount = line.Split(';').Length;
                if (lineCount > totalcols)
                {
                    totalcols = lineCount;
                }
            }

            List<String> linesList = new List<String>();

            using (StreamReader reader = new StreamReader(pathOut, Encoding.Default))
            {
                String line;

                while ((line = reader.ReadLine()) != null)
                {
                    var lineCount = line.Split(';').Length;
                    if (lineCount < totalcols)
                    {
                        String[] split = line.Split(';');

                        int missingLinesCount = totalcols - lineCount;
                        var missingLines = "";

                        for (int ii = 0; ii < missingLinesCount; ii++)
                        {
                            missingLines = missingLines + ";";
                        }


                        split[split.Length - 3] = split[split.Length - 3] + missingLines;
                        line = String.Join(";", split);

                    }


                    linesList.Add(line);
                }
            }

            using (var writer = new StreamWriter(new FileStream(pathOut, FileMode.Open), utf8WithoutBom))
            {
               
                foreach (String line in linesList)
                    writer.WriteLine(line);
            }

            
            
            
        }

    }
}
