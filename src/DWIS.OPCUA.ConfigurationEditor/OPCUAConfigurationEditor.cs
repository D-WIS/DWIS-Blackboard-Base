using DWIS.OPCUA.Vocabulary;
using DWIS.Vocabulary.Development;

namespace DWIS.OPCUA.ConfigurationEditor
{
    public partial class OPCUAConfigurationEditor : Form
    {
        private DWISVocabulary _vocabulary;
        private VocabularyOPCUAConfiguration _configuration;
        private TreeNode _nounsNode = new TreeNode("Nouns");
        private TreeNode _verbsNode = new TreeNode("Verbs");

        private string _configurationFileName = @"..\..\..\..\..\..\docs\opcua\configuration\opcuaConfiguration.json";

        private string _absentItemsFile = @".\absentItems.txt";
        private string _removedItemsFile = @".\removedItems.txt";

        public OPCUAConfigurationEditor()
        {
            InitializeComponent();
        }
        private void OPCUAConfigurationEditor_Load(object sender, EventArgs e)
        {
            _vocabulary = DWIS.Vocabulary.Standard.VocabularyProvider.Vocabulary;

            _configuration = VocabularyOPCUAConfiguration.GetConfiguration();

            //duplicates
            List<NounOPCUAConfiguration> duplicatesToRemove = new List<NounOPCUAConfiguration>();
            var confGroups = _configuration.NounOPCUAConfigurations.GroupBy(c => c.Noun.Name);
            foreach (var group in confGroups)
            {
                if (group.Count() > 1)
                {
                    bool exportAsType = group.Any(c => c.ExportAsType);
                    var toKeep = group.First();
                    toKeep.ExportAsType = exportAsType;
                    foreach (var c in group)
                    {
                        if (c != toKeep)
                        {
                            duplicatesToRemove.Add(c);
                        }
                    }
                }
            }

            var tempList = _configuration.NounOPCUAConfigurations.ToList();
            foreach (var dup in duplicatesToRemove)
            {
                tempList.Remove(dup);
            }
            _configuration.NounOPCUAConfigurations = tempList.ToArray();

            List<VerbOPCUAConfiguration> duplicatesToRemove2 = new List<VerbOPCUAConfiguration>();
            var confGroups2 = _configuration.VerbOPCUAConfigurations.GroupBy(c => c.Verb.Name);
            foreach (var group in confGroups2)
            {
                if (group.Count() > 1)
                {                   
                    var toKeep = group.First();
                    
                    foreach (var c in group)
                    {
                        if (c != toKeep)
                        {
                            duplicatesToRemove2.Add(c);
                        }
                    }
                }
            }

            var tempList2 = _configuration.VerbOPCUAConfigurations.ToList();
            foreach (var dup in duplicatesToRemove2)
            {
                tempList2.Remove(dup);
            }
            _configuration.VerbOPCUAConfigurations = tempList2.ToArray();


            List<Noun> absentNouns = new List<Noun>();
            List<Verb> absentVerbs = new List<Verb>();

            List<Noun> removedNouns = new List<Noun>();
            List<Verb> removedVerbs = new List<Verb>();

            foreach (Noun n in _vocabulary.Nouns)
            {
                var nounConf = _configuration.FindNounConfiguration(n.Name, true);
                if (nounConf != null)
                {
                    nounConf.Noun = n;
                }
                else
                {
                    NounOPCUAConfiguration nounOPCUAConfiguration = new NounOPCUAConfiguration() { Noun = n, ExportAsType = true };
                    var l = _configuration.NounOPCUAConfigurations.ToList();
                    l.Add(nounOPCUAConfiguration);
                    _configuration.NounOPCUAConfigurations = l.ToArray();
                    absentNouns.Add(n);
                }               
            }

            foreach (Verb v in _vocabulary.Verbs)
            {
                var verbConf = _configuration.FindVerbConfiguration(v.Name, true);

                if (verbConf != null)
                {
                    verbConf.Verb = v;
                }
                else //if (_configuration.Find(v) == null)
                {
                    VerbOPCUAConfiguration nounOPCUAConfiguration = new VerbOPCUAConfiguration() { Verb = v, Export = true, OPCUAParentReference = "NonHierarchicalReferences" };
                    var l = _configuration.VerbOPCUAConfigurations.ToList();
                    l.Add(nounOPCUAConfiguration);
                    _configuration.VerbOPCUAConfigurations = l.ToArray();
                    absentVerbs.Add(v);
                }
            }

            foreach (var nc in _configuration.NounOPCUAConfigurations)
            {
                if (!_vocabulary.GetNoun(nc.Noun.Name, out var noun))
                {
                    removedNouns.Add(nc.Noun);
                }
            }

            if (removedNouns.Any())
            {
                List<NounOPCUAConfiguration> nouns = _configuration.NounOPCUAConfigurations.ToList();
                foreach (var n in removedNouns)
                {
                    var nc = _configuration.FindNounConfiguration(n.Name);
                    nouns.Remove(nc);
                }
                _configuration.NounOPCUAConfigurations = nouns.ToArray();
            }

            foreach (var vc in _configuration.VerbOPCUAConfigurations)
            {
                if (!_vocabulary.GetVerb(vc.Verb.Name, out var noun))
                {
                    removedVerbs.Add(vc.Verb);
                }
            }

            if (removedVerbs.Any())
            {
                List<VerbOPCUAConfiguration> verbs = _configuration.VerbOPCUAConfigurations.ToList();
                foreach (var v in removedVerbs)
                {
                    var vc = _configuration.FindVerbConfiguration(v.Name);
                    verbs.Remove(vc);
                }
                _configuration.VerbOPCUAConfigurations= verbs.ToArray();
            }

            if (_configuration.NounOPCUAConfigurations.Length != _vocabulary.Nouns.Count)
            {
                Console.WriteLine("Problm");
            }
            if (_configuration.VerbOPCUAConfigurations.Length != _vocabulary.Verbs.Count)
            {
                Console.WriteLine("Problm");
            }


            if (absentNouns.Any() || absentVerbs.Any())
            {
                System.IO.File.WriteAllLines(_absentItemsFile, absentNouns.Select(n => n.Name).Concat(absentVerbs.Select(v => v.Name)).ToArray());
            }

            if (removedNouns.Any() || removedVerbs.Any())
            {
                System.IO.File.WriteAllLines(_removedItemsFile, removedNouns.Select(n => n.Name).Concat(removedVerbs.Select(v => v.Name)).ToArray());
            }

            foreach (var vb in _configuration.VerbOPCUAConfigurations)
            {
                if (string.IsNullOrEmpty(vb.OPCUAParentReference))
                {
                    vb.OPCUAParentReference = "NonHierarchicalReferences";
                }
            }


            PopulateTree();
        }

