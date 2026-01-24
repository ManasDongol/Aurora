using AuroraJournalingApp.Data;

namespace AuroraJournalingApp
{
    public partial class App : Application
    {
        public App(AuroraDbContext db)
        {
            InitializeComponent();
            _=db.InitializeAsync();

         
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "AuroraJournalingApp" };
        }
    }
}
