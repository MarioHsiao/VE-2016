//using RoughsExtensions;

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinExplorer
{
    public partial class FolderLister : UserControl
    {
        

        public FolderLister()
        {
            InitializeComponent();
            ts = toolStrip1;
            lb = listView1;
            tf = textBox1;
            tf.Enabled = false;
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            rightClickMenuStrip = contextMenuStrip1;
            tb = new TextBox();
            open = toolStripButton1;
            PrepareListView();
            GetDrives();
            LoadCurrentFolder();
        }

        private ToolStrip ts { get; set; }

        public ListView lb { get; set; }

        public ToolStripButton open { get; set; }

        private ContextMenuStrip rightClickMenuStrip { get; set; }

        public TextBox tf { get; set; }

        //public WorkspaceCtr ws { get; set; }

        public void PrepareListView()
        {
            lb.Columns.Add("", 100);

            
            lb.View = View.Details;
            lb.FullRowSelect = true;

            lb.SmallImageList = new ImageList();

            lb.HeaderStyle = ColumnHeaderStyle.None;
        }

        public void GetDrives()
        {
            string[] drives = System.IO.Directory.GetLogicalDrives();

            foreach (string str in drives)
            {
                System.Console.WriteLine(str);
                CreateButton(str);
            }
        }

        public void CreateButton(string name)
        {
            ToolStripButton b = new ToolStripButton();
            b.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            b.Image = resource_vsc.harddrive;
            b.Text = name;
            b.Click += new EventHandler(OnDriveSelect);
            b.Tag = name;

            ts.Items.Add(b);
        }

        public void OnDriveSelect(object sender, EventArgs e)
        {
            ToolStripButton b = sender as ToolStripButton;

            if (b == null)
                return;

            if (b.Tag == null)
                return;

            string p = b.Tag as string;

            //string[] filePaths = null;

            //try
            //{
            //    filePaths = Directory.GetFiles(@p);

            //}
            //catch(Exception ex)
            //{
            //    return;
            //}

            lb.Items.Clear();

            ListDirectoryContent(p);
        }

        public void LoadCurrentFolder()
        {
            lb.Items.Clear();

            string p = AppDomain.CurrentDomain.BaseDirectory;

            ListDirectoryContent(p);
        }

        public string pwd { get; set; }

        public void ListDirectoryContent(string path)
        {
            IntPtr hImgSmall; //the handle to the system image list
            IntPtr hImgLarge; //the handle to the system image list

            //label1.Text = path;
            listView1.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(path);

            // first list sub-directories
            DirectoryInfo[] dirs = null;

            try
            {
                dirs = di.GetDirectories();
            }
            catch (Exception ex)
            {
                return;
            }

            ListViewItem bb = new ListViewItem();
            bb.Text = "..";

            bb.Tag = di;
            lb.Items.Add(bb);

            foreach (DirectoryInfo dir in dirs)
            {
                if (lb.SmallImageList.Images.ContainsKey(".dir") == false)
                {
             
                  
                }

                ListViewItem b = new ListViewItem();
                b.Text = dir.Name;
                b.ImageKey = ".dir";
                b.Tag = dir;

                lb.Items.Add(b);
            }
            // then list the files
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                if (lb.SmallImageList.Images.ContainsKey(file.Extension) == false)
                {
                    
                }

                ListViewItem b = new ListViewItem();
                b.Text = file.Name;
                b.ImageKey = file.Extension;
                b.Tag = file;

                lb.Items.Add(b);
            }

            pwd = path;

            tf.Text = pwd;
        }

        public void Refreshes()
        {
            ListDirectoryContent(pwd);
        }

        public TextBox tb { get; set; }

        public PictureBox pb { get; set; }

        public bool filenameonly = false;

        public bool LoadByClick = false;

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(LoadByClick == true)
            {

                listView1_DoubleClick(this, null);



            }

            if (lb.SelectedIndices == null)
                return;
            if (lb.SelectedIndices.Count <= 0)
                return;
            int i = lb.SelectedIndices[0];

            ListViewItem v = lb.Items[i];

            if (v.Tag == null)
                return;

            DirectoryInfo d = v.Tag as DirectoryInfo;

            if (d != null)
            {
                if (tb == null)
                    return;

                string s = d.FullName;

                tb.Text = s;

                tf.Text = s;

                return;
            }

            FileInfo f = v.Tag as FileInfo;

            if (f != null)
            {
                if (tb == null)
                    return;

                string s = f.FullName;

                if (filenameonly == true)
                {
                    s = Path.GetFileName(s);

                    tb.ForeColor = Color.DarkBlue;
                }

                tb.Text = s;

                tf.Text = s;

                if (pb == null)
                    return;

                string ext = f.Extension;

                if (ext == ".bmp" || ext == ".png" || ext == "jpeg" || ext == "jpg")
                {
                    Image image = Image.FromFile(s);

                    pb.Image = new Bitmap(image);

                    bmp = image;
                }

                return;
            }
        }

        public void LoadFolder(string p)
        {
            lb.Items.Clear();
            
            ListDirectoryContent(p);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (lb.SelectedIndices == null)
                return;
            if (lb.SelectedIndices.Count <= 0)
                return;
            int i = lb.SelectedIndices[0];

            ListViewItem v = lb.Items[i];

            if (v.Text == "..")
            {
                DirectoryInfo d = v.Tag as DirectoryInfo;

                if (d == null)
                    return;

                DirectoryInfo p = d.Parent;

                if (p == null)
                    return;

                ListDirectoryContent(p.FullName);

                return;
            }
            else
            {
                DirectoryInfo d = v.Tag as DirectoryInfo;

                if (d == null)
                    return;

                DirectoryInfo p = d;

                ListDirectoryContent(p.FullName);
            }
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            Refresh();

            if (lb == null)
                return;

            Size sz = lb.ClientSize;

            int w = sz.Width;

            lb.Columns[0].Width = w;
        }

        public static string Truncate(string text, int maxLength)
        {
            return text.Substring(0, Math.Min(maxLength, text.Length));
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        rightClickMenuStrip.Show(this, new Point(e.X, e.Y));
                    }
                    break;
            }
        }

        public string SelectedFile()
        {
            ListView vv = listView1;

            if (vv.SelectedIndices == null)
                return "";

            if (vv.SelectedIndices.Count <= 0)
                return "";

            int i = vv.SelectedIndices[0];

            ListViewItem v = vv.Items[i];

            if (v.Text == "..")
            {
                return "";
            }
            else
            {
                DirectoryInfo d = v.Tag as DirectoryInfo;

                if (d != null)

                    return d.FullName;

                FileInfo f = v.Tag as FileInfo;

                if (f == null)
                    return "";

                return f.FullName;
            }
        }

        public Image bmp { get; set; }

        private void importToWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (ws == null)
            //    return;
            //if (bmp == null)
            //    return;
            //ws.Import2Workspace(bmp);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

        public void OpenSolution()
        {
        }
    }
}