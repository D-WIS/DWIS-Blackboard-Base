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
    public partial class NounOPCUAConfigurationEditor : UserControl
    {
        public NounOPCUAConfiguration[] Configurations { get; set; }
        private bool _updateModel = true;

        private EventHandler onConfigurationChanged;

        public event EventHandler ConfigurationChanged
        {
            add { onConfigurationChanged += value; }
            remove { onConfigurationChanged -= value; }
        }


        public NounOPCUAConfigurationEditor()
        {
            InitializeComponent();
        }

        public void UpdateView()
        {
            _updateModel = false;
            if (Configurations != null)
            {
                string[] lines = Configurations.Select(c => c.Noun.DisplayName).ToArray();
                string text = lines.Aggregate((l, t) => t += l);

                MainGroupBox.Text = text;

                int exportCount = Configurations.Count(c => c.ExportAsType);

                ExportAsTypeCheckBox.Checked = exportCount > Configurations.Length - exportCount;
            }
            _updateModel = true;
        }

        private void ExportAsTypeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_updateModel)
            {
                if (Configurations != null)
                {
                    foreach (var conf in Configurations)
                    {
                        conf.ExportAsType = ExportAsTypeCheckBox.Checked;
                    }
                    onConfigurationChanged?.Invoke(this, new NounConfigurationChangedEventsArgs() { Configurations = Configurations });
                }
            }
        }
    }

    public class NounConfigurationChangedEventsArgs : EventArgs
    {

        public NounConfigurationChangedEventsArgs() : base() { }

        public NounOPCUAConfiguration[] Configurations { get; set; }
    }
}
