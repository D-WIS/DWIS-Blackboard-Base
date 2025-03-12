using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DWIS.OPCUA.Vocabulary;

namespace DWIS.OPCUA.ConfigurationEditor
{
    public partial class VerbOPCUAConfigurationEditor : UserControl
    {
        public VerbOPCUAConfiguration[] Configurations { get; set; }
        private bool _updateModel = true;


        public VerbOPCUAConfigurationEditor()
        {
            InitializeComponent();

            var fields = typeof(UnifiedAutomation.UaBase.ReferenceTypeIds).GetFields();
            var names = fields
                .Where(f => f.IsStatic && f.FieldType == typeof(UnifiedAutomation.UaBase.NodeId))
                .Select(f => f.Name);

            foreach (var f in names)
            {
                BaseReferenceComboBox.Items.Add(f);
            }
        }
        public void UpdateView()
        {
            _updateModel = false;
            if (Configurations != null)
            {
                string[] lines = Configurations.Select(c => c.Verb.DisplayName).ToArray();
                string text = lines.Aggregate((l, t) => t += l);

                MainGroupBox.Text = text;

                if (Configurations.Length > 0 && !string.IsNullOrEmpty(Configurations[0].OPCUAParentReference) && Configurations.All(v => v.OPCUAParentReference == Configurations[0].OPCUAParentReference))
                {
                    BaseReferenceComboBox.SelectedItem = Configurations[0].OPCUAParentReference;
                }

                ExportCheckBox.Checked = Configurations[0].Export;
            }
            _updateModel = true;
        }

        private void BaseReferenceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updateModel)
            {
                if (Configurations != null)
                {
                    foreach (var conf in Configurations)
                    {
                        conf.OPCUAParentReference = (string)BaseReferenceComboBox.SelectedItem;
                    }
                }
            }
        }

        private void ExportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_updateModel)
            {
                if (Configurations != null)
                {
                    foreach (var conf in Configurations)
                    {
                        conf.Export = ExportCheckBox.Checked;
                    }
                }
            }
        }
    }

}
