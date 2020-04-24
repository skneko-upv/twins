using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Twins.ViewModels
{
    public class ViewModelBase : BindableObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Convention convenience private method")]
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
        }
    }
}