        private void PopulateTree()
        {           
            vocabularyTreeView.Nodes.Clear();

            vocabularyTreeView.Nodes.Add(_nounsNode);
            vocabularyTreeView.Nodes.Add(_verbsNode);

            if (_vocabulary != null)
            {
                _vocabulary.ToTrees(out Tree<Noun> nouns, out Tree<Verb> verbs);
                foreach (var t in nouns.Children)
                {
                    AddNode(t, _nounsNode);
                }
                foreach (var t in verbs.Children)
                {
                    AddNode(t, _verbsNode);
                }
            }

            vocabularyTreeView.Refresh();
        }

        private void AddNode(Tree<Noun> tree, TreeNode parent)
        {
            TreeNode current = new TreeNode(tree.RootItem.DisplayName) { Tag = tree.RootItem };

            current.ForeColor = Color.Red;
            if (_configuration != null)
            {
                var conf = _configuration.Find(tree.RootItem);
                if (conf != null)
                {
                    current.ForeColor = Color.Black;
                }
            }
            parent.Nodes.Add(current);
            if (tree.Children != null)
            {
                foreach (var child in tree.Children)
                {
                    AddNode(child, current);
                }
            }
        }

        private void AddNode(Tree<Verb> tree, TreeNode parent)
        {
            TreeNode current = new TreeNode(tree.RootItem.DisplayName) { Tag = tree.RootItem };

            current.ForeColor = Color.Red;
            if (_configuration != null)
            {
                var conf = _configuration.Find(tree.RootItem);
                if (conf != null)
                {
                    current.ForeColor = Color.Black;
                }
            }

            parent.Nodes.Add(current);
            if (tree.Children != null)
            {
                foreach (var child in tree.Children)
                {
                    AddNode(child, current);
                }
            }
        }


        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static string GetRelativePath(string relativeTo, string path)
        {
            var uri = new Uri(relativeTo);
            var rel = Uri.UnescapeDataString(uri.MakeRelativeUri(new Uri(path)).ToString()).Replace(System.IO.Path.AltDirectorySeparatorChar, System.IO.Path.DirectorySeparatorChar);
            if (rel.Contains(System.IO.Path.DirectorySeparatorChar.ToString()) == false)
            {
                rel = $".{System.IO.Path.DirectorySeparatorChar}{rel}";
            }
            return rel;
        }

