namespace WebFormsDemo
{
    public partial class Form1 : Form
    {
        string winDir = Environment.GetEnvironmentVariable("windir");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            //How to read a text file.
            //try...catch is to deal with a 0 byte file.
            this.listBox1.Items.Clear();
            StreamReader reader = new StreamReader(winDir + "\\system.ini");
            try
            {
                do
                {
                    addListItem(reader.ReadLine());
                }
                while (reader.Peek() != -1);
            }
            catch
            {
                addListItem("File is empty");
            }
            finally
            {
                reader.Close();
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            //Demonstrates how to create and write to a text file.
            StreamWriter writer = new StreamWriter("C:\\shit crap\\RKIT\\Shivam.txt");
            writer.WriteLine("File created using StreamWriter class.");
            writer.Close();
            this.listBox1.Items.Clear();
            addListItem("File Written to C:\\shit crap\\RKIT\\Shivam.txt");
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            //How to retrieve file properties (example uses Notepad.exe).
            this.listBox1.Items.Clear();
            FileInfo FileProps = new FileInfo(winDir + "\\notepad.exe");
            addListItem("File Name = " + FileProps.FullName);
            addListItem("Creation Time = " + FileProps.CreationTime);
            addListItem("Last Access Time = " + FileProps.LastAccessTime);
            addListItem("Last Write TIme = " + FileProps.LastWriteTime);
            addListItem("Size = " + FileProps.Length);
            FileProps = null;
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            //Demonstrates how to obtain a list of disk drives.
            this.listBox1.Items.Clear();
            string[] drives = Directory.GetLogicalDrives();
            foreach (string drive in drives)
            {
                addListItem(drive);
            }
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            //How to get a list of folders (example uses Windows folder). 
            this.listBox1.Items.Clear();
            string[] dirs = Directory.GetDirectories(winDir);
            foreach (string dir in dirs)
            {
                addListItem(dir);
            }
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            //How to obtain list of files (example uses Windows folder).
            this.listBox1.Items.Clear();
            string[] files = Directory.GetFiles(winDir);
            foreach (string i in files)
            {
                addListItem(i);
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.button1.Text = "Read Text File";
            this.button2.Text = "Write Text File";
            this.button3.Text = "View File Information";
            this.button4.Text = "List Drives";
            this.button5.Text = "List Subfolders";
            this.button6.Text = "List Files";
        }

        private void addListItem(string value)
        {
            this.listBox1.Items.Add(value);
        }
    }
}
