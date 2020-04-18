using FlagData;
using Xamarin.Forms;
using FlagFacts.Extensions;
using System;
using System.Collections;

namespace FlagFacts
{
    public partial class MainPage : ContentPage
    {
        FlagRepository repository;
        int currentFlag;

        public MainPage()
        {
            InitializeComponent();

            // Load our data
            repository = new FlagRepository();

            // Setup the view
            InitializeData();
        }

        public Flag CurrentFlag
        {
            get {
                return repository.Flags[currentFlag];
            }
        }

        private void InitializeData()
        {
            country.ItemsSource = (IList) repository.Countries;
            //country.SelectedItem = CurrentFlag.Country;
            //country.SelectedIndexChanged += (s, e) => CurrentFlag.Country = repository.Countries[country.SelectedIndex];

            /**
            country.BindingContext = CurrentFlag;
            country.SetBinding(Picker.SelectedItemProperty, new Binding(nameof(CurrentFlag.Country)));


            1. ¿Qué afirmación es cierta sobre el objeto de origen de un enlace de Xamarin.Forms?
            Puede ser de cualquier tipo.

            2. ¿Qué afirmación es cierta sobre la propiedad de destino de un enlace de Xamarin.Forms?
            Debe ser una propiedad BindableProperty.

            3. Si en todos los enlaces de los controles de un elemento StackLayout se usa el mismo objeto de origen, ¿cuál es la estrategia más segura para establecer el objeto de origen una sola vez?
            Establecer la propiedad BindingContext del objeto StackLayout.

            **/

            flagImage.Source = CurrentFlag.GetImageSource();

            //adopted.Date = CurrentFlag.DateAdopted;
            //adopted.DateSelected += (s, e) => CurrentFlag.DateAdopted = e.NewDate;

            //hasShield.IsToggled = CurrentFlag.IncludesShield;
            //hasShield.Toggled += (s, e) => CurrentFlag.IncludesShield = hasShield.IsToggled;

            //description.Text = CurrentFlag.Description;
        }

        private async void OnShow(object sender, EventArgs e)
        {
            await DisplayAlert(CurrentFlag.Country,
                $"{CurrentFlag.DateAdopted:D} - {CurrentFlag.IncludesShield}: {CurrentFlag.MoreInformationUrl}", 
                "OK");
        }

        private void OnMoreInformation(object sender, EventArgs e)
        {
            Device.OpenUri(CurrentFlag.MoreInformationUrl);
        }

        private void OnPrevious(object sender, EventArgs e)
        {
            currentFlag--;
            if (currentFlag < 0)
                currentFlag = 0;
            InitializeData();
        }

        private void OnNext(object sender, EventArgs e)
        {
            currentFlag++;
            if (currentFlag >= repository.Flags.Count)
                currentFlag = repository.Flags.Count-1;
            InitializeData();
        }
    }
}