        private void vocabularyTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            var tag = vocabularyTreeView.SelectedNode.Tag;
            if (tag != null && tag is Noun)
            {
                var conf = _configuration.Find((Noun)tag);
                if (conf != null)
                {
                    NounOPCUAConfigurationEditor editor = new NounOPCUAConfigurationEditor()
                    { Dock = DockStyle.Fill, Configurations = new NounOPCUAConfiguration[1] { conf } };
                    editor.ConfigurationChanged += Editor_NounConfigurationChanged;
                    splitContainer1.Panel2.Controls.Add(editor);
                    editor.UpdateView();
                }
            }
            else if (tag != null && tag is Verb)
            {
                var conf = _configuration.Find((Verb)tag);
                if (conf != null)
                {
                    VerbOPCUAConfigurationEditor editor = new VerbOPCUAConfigurationEditor()
                    { Dock = DockStyle.Fill, Configurations = new VerbOPCUAConfiguration[1] { conf } };
                    splitContainer1.Panel2.Controls.Add(editor);
                    editor.UpdateView();
                }
            }
        }

        private void Editor_NounConfigurationChanged(object sender, EventArgs e)
        {
            if (e != null && e is NounConfigurationChangedEventsArgs)
            {
                var confs = ((NounConfigurationChangedEventsArgs)e).Configurations;
                _vocabulary.ToTrees(out Tree<Noun> nouns, out Tree<Verb> verbs);
                foreach (var c in confs)
                {
                    bool ex = c.ExportAsType;
                    SetExport(nouns.GetOrDefault(c.Noun), ex);
                }
            }
        }

        private void SetExport(Tree<Noun> noun, bool val)
        {
            var conf = _configuration.FindNounConfiguration(noun.RootItem.Name);
            conf.ExportAsType = val;
            foreach (var c in noun.Children)
            {
                SetExport(c, val);
            }
        }

        //private void SetExport(Tree<Verb> noun, string opcuaParentReference)
        //{
        //    var conf = _configuration.FindVerbConfiguration(noun.RootItem.Name);
        //    conf.OPCUAParentReference = opcuaParentReference;
        //    foreach (var c in noun.Children)
        //    {
        //        SetExport(c, opcuaParentReference);
        //    }
        //}


        private void Save()
        {
            List<Noun> modified = new List<Noun>();
            bool save = true;
            if (!_configuration.CheckClasses(modified))
            {
                string text = "The following noun configurations have been modified automatically. Proceed? ";
                foreach (var c in modified)
                {
                    text += "\r\n" + c;
                }

                save = MessageBox.Show(text) == DialogResult.OK;
            }
            if (save)
            {
                VocabularyOPCUAConfiguration.ToJSONFile(_configuration, _configurationFileName);
            }
        }

        private void Open()
        {
            if (!string.IsNullOrEmpty(_configurationFileName) && System.IO.File.Exists(_configurationFileName))
            {
                _configuration = VocabularyOPCUAConfiguration.FromJSONFile(_configurationFileName);
            }
        }


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { FileName = _configurationFileName };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _configurationFileName = saveFileDialog.FileName;

                Save();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _configurationFileName = dialog.FileName;
                Open();
            }
        }
    }

}