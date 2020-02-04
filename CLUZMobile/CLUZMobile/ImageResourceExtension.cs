using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CLUZMobile
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        private void CheckImage()
        {
            var assembly = typeof(ImageResourceExtension).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                Console.WriteLine("Found resource: " + res);
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            //CheckImage();

            if (Source == null)
            {
                return null;
            }

            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

            return imageSource;
        }
    }
}
