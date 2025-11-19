using shittyFileManager;
using System.Diagnostics;
using System.Windows.Forms;
namespace testSMBUryuUtility
{
    public partial class Form1 : Form
    {
        SBMUFM shittybigboyTools = new SBMUFM();
        string SelectedLmao;

        private void refreshList()
        {
            treeView1.Nodes.Clear();
            Dictionary<string, Dictionary<string, string>> AllMods = shittybigboyTools.getAllMods();
            foreach (var item in AllMods)
            {
                TreeNode mainNode = new();
                mainNode.Text = item.Key;
                foreach (var item1 in item.Value)
                {
                    TreeNode node = new();
                    node.Text = item1.Key;
                    node.ToolTipText = item1.Value;
                    mainNode.Nodes.Add(node);
                }

                treeView1.Nodes.Add(mainNode);
            }
        }
        public Form1()
        {
            InitializeComponent();
            refreshList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            shittybigboyTools.Toggle(SelectedLmao);
            refreshList();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SelectedLmao = e.Node.ToolTipText;
            label1.Text = SelectedLmao;
            checkBox1.CheckState = !shittybigboyTools.modState(SelectedLmao) ? CheckState.Checked : CheckState.Unchecked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            shittybigboyTools.addArc();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            shittybigboyTools.addSkyline();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(SelectedLmao))
            {
                var outp = shittybigboyTools.infoToml(SelectedLmao);
                if (outp.GetType() == typeof(Dictionary<string, string>))
                {
                    richTextBox1.Text = "";
                    foreach (var item in (Dictionary<string, string>)outp)
                    {
                        richTextBox1.Text += item.Key + " = " + item.Value + "\n";
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            shittybigboyTools.CheckConflicts();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(SelectedLmao))
            {
                var outp = shittybigboyTools.infoToml(SelectedLmao, 1);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var Conflicts = shittybigboyTools.CheckConflicts(1);
            if(Conflicts==null) return;
            if (Conflicts.GetType() != typeof(List<Dictionary<string, string>>)) return;
            List<Dictionary<string, string>> conflicts = (List<Dictionary<string, string>>)Conflicts;
            label2.Text = conflicts.Count.ToString();
            foreach (Dictionary<string, string> thing in conflicts)
            {
                Debug.WriteLine(thing["reason"]);
                Debug.WriteLine(thing["mod"]);
                Debug.WriteLine(thing["hated"]);
            }
        }
    }
}
