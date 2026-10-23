using Microsoft.AspNetCore.Components;
using Web.Services.Interface;

namespace Web.Services
{
    public class ApplicationNavigation : IApplicationNavigation
    {
        private readonly NavigationManager _navigation;

        public ApplicationNavigation(NavigationManager navigation)
        {
            _navigation = navigation;
        }

        public void NavigateToFitur() => _navigation.NavigateTo("/#fitur");
        public void NavigateToKontak() => _navigation.NavigateTo("/#kontak");
        public void NavigateToFaq() => _navigation.NavigateTo("/#faq");
        public void NavigateToPage() => _navigation.NavigateTo("/");
        public void NavigateToEvents() => _navigation.NavigateTo("/development");
        public void NavigateToChallenge(string encodeReturnUrl) => _navigation.NavigateTo($"http://localhost:8081/api/google-login/challenge?returnUrl={encodeReturnUrl}", forceLoad: true);
        public void NavigateToDashboard() => _navigation.NavigateTo("/dashboard");
        public void NavigateToParticipants() => _navigation.NavigateTo("/participants");
        public void NavigateToParticipantsCheckIn() => _navigation.NavigateTo("/participants/check-in");
        public void NavigateToFormEdit() => _navigation.NavigateTo("/form/edit");
        public void NavigateToFormPreview() => _navigation.NavigateTo("/form/preview");
        public void NavigateToFormShare() => _navigation.NavigateTo("/form/share");
        public void NavigateToTicketEdit() => _navigation.NavigateTo("/ticket/edit");
        public void NavigateToTicketPreview() => _navigation.NavigateTo("/ticket/preview");
        public void NavigateToTicketPrice() => _navigation.NavigateTo("/ticket/price");
        public void NavigateToFunds() => _navigation.NavigateTo("/funds");
    }
}
