using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App3.Model;
using App3.Service;

namespace App3.ViewModel
{
   public class UsuariosViewModel : BaseViewModel
   {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged();
                CarregarCommand.ChangeCanExecute(); }
        }

        private Servico _servico;

        public Command CarregarCommand { get; }

        private List<UsuarioModel> _user;

        public List<UsuarioModel> User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        public UsuariosViewModel()
        {
            CarregarCommand = new Command(
                async () => await ExecuteCarregarCommand(), () => !IsBusy);
            User = new List<UsuarioModel>();
            _servico = new Servico();
        }

        async Task ExecuteCarregarCommand()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    User = await _servico.PegarUsuariosAsync();
                }
                catch (Exception ex)
                {

                    Error = ex;
                    await DisplayAlert("Algo deu errado",
                        $"Ocorreu o erro:{Error.Message}.", "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}
