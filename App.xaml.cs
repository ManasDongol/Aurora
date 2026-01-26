using AuroraJournalingApp.Data;
using QuestPDF.Infrastructure;

namespace AuroraJournalingApp
{
    public partial class App : Application
    {
        public App(AuroraDbContext db)
        {
            InitializeComponent();
            _=db.InitializeAsync();
            QuestPDF.Settings.License = LicenseType.Community;


        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "AuroraJournalingApp" };
        }
    }
}
