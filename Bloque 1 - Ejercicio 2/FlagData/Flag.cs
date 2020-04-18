using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlagData
{
    /// <summary>
    /// This model object represents a single flag
    /// </summary>
    public class Flag : INotifyPropertyChanged
    {
        private DateTime _dateAdopted;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Name of the country that this flag belongs to
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Image URL for the flag (from resources)
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// The date this flag was adopted
        /// </summary>
        public DateTime DateAdopted
        {
            get { return _dateAdopted; }
            set
            {
                if (_dateAdopted != value)
                {
                    _dateAdopted = value;
                    // Can pass the property name as a string,
                    // -or- let the compiler do it because of the
                    // CallerMemberNameAttribute on the RaisePropertyChanged method.
                    RaisePropertyChanged();
                }
            }
        }
        /// <summary>
        /// Whether the flag includes an image/shield as part of the design
        /// </summary>
        public bool IncludesShield { get; set; }
        /// <summary>
        /// Some trivia or commentary about the flag
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// A URL for more information
        /// </summary>
        public Uri MoreInformationUrl { get; set; }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    /**
     * 
     * 1. Imagine que tiene una página con un elemento Entry y un elemento Label. La etiqueta muestra una lista de países. Cuando el usuario escribe en el campo de entrada, la lista de etiquetas se filtra exclusivamente por los elementos que coinciden. Por ejemplo, si el usuario escribe "Aus", la lista solo muestra "Australia" y "Austria". El elemento BindingContext de la página está establecido en una instancia de CountrySearchData. Entry.Text tiene un enlace a la propiedad de cadena CountrySearchData.CountryFilter. De estas técnicas, ¿cuál es probablemente más necesaria para que el enlace funcione de la forma descrita?
     * Un enlace bidireccional.
     * 
     * 2. ¿Qué comportamiento habilita la implementación de INotifyPropertyChanged en los objetos de datos?
     * Actualizaciones de la interfaz de usuario cuando cambian los datos de código subyacente.
     * 
     */

}
