using Prism.Services.Dialogs;
using System.Windows.Controls;

namespace mteGuides.Views
{
    /// <summary>
    /// Interaction logic for GuidesEnterprises
    /// </summary>
    public partial class GuidesEnterprises : UserControl
    {
        public GuidesEnterprises()
        {
            InitializeComponent();
            this.EnterprisesName.Focus();
        }
    }
}
