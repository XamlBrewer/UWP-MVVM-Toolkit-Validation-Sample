namespace XamlBrewer.UWP.MvvmToolkitValidation.Sample.ViewModels
{
    public class MainPageViewModel
    {
        private Suspect _suspect = new Suspect();
        private NoDeLorean _noDelorean = new NoDeLorean();
        private Countdown _countdown = new Countdown();
        private SuspectWithDelayedValidation _suspectWithDelayedValidation = new SuspectWithDelayedValidation();
        private NotYoda _notYoda = new NotYoda(); // private NotYoda _notYoda new NotYoda() is

        public Suspect Suspect => _suspect;

        public NoDeLorean NoDelorean => _noDelorean;

        public Countdown Countdown => _countdown;

        public SuspectWithDelayedValidation SuspectWithDelayedValidation => _suspectWithDelayedValidation;

        public NotYoda NotYoda => _notYoda;
    }
}
